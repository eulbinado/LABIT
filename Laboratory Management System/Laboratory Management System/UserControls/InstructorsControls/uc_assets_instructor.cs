﻿using Laboratory_Management_System.UserControls.AdminControls.Crud;
using MySql.Data.MySqlClient;
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
    public partial class uc_assets_instructor : UserControl
    {
        private string userId;
        private string scheduledRoom;

        private DatabaseHelper dbHelper;
        private DataTable dataTable;
        private Timer refreshTimer;
        public uc_assets_instructor(string userId)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            this.userId = userId;
            CustomizeDataGridView();           
            LoadData();
            EnableDoubleBuffering(dataGridView1);
            PopulateAssetsDropdown();
            PopulateStatusDropdown();
            PopulateEquippedDropdown();
            CreateDashboardPanels();
            dataGridView1.Paint += DataGridView1_Paint;           
            assetsDropdown.SelectedIndexChanged += assetsDropdown_SelectedIndexChanged;

            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            panelBlocks.SizeChanged += panelBlocks_SizeChanged;

            this.scheduledRoom = GetAssignedRoomForCurrentUser();
            label2.Text = $"Computer Assets - {scheduledRoom}";

            refreshTimer = new Timer();
            refreshTimer.Interval = 1000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }
        private void EnableDoubleBuffering(DataGridView dgv)
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(dgv, true, null);
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadData();
            CreateDashboardPanels();
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
            string currentDay = DateTime.Now.DayOfWeek.ToString();
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            string tempScheduleQuery = $@"
    SELECT room 
    FROM temporary_schedules 
    WHERE instructor = '{currentUserId}'
        AND day = '{currentDay}'
        AND status = 'Approved'
        AND '{currentTime}' BETWEEN class_start AND class_end";

            DataTable tempResult = dbHelper.GetData(tempScheduleQuery);

            if (tempResult.Rows.Count > 0)
            {
                return tempResult.Rows[0]["room"].ToString(); 
            }

            string scheduleQuery = $@"
    SELECT room 
    FROM schedules 
    WHERE instructor = '{currentUserId}'
        AND day = '{currentDay}'
        AND '{currentTime}' BETWEEN class_start AND class_end";

            DataTable result = dbHelper.GetData(scheduleQuery);

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["room"].ToString();
            }
            return "No Schedule";
        }

        private void LoadData()
        {
            string assetFilter = assetsDropdown.SelectedValue?.ToString() ?? "All";
            string equippedFilter = equippedDropdown.SelectedValue?.ToString() ?? "All";
            string statusFilter = statusDropdown.SelectedValue?.ToString() ?? "All";
            string searchText = searchFilter.Text.Trim();

            string assignedRoom = GetAssignedRoomForCurrentUser();

            string query = $@"
SELECT asset_id AS ID, asset_type AS 'Asset Type', asset_brand AS 'Brand', asset_description AS 'Description', 
       system_unit, workstation, status
FROM assets 
WHERE location = '{assignedRoom}'";

            if (assetFilter != "All")
            {
                query += $" AND asset_type = '{assetFilter}'";
            }

            if (statusFilter != "All")
            {
                query += $" AND status = '{statusFilter}'";
            }

            string equippedCondition = equippedFilter == "Equipped"
     ? "workstation <> 'Unequipped'"
     : equippedFilter == "Unequipped"
         ? "workstation = 'Unequipped'"
         : "1=1";

            if (!string.IsNullOrEmpty(searchText))
            {
                query += $@"
    AND (
        asset_id LIKE '%{searchText}%' OR
        asset_type LIKE '%{searchText}%' OR
        asset_brand LIKE '%{searchText}%' OR
        asset_description LIKE '%{searchText}%' OR
        system_unit LIKE '%{searchText}%' OR
        workstation LIKE '%{searchText}%' OR
        status LIKE '%{searchText}%'
    )";
            }

            query += " ORDER BY date_added DESC";

            DatabaseHelper dbHelper = new DatabaseHelper();
            DataTable newData = dbHelper.GetData(query);

            if (newData == null)
            {
                MessageBox.Show("Error: Failed to retrieve data.");
                return;
            }

            if (dataTable == null || newData.Rows.Count != dataTable.Rows.Count || !AreDataTablesEqual(dataTable, newData))
            {
                dataTable = newData;

                var checkedItems = dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Where(row => Convert.ToBoolean(row.Cells["Select"]?.Value))
                    .Select(row => row.Cells["ID"].Value.ToString())
                    .ToList();

                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns["ID"].HeaderText = "Asset ID";
                dataGridView1.Columns["Asset Type"].HeaderText = "Asset Type";
                dataGridView1.Columns["Brand"].HeaderText = "Brand";
                dataGridView1.Columns["Description"].HeaderText = "Description";
                dataGridView1.Columns["system_unit"].HeaderText = "System Unit";
                dataGridView1.Columns["workstation"].HeaderText = "Workstation";
                dataGridView1.Columns["status"].HeaderText = "Status";

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var assetId = row.Cells["ID"].Value.ToString();
                    row.Cells["Select"].Value = checkedItems.Contains(assetId);
                    row.Tag = assetId;
                }

                AdjustDataGridViewHeight();
            }
        }

        private bool AreDataTablesEqual(DataTable dt1, DataTable dt2)
        {
            if (dt1 == null || dt2 == null) return false;
            if (dt1.Rows.Count != dt2.Rows.Count) return false;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    if (!Equals(dt1.Rows[i][j], dt2.Rows[i][j]))
                        return false;
                }
            }

            return true;
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

            LoadData();
            CreateDashboardPanels();
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
        "Storage",
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
            string searchText = searchFilter.Text;

            string assetFilter = assetsDropdown.SelectedItem?.ToString() ?? "All";
            string statusFilter = statusDropdown.SelectedItem?.ToString() ?? "All";
            string equippedFilter = equippedDropdown.SelectedItem?.ToString() ?? "All";

            LoadData();
        }

        private void statusDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAsset = assetsDropdown.SelectedItem.ToString();
           
            string selectedStatus = statusDropdown.SelectedItem.ToString();
            string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";

            LoadData();
            CreateDashboardPanels();
        }

        private void equippedDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedAsset = assetsDropdown.SelectedItem.ToString();      
            string selectedStatus = statusDropdown.SelectedItem?.ToString() ?? "All";
            string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";

            LoadData();
            CreateDashboardPanels();
        }

        private void CreateDashboardPanels()
        {
            string selectedAssetType = assetsDropdown.SelectedItem?.ToString() ?? "All";
            string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";
            string selectedStatus = statusDropdown.SelectedItem?.ToString() ?? "All";

            string assignedRoom = GetAssignedRoomForCurrentUser();

            DatabaseHelper dbHelper = new DatabaseHelper();
            int totalCount = GetAssetCount(dbHelper, assignedRoom, selectedAssetType, selectedEquipped, selectedStatus);
            int equippedCount = (selectedEquipped == "Equipped" || selectedEquipped == "All")
                ? GetAssetCount(dbHelper, assignedRoom, selectedAssetType, "Equipped", selectedStatus)
                : 0;
            int unequippedCount = (selectedEquipped == "Unequipped" || selectedEquipped == "All")
                ? GetAssetCount(dbHelper, assignedRoom, selectedAssetType, "Unequipped", selectedStatus)
                : 0;
            int workingCount = (selectedStatus == "Working" || selectedStatus == "All")
                ? GetAssetCount(dbHelper, assignedRoom, selectedAssetType, selectedEquipped, "Working")
                : 0;
            int repairCount = (selectedStatus == "Repair" || selectedStatus == "All")
                ? GetAssetCount(dbHelper, assignedRoom, selectedAssetType, selectedEquipped, "Repair")
                : 0;
            int replacementCount = (selectedStatus == "Replacement" || selectedStatus == "All")
                ? GetAssetCount(dbHelper, assignedRoom, selectedAssetType, selectedEquipped, "Replacement")
                : 0;

            int[] counts = { totalCount, equippedCount, unequippedCount, workingCount, repairCount, replacementCount };

            if (panelBlocks.Controls.Count == 0)
            {
                InitializeDashboardPanels(counts);
            }
            else
            {
                UpdateDashboardPanelValues(counts);
            }
        }

        private void InitializeDashboardPanels(int[] counts)
        {
            string[] categories = { "All", "Equipped", "Unequipped", "Working", "Repair", "Replacement" };
            Color[] colors =
            {
        Color.FromArgb(52, 73, 94),
        Color.FromArgb(39, 174, 96),
        Color.FromArgb(192, 57, 43),
        Color.FromArgb(41, 128, 185),
        Color.FromArgb(241, 196, 15),
        Color.FromArgb(127, 140, 141)
    };

            int panelCount = categories.Length;

            for (int i = 0; i < panelCount; i++)
            {
                Panel smallPanel = new Panel
                {
                    BorderStyle = BorderStyle.None,
                    BackColor = colors[i],
                    Padding = new Padding(10),
                    Tag = i
                };

                Label categoryLabel = new Label
                {
                    Text = categories[i],
                    TextAlign = ContentAlignment.TopLeft,
                    Dock = DockStyle.Top,
                    Font = new Font("Montserrat", 10, FontStyle.Regular),
                    ForeColor = Color.White
                };

                Label countLabel = new Label
                {
                    Text = counts[i].ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Montserrat", 22, FontStyle.Bold),
                    ForeColor = Color.White,
                    Dock = DockStyle.Fill,
                    Name = "CountLabel"
                };

                smallPanel.Controls.Add(countLabel);
                smallPanel.Controls.Add(categoryLabel);

                panelBlocks.Controls.Add(smallPanel);
            }

            AdjustDashboardPanelLayout();
        }

        private void UpdateDashboardPanelValues(int[] counts)
        {
            foreach (Control control in panelBlocks.Controls)
            {
                if (control is Panel panel && panel.Tag is int index)
                {
                    Label countLabel = panel.Controls["CountLabel"] as Label;
                    if (countLabel != null)
                    {
                        countLabel.Text = counts[index].ToString();
                    }
                }
            }

            AdjustDashboardPanelLayout();
        }

        private void AdjustDashboardPanelLayout()
        {
            int panelCount = panelBlocks.Controls.Count;
            int padding = 10;
            int panelWidth = (panelBlocks.Width - (padding * (panelCount + 1))) / panelCount;
            int panelHeight = panelBlocks.Height - (2 * padding);

            for (int i = 0; i < panelCount; i++)
            {
                Panel smallPanel = panelBlocks.Controls[i] as Panel;
                if (smallPanel != null)
                {
                    smallPanel.Size = new Size(panelWidth, panelHeight);
                    smallPanel.Location = new Point(padding + i * (panelWidth + padding), padding);
                }
            }
        }

        private void panelBlocks_SizeChanged(object sender, EventArgs e)
        {
            AdjustDashboardPanelLayout();
        }



        private int GetAssetCount(DatabaseHelper dbHelper, string room, string assetType, string equippedStatus, string assetStatus)
        {
            string roomCondition = room == "All" ? "1=1" : $"location = '{room}'";
            string assetCondition = assetType == "All" ? "1=1" : $"asset_type = '{assetType}'";

            string equippedCondition;
            if (equippedStatus == "Unequipped")
            {
                equippedCondition = "workstation = 'Unequipped'";
            }
            else if (equippedStatus == "Equipped")
            {
                equippedCondition = "workstation <> 'Unequipped'";
            }
            else
            {
                equippedCondition = "1=1";
            }

            string statusCondition = assetStatus == "All" ? "1=1" : $"status = '{assetStatus}'";

            string query = $@"
    SELECT COUNT(*) AS AssetCount 
    FROM assets 
    WHERE {roomCondition} 
    AND {assetCondition} 
    AND {equippedCondition} 
    AND {statusCondition}";

            // Log the query for debugging purposes
            Console.WriteLine("Query Executed: " + query);
            Console.WriteLine($"Room: {room}, AssetType: {assetType}, EquippedStatus: {equippedStatus}, AssetStatus: {assetStatus}");

            return GetRowCount(query);
        }


        private int GetRowCount(string query)
        {
            DataTable data = dbHelper.GetData(query);

            if (data.Rows.Count > 0)
            {
                Console.WriteLine("Count Result: " + data.Rows[0]["AssetCount"]);
                return Convert.ToInt32(data.Rows[0]["AssetCount"]);
            }
            else
            {
                Console.WriteLine("No Rows Returned");
                return 0;
            }
        }

        private void requestButton_Click(object sender, EventArgs e)
        {
            List<string> selectedAssetIds = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBox = row.Cells["Select"] as DataGridViewCheckBoxCell;
                if (checkBox != null && Convert.ToBoolean(checkBox.Value) == true)
                {
                    string assetId = row.Cells["ID"].Value.ToString();
                    selectedAssetIds.Add(assetId);
                }
            }

            if (selectedAssetIds.Count > 0)
            {
                // Check if selected assets have a pending request
                string assetIdsString = string.Join(",", selectedAssetIds.Select(id => $"'{id}'"));
                string query = $"SELECT `asset_id`, `status` FROM `assets` WHERE `asset_id` IN ({assetIdsString}) AND `status` != 'Working'";

                DataTable pendingAssets = dbHelper.GetData(query);

                if (pendingAssets.Rows.Count > 0)
                {
                    MessageBox.Show("One or more of the selected assets already have a pending request.", "Pending Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    RequestForm requestForm = new RequestForm(userId, scheduledRoom, selectedAssetIds);
                    requestForm.ShowDialog();

                    if (requestForm.DialogResult == DialogResult.OK)
                    {
                        string selectedAsset = assetsDropdown.SelectedItem.ToString();
                        string selectedStatus = statusDropdown.SelectedItem?.ToString() ?? "All";
                        string selectedEquipped = equippedDropdown.SelectedItem?.ToString() ?? "All";

                        LoadData();
                        CreateDashboardPanels();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select at least one asset to request.", "No Asset Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
