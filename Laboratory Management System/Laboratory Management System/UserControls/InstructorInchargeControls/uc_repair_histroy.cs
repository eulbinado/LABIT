﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Windows.Input;
using System.Collections.Generic;

namespace Laboratory_Management_System.UserControls.InstructorsControls
{
    public partial class uc_repair_history : UserControl
    {
        private DatabaseHelper dbHelper;
        private DataTable dataTable;
        private string userId;
        private PrintDocument printDocument1;
        private int totalWidth = 0;
        private int rowPos = 0;
        private int currentPage = 1;
        private Timer refreshTimer;

        public uc_repair_history(string userId)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            this.userId = userId;
            printDocument1 = new PrintDocument();
            CustomizeDataGridView();
            LoadData();
            EnableDoubleBuffering(dataGridView1);
            dataGridView1.Paint += DataGridView1_Paint;

            string assignedRoom = GetAssignedRoom(userId);
            titleLabel.Text = $"Repair History - {assignedRoom}";

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
            DateTime from = fromDate.Value.Date;
            DateTime to = toDate.Value.Date.AddDays(1).AddTicks(-1);

            string query = @"
SELECT 
    rh.repair_history_id,
    rh.request_id,
    rh.system_unit_id,
    rh.workstation_id,
    rh.room,
    rh.asset AS repaired_asset,
    IFNULL(CONCAT(reqUser.firstname, ' ', reqUser.lastname), 'N/A') AS requested_by,
    IFNULL(DATE_FORMAT(rh.date_requested, '%Y-%m-%d %H:%i:%s'), 'N/A') AS date_requested,
    IFNULL(CONCAT(procUser.firstname, ' ', procUser.lastname), 'N/A') AS processed_by,
    IFNULL(DATE_FORMAT(rh.date_processed, '%Y-%m-%d %H:%i:%s'), 'N/A') AS date_processed
FROM repair_history rh
LEFT JOIN users reqUser ON rh.requested_by = reqUser.user_id
LEFT JOIN users procUser ON rh.processed_by = procUser.user_id
WHERE rh.room = @AssignedRoom
    AND (rh.date_processed BETWEEN @From AND @To)
    AND (rh.asset LIKE @SearchTerm 
         OR IFNULL(reqUser.firstname, '') LIKE @SearchTerm
         OR IFNULL(reqUser.lastname, '') LIKE @SearchTerm
         OR IFNULL(procUser.firstname, '') LIKE @SearchTerm
         OR IFNULL(procUser.lastname, '') LIKE @SearchTerm)
ORDER BY rh.date_processed DESC";

            var parameters = new List<MySqlParameter>
    {
        new MySqlParameter("@AssignedRoom", assignedRoom),
        new MySqlParameter("@From", from),
        new MySqlParameter("@To", to),
        new MySqlParameter("@SearchTerm", "%" + searchTerm + "%")
    };

            DataTable dataTable = dbHelper.GetData(query, parameters.ToArray());
            dataGridView1.DataSource = dataTable;

            if (dataGridView1.Columns["repair_history_id"] != null)
                dataGridView1.Columns["repair_history_id"].Visible = false;

            if (dataGridView1.Columns["request_id"] != null)
                dataGridView1.Columns["request_id"].Visible = false;

            if (dataGridView1.Columns["room"] != null)
                dataGridView1.Columns["room"].Visible = false;

            dataGridView1.Columns["system_unit_id"].HeaderText = "System Unit";
            dataGridView1.Columns["workstation_id"].HeaderText = "Workstation";
            dataGridView1.Columns["repaired_asset"].HeaderText = "Repaired Asset";
            dataGridView1.Columns["requested_by"].HeaderText = "Requested By";
            dataGridView1.Columns["date_requested"].HeaderText = "Date Requested";
            dataGridView1.Columns["processed_by"].HeaderText = "Processed By";
            dataGridView1.Columns["date_processed"].HeaderText = "Date Processed";

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


        private void printButton_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrintPage += new PrintPageEventHandler(PrintDocument1_PrintPage);
                printDocument1.Print();
            }
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                int rowCount = dataGridView1.Rows.Count;
                bool morePagesToPrint = false;
                int rowPos = 0;

                Font titleFont = new Font("Montserrat", 16, FontStyle.Bold);
                Font subtitleFont = new Font("Montserrat", 10, FontStyle.Regular);

                // Title
                string title = $"Repair History Report ({GetAssignedRoom(userId)})";
                SizeF titleSize = e.Graphics.MeasureString(title, titleFont);
                e.Graphics.DrawString(title, titleFont, Brushes.Black,
                    new PointF((e.PageBounds.Width - titleSize.Width) / 2, e.MarginBounds.Top - 15));

