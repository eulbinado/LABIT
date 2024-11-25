using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratory_Management_System.UserControls.LaboratoryInchargeControls
{
    public partial class uc_asset_reports : UserControl
    {
        private string userId;
        private DatabaseHelper dbHelper;
        private DataTable dataTable;

        public uc_asset_reports(string userId)
        {
            InitializeComponent();
            this.userId = userId;
            dbHelper = new DatabaseHelper();
            CustomizeDataGridView();
            LoadData("All", "All", "All", "All");
            PopulateAssetsDropdown();
            ReportTypeDropdown();
            PopulateRoomsDropdown();
            PopulateReportedByDropdown();
            dataGridView1.Paint += DataGridView1_Paint;
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
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Add checkbox column for selection
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Select",
                HeaderText = "",
                Width = 50,
                ReadOnly = false,
                TrueValue = true,
                FalseValue = false
            };
            dataGridView1.Columns.Insert(0, checkBoxColumn);

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(29, 60, 170),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 11, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(225, 235, 245),
                SelectionForeColor = Color.Black,
                Padding = new Padding(5, 10, 0, 10),
                WrapMode = DataGridViewTriState.True
            };
            dataGridView1.DefaultCellStyle = rowStyle;

            DataGridViewCellStyle alternatingRowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 245, 245)
            };
            dataGridView1.AlternatingRowsDefaultCellStyle = alternatingRowStyle;

            if (dataGridView1.Columns["note"] != null)
            {
                dataGridView1.Columns["note"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            dataGridView1.RowTemplate.Height = 45;
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.True;

            // Set up custom cell painting for enhanced visuals
            dataGridView1.CellPainting += DataGridView1_CellPainting;
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                using (Brush gridBrush = new SolidBrush(dataGridView1.GridColor),
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

        private void LoadData(string roomFilter = "All", string assetFilter = "All", string reportTypeFilter = "All", string statusFilter = "All")
        {
            string query = @"
        SELECT reports.report_id, reports.asset_id, reports.report_type, reports.note, reports.status, 
               users.username AS reported_by, reports.date_reported,
               COALESCE(m.location, k.location, mo.location, a.location, mb.location, 
                        c.location, g.location, r.location, psu.location, 
                        sc.location) AS asset_location,
               CASE 
                   WHEN m.mouse_id IS NOT NULL THEN 'Mouse'
                   WHEN k.keyboard_id IS NOT NULL THEN 'Keyboard'
                   WHEN mo.monitor_id IS NOT NULL THEN 'Monitor'
                   WHEN a.avr_id IS NOT NULL THEN 'AVR'
                   WHEN mb.motherboard_id IS NOT NULL THEN 'Motherboard'
                   WHEN c.cpu_id IS NOT NULL THEN 'CPU'
                   WHEN g.gpu_id IS NOT NULL THEN 'GPU'
                   WHEN r.ram_id IS NOT NULL THEN 'RAM'
                   WHEN psu.psu_id IS NOT NULL THEN 'PSU'
                   WHEN sc.system_unit_case_id IS NOT NULL THEN 'System Unit Case'
                   ELSE 'Unknown'
               END AS asset_type
        FROM reports
        LEFT JOIN users ON reports.reported_by = users.user_id
        LEFT JOIN mouse m ON reports.asset_id = m.mouse_id
        LEFT JOIN keyboards k ON reports.asset_id = k.keyboard_id
        LEFT JOIN monitors mo ON reports.asset_id = mo.monitor_id
        LEFT JOIN avr a ON reports.asset_id = a.avr_id
        LEFT JOIN motherboards mb ON reports.asset_id = mb.motherboard_id
        LEFT JOIN cpu c ON reports.asset_id = c.cpu_id
        LEFT JOIN gpu g ON reports.asset_id = g.gpu_id
        LEFT JOIN ram r ON reports.asset_id = r.ram_id
        LEFT JOIN psu psu ON reports.asset_id = psu.psu_id
        LEFT JOIN system_unit_case sc ON reports.asset_id = sc.system_unit_case_id
        WHERE (@roomFilter IS NULL OR m.location = @roomFilter OR k.location = @roomFilter OR mo.location = @roomFilter 
               OR a.location = @roomFilter OR mb.location = @roomFilter OR c.location = @roomFilter 
               OR g.location = @roomFilter OR r.location = @roomFilter OR psu.location = @roomFilter 
               OR sc.location = @roomFilter)
        AND (@assetFilter IS NULL OR 
               (CASE 
                   WHEN m.mouse_id IS NOT NULL THEN 'Mouse'
                   WHEN k.keyboard_id IS NOT NULL THEN 'Keyboard'
                   WHEN mo.monitor_id IS NOT NULL THEN 'Monitor'
                   WHEN a.avr_id IS NOT NULL THEN 'AVR'
                   WHEN mb.motherboard_id IS NOT NULL THEN 'Motherboard'
                   WHEN c.cpu_id IS NOT NULL THEN 'CPU'
                   WHEN g.gpu_id IS NOT NULL THEN 'GPU'
                   WHEN r.ram_id IS NOT NULL THEN 'RAM'
                   WHEN psu.psu_id IS NOT NULL THEN 'PSU'
                   WHEN sc.system_unit_case_id IS NOT NULL THEN 'System Unit Case'
                   ELSE 'Unknown'
               END) = @assetFilter)
        AND (@reportTypeFilter IS NULL OR reports.report_type = @reportTypeFilter)
       AND (@statusFilter IS NULL OR reports.status = @statusFilter)

        ORDER BY reports.date_reported DESC";

            // Prepare parameters
            MySqlParameter[] parameters = new MySqlParameter[]
            {
        new MySqlParameter("@roomFilter", roomFilter == "All" ? (object)DBNull.Value : roomFilter),
        new MySqlParameter("@assetFilter", assetFilter == "All" ? (object)DBNull.Value : assetFilter),
        new MySqlParameter("@reportTypeFilter", reportTypeFilter == "All" ? (object)DBNull.Value : reportTypeFilter),
        new MySqlParameter("@statusFilter", statusFilter == "All" ? (object)DBNull.Value : statusFilter)
            };

            dataTable = dbHelper.GetData(query, parameters);
            dataGridView1.DataSource = dataTable;

            CustomizeDataGridViewColumns();
        }

        private void CustomizeDataGridViewColumns()
        {
            dataGridView1.Columns["report_id"].Visible = false;
            dataGridView1.Columns["asset_id"].HeaderText = "Asset ID";
            dataGridView1.Columns["asset_type"].HeaderText = "Asset Type";
            dataGridView1.Columns["asset_location"].HeaderText = "Location";
            dataGridView1.Columns["report_type"].HeaderText = "Report Type";
            dataGridView1.Columns["note"].HeaderText = "Note";
            dataGridView1.Columns["status"].HeaderText = "Status";  
            dataGridView1.Columns["reported_by"].HeaderText = "Reported By";
            dataGridView1.Columns["date_reported"].HeaderText = "Date Reported";

            dataGridView1.Columns["asset_type"].DisplayIndex = 3;
            dataGridView1.Columns["asset_location"].DisplayIndex = 4;
            dataGridView1.Columns["note"].DisplayIndex = 5; 
            dataGridView1.Columns["status"].DisplayIndex = 6; 

            AdjustDataGridViewHeight();
        }

        private void AdjustDataGridViewHeight()
        {
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

        private void approveButton_Click(object sender, EventArgs e)
        {
            int selectedCount = 0;
            string selectedReportId = null;
            string selectedReportType = null;
            string selectedReportStatus = null;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the checkbox is selected
                if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    selectedCount++;
                    selectedReportId = row.Cells["report_id"].Value.ToString();
                    selectedReportType = row.Cells["report_type"].Value.ToString();
                    selectedReportStatus = row.Cells["status"].Value.ToString(); // Get the report status
                }
            }

            // If no rows are selected, show a message
            if (selectedCount == 0)
            {
                MessageBox.Show("Please select a report to proceed.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedCount > 1)
            {
                MessageBox.Show("Please select only one report at a time.", "Multiple Selections", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the selected report is already completed
            if (selectedReportStatus == "Completed")
            {
                MessageBox.Show("This report is already completed.", "Completed Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectedReportType == "Replacement")
            {
                ReplacementForm replacementForm = new ReplacementForm(selectedReportId, userId);
                replacementForm.Show();
            }
            else if (selectedReportType == "Repair")
            {
                RepairForm repairForm = new RepairForm(selectedReportId);
                repairForm.Show();
            }
            else
            {
                MessageBox.Show("Unsupported report type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void ReportTypeDropdown()
        {
            var reportType = new List<string>
    {
        "All",
        "Repair",
        "Replacement",
    };

            reportDropdown.DataSource = reportType;
        }

        private void PopulateRoomsDropdown()
        {
            string query = "SELECT laboratory_room FROM rooms";
            DataTable roomsTable = dbHelper.GetData(query);

            DataTable combinedRoomsTable = new DataTable();
            combinedRoomsTable.Columns.Add("laboratory_room");

            DataRow allRow = combinedRoomsTable.NewRow();
            allRow["laboratory_room"] = "All";
            combinedRoomsTable.Rows.Add(allRow);


            foreach (DataRow row in roomsTable.Rows)
            {
                combinedRoomsTable.ImportRow(row);
            }

            roomsDropdown.DataSource = combinedRoomsTable;
            roomsDropdown.DisplayMember = "laboratory_room";
            roomsDropdown.ValueMember = "laboratory_room";
        }

        private void PopulateReportedByDropdown()
        {
            var status = new List<string>
    {
        "All",
        "Pending",
        "Completed",
    };

            statusDropdown.DataSource = status;
        }

        private void roomsDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRoom = roomsDropdown.SelectedValue?.ToString() ?? "All";
            LoadData(selectedRoom, assetsDropdown.SelectedValue?.ToString() ?? "All", reportDropdown.SelectedValue?.ToString() ?? "All", statusDropdown.SelectedValue?.ToString() ?? "All");
        }

        private void assetsDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAsset = assetsDropdown.SelectedValue?.ToString() ?? "All";
            LoadData(roomsDropdown.SelectedValue?.ToString() ?? "All", selectedAsset, reportDropdown.SelectedValue?.ToString() ?? "All", statusDropdown.SelectedValue?.ToString() ?? "All");
        }

        private void reportDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReportType = reportDropdown.SelectedValue?.ToString() ?? "All";
            LoadData(roomsDropdown.SelectedValue?.ToString() ?? "All", assetsDropdown.SelectedValue?.ToString() ?? "All", selectedReportType, statusDropdown.SelectedValue?.ToString() ?? "All");
        }

        private void statusDropdown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedStatus = statusDropdown.SelectedValue?.ToString() ?? "All";
            LoadData(roomsDropdown.SelectedValue?.ToString() ?? "All", assetsDropdown.SelectedValue?.ToString() ?? "All", reportDropdown.SelectedValue?.ToString() ?? "All", selectedStatus);
        }
    }
}
