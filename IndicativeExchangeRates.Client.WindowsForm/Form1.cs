using IndicativeExchangeRates.FilterSort;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndicativeExchangeRates.Client.WindowsForm
{
    public partial class Form1 : Form
    {
        Host.ExchangeRateAPI era = new Host.ExchangeRateAPI("muratyasar", "denemetry!4");

        public Form1()
        {
            InitializeComponent();            

            Point newLoc = new Point(5, 5);

            foreach (var item in era.GetAvailableOutputTypes)
            {
                Button b = new Button();
                b.Text = item;
                b.Click += B_Click;
                b.Size = new Size(80, 50);
                b.Location = newLoc;
                newLoc.Offset(0, b.Height + 5);
                splitContainer1.Panel1.Controls.Add(b);
            }

        }

        private void B_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b != null)
            {
                ButtonClick(b.Text);
            }
        }

        private async void ButtonClick(string outputtype)
        {
            textboxAllData.Text = string.Empty;
            labelAllData.Text = string.Empty;
            labelFilteredData.Text = string.Empty;
            labelSortedData.Text = string.Empty;
            labelFilteredAndSortedData.Text = string.Empty;
            panel2.BackColor = SystemColors.Control;
            
            switch (outputtype)
            {
                case "OutputXML":
                    era.RequestedOutput = "OutputXML";
                    break;
                case "OutputCSV":
                    era.RequestedOutput = "OutputCSV";
                    break;
                case "OutputJSON":
                    era.RequestedOutput = "OutputJSON";
                    break;
            }            

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            List<ExpressionFilter> filters = new List<ExpressionFilter>
            {
                new ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="USD",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator=FilterSort.Enums.LogicalOperation.Or
                },
                new ExpressionFilter
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    Value ="EUR",
                    Comparison =FilterSort.Enums.Comparison.Equal,
                    LogicalOperator=FilterSort.Enums.LogicalOperation.Or
                }
            };

            List<ExpressionSort> sorts = new List<ExpressionSort>
            {
                new ExpressionSort
                {                    
                    PropertyName =FilterSort.Enums.FilterColumNames.CurrencyCode,
                    SortDirection = FilterSort.Enums.SortDirection.Ascending
                },
                new ExpressionSort
                {
                    PropertyName =FilterSort.Enums.FilterColumNames.ForexBuying,
                    SortDirection = FilterSort.Enums.SortDirection.Descending
                },
            };
                        
            var resAllData = await era.GetAllData();                       

            stopwatch.Stop();
            labelAllData.Text = "GetAllData took " + stopwatch.ElapsedMilliseconds.ToString() + " ms";

            stopwatch.Reset();
            stopwatch.Start();
            var resFilteredData = await era.GetFilteredData(filters);            
            stopwatch.Stop();
            labelFilteredData.Text = "GetFilteredData took " + stopwatch.ElapsedMilliseconds.ToString() + " ms";

            stopwatch.Reset();
            stopwatch.Start();
            var resSortedData = await era.GetSortedData(sorts);
            stopwatch.Stop();
            labelSortedData.Text = "GetSortedData took " + stopwatch.ElapsedMilliseconds.ToString() + " ms";

            stopwatch.Reset();
            stopwatch.Start();
            var resFilteredAndSortedData = await era.GetFilteredAndSortedData(filters, sorts);
            stopwatch.Stop();
            labelFilteredAndSortedData.Text = "FilteredAndSorted took " + stopwatch.ElapsedMilliseconds.ToString() + " ms";

            textboxAllData.Text =
                "-----All Data-----" +
                System.Environment.NewLine + System.Environment.NewLine +
                resAllData.ToString();
            textboxFilteredData.Text =
                "-----Filtered Data-----" +
                System.Environment.NewLine + System.Environment.NewLine +
                resFilteredData.ToString();
            textboxSortedData.Text =                 
                "-----Sorted Data-----" +
                System.Environment.NewLine + System.Environment.NewLine +
                resSortedData.ToString();
            textboxFilteredAndSortedData.Text =
                "-----Filtered And Sorted Data-----" +
                System.Environment.NewLine + System.Environment.NewLine +
                resFilteredAndSortedData.ToString();

            panel2.BackColor = Color.Khaki;
        }
    }
}
