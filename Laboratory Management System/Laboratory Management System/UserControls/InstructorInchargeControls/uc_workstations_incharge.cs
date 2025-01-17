﻿using Laboratory_Management_System.AddingUpdatingForms;
using Laboratory_Management_System.AdminForms;
using Laboratory_Management_System.UserControls.AdminControls;
using Laboratory_Management_System.UserControls.AdminControls.Crud;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratory_Management_System.Controls
{
    public partial class uc_workstations_incharge : UserControl
    {
        private string userId;

        private DatabaseHelper dbHelper;
        private DataTable dataTable;
        private string assignedRoom;
        private Timer refreshTimer;

        public uc_workstations_incharge(string userId)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            CustomizeDataGridView();
            BlackText();

            deleteButton.Click += deleteButton_Click;

            dataGridView1.Paint += DataGridView1_Paint;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            this.userId = userId;
            this.assignedRoom = GetAssignedRoom(userId);
            LoadData();
            EnableDoubleBuffering(dataGridView1);
            titleLabel.Text = $"Workstations - {assignedRoom}";

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
        }

        private string GetAssignedRoom(string userId)
        {
            string query = $"SELECT assigned_room FROM users WHERE user_id = '{userId}'";
            DataTable dt = dbHelper.GetData(query);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["assigned_room"].ToString();
            }
            else
            {
                return "Unassigned";
            }
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var workstationIdsToDelete = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    string workstationId = row.Cells["workstation_id"].Value.ToString();
                    workstationIdsToDelete.Add(workstationId);
                }
            }

            if (workstationIdsToDelete.Count > 0)
            {
                var confirmResult = MessageBox.Show(
                    "Are you sure you want to delete the selected records?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    string ids = string.Join("','", workstationIdsToDelete);
                    string query = $"DELETE FROM workstations WHERE workstation_id IN ('{ids}')";

                    try
                    {
                        dbHelper.ExecuteQuery(query);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("No records selected for deletion.");
            }
        }

        private void SearchFilter_Enter(object sender, EventArgs e)
        {
            if (searchFilter.Text == "Search")
            {
                searchFilter.Text = "";
                searchFilter.ForeColor = Color.Black;
            }
        }

        private void SearchFilter_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchFilter.Text))
            {
                searchFilter.Text = "Search";
                searchFilter.ForeColor = Color.Gray;
            }
        }

        private void uc_workstations_Load(object sender, EventArgs e)
        {
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
                Padding = new Padding(5, 0, 0, 0)
            };
            dataGridView1.DefaultCellStyle = rowStyle;

            DataGridViewCellStyle alternatingRowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 245, 245)
            };
            dataGridView1.AlternatingRowsDefaultCellStyle = alternatingRowStyle;

            dataGridView1.RowTemplate.Height = 45;
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.False;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name != "Select")
                {
                    column.ReadOnly = true;
                }
            }

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

        private void LoadData()
        {
            string assignedRoom = GetAssignedRoom(userId);
            string searchTerm = searchFilter.Text;

            string query = @"
SELECT 
    workstations.workstation_id, 
    workstations.system_unit,
    workstations.room,         
    workstations.monitor AS MonitorID,
    workstations.keyboard AS KeyboardID,
    workstations.mouse AS MouseID,
    workstations.avr AS AvrID,
    workstations.date_added AS DateAdded
FROM workstations
LEFT JOIN system_units ON workstations.system_unit = system_units.system_unit_id
WHERE workstations.room = @assignedRoom";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " AND (workstations.monitor LIKE @searchTerm OR " +
                         "workstations.keyboard LIKE @searchTerm OR " +
                         "workstations.mouse LIKE @searchTerm OR " +
                         "workstations.avr LIKE @searchTerm)";
            }

            query += " ORDER BY workstations.date_added DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>
    {
        new MySqlParameter("@assignedRoom", assignedRoom)
    };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                parameters.Add(new MySqlParameter("@searchTerm", "%" + searchTerm + "%"));
            }

            DataTable newData = dbHelper.GetData(query, parameters.ToArray());

            if (newData == null)
            {
                MessageBox.Show("Error: No data retrieved.");
                return;
            }

            if (dataTable == null || newData.Rows.Count != dataTable.Rows.Count)
            {
                dataTable = newData;

                dataGridView1.DataSource = dataTable;

                if (dataGridView1.Columns["workstation_id"] != null)
                {
                    dataGridView1.Columns["workstation_id"].Visible = false;
                }

                if (dataGridView1.Columns["Select"] == null)
                {
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                    {
                        Name = "Select",
                        HeaderText = "Select",
                        DataPropertyName = "workstation_id"
                    };
                    dataGridView1.Columns.Insert(0, checkBoxColumn);
                }

                dataGridView1.Columns["workstation_id"].HeaderText = "Workstation";
                dataGridView1.Columns["system_unit"].HeaderText = "System Unit";
                dataGridView1.Columns["room"].Visible = false;
                dataGridView1.Columns["MonitorID"].HeaderText = "Monitor";
                dataGridView1.Columns["KeyboardID"].HeaderText = "Keyboard";
                dataGridView1.Columns["MouseID"].HeaderText = "Mouse";
                dataGridView1.Columns["AvrID"].HeaderText = "AVR";
                dataGridView1.Columns["DateAdded"].HeaderText = "Date Added";

                AdjustDataGridViewHeight();
            }
            else
            {

            }
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

        private void viewButton_Click_1(object sender, EventArgs e)
        {
            var selectedRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    selectedRows.Add(row);
                }
            }

            if (selectedRows.Count == 1)
            {
                string selectedWorkstationId = selectedRows[0].Cells["workstation_id"].Value.ToString();

                if (!string.IsNullOrEmpty(selectedWorkstationId))
                {
                    WorkStationView workstationViewForm = new WorkStationView(selectedWorkstationId);
                    workstationViewForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not retrieve Workstation ID.");
                }
            }
            else if (selectedRows.Count > 1)
            {
                MessageBox.Show("You can only view one workstation at a time.");
            }
            else
            {
                MessageBox.Show("Please select a workstation to view.");
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["MonitorID"].Index ||
                e.ColumnIndex == dataGridView1.Columns["KeyboardID"].Index ||
                e.ColumnIndex == dataGridView1.Columns["MouseID"].Index ||
                e.ColumnIndex == dataGridView1.Columns["AvrID"].Index)
            {
                // Get the asset ID from the cell value
                string assetId = e.Value?.ToString();

                // Query the asset status based on the asset ID
                string statusQuery = "SELECT status FROM assets WHERE asset_id = @assetId";
                MySqlParameter[] parameters = { new MySqlParameter("@assetId", assetId ?? (object)DBNull.Value) };
                DataTable statusTable = dbHelper.GetData(statusQuery, parameters);

                if (statusTable.Rows.Count > 0)
                {
                    string status = statusTable.Rows[0]["status"].ToString();

                    // Set the background color based on status
                    if (status.Equals("Replacement", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = Color.LightCoral; // Replacement
                    }
                    else if (status.Equals("Repair", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.BackColor = Color.Yellow; // Repair
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.White; // Default
                    }
                }
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddWorkstationIncharge addForm = new AddWorkstationIncharge(userId);

            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                //MessageBox.Show("System unit added successfully!");
            }
        }

        private void BlackText()
        {
            label1.ForeColor = Color.Black;
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var selectedRows = new List<DataGridViewRow>();

            // Collect selected rows based on checkbox
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value)) // Assuming there's a checkbox column named "Select"
                {
                    selectedRows.Add(row);
                }
            }

            // Check the number of selected rows
            if (selectedRows.Count == 1)
            {
                string selectedWorkstationId = selectedRows[0].Cells["workstation_id"].Value.ToString();

                if (!string.IsNullOrEmpty(selectedWorkstationId))
                {
                    EditWorkstationIncharge editForm = new EditWorkstationIncharge(selectedWorkstationId, userId);

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                        //MessageBox.Show("Workstation updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Could not retrieve Workstation ID.");
                }
            }
            else if (selectedRows.Count > 1)
            {
                MessageBox.Show("You can only edit one workstation at a time.");
            }
            else
            {
                MessageBox.Show("Please select a workstation to edit.");
            }
        }

    }
}