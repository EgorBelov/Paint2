using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyNewPaint
{
    public partial class MainForm : Form
    {
        public static Color CurrentColor = Color.Black;
        public static Tools CurrentTool = Tools.Pen;
        public static int CurrentLineWidth = 3;
        public static int CurrentStarCount = 5;
        private int NumberOfDoc = 1;

        public MainForm()
        {
            InitializeComponent();
        }



        #region Расположение окон

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        #endregion



        



        #region Выбор инструмента
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Pen;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Circle;
        }
        private void EraserButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Eraser;
        }
        private void LineButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Line;
        }
        private void StarButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Star;
        }

        #endregion



        #region Пункты меню
        private void файлToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            сохранитьКакToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }
        private void рисунокToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            отменитьДействиеToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            вернутьДействиеToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }
        private void окноToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            каскадомToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            слеваНаправоToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            сверхуВнизToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            упорядочитьЗначкиToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }
        #endregion


        #region Файлы

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((DocumentForm)ActiveMdiChild).Save();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((DocumentForm)ActiveMdiChild).SaveAs();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new DocumentForm();
            f.Text = $"Документ {NumberOfDoc++}";
            f.MdiParent = this;
            f.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg|Все файлы ()*.*|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DocumentForm frmChild = new DocumentForm(dlg.FileName);
                frmChild.MdiParent = this;
                frmChild.Show();
            }
            dlg.Dispose();
        }

        #endregion


        #region Настройки
        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSizeForm cs = new CanvasSizeForm();
            cs.CanvasWidth = ((DocumentForm)ActiveMdiChild).CanvasWidth;
            cs.CanvasHeight = ((DocumentForm)ActiveMdiChild).CanvasHeight;
            if (cs.ShowDialog() == DialogResult.OK)
            {
                ((DocumentForm)ActiveMdiChild).CanvasWidth = cs.CanvasWidth;
                ((DocumentForm)ActiveMdiChild).CanvasHeight = cs.CanvasHeight;
            }
        }

        private void WidthBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(WidthBox.Text, out CurrentLineWidth) && CurrentLineWidth > 0)
                WidthBox.ForeColor = SystemColors.ControlText;
            else
            {
                CurrentLineWidth = 3;
                WidthBox.ForeColor = Color.Red;
            }
        }

        private void StarCountBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(StarCountBox.Text, out CurrentStarCount) && CurrentStarCount > 4)
                StarCountBox.ForeColor = SystemColors.ControlText;
            else
            {
                CurrentStarCount = 5;
                StarCountBox.ForeColor = Color.Red;
            }
        }
        #endregion



        #region Всякие кнопки

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new AboutBoxForm();
            f.ShowDialog();
        }

        

        public void SetMouseLocation(string text)
        {
            MouseLocation.Text = text;
        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {
            ((DocumentForm)ActiveMdiChild).ZoomImage();
        }

        private void ReduceButton_Click(object sender, EventArgs e)
        {
            ((DocumentForm)ActiveMdiChild).ReduceImage();
        }

        #endregion

        private void черныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Black;
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Red;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Blue;
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentColor = Color.Green;
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new ColorDialog();
            if (d.ShowDialog() == DialogResult.OK)
                CurrentColor = d.Color;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
