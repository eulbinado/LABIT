using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Laboratory_Management_System.AdminForms
{
    public partial class WorkStationView : Form
    {
        private string workstationID;
        private DatabaseHelper dbHelper;

        public WorkStationView(string workstationId)
        {
            InitializeComponent();
            CustomizeForm();
            this.workstationID = workstationId;
            dbHelper = new DatabaseHelper();
            label6.Text = workstationID;
            label9.Text = workstationID;
        }

        private void WorkStationView_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(workstationID))
            {
                LoadWorkstationDetails();
                LoadWorkstationHistory();
                LoadUsageHistory();
            }

            okButton.Focus();
            this.AcceptButton = okButton;
        }

        private void CustomizeForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void LoadWorkstationDetails()
        {
            string query = @"
SELECT workstations.workstation_id, 
       workstations.system_unit AS systemUnitId, 
       system_units.operating_system AS operatingSystem, 
       system_units.hostname AS hostname, 
       workstations.room, 
       workstations.added_by AS addedBy, 
       workstations.date_added AS dateAdded, 
       workstations.time_usage AS timeUsage, 
       MAX(CASE WHEN assets.asset_type = 'Monitor' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS monitor, 
       MAX(CASE WHEN assets.asset_type = 'Keyboard' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS keyboard, 
       MAX(CASE WHEN assets.asset_type = 'Mouse' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS mouse, 
       MAX(CASE WHEN assets.asset_type = 'AVR' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS avr, 
       MAX(CASE WHEN assets.asset_type = 'Motherboard' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS motherboard, 
       MAX(CASE WHEN assets.asset_type = 'CPU' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS cpu, 
       MAX(CASE WHEN assets.asset_type = 'RAM' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS ram, 
       MAX(CASE WHEN assets.asset_type = 'GPU' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS gpu, 
       MAX(CASE WHEN assets.asset_type = 'PSU' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS psu, 
       MAX(CASE WHEN assets.asset_type = 'Storage' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS storage,
       MAX(CASE WHEN assets.asset_type = 'System Unit Case' THEN CONCAT(assets.asset_brand, ' | ', IFNULL(assets.asset_description, 'N/A')) END) AS systemCase
FROM workstations
LEFT JOIN system_units ON workstations.system_unit = system_units.system_unit_id
LEFT JOIN assets ON assets.asset_id IN (workstations.monitor, workstations.keyboard, workstations.mouse, workstations.avr, system_units.motherboard, system_units.cpu, system_units.ram, system_units.gpu, system_units.psu, system_units.storage, system_units.system_unit_case)
WHERE workstations.workstation_id = @WorkstationId
GROUP BY workstations.workstation_id";

            try
            {
                var parameters = new[] { new MySqlParameter("@WorkstationId", workstationID) };
                DataTable dt = dbHelper.GetData(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    label1.Text = row["workstation_id"].ToString() ?? "N/A";
                    systemUnitId.Text = row["systemUnitId"].ToString() ?? "N/A";
                    operatingSystem.Text = row["operatingSystem"].ToString() ?? "N/A";
                    hostname.Text = row["hostname"].ToString() ?? "N/A";
                    room.Text = row["room"].ToString() ?? "N/A";
                    dateAdded.Text = row["dateAdded"] != DBNull.Value ? Convert.ToDateTime(row["dateAdded"]).ToString("yyyy-MM-dd") : "N/A";
                    addedBy.Text = row["addedBy"].ToString() ?? "N/A";

                    long timeUsageInSeconds = row["timeUsage"] != DBNull.Value ? Convert.ToInt64(row["timeUsage"]) : 0;
                    string formattedTimeUsage = FormatTimeUsage(timeUsageInSeconds);
                    usageTime.Text = formattedTimeUsage;

                    if (row["operatingSystem"].ToString() == "MAC")
                    {
                        monitor.Text = "N/A";
                        motherboard.Text = "N/A";
                        cpu.Text = "N/A";
                        ram.Text = "N/A";
                        gpu.Text = "N/A";
                        psu.Text = "N/A";
                        storage.Text = "N/A";
                        sucase.Text = "N/A";
                    }
                    else
                    {
                        monitor.Text = row["monitor"].ToString() ?? "N/A";
                        motherboard.Text = row["motherboard"].ToString() ?? "N/A";
                        cpu.Text = row["cpu"].ToString() ?? "N/A";
                        ram.Text = row["ram"].ToString() ?? "N/A";
                        gpu.Text = row["gpu"].ToString() ?? "N/A";
                        psu.Text = row["psu"].ToString() ?? "N/A";
                        storage.Text = row["storage"].ToString() ?? "N/A";
                        sucase.Text = row["systemCase"].ToString() ?? "N/A";
                    }

                    keyboard.Text = row["keyboard"].ToString() ?? "N/A";
                    mouse.Text = row["mouse"].ToString() ?? "N/A";
                    avr.Text = row["avr"].ToString() ?? "N/A";
                }
                else
                {
                    MessageBox.Show("No details found for the selected workstation.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private string FormatTimeUsage(long totalSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2} hr/s", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        private void LoadWorkstationHistory()
        {
            flowLayoutPanelHistory.Controls.Clear(); // Clear previous entries

            // Load Repair History
            string repairHistoryQuery = @"
        SELECT rh.repair_history_id, rh.asset, rh.date_processed, a.asset_type,
               u.firstname, u.lastname
        FROM repair_history rh
        LEFT JOIN assets a ON rh.asset = a.asset_id
        LEFT JOIN users u ON rh.processed_by = u.user_id
        WHERE rh.workstation_id = @WorkstationId";

            var repairParams = new[] { new MySqlParameter("@WorkstationId", workstationID) };
            DataTable repairDt = dbHelper.GetData(repairHistoryQuery, repairParams);

            foreach (DataRow row in repairDt.Rows)
            {
                string assetType = row["asset_type"].ToString();
                string asset = row["asset"].ToString();
                string processedBy = $"{row["firstname"]} {row["lastname"]}";
                string dateProcessed = row["date_processed"] != DBNull.Value
                    ? Convert.ToDateTime(row["date_processed"]).ToString("yyyy-MM-dd hh:mm tt")
                    : "N/A";

                string repairText = $"[ {dateProcessed} ]          {assetType}: {asset} has been repaired/processed by {processedBy}";
                Label repairLabel = new Label { Text = repairText, AutoSize = true };
                flowLayoutPanelHistory.Controls.Add(repairLabel);
            }

            // Load Replacement History
            string replacementHistoryQuery = @"
        SELECT rh.current_asset, rh.new_asset, rh.date_processed, ac.asset_type AS current_asset_type,
               u.firstname, u.lastname
        FROM replacement_history rh
        LEFT JOIN assets ac ON rh.current_asset = ac.asset_id
        LEFT JOIN users u ON rh.processed_by = u.user_id
        WHERE rh.workstation_id = @WorkstationId";

            var replacementParams = new[] { new MySqlParameter("@WorkstationId", workstationID) };
            DataTable replacementDt = dbHelper.GetData(replacementHistoryQuery, replacementParams);

            foreach (DataRow row in replacementDt.Rows)
            {
                string currentAssetType = row["current_asset_type"].ToString();
                string currentAsset = row["current_asset"].ToString();
                string newAsset = row["new_asset"].ToString();
                string processedBy = $"{row["firstname"]} {row["lastname"]}";
                string dateProcessed = row["date_processed"] != DBNull.Value
                    ? Convert.ToDateTime(row["date_processed"]).ToString("yyyy-MM-dd hh:mm tt")
                    : "N/A";

                string replacementText = $"[ {dateProcessed} ]          {currentAssetType}: {currentAsset} has been replaced to {newAsset} by {processedBy}";
                Label replacementLabel = new Label { Text = replacementText, AutoSize = true };
                flowLayoutPanelHistory.Controls.Add(replacementLabel);
            }
        }

        private void LoadUsageHistory()
        {
            flowLayoutUsageHistory.Controls.Clear(); // Clear previous entries

            // Step 1: Retrieve the hostname for the workstation's system unit
            string getHostnameQuery = @"
SELECT su.hostname
FROM workstations w
LEFT JOIN system_units su ON w.system_unit = su.system_unit_id
WHERE w.workstation_id = @WorkstationId";

            string hostname = null;
            var hostnameParams = new[] { new MySqlParameter("@WorkstationId", workstationID) };
            DataTable hostnameDt = dbHelper.GetData(getHostnameQuery, hostnameParams);

            if (hostnameDt.Rows.Count > 0)
            {
                hostname = hostnameDt.Rows[0]["hostname"].ToString();
            }

            if (string.IsNullOrEmpty(hostname))
            {
                MessageBox.Show("Hostname not found for the selected workstation.");
                return;
            }

            // Step 2: Retrieve attendance records for this hostname
            string attendanceQuery = @"
SELECT a.student_id, a.time_in, a.time_out, a.attendance_date,
       s.firstname, s.lastname
FROM attendance a
LEFT JOIN students s ON a.student_id = s.student_id
WHERE a.workstation = @Hostname
ORDER BY a.attendance_date DESC, a.time_in DESC";

            var attendanceParams = new[] { new MySqlParameter("@Hostname", hostname) };
            DataTable attendanceDt = dbHelper.GetData(attendanceQuery, attendanceParams);

            // Step 3: Display each attendance entry in the format "${student_name} used the workstation from ${time_in} to ${time_out} at ${attendance_date}"
            foreach (DataRow row in attendanceDt.Rows)
            {
                string studentName = $"{row["firstname"]} {row["lastname"]}";

                // Convert TimeSpan to DateTime for formatting with AM/PM
                string timeIn = row["time_in"] != DBNull.Value ?
                    DateTime.Today.Add((TimeSpan)row["time_in"]).ToString("hh:mm tt") : "N/A";
                string timeOut = row["time_out"] != DBNull.Value ?
                    DateTime.Today.Add((TimeSpan)row["time_out"]).ToString("hh:mm tt") : "N/A";

                string attendanceDate = row["attendance_date"] != DBNull.Value ?
                    Convert.ToDateTime(row["attendance_date"]).ToString("yyyy-MM-dd") : "N/A";

                string usageText = $"[ {attendanceDate} ]          {studentName} used the workstation from {timeIn} to {timeOut}";
                Label usageLabel = new Label { Text = usageText, AutoSize = true };
                flowLayoutUsageHistory.Controls.Add(usageLabel);
            }
        }



        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
