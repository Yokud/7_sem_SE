using Kolmogor;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int states;

        public Form1()
        {
            InitializeComponent();

            ResultDataGridView.Columns.Add("ultProb", "Предельная вероятность");
            ResultDataGridView.Columns.Add("statTime", "Точка стабилизации системы");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            states = int.Parse(StatesValue.Text);

            if (states > KolmogorMath.MaxStatesCount)
            {
                MessageBox.Show("States must be less or equal " + KolmogorMath.MaxStatesCount);
                MainDataGridView.Rows.Clear();
                MainDataGridView.Columns.Clear();
                return;
            }

            MainDataGridView.Rows.Clear();
            MainDataGridView.Columns.Clear();

            for (int i = 0; i < states; i++)
            {
                MainDataGridView.Columns.Add("P" + i, i.ToString());
                MainDataGridView.Rows.Add();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Matrix<double> matrix = Matrix<double>.Build.Dense(states, states);

            for (int i = 0; i < states; i++)
            {
                for (int j = 0; j < states; j++)
                    matrix[i, j] = double.Parse(MainDataGridView.Rows[i].Cells[j].Value.ToString(), CultureInfo.InvariantCulture);
            }

            var probs = KolmogorMath.GetUltimatePropabilities(matrix);
            var stats = KolmogorMath.GetStabilizationTimes(matrix, probs.EnumerateColumns().First()).ToArray();

            ResultDataGridView.Rows.Clear();

            for (int i = 0; i < states; i++)
                ResultDataGridView.Rows.Add(probs[i, 0], stats[i]);
        }
    }
}
