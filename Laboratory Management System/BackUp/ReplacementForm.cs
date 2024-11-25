using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Laboratory_Management_System.UserControls.LaboratoryInchargeControls
{
    public partial class ReplacementForm : Form
    {
        private DatabaseHelper dbHelper;
        private string UserId;
        private string reportId;

        public ReplacementForm(string reportId, string userId)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            this.reportId = reportId;
            LoadAssetId(reportId);           
            UserId = userId;
        }

        private void LoadAssetId(string reportId)
        {
            string query = "SELECT asset_id FROM reports WHERE report_id = @reportId";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@reportId", reportId)
            };

            DataTable result = dbHelper.GetData(query, parameters);

            if (result.Rows.Count > 0)
            {
                string assetId = result.Rows[0]["asset_id"].ToString();
                currentAsset.Text = assetId;
            }
            else
            {
                MessageBox.Show("Asset ID not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string currentAssetId = currentAsset.Text;
            string replacementAssetId = replacementAsset.Text;

            string currentAssetType = GetAssetType(currentAssetId);
            string currentLocation = GetAssetLocation(currentAssetId);

            if (string.IsNullOrEmpty(currentAssetType) || string.IsNullOrEmpty(currentLocation))
            {
                MessageBox.Show("Current asset not found in workstations or system units.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string replacementAssetType = GetAssetType(replacementAssetId);

            if (currentAssetType != replacementAssetType)
            {
                MessageBox.Show("The replacement asset type does not match the current asset type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!UpdateAsset(currentAssetId, replacementAssetId, currentLocation))
            {
                MessageBox.Show("Failed to replace the asset. Please check the asset IDs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InsertReplacementHistory(currentAssetId, replacementAssetId);

            UpdateReportStatus(reportId); 

            MessageBox.Show("Asset replacement successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void InsertReplacementHistory(string currentAssetId, string replacementAssetId)
        {
            string query = "INSERT INTO replacement_history (asset_id, replacement_asset, replaced_by) VALUES (@currentAssetId, @replacementAssetId, @replacedBy)";

            var parameters = new MySqlParameter[]
            {
        new MySqlParameter("@currentAssetId", currentAssetId),
        new MySqlParameter("@replacementAssetId", replacementAssetId),
        new MySqlParameter("@replacedBy", UserId)
            };

            dbHelper.ExecuteQuery(query, parameters);
        }


        private bool UpdateAsset(string currentAssetId, string replacementAssetId, string currentLocation)
        {
            string currentAssetType = GetAssetType(currentAssetId);
            string replacementAssetType = GetAssetType(replacementAssetId);

            if (currentAssetType != replacementAssetType)
            {
                MessageBox.Show("The replacement asset type does not match the current asset type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            UpdateCurrentAssetStatus(currentAssetId);

            AssignReplacementAsset(replacementAssetId, currentAssetId);

            return true;
        }

        private void UpdateCurrentAssetStatus(string assetId)
        {
            string assetType = GetAssetType(assetId);
            string assetTable = GetAssetTable(assetType);
            string query = $"UPDATE {assetTable} SET status = 'Replaced' WHERE {GetAssetColumn(assetType)} = @assetId";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@assetId", assetId)
            };

            dbHelper.ExecuteQuery(query, parameters);
        }

        private void UnassignCurrentAssetFromWorkstation(string assetId, string currentLocation)
        {
            string assetType = GetAssetType(assetId);
            string columnName = GetWorkstationAssetColumn(assetType);
            string query = $"UPDATE workstations SET {columnName} = NULL WHERE room = @currentLocation AND {columnName} = @assetId";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@currentLocation", currentLocation),
                new MySqlParameter("@assetId", assetId)
            };

            dbHelper.ExecuteQuery(query, parameters);
        }

        private void AssignReplacementAsset(string replacementAssetId, string currentAssetId)
        {
            // Get the asset type for replacement
            string assetType = GetAssetType(replacementAssetId);
            string assetTable = GetAssetTable(assetType);
            string assetColumn = GetAssetColumn(assetType);

            // Update the replacement asset status to 'Working'
            string updateReplacementStatusQuery = $"UPDATE {assetTable} SET status = 'Working', workstation = (SELECT workstation_id FROM workstations WHERE {GetWorkstationAssetColumn(assetType)} = @currentAssetId), location = (SELECT room FROM workstations WHERE {GetWorkstationAssetColumn(assetType)} = @currentAssetId) WHERE {assetColumn} = @replacementAssetId";
            dbHelper.ExecuteQuery(updateReplacementStatusQuery, new MySqlParameter[]
            {
        new MySqlParameter("@replacementAssetId", replacementAssetId),
        new MySqlParameter("@currentAssetId", currentAssetId)
            });

            // Now that the replacement asset is assigned, unassign the current asset by setting the workstation column to NULL
            string workstationColumn = GetWorkstationAssetColumn(assetType);

            // Finally, assign the replacement asset to the workstation in place of the current asset
            string assignReplacementAssetQuery = $"UPDATE workstations SET {workstationColumn} = @replacementAssetId WHERE {workstationColumn} = @currentAssetId";
            dbHelper.ExecuteQuery(assignReplacementAssetQuery, new MySqlParameter[]
            {
        new MySqlParameter("@replacementAssetId", replacementAssetId),
        new MySqlParameter("@currentAssetId", currentAssetId)
            });
        }



        private string GetWorkstationAssetColumn(string assetType)
        {
            switch (assetType)
            {
                case "Monitor":
                    return "monitor";
                case "Keyboard":
                    return "keyboard";
                case "Mouse":
                    return "mouse";
                case "AVR":
                    return "avr";
                default:
                    throw new Exception("Invalid workstation asset type");
            }
        }

        private string GetSystemUnitAssetColumn(string assetType)
        {
            switch (assetType)
            {
                case "Motherboard":
                    return "motherboard";
                case "CPU":
                    return "cpu";
                case "RAM":
                    return "ram";
                case "PSU":
                    return "psu";
                case "GPU":
                    return "gpu";
                case "Case":
                    return "system_unit_case";
                default:
                    throw new Exception("Invalid system unit asset type");
            }
        }

        private string GetAssetType(string assetId)
        {
            if (assetId.StartsWith("M"))
            {
                return "Monitor";
            }
            else if (assetId.StartsWith("MB"))
            {
                return "Motherboard";
            }
            else if (assetId.StartsWith("MO"))
            {
                return "Mouse";
            }
            else if (assetId.StartsWith("A"))
            {
                return "AVR";
            }
            else if (assetId.StartsWith("C"))
            {
                return "CPU";
            }
            else if (assetId.StartsWith("R"))
            {
                return "RAM";
            }
            else if (assetId.StartsWith("G"))
            {
                return "GPU";
            }
            else if (assetId.StartsWith("P"))
            {
                return "PSU";
            }
            else if (assetId.StartsWith("SUC"))
            {
                return "Case";
            }
            else if (assetId.StartsWith("K"))
            {
                return "Keyboard";
            }

            return "Unknown"; 
        }

        private string GetAssetLocation(string assetId)
        {      
            string assetType = GetAssetType(assetId);
    
            string query = string.Empty;

            if (assetType == "Monitor")
            {
                query = "SELECT room FROM workstations WHERE monitor = @assetId";
            }
            else if (assetType == "Keyboard")
            {
                query = "SELECT room FROM workstations WHERE keyboard = @assetId";
            }
            else if (assetType == "Mouse")
            {
                query = "SELECT room FROM workstations WHERE mouse = @assetId";
            }
            else if (assetType == "AVR")
            {
                query = "SELECT room FROM workstations WHERE avr = @assetId";
            }
            else if (assetType == "Motherboard")
            {
                query = "SELECT room FROM system_units WHERE motherboard = @assetId";
            }
            else if (assetType == "CPU")
            {
                query = "SELECT room FROM system_units WHERE cpu = @assetId";
            }
            else if (assetType == "RAM")
            {
                query = "SELECT room FROM system_units WHERE ram = @assetId";
            }
            else if (assetType == "GPU")
            {
                query = "SELECT room FROM system_units WHERE gpu = @assetId";
            }
            else if (assetType == "PSU")
            {
                query = "SELECT room FROM system_units WHERE psu = @assetId";
            }
            else if (assetType == "Case")
            {
                query = "SELECT room FROM system_units WHERE system_unit_case = @assetId";
            }

            else
            {
                MessageBox.Show("Asset type not recognized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var parameters = new MySqlParameter[]
            {
        new MySqlParameter("@assetId", assetId)
            };

            DataTable result = dbHelper.GetData(query, parameters);

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["room"].ToString();
            }

            return null;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetAssetColumn(string assetType)
        {
            switch (assetType)
            {
                case "Monitor":
                    return "monitor_id";
                case "Keyboard":
                    return "keyboard_id";
                case "Mouse":
                    return "mouse_id";
                case "AVR":
                    return "avr_id";
                case "Motherboard":
                    return "motherboard_id";
                case "CPU":
                    return "cpu_id";
                case "RAM":
                    return "ram_id";
                case "PSU":
                    return "psu_id";
                case "Case":
                    return "system_unit_case_id";
                default:
                    throw new Exception("Invalid asset type");
            }
        }

        private string GetAssetTable(string assetType)
        {
            switch (assetType)
            {
                case "Monitor":
                    return "monitors";
                case "Keyboard":
                    return "keyboards";
                case "Mouse":
                    return "mouse";
                case "AVR":
                    return "avr";
                case "Motherboard":
                    return "motherboards";
                case "CPU":
                    return "cpu";
                case "RAM":
                    return "ram";
                case "PSU":
                    return "psu";
                case "Case":
                    return "system_unit_case";
                case "GPU":
                    return "gpu";
                default:
                    throw new Exception("Invalid asset type");
            }
        }

        private void UpdateReportStatus(string reportId)
        {
            string query = "UPDATE reports SET status = 'Completed' WHERE report_id = @reportId";
            var parameters = new MySqlParameter[]
            {
        new MySqlParameter("@reportId", reportId)
            };

            dbHelper.ExecuteQuery(query, parameters);
        }

    }
}
