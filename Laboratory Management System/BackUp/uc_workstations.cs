using Laboratory_Management_System.AddingUpdatingForms;
using Laboratory_Management_System.AdminForms;
using Laboratory_Management_System.UserControls.AdminControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratory_Management_System.Controls
{
    public partial class uc_workstations : UserControl
    {
        private string userId;

        private DatabaseHelper dbHelper;
        private DataTable dataTable;

        public uc_workstations(string userId)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            CustomizeDataGridView();
            BlackText();
            LoadData();
            LoadRooms();
            labDropdown.SelectedIndexChanged += labDropdown_SelectedIndexChanged;
            deleteButton.Click += deleteButton_Click;

            dataGridView1.Paint += DataGridView1_Paint;
            this.userId = userId;
            modifyTitleLabel();
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
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
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

        private void LoadData(string roomFilter = null)
        {
            string query = @"
    SELECT workstations.workstation_id, workstations.room, 
           system_units.hostname AS hostname, 
           monitors.brand AS monitor, monitors.status AS monitorStatus,
           keyboards.brand AS keyboard, keyboards.status AS keyboardStatus,
           mouse.brand AS mouse, mouse.status AS mouseStatus,
           avr.brand AS avr, avr.status AS avrStatus, 
           workstations.system_unit
    FROM workstations
    LEFT JOIN monitors ON workstations.monitor = monitors.monitor_id
    LEFT JOIN keyboards ON workstations.keyboard = keyboards.keyboard_id
    LEFT JOIN mouse ON workstations.mouse = mouse.mouse_id
    LEFT JOIN avr ON workstations.avr = avr.avr_id
    LEFT JOIN system_units ON workstations.system_unit = system_units.system_unit_id";

            if (!string.IsNullOrEmpty(roomFilter))
            {
                query += $" WHERE workstations.room = '{roomFilter}'";
            }

            query += " ORDER BY workstations.date_added DESC";

            dataTable = dbHelper.GetData(query);
            dataGridView1.DataSource = dataTable;

            // Set column headers
            dataGridView1.Columns["workstation_id"].HeaderText = "Workstation ID";
            dataGridView1.Columns["system_unit"].HeaderText = "System Unit";
            dataGridView1.Columns["room"].HeaderText = "Room";
            dataGridView1.Columns["hostname"].HeaderText = "Hostname";
            dataGridView1.Columns["monitor"].HeaderText = "Monitor";
            dataGridView1.Columns["keyboard"].HeaderText = "Keyboard";
            dataGridView1.Columns["mouse"].HeaderText = "Mouse";
            dataGridView1.Columns["avr"].HeaderText = "Avr";

            dataGridView1.Columns["monitorStatus"].Visible = false;
            dataGridView1.Columns["keyboardStatus"].Visible = false;
            dataGridView1.Columns["mouseStatus"].Visible = false;
            dataGridView1.Columns["avrStatus"].Visible = false;
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

        private void LoadRooms()
        {
            DataTable roomsData = dbHelper.GetData("SELECT laboratory_room FROM rooms");
            DataRow placeholderRow = roomsData.NewRow();
            placeholderRow["laboratory_room"] = "All"; 
            roomsData.Rows.InsertAt(placeholderRow, 0);

            labDropdown.DataSource = roomsData;
            labDropdown.DisplayMember = "laboratory_room";
            labDropdown.ValueMember = "laboratory_room"; 
            labDropdown.SelectedIndex = 0; 
        }

        private void labDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labDropdown.SelectedItem is DataRowView rowView)
            {
                string selectedRoom = rowView["laboratory_room"].ToString();

                if (selectedRoom == "All")
                {
                    selectedRoom = null;
                }

                LoadData(selectedRoom); 
            }
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


        private void addButton_Click(object sender, EventArgs e)
        {
            Add_Workstation addWorkstationForm = new Add_Workstation(userId);

            addWorkstationForm.ShowDialog();

            LoadData();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                if (e.ColumnIndex == dataGridView1.Columns["monitor"].Index)
                {
                    var status = dataGridView1.Rows[e.RowIndex].Cells["monitorStatus"].Value?.ToString();
                    SetCellColorBasedOnStatus(e, status);
                }
                else if (e.ColumnIndex == dataGridView1.Columns["keyboard"].Index)
                {
                    var status = dataGridView1.Rows[e.RowIndex].Cells["keyboardStatus"].Value?.ToString();
                    SetCellColorBasedOnStatus(e, status);
                }
                else if (e.ColumnIndex == dataGridView1.Columns["mouse"].Index)
                {
                    var status = dataGridView1.Rows[e.RowIndex].Cells["mouseStatus"].Value?.ToString();
                    SetCellColorBasedOnStatus(e, status);
                }
                else if (e.ColumnIndex == dataGridView1.Columns["avr"].Index)
                {
                    var status = dataGridView1.Rows[e.RowIndex].Cells["avrStatus"].Value?.ToString();
                    SetCellColorBasedOnStatus(e, status);
                }
            }
        }

        private void SetCellColorBasedOnStatus(DataGridViewCellFormattingEventArgs e, string status)
        {
            if (status == "Repair")
            {
                e.CellStyle.BackColor = Color.Yellow;
            }
            else if (status == "Replacement")
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.ForeColor = Color.White;
            }
            else if (status == "Working")
            {
                e.CellStyle.BackColor = Color.White;
            }
        }
        private void BlackText()
        {
            label1.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
        }

        private void modifyTitleLabel()
        {
            string selectedRoom = labDropdown.SelectedValue?.ToString() ?? "All";
            titleLabel.Text = $"Workstations - {selectedRoom}";
        }
    }
}
