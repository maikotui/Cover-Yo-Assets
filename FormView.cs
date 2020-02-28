using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoverYourAssets
{
    public partial class FormView : Form, IGameView
    {
        public FormView()
        {
            InitializeComponent();
        }

        public int PromptForNumberOfPlayers()
        {
            return ShowDialog("How many people are playing?", "Cover Your Assets");
        }

        public static int ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 200;
            prompt.Height = 100;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 10, Text = text };
            NumericUpDown inputBox = new NumericUpDown() { Left = 25, Top = 30, Width = 150 };
            inputBox.Minimum = 2;
            inputBox.Maximum = 10;
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return (int)inputBox.Value;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
