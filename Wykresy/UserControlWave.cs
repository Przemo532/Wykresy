using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wykresy
{
    public partial class UserControlWave : UserControl
    {
        public delegate void WaveAction(UserControlWave sender);
        public event WaveAction WaveChanged;
        public UserControlWave()
        {
            InitializeComponent();
        }
        public UserControlWave(string name, double fq, double amp, Color c)
        {
            InitializeComponent();
            groupBox1.Text = name;
            numericUpDownFrequency.Value = Convert.ToDecimal(fq);
            numericUpDownAmplitude.Value = Convert.ToDecimal(amp);
            groupBox1.ForeColor = c;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public double getY(double x)
        {
            return Convert.ToDouble(numericUpDownAmplitude.Value) * (Math.Sin(Convert.ToDouble(numericUpDownFrequency.Value) * x));
        }

        public string waveName
        {
            get => groupBox1.Text;
        }
        public Color waveColor
        {
            get => groupBox1.ForeColor;
        }

        public bool checkBoxVisibleState
        {
            get => checkBoxVisible.Checked;
        }

        private void checkBoxVisible_CheckedChanged(object sender, EventArgs e)
        {
            WaveChanged?.Invoke(this);
        }

        private void numericUpDownFrequency_ValueChanged(object sender, EventArgs e)
        {
            WaveChanged?.Invoke(this);
        }

        private void numericUpDownAmplitude_ValueChanged(object sender, EventArgs e)
        {
            WaveChanged?.Invoke(this);
        }
    }
}