                // Date Range Filter
                DateTime startDate = fromDate.Value;
                DateTime endDate = toDate.Value;
                string dateRange = $"{startDate:MMMM d, yyyy} - {endDate:MMMM d, yyyy}";

                SizeF dateRangeSize = e.Graphics.MeasureString(dateRange, subtitleFont);
                e.Graphics.DrawString(dateRange, subtitleFont, Brushes.Black,
                    new PointF((e.PageBounds.Width - dateRangeSize.Width) / 2, e.MarginBounds.Top + titleSize.Height + 10));

                // Generated Date
                DateTime date = DateTime.Now;
                string dateSubtitle = $"Generated on: {date: MMMM d, yyyy}";
                SizeF dateSubtitleSize = e.Graphics.MeasureString(dateSubtitle, subtitleFont);
                e.Graphics.DrawString(dateSubtitle, subtitleFont, Brushes.Black,
                    new PointF((e.PageBounds.Width - dateSubtitleSize.Width) / 2, e.MarginBounds.Top + titleSize.Height + dateRangeSize.Height + 20));

                float tableTopPos = e.MarginBounds.Top + titleSize.Height + dateRangeSize.Height + dateSubtitleSize.Height + 40;

                // Column Width Calculations
                float totalWidth = 0;
                string[] columnOrder = { "repair_history_id", "repaired_asset", "requested_by", "processed_by", "date_processed" };
                float[] columnWidths = new float[columnOrder.Length];

                for (int i = 0; i < columnOrder.Length; i++)
                {
                    string colName = columnOrder[i];
                    if (dataGridView1.Columns.Contains(colName) && dataGridView1.Columns[colName] != null)
                    {
                        if (dataGridView1.Columns[colName].Visible)
                        {
                            columnWidths[i] = dataGridView1.Columns[colName].Width;
                            totalWidth += columnWidths[i];
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Column '{colName}' not found in the dataGridView.", "Column Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                float maxWidth = e.MarginBounds.Width - 20; // Leave a little margin for the sides
                float scaleFactor = totalWidth > maxWidth ? maxWidth / totalWidth : 1;

                // Scale column widths
                for (int i = 0; i < columnOrder.Length; i++)
                {
                    if (dataGridView1.Columns[columnOrder[i]].Visible)
                    {
                        columnWidths[i] *= scaleFactor; // Scale down if needed
                    }
                }

                // Adjust left margin based on available space
                float leftMargin = e.MarginBounds.Left + (e.MarginBounds.Width - totalWidth * scaleFactor) / 2;

                // Table Headers with Center Alignment
                Font headerFont = new Font("Montserrat", 10, FontStyle.Bold);
                float headerHeight = 40;
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                for (int i = 0; i < columnOrder.Length; i++)
                {
                    string colName = columnOrder[i];
                    if (!dataGridView1.Columns[colName].Visible)
                        continue;

                    e.Graphics.DrawString(dataGridView1.Columns[colName].HeaderText, headerFont, Brushes.Black,
                        new RectangleF(leftMargin, tableTopPos, columnWidths[i], headerHeight), centerFormat);

                    leftMargin += columnWidths[i];
                }

                // Table Rows with Center Alignment
                int startRow = rowPos;
                for (int i = startRow; i < rowCount; i++)
                {
                    leftMargin = e.MarginBounds.Left + (e.MarginBounds.Width - totalWidth * scaleFactor) / 2; // Reset left margin for rows
                    for (int j = 0; j < columnOrder.Length; j++)
                    {
                        string colName = columnOrder[j];
                        if (!dataGridView1.Columns[colName].Visible)
                            continue;

                        e.Graphics.DrawString(dataGridView1.Rows[i].Cells[colName].Value?.ToString() ?? string.Empty,
                            dataGridView1.Font, Brushes.Black,
                            new RectangleF(leftMargin, tableTopPos + headerHeight + dataGridView1.RowTemplate.Height * (i - startRow), columnWidths[j], dataGridView1.RowTemplate.Height), centerFormat);

                        leftMargin += columnWidths[j];
                    }

                    if ((tableTopPos + headerHeight + dataGridView1.RowTemplate.Height * (i - startRow + 1)) >= e.MarginBounds.Bottom)
                    {
                        morePagesToPrint = true;
                        rowPos = i + 1;
                        break;
                    }
                }

                e.HasMorePages = morePagesToPrint;
                if (!morePagesToPrint)
                {
                    rowPos = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
