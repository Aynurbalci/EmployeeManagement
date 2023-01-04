using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace EmployeeManagement
{
    public partial class Form3 : Form
    {
        public int male;
        public int female;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            pieChart2.LegendLocation = LegendLocation.Right;
            LiveCharts.SeriesCollection series = new LiveCharts.SeriesCollection();

            PieSeries pie1 = new PieSeries();
            pie1.Title = "Female";
            pie1.Values = new ChartValues<int> { female };
            pie1.DataLabels = true;
           // pie1.LabelPoint = lab;
            series.Add(pie1);
            PieSeries pie2 = new PieSeries();
            pie2.Title = "Male";
            pie2.Values = new ChartValues<int> { male };
            pie2.DataLabels = true;
          //  pie2.LabelPoint = labelPiont;
            series.Add(pie2);

            pieChart2.Series = series;
        }
    }
}
