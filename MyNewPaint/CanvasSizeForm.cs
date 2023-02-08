using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyNewPaint
{
    public partial class CanvasSizeForm : Form
    {
        

        private int _canvasWidth;
        private int _canvasHeight;

        public int CanvasWidth
        {
            get
            {
                return _canvasWidth;
            }
            set
            {
                _canvasWidth = value;
                TextboxWidth.Text = Convert.ToString(value);
            }
        }

        public int CanvasHeight
        {
            get
            {
                return _canvasHeight;
            }
            set
            {
                _canvasHeight = value;
                TextboxHeight.Text = Convert.ToString(value);
            }
        }

        public CanvasSizeForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _canvasWidth = Convert.ToInt32(TextboxWidth.Text);
            _canvasHeight = Convert.ToInt32(TextboxHeight.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse((sender as TextBox).Text, out _canvasWidth) && _canvasWidth > 0)
            {
                (sender as TextBox).ForeColor = SystemColors.ControlText;
                if (int.TryParse(TextboxHeight.Text, out _canvasWidth) && _canvasWidth > 0)
                    OKButton.Enabled = true;
            }
            else
            {
                (sender as TextBox).ForeColor = Color.Red;
                OKButton.Enabled = false;
            }
        }

        private void TbHeight_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse((sender as TextBox).Text, out _canvasWidth) && _canvasWidth > 0)
            {
                (sender as TextBox).ForeColor = SystemColors.ControlText;
                if (int.TryParse(TextboxWidth.Text, out _canvasWidth) && _canvasWidth > 0)
                    OKButton.Enabled = true;
            }
            else
            {
                (sender as TextBox).ForeColor = Color.Red;
                OKButton.Enabled = false;
            }
        }
    }
}
