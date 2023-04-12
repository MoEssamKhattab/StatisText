using LiveCharts;
using LiveCharts.Wpf;
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

namespace Statistical_Analysis
{

    public partial class Form1 : Form
    {
        ApplicationManager AppManager = new ApplicationManager();  //Instanciating an object of ApplicationManager class
        StreamReader inFile;            // Input text file

        List<Character> CharactersList;   //Creating a list of Characters

        int totalCharCount;

        MomentsOfRV Moments = new MomentsOfRV();                //Instanciating an object of MomentsofRV class
        public Form1()
        {
            InitializeComponent();

            CharactersList = new List<Character>();

            AppManager.intialize(ref CharactersList);

        }

        //Update Charts
        void UpdateCharts()
        {
            #region Clear the previous data of each chart
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            cartesianChart2.Series.Clear();
            cartesianChart2.AxisX.Clear();
            cartesianChart2.AxisY.Clear();
            cartesianChart3.Series.Clear();
            cartesianChart3.AxisX.Clear();
            cartesianChart3.AxisY.Clear();
            #endregion

            #region Y-Axes
            //Update probability & PMF Charts
            ColumnSeries col1 = new ColumnSeries()      //Column Series of the Probability Chart
            {
                //DataLabels = true,
                Title = "Probability",
                Values = new ChartValues<double>(),
                LabelPoint = point => point.Y.ToString(),
            };
            
            ColumnSeries col2 = new ColumnSeries()      //Column Series of the PMF Chart    //We have to instanciate another column series instead of 
                                                                                            //directly using col1, since LIVE CHARTS diallows using the same
                                                                                            //Series for different charts
            {
                Title = "PMF",
                Values = new ChartValues<double>(),
                LabelPoint = point => point.Y.ToString()
            };
            
            StepLineSeries CDF_y = new StepLineSeries()       //Vertical axis of CDF Chart
            {
                Title = "CDF",
                Values = new ChartValues<double>(),
                Stroke = null
            };

            
            Axis y1 = new Axis()
            {
                Title = "Probability",
                LabelFormatter = value => value.ToString("N")
            };

            Axis y2 = new Axis()
            {
                Title = "PMF",
                LabelFormatter = value => value.ToString("N")
            };

            Axis y3 = new Axis()
            {
                Title = "CDF",
                LabelFormatter = value => value.ToString("N")
            };
            #endregion

            #region X-Axes
            //Horizontal axis of ProbabilityChart
            Axis x1 = new Axis()
            {
                Title = "Character",
                Labels = new List<string>() { },
                Separator = new Separator() { Step = 1, IsEnabled = false },
            };

            //Horizontal axis of both PMF & CDF charts
            Axis x2 = new Axis() 
            {
                Title = "X",
                Labels = new List<string>() { },
                Separator = new Separator() { Step = 1, IsEnabled = false } 
            };

            Axis x3 = new Axis()
            {
                Title = "X",
                Labels = new List<string>() { },
                Separator = new Separator() { Step = 1, IsEnabled = false }
            };

            double sum = 0.0;
            for (int i = 0; i < CharactersList.Count(); i++)
            {
                double PMF = (double)CharactersList[i].occurrence_no / (double)totalCharCount;
                sum += PMF;

                col1.Values.Add(PMF);   //Math.Round(PMF, 7, MidpointRounding.ToEven)
                col2.Values.Add(PMF);
                CDF_y.Values.Add(sum);

                x1.Labels.Add(CharactersList[i].Char.ToString());
                x2.Labels.Add(i.ToString()); 
                x3.Labels.Add(i.ToString());
            }
            #endregion

            #region Fill Charts
            //Propability Chart
            cartesianChart1.Series.Add(col1);
            cartesianChart1.AxisX.Add(x1);
            cartesianChart1.AxisY.Add(y1);

            cartesianChart1.Zoom = ZoomingOptions.X;

            //PMF Chart
            cartesianChart2.Series.Add(col2);
            cartesianChart2.AxisX.Add(x2);
            cartesianChart2.AxisY.Add(y2);

            cartesianChart2.Zoom = ZoomingOptions.X;

            //CDF Chart
            cartesianChart3.Series.Add(CDF_y);
            cartesianChart3.AxisX.Add(x3);
            cartesianChart3.AxisY.Add(y3);

            cartesianChart3.Zoom = ZoomingOptions.X;
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            int n = int.Parse(CharNumberText.Text);
            
            for (int i=0; i<n; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = CharactersList[i].Char;
                row.Cells[1].Value = CharactersList[i].occurrence_no;
                dataGridView1.Rows.Add(row);
            }
        }

        private void browseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose .txt File";
            //openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Text File|*.txt";      //Select .txt files only

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                CharNumberText.Clear();

                string filePath = openFileDialog.FileName;     //files path

                this.inFile = AppManager.readFile(filePath);

                AppManager.resetCharactersList(ref CharactersList);

                this.totalCharCount = AppManager.extractFileData(inFile, ref CharactersList);

                if (this.totalCharCount == 0)
                {
                    //Switch to SimulationMode    VIEW 0
                    tabControl1.SelectTab(tabPage1);
                    MessageBox.Show("File is empty, or has no characters [0-9, a-z & A-Z].\nPlease enter a file with valid data.");
                }
                else
                {
                    //AppManager.updatePMFandCDF(CharactersList, totalCharCount);
                    //start ineractiveMode

                    UpdateCharts();
                    Moments.updateMoments(CharactersList, totalCharCount);
                    
                    AppManager.sortList(ref CharactersList);

                    //Update Moments Labels
                    MeanLabel.Text = this.Moments.Mean.ToString(); //.Substring(0,4);
                    Variancelabel.Text = this.Moments.Variance.ToString(); //.Substring(0,4);
                    Skewnesslabel.Text = this.Moments.Skewness.ToString(); //.Substring(0,4);
                    Kurtosislabel.Text = this.Moments.Kurtosis.ToString(); //.Substring(0,4);

                    tabControl1.SelectTab(tabPage2);
                }
            }
        }

        private void uploadAnotherFileBtn_Click(object sender, EventArgs e)
        {
            browseFileBtn_Click(sender, e);
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            if (!int.TryParse(CharNumberText.Text, out int value) ||
            string.IsNullOrEmpty(CharNumberText.Text) ||
            int.Parse(CharNumberText.Text) <= 0 || int.Parse(CharNumberText.Text) > 62)
            {
                MessageBox.Show("Please Enter a number from 1 to 62");
                CharNumberText.Clear();
            }
            else
            {
                int n = int.Parse(CharNumberText.Text);

                for (int i = 0; i < n; i++)
                {
                    string[] row = new string[] { $"{CharactersList[i].Char}", $"{CharactersList[i].occurrence_no}" };
                    dataGridView1.Rows.Add(row);
                }
            }
        }
        private void CharNumberText_TextChanged(object sender, EventArgs e)
        {
        //    if (!int.TryParse(CharNumberText.Text, out int value) ||
        //    string.IsNullOrEmpty(CharNumberText.Text) ||
        //    int.Parse(CharNumberText.Text) <= 0 || int.Parse(CharNumberText.Text) > 62)
        //    {
        //        ShowBtn.Enabled = false;
        //    }
        //    else
        //    {
        //        ShowBtn.Enabled = true;
        //    }
        }
    }
}