using ExcelDataReader;
using Laboratory_Management_System.UserControls.AdminControls.Crud;
using MySql.Data.MySqlClient;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Data.Filtering.Helpers.PropertyDescriptorCriteriaCompilationSupport;

namespace Laboratory_Management_System.UserControls.AdminControls
{
    public partial class uc_rooms : UserControl
    {
        private DatabaseHelper dbHelper;
        private DataTable dataTable;
        private Timer refreshTimer;

        public uc_rooms()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            CustomizeDataGridView();
            BlackText();
            LoadData();
            EnableDoubleBuffering(dataGridView1);  

            dataGridView1.Paint += DataGridView1_Paint;

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
            string search = searchFilter.Text;
            string query = @"
        SELECT id, laboratory_room, date_added
        FROM rooms";

            if (!string.IsNullOrEmpty(search))
            {
                query += " WHERE laboratory_room LIKE @search";
            }

            query += " ORDER BY date_added DESC";

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            if (!string.IsNullOrEmpty(search))
            {
                parameters.Add(new MySqlParameter("@search", $"%{search}%"));
            }

            DataTable newData = dbHelper.GetData(query, parameters.ToArray());

            if (newData == null)
            {
                MessageBox.Show("Error: Failed to retrieve data.");
                return;
            }

            if (!AreDataTablesEqual(dataTable, newData))
            {
                dataTable = newData;

                dataGridView1.DataSource = dataTable;

                if (dataTable != null)
                {
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["laboratory_room"].HeaderText = "Laboratory Room";
                    dataGridView1.Columns["date_added"].HeaderText = "Date Added";
                    dataGridView1.Columns["id"].HeaderText = "ID";
                }

                AdjustDataGridViewHeight();
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var roomIdsToDelete = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    string roomId = row.Cells["id"].Value.ToString();
                    roomIdsToDelete.Add(roomId);
                }
            }

            if (roomIdsToDelete.Count > 0)
            {
                var confirmResult = MessageBox.Show(
                    "Are you sure you want to delete the selected rooms?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    string ids = string.Join("','", roomIdsToDelete);
                    string query = $"DELETE FROM rooms WHERE id IN ('{ids}')";

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
                MessageBox.Show("No rooms selected for deletion.");
            }
        }

        private void BlackText()
        {
            searchText.ForeColor = Color.Black;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddRoom addRoomForm = new AddRoom();

            DialogResult result = addRoomForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            int roomId = 0;
            int selectedCount = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBox = row.Cells[0] as DataGridViewCheckBoxCell;

                if (checkBox != null && Convert.ToBoolean(checkBox.Value) == true)
                {
                    roomId = Convert.ToInt32(row.Cells["id"].Value); 
                    selectedCount++;
                    if (selectedCount > 1)
                    {
                        MessageBox.Show("Please select only one room to edit.");
                        return;
                    }
                }
            }

            if (selectedCount == 1)
            {
                EditRoom editRoomForm = new EditRoom(roomId);

                DialogResult result = editRoomForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a room to edit.");
            }
        }

        private bool AreDataTablesEqual(DataTable dt1, DataTable dt2)
        {
            if (dt1 == null || dt2 == null)
            {
                return false;
            }

            if (dt1.Rows.Count != dt2.Rows.Count || dt1.Columns.Count != dt2.Columns.Count)
            {
                return false;
            }

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                for (int j = 0; j < dt1.Columns.Count; j++)
                {
                    if (!Equals(dt1.Rows[i][j], dt2.Rows[i][j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }


    }
}
