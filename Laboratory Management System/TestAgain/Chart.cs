using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestAgain
{
    public partial class Chart : Form
    {
        public Chart()
        {
            InitializeComponent();
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            InitializeChart();
        }

        private void InitializeChart()
        {
            // Clear existing series
            chart1.Series.Clear();

            // Create a new series for the bar chart
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Sample Series",
                Color = Color.Blue,
                IsValueShownAsLabel = true,
                ChartType = SeriesChartType.Bar
            };
            chart1.Series.Add(series);

            // Add data points
            series.Points.AddXY("Category 1", 10);
            series.Points.AddXY("Category 2", 20);
            series.Points.AddXY("Category 3", 30);
            series.Points.AddXY("Category 4", 40);
            series.Points.AddXY("Category 5", 25);

            // Set chart titles
            chart1.Titles.Add("Sample Bar Graph");
            chart1.ChartAreas[0].AxisX.Title = "Categories";
            chart1.ChartAreas[0].AxisY.Title = "Values";
        }
    }
}
