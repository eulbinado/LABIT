using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_Management_System.UserControls.InstructorsControls
{
    public partial class uc_assets : UserControl
    {
        private string userId;

        private DatabaseHelper dbHelper;
        private DataTable dataTable;
        public uc_assets(string userId)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            this.userId = userId;
            UpdateLabelWithAssignedRoom(userId);
            CustomizeDataGridView();           
            LoadData("All", "All", "All");
            PopulateAssetsDropdown();
            PopulateStatusDropdown();
            PopulateEquippedDropdown();
            dataGridView1.Paint += DataGridView1_Paint;           
            assetsDropdown.SelectedIndexChanged += assetsDropdown_SelectedIndexChanged;

            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
        }

        private void DataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                string message = "No data available";
                Font font = new Font("Montserrat", 14, FontStyle.Regular);
                Color textColor = Color.Gray;

                SizeF textSize = e.Graphics.MeasureString(message, font);
                float textX = (dataGridView1.Width - textSize.Width) / 2;
                float textY = (dataGridView1.Height - textSize.Height) / 2;

                e.Graphics.DrawString(message, font, new SolidBrush(textColor), textX, textY);
            }
        }

        private void CustomizeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Dock = DockStyle.Fill;

            // Set the AutoSizeRowsMode to allow rows to expand based on content
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Create and insert a checkbox column
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Select",
                HeaderText = "",
                Width = 20,
                ReadOnly = false,
                TrueValue = true,
                FalseValue = false
            };
            dataGridView1.Columns.Insert(0, checkBoxColumn);

            // Column header style
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(29, 60, 170),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Row style with padding for top, bottom, left
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 11, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(225, 235, 245),
                SelectionForeColor = Color.Black,
                Padding = new Padding(5, 10, 0, 10), // Padding: Left=5, Top=10, Right=0, Bottom=10
                WrapMode = DataGridViewTriState.True // Enable text wrapping
            };
            dataGridView1.DefaultCellStyle = rowStyle;

            // Alternating row style
            DataGridViewCellStyle alternatingRowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 245, 245)
            };
            dataGridView1.AlternatingRowsDefaultCellStyle = alternatingRowStyle;

            // Set specific style for the "note" column to handle long text
            if (dataGridView1.Columns["note"] != null)
            {
                dataGridView1.Columns["note"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            dataGridView1.RowTemplate.Height = 45;
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.True; // Allow rows to resize
            dataGridView1.CellPainting += dataGridView1_CellPainting;
        }




        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                using (Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                            e.CellBounds.Top, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);

                        e.PaintContent(e.CellBounds);
                        e.Handled = true;
                    }
                }
            }
        }

        private string GetAssignedRoomForCurrentUser()
        {
            string currentUserId = userId; 

            string query = $"SELECT assigned_room FROM users WHERE user_id = '{currentUserId}'";

            DataTable result = dbHelper.GetData(query);

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["assigned_room"].ToString();
            }

            return string.Empty;
        }

        private void LoadData(string assetFilter, string statusFilter, string equippedFilter)
        {
            string assignedRoom = GetAssignedRoomForCurrentUser();

            string query = "";

            // Add equipped filtering logic
            string equippedCondition = equippedFilter == "Equipped" ? "workstation <> 'Unequipped'" : "1=1"; // "1=1" allows all records

            switch (assetFilter)
            {
                case "Monitor":
                    query = $@"SELECT monitor_id AS ID, brand, description, location, workstation, status 
                FROM monitors 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "Keyboard":
                    query = $@"SELECT keyboard_id AS ID, brand, description, location, workstation, status 
                FROM keyboards 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "Mouse":
                    query = $@"SELECT mouse_id AS ID, brand, description, location, workstation, status 
                FROM mouse 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "AVR":
                    query = $@"SELECT avr_id AS ID, brand, description, location, workstation, status 
                FROM avr 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "Motherboard":
                    query = $@"SELECT motherboard_id AS ID, brand, description, location, workstation, status 
                FROM motherboards 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "CPU":
                    query = $@"SELECT cpu_id AS ID, brand, description, location, workstation, status 
                FROM cpu 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "GPU":
                    query = $@"SELECT gpu_id AS ID, brand, description, location, workstation, status 
                FROM gpu 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "RAM":
                    query = $@"SELECT ram_id AS ID, brand, description, location, workstation, status 
                FROM ram 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "PSU":
                    query = $@"SELECT psu_id AS ID, brand, description, location, workstation, status 
                FROM psu 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "System Unit Case":
                    query = $@"SELECT system_unit_case_id AS ID, brand, description, location, workstation, status 
                FROM system_unit_case 
                WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition} 
                ORDER BY brand";
                    break;
                case "All":
                    query = $@"
        SELECT monitor_id AS ID, brand, description, location, workstation, status 
        FROM monitors 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT keyboard_id AS ID, brand, description, location, workstation, status 
        FROM keyboards 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT mouse_id AS ID, brand, description, location, workstation, status 
        FROM mouse 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT avr_id AS ID, brand, description, location, workstation, status 
        FROM avr 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT motherboard_id AS ID, brand, description, location, workstation, status 
        FROM motherboards 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT cpu_id AS ID, brand, description, location, workstation, status 
        FROM cpu 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT gpu_id AS ID, brand, description, location, workstation, status 
        FROM gpu 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT ram_id AS ID, brand, description, location, workstation, status 
        FROM ram 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT psu_id AS ID, brand, description, location, workstation, status 
        FROM psu 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        UNION ALL
        SELECT system_unit_case_id AS ID, brand, description, location, workstation, status 
        FROM system_unit_case 
        WHERE location = '{assignedRoom}' AND status = @statusFilter AND {equippedCondition}
        ORDER BY brand";
                    break;
                default:
                    return;
            }

            query = query.Replace("@statusFilter", statusFilter == "All" ? "status" : $"'{statusFilter}'");

            dataTable = dbHelper.GetData(query);
            dataGridView1.DataSource = dataTable;

            dataGridView1.Columns[1].HeaderText = "Asset ID";
            dataGridView1.Columns[2].HeaderText = "Brand";
            dataGridView1.Columns[3].HeaderText = "Description";
            dataGridView1.Columns[4].HeaderText = "Location";
            dataGridView1.Columns[5].HeaderText = "Workstation";
            dataGridView1.Columns[6].HeaderText = "Status";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["Select"].Value = false;
                row.Tag = row.Cells["ID"].Value;
            }

            AdjustDataGridViewHeight();
        }



        private void AdjustDataGridViewHeight()
        {
            int rowCount = dataGridView1.Rows.Count;

            int rowHeight = dataGridView1.RowTemplate.Height;
            int headerHeight = dataGridView1.ColumnHeadersHeight;
            int totalHeight = (rowHeight * rowCount) + headerHeight;

            int maxHeight = 500;
            if (totalHeight > maxHeight)
            {
                totalHeight = maxHeight;
            }

            dataGridView1.Height = totalHeight;
        }

        private void assetsDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAsset = assetsDropdown.SelectedItem.ToString();      
            string selectedStatus = statusDropdown.SelectedItem?.ToString() ?? "All";
            string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";

            LoadData(selectedAsset, selectedStatus, selectedEquipped);
        }

        private void PopulateAssetsDropdown()
        {
            var assets = new List<string>
    {
        "All",
        "Monitor",
        "Keyboard",
        "Mouse",
        "AVR",
        "Motherboard",
        "CPU",
        "GPU",
        "RAM",
        "PSU",
        "System Unit Case",
    };

            assetsDropdown.DataSource = assets;
        }

        private void PopulateStatusDropdown()
        {
            var status = new List<string>
    {
        "All",
        "Working",
        "Repair",
        "Replacement",
    };

            statusDropdown.DataSource = status;
        }

        private void PopulateEquippedDropdown()
        {
            var equipped = new List<string>
    {
        "All",
        "Equipped",
        "Unequipped",
    };

            equippedDropdown.DataSource = equipped;
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            List<string> selectedAssetIds = new List<string>();
            List<string> pendingAssets = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    string assetId = row.Tag?.ToString() ?? row.Cells["ID"].Value.ToString();
                    selectedAssetIds.Add(assetId);

                    string status = row.Cells["status"].Value?.ToString();
                    if (status != "Working")
                    {
                        pendingAssets.Add(assetId);
                    }
                }
            }

            if (pendingAssets.Count > 0)
            {
                MessageBox.Show("The following assets have a pending report: " + string.Join(", ", pendingAssets), "Pending Reports", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (selectedAssetIds.Count > 0)
            {
                ReportForm reportForm = new ReportForm(userId, selectedAssetIds);

                // Subscribe to the ReportSubmitted event to refresh the data
                reportForm.ReportSubmitted += (s, args) =>
                {
                    // Re-load the data after the report is submitted
                    string selectedAsset = assetsDropdown.SelectedItem.ToString();
                    string selectedStatus = statusDropdown.SelectedItem?.ToString() ?? "All";
                    string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";

                    LoadData(selectedAsset, selectedStatus, selectedEquipped);
                };

                reportForm.Show();
            }
            else
            {
                MessageBox.Show("No assets selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string status = row.Cells["status"].Value?.ToString();

                if (status == "Repair")
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (status == "Replacement")
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }

        private void searchFilter_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAsset = assetsDropdown.SelectedItem.ToString();
           
            string selectedStatus = statusDropdown.SelectedItem.ToString();
            string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";

            LoadData(selectedAsset, selectedStatus, selectedEquipped);
        }

        private void equippedDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAsset = assetsDropdown.SelectedItem.ToString();      
            string selectedStatus = statusDropdown.SelectedItem?.ToString() ?? "All";
            string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";

            LoadData(selectedAsset, selectedStatus, selectedEquipped);
        }

        private void UpdateLabelWithAssignedRoom(string userId)
        {
            string assignedRoom = GetAssignedRoomForCurrentUser(); 

            if (!string.IsNullOrEmpty(assignedRoom))
            {
                label2.Text = $"Computer Assets - {assignedRoom}";
            }
            else
            {
                label2.Text = "Computer Assets - Unknown Room"; 
            }
        }
    }
}
