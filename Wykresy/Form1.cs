using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Wykresy
{
    public partial class Form1 : Form
    {
        delegate double functionDelegate(double x);
        double quadraticFunction(double x)
        {
            return x * x;
        }
        public Form1()
        {
            InitializeComponent();

            chartWaves.Series.Clear();

            /*functionDelegate fun = Math.Sin;
            
            drawFunction(fun);
            drawFunction(Math.Cos);
            drawFunction(quadraticFunction);
            */

            flowLayoutPanelConfig.Controls.Add(new UserControlWave("Delta", 5, 20, Color.Red));
            flowLayoutPanelConfig.Controls.Add(new UserControlWave("Teta", 10, 10, Color.Blue));
            flowLayoutPanelConfig.Controls.Add(new UserControlWave("Alfa", 8, 8, Color.Green));
            flowLayoutPanelConfig.Controls.Add(new UserControlWave("SMR", 16, 4, Color.Magenta));
            flowLayoutPanelConfig.Controls.Add(new UserControlWave("Beta", 18, 3, Color.Orange));
            flowLayoutPanelConfig.Controls.Add(new UserControlWave("Beta 2", 35, 7, Color.Brown));

            foreach (Object o in flowLayoutPanelConfig.Controls)
            {
                if (o is UserControlWave)
                {
                    UserControlWave wave = o as UserControlWave;
                    wave.WaveChanged += W_WaveChanged;
                    W_WaveChanged(wave);
                }
            }
        }

        private void drawFunction(functionDelegate f)
        {
            Series s = new Series();

            s.ChartType = SeriesChartType.Line;
            //s.Color = Color.Red;
            //s.Name = "Funkcja";

            for (double x = -3; x <= 3; x += 0.1)    //creating x and y values for quadratic function and adding them straight to the chart
            {
                s.Points.AddXY(x, f(x));
            }

            chartWaves.Series.Add(s);
        }

        /*private void drawButton_Click(object sender, EventArgs e)
        {
            chartWaves.Series.Clear();
            foreach (Object o in flowLayoutPanelConfig.Controls)
            {
                if(o is UserControlWave)
                {
                    UserControlWave wave = o as UserControlWave;

                    Series s = new Series();
                    s.ChartType = SeriesChartType.Line;
                    s.Name = wave.waveName;
                    s.Color = wave.waveColor;
                    if (wave.checkBoxVisibleState)
                    {
                        for (double x = 0; x < 10; x += 0.01)
                        {
                            s.Points.AddXY(x, (wave as UserControlWave).getY(x));
                        }
                    }

                    chartWaves.Series.Add(s);
                }
            }
        }*/

        private void W_WaveChanged(UserControlWave sender)
        {
            //MessageBox.Show("Zmiana " + sender.waveName);

            //chartWaves.Series.Clear();

            //UserControlWave wave = o as UserControlWave;

            Series s;

            s = chartWaves.Series.SingleOrDefault(x => x.Name == sender.waveName);

            if (s == null)
            {
                s = new Series();
                s.Name = sender.waveName;
                s.Color = sender.waveColor;
                s.ChartType = SeriesChartType.Line;
                chartWaves.Series.Add(s);
            }
            else
            {
               s.Points.Clear();
            }
            if (sender.checkBoxVisibleState)
            {
                for (double x = 0; x < 10; x += 0.02)
                {
                    s.Points.AddXY(x, sender.getY(x));
                }
            }

            /*if(wave == sender)
            {
                Series s = new Series();
                s.ChartType = SeriesChartType.Line;
                s.Name = wave.waveName;
                s.Color = wave.waveColor;
                if (wave.checkBoxVisibleState)
                    {
                    for (double x = 0; x < 10; x += 0.01)
                    {
                        s.Points.AddXY(x, (wave as UserControlWave).getY(x));
                    }
                }
                chartWaves.Series.Add(s);
            }*/

            chartWaves.Series.Remove(chartWaves.Series.SingleOrDefault(x => x.Name == "EEG"));
            Series sum = new Series();
            sum.Name = "EEG";
            sum.Color = Color.Black;
            sum.BorderWidth = 5;
            sum.ChartType = SeriesChartType.Line;

            for (double x = 0; x < 10; x += 0.02)
            {
                Double ySum = 0;
                //foreach(UserControlWave wave in flowLayoutPanelConfig.Controls)
                foreach(Series tmp in chartWaves.Series)
                {
                    if(tmp.Points.Count > 0)
                    {
                        ySum += tmp.Points.Single(p => p.XValue == x).YValues[0];
                        //ySum += wave.getY(x);
                    }
                }
                sum.Points.AddXY(x, ySum);
            }

            chartWaves.Series.Add(sum);
        }
    }
}
