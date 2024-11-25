using Laboratory_Management_System.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Laboratory_Management_System.UserControls.AdminControls
{
    public partial class uc_workstations_view : UserControl
    {
        private string workstationId;
        private DataGridView infoDataGridView;
        private DataGridView componentsDataGridView;
        private DatabaseHelper dbHelper = new DatabaseHelper();

        public uc_workstations_view()
        {
            InitializeComponent();
            backButton.Click += backButton_Click;
            InitializeTabControl();
        }

        public string WorkstationId
        {
            get { return workstationId; }
            set
            {
                workstationId = value;
                LoadWorkstationDetails(workstationId);
                LoadWorkstationComponents(workstationId);
            }
        }

        private void LoadWorkstationDetails(string id)
        {
            string query = @"
            SELECT workstations.*, system_units.hostname, system_units.operating_system 
            FROM workstations
            LEFT JOIN system_units ON workstations.system_unit = system_units.system_unit_id
            WHERE workstations.workstation_id = @workstationId";

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@workstationId", id)
            };

            DataTable workstationDetails = dbHelper.GetData(query, parameters);

            if (workstationDetails.Rows.Count > 0)
            {
                var row = workstationDetails.Rows[0];
                infoDataGridView.Rows.Clear();

                AddDetailRow("Workstation ID", row["workstation_id"].ToString());
                AddDetailRow("System Unit ID", row["system_unit"].ToString());
                AddDetailRow("Operating System", row["operating_system"].ToString());
                AddDetailRow("Hostname", row["hostname"].ToString());
                AddDetailRow("Room", row["room"].ToString());
                AddDetailRow("Asset Cost", row["asset_cost"].ToString());
                AddDetailRow("Added By", row["added_by"].ToString());
                AddDetailRow("Date Added", row["date_added"].ToString());
                AddDetailRow("Updated By", row["updated_by"]?.ToString() ?? "N/A"); // Handle nulls
                AddDetailRow("Date Updated", row["date_updated"]?.ToString() ?? "N/A"); // Handle nulls
            }
            else
            {
                MessageBox.Show("No details found for workstation ID: " + id);
            }
        }

        private void LoadWorkstationComponents(string workstationId)
        {
            string query = @"
            SELECT 
                workstations.workstation_id,
                workstations.room, 
                system_units.hostname AS hostname, 
                monitors.brand AS monitor_brand, 
                monitors.description AS monitor_description, 
                keyboards.brand AS keyboard_brand, 
                keyboards.description AS keyboard_description, 
                mouse.brand AS mouse_brand, 
                mouse.description AS mouse_description, 
                avr.brand AS avr_brand, 
                avr.description AS avr_description,
                motherboards.brand AS motherboard_brand,
                motherboards.description AS motherboard_description,
                ram.brand AS ram_brand,
                ram.description AS ram_description,
                cpu.brand AS cpu_brand,
                cpu.description AS cpu_description,
                gpu.brand AS gpu_brand,
                gpu.description AS gpu_description,
                psu.brand AS psu_brand,
                psu.description AS psu_description,
                system_unit_case.brand AS system_unit_case_brand,
                system_unit_case.description AS system_unit_case_description
            FROM workstations
            LEFT JOIN monitors ON workstations.monitor = monitors.monitor_id
            LEFT JOIN keyboards ON workstations.keyboard = keyboards.keyboard_id
            LEFT JOIN mouse ON workstations.mouse = mouse.mouse_id
            LEFT JOIN avr ON workstations.avr = avr.avr_id
            LEFT JOIN system_units ON workstations.system_unit = system_units.system_unit_id
            LEFT JOIN motherboards ON system_units.motherboard = motherboards.motherboard_id
            LEFT JOIN ram ON system_units.ram = ram.ram_id
            LEFT JOIN cpu ON system_units.cpu = cpu.cpu_id
            LEFT JOIN gpu ON system_units.gpu = gpu.gpu_id
            LEFT JOIN psu ON system_units.psu = psu.psu_id
            LEFT JOIN system_unit_case ON system_units.system_unit_case = system_unit_case.system_unit_case_id
            WHERE workstations.workstation_id = @workstationId";

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@workstationId", workstationId)
            };

            DataTable componentsData = dbHelper.GetData(query, parameters);

            if (componentsData.Rows.Count > 0)
            {
                var row = componentsData.Rows[0];
                componentsDataGridView.Rows.Clear();

                AddComponentRow("Monitor", row["monitor_brand"]?.ToString() ?? "N/A", row["monitor_description"]?.ToString() ?? "N/A");
                AddComponentRow("Keyboard", row["keyboard_brand"]?.ToString() ?? "N/A", row["keyboard_description"]?.ToString() ?? "N/A");
                AddComponentRow("Mouse", row["mouse_brand"]?.ToString() ?? "N/A", row["mouse_description"]?.ToString() ?? "N/A");
                AddComponentRow("AVR", row["avr_brand"]?.ToString() ?? "N/A", row["avr_description"]?.ToString() ?? "N/A");
                AddComponentRow("Motherboard", row["motherboard_brand"]?.ToString() ?? "N/A", row["motherboard_description"]?.ToString() ?? "N/A");
                AddComponentRow("CPU", row["cpu_brand"]?.ToString() ?? "N/A", row["cpu_description"]?.ToString() ?? "N/A");
                AddComponentRow("RAM", row["ram_brand"]?.ToString() ?? "N/A", row["ram_description"]?.ToString() ?? "N/A");
                AddComponentRow("GPU", row["gpu_brand"]?.ToString() ?? "N/A", row["gpu_description"]?.ToString() ?? "N/A");
                AddComponentRow("PSU", row["psu_brand"]?.ToString() ?? "N/A", row["psu_description"]?.ToString() ?? "N/A");
                AddComponentRow("System Unit Case", row["system_unit_case_brand"]?.ToString() ?? "N/A", row["system_unit_case_description"]?.ToString() ?? "N/A");
            }
            else
            {
                MessageBox.Show("No components found for workstation ID: " + workstationId);
            }
        }

        private void AddDetailRow(string title, string value)
        {
            infoDataGridView.Rows.Add(title, value);
        }

        private void AddComponentRow(string header, string brand, string description)
        {
            componentsDataGridView.Rows.Add(header, brand, description);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var parentForm = this.ParentForm as Admin;
            if (parentForm != null)
            {
                parentForm.ShowWorkstationsControl();
            }
        }

        private void InitializeTabControl()
        {
            Panel infoPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            infoDataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                EnableHeadersVisualStyles = false,
                GridColor = Color.LightGray,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                Enabled = false,
                Height = 500,
                ScrollBars = ScrollBars.None
            };

            infoDataGridView.Columns.Add("TitleColumn", "Title");
            infoDataGridView.Columns.Add("ValueColumn", "Value");

            infoDataGridView.ColumnHeadersVisible = false;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(29, 60, 170),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            infoDataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Montserrat", 10, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(255, 255, 255),
                SelectionForeColor = Color.Black,
                WrapMode = DataGridViewTriState.True,
                Padding = new Padding(10, 0, 0, 0)
            };
            infoDataGridView.DefaultCellStyle = rowStyle;

            DataGridViewCellStyle alternatingRowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 245, 245)
            };
            infoDataGridView.AlternatingRowsDefaultCellStyle = alternatingRowStyle;

            infoDataGridView.RowTemplate.Height = 60;
            infoDataGridView.RowTemplate.Resizable = DataGridViewTriState.False;

            infoPanel.Controls.Add(infoDataGridView);

            var componentsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            componentsDataGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                EnableHeadersVisualStyles = false,
                GridColor = Color.LightGray,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                Enabled = false,
                Height = 500,
                ScrollBars = ScrollBars.None
            };

            componentsDataGridView.Columns.Add("ComponentColumn", "Component");
            componentsDataGridView.Columns.Add("BrandColumn", "Brand");
            componentsDataGridView.Columns.Add("DescriptionColumn", "Description");

            componentsDataGridView.ColumnHeadersVisible = false;

            componentsDataGridView.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            componentsDataGridView.DefaultCellStyle = rowStyle;
            componentsDataGridView.AlternatingRowsDefaultCellStyle = alternatingRowStyle;

            componentsDataGridView.RowTemplate.Height = 60;
            componentsDataGridView.RowTemplate.Resizable = DataGridViewTriState.False;

            componentsPanel.Controls.Add(componentsDataGridView);

            viewTabControl.TabPages["infoPage"].Controls.Add(infoPanel);
            viewTabControl.TabPages["componentsPage"].Controls.Add(componentsPanel);
        }
    }
}
