using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laboratory_Management_System.UserControls.InstructorsControls
{
    public partial class ReportForm : Form
    {
        public event EventHandler ReportSubmitted;

        private string userId;
        private List<string> selectedAssetIds;

        public ReportForm(string userId, List<string> selectedAssetIds)
        {
            InitializeComponent();
            PopulateTypeDropdown();
            this.userId = userId;
            this.selectedAssetIds = selectedAssetIds;
        }

        private void PopulateTypeDropdown()
        {
            var type = new List<string>
            {
                "Repair",
                "Replacement",
            };

            reportTypeDropdown.DataSource = type;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string reportType = reportTypeDropdown.SelectedValue.ToString();
            string note = reportNote.Text; 

            DatabaseHelper dbHelper = new DatabaseHelper();

            foreach (var assetId in selectedAssetIds)
            {
                string reportQuery = "INSERT INTO reports (asset_id, report_type, note, reported_by) VALUES (@assetId, @reportType, @note, @userId)";

                MySqlParameter[] reportParameters = new MySqlParameter[]
                {
                    new MySqlParameter("@assetId", assetId),
                    new MySqlParameter("@reportType", reportType),
                    new MySqlParameter("@note", note),
                    new MySqlParameter("@userId", userId) 
                };

                dbHelper.ExecuteQuery(reportQuery, reportParameters);

                var assetInfo = GetAssetTableAndIdColumnByAssetId(assetId);

                if (assetInfo != null)
                {
                    string assetTable = assetInfo.Item1;  
                    string idColumn = assetInfo.Item2;   

                    string newStatus = reportType == "Repair" ? "Repair" : "Replacement";

                    string updateStatusQuery = $"UPDATE {assetTable} SET status = @newStatus WHERE {idColumn} = @assetId";

                    MySqlParameter[] statusParameters = new MySqlParameter[]
                    {
                        new MySqlParameter("@newStatus", newStatus),
                        new MySqlParameter("@assetId", assetId)
                    };

                    dbHelper.ExecuteQuery(updateStatusQuery, statusParameters);
                }
            }

            MessageBox.Show("Report saved and asset status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ReportSubmitted?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private Tuple<string, string> GetAssetTableAndIdColumnByAssetId(string assetId)
        {
            if (assetId.StartsWith("K")) 
            {
                return Tuple.Create("keyboards", "keyboard_id");
            }
            else if (assetId.StartsWith("MO")) 
            {
                return Tuple.Create("mouse", "mouse_id");
            }
            else if (assetId.StartsWith("M")) 
            {
                return Tuple.Create("monitors", "monitor_id");
            }
            else if (assetId.StartsWith("A")) 
            {
                return Tuple.Create("avr", "avr_id");
            }
            else if (assetId.StartsWith("MB")) 
            {
                return Tuple.Create("motherboards", "motherboard_id");
            }
            else if (assetId.StartsWith("M")) 
            {
                return Tuple.Create("monitors", "monitor_id");
            }

            return null;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
