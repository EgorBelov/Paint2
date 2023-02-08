using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MyNewPaint
{
    public partial class DocumentForm : Form
    {
        Point from;
        Point start;
        public Bitmap bitmap;
        private Bitmap startBitmap;
        private double ratio;
        public string FilePath = String.Empty;
        public bool saved = false;

        public DocumentForm()
        {
            InitializeComponent();
            bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            pictureBox1.Image = bitmap;
            ratio = (double)CanvasWidth / (double)CanvasHeight;
            MouseWheel += Zoom;
            saved = false;
        }

        public DocumentForm(string FileName)
        {
            InitializeComponent();
            Image image = Image.FromFile(FileName);
            bitmap = new Bitmap(image);
            image.Dispose();
            Graphics g = Graphics.FromImage(bitmap);
            pictureBox1.Width = bitmap.Width;
            pictureBox1.Height = bitmap.Height;
            ratio = (double)CanvasWidth / (double)CanvasHeight;
            pictureBox1.Image = bitmap;
            MouseWheel += Zoom;
            FilePath = FileName;
            Text = FileName;
            saved = true;
        }


        #region Длина и ширина канваса
        public int CanvasWidth
        {
            get
            {
                return bitmap.Width;
            }

            set
            {
                Size = new Size(value, CanvasHeight);
                pictureBox1.Width = value;
                Bitmap tempBitmap = new Bitmap(value, pictureBox1.Height);
                Graphics g = Graphics.FromImage(tempBitmap);
                g.Clear(Color.White);
                g.DrawImage(bitmap, new Point(0, 0));
                bitmap = tempBitmap;
                pictureBox1.Image = bitmap;
            }
        }

        public int CanvasHeight
        {
            get
            {
                return bitmap.Height;
            }
            set
            {
                Size = new Size(CanvasWidth, value);
                pictureBox1.Height = value;
                Bitmap tempBitmap = new Bitmap(pictureBox1.Width, value);
                Graphics g = Graphics.FromImage(tempBitmap);
                g.Clear(Color.White);
                g.DrawImage(bitmap, new Point(0, 0));
                bitmap = tempBitmap;
                pictureBox1.Image = bitmap;
            }
        }
        #endregion



        #region Рисование и инструменты
        private void DocumentForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Graphics graphics = Graphics.FromImage(bitmap);
                
                switch (MainForm.CurrentTool)
                {
                    case Tools.Pen:                        
                        DrawPen(e, graphics);
                        break;
                    case Tools.Eraser:
                        DrawEraser(e, graphics);
                        break;
                    case Tools.Star:
                        DrawStar(e.Location);
                        break;
                    case Tools.Circle:
                        DrawEllipse(e.Location);
                        break;             
                    case Tools.Line:
                        DrawLine(e.Location);
                        break;
                }
                from = e.Location;
                pictureBox1.Invalidate();
            }
            ((MainForm)MdiParent).SetMouseLocation($"X: {e.X} Y: {e.Y}");
        }

        private void DocumentForm_MouseDown(object sender, MouseEventArgs e)
        {
            from = e.Location;
            start = e.Location;
            startBitmap = new Bitmap(bitmap);
            saved = false;

            if (e.Button == MouseButtons.Left)
            {
                Graphics graphics = Graphics.FromImage(bitmap);

                switch (MainForm.CurrentTool)
                {
                    case Tools.Pen:
                        DrawPen(e, graphics);
                        break;
                    case Tools.Eraser:
                        DrawEraser(e, graphics);
                        break;
                }
                from = e.Location;
                pictureBox1.Invalidate();
            }
            ((MainForm)MdiParent).SetMouseLocation($"X: {e.X} Y: {e.Y}");
        }

        private void DrawPen(MouseEventArgs e, Graphics graphics)
        {
            var size = new Size(Math.Abs(MainForm.CurrentLineWidth), Math.Abs(MainForm.CurrentLineWidth));
            var location = new Point(e.X - MainForm.CurrentLineWidth / 2 , e.Y - MainForm.CurrentLineWidth/ 2);
            graphics.DrawLine(new Pen(MainForm.CurrentColor, MainForm.CurrentLineWidth), from, e.Location);       
            graphics.FillEllipse(new SolidBrush(MainForm.CurrentColor), new Rectangle(location, size));
        }
        private void DrawEraser(MouseEventArgs e, Graphics graphics)
        {
            var size = new Size(Math.Abs(MainForm.CurrentLineWidth), Math.Abs(MainForm.CurrentLineWidth));
            var location = new Point(e.X - MainForm.CurrentLineWidth / 2, e.Y - MainForm.CurrentLineWidth / 2);
            graphics.DrawLine(new Pen(Color.White, MainForm.CurrentLineWidth), from, e.Location);
            graphics.FillEllipse(new SolidBrush(Color.White), new Rectangle(location, size));
        }

        private void DrawLine(Point endLocation)
        {
            bitmap = new Bitmap(startBitmap);
            saved = false;
            Graphics graphics = Graphics.FromImage(bitmap);
            var pen = new Pen(MainForm.CurrentColor, MainForm.CurrentLineWidth);
            graphics.DrawLine(pen, start, endLocation);
            pictureBox1.Image = bitmap;
        }

        private void DrawEllipse(Point endLocation)
        {
            bitmap = new Bitmap(startBitmap);
            saved = false;
            Graphics graphics = Graphics.FromImage(bitmap);
            var size = new Size(Math.Abs(endLocation.X - start.X), Math.Abs(endLocation.Y - start.Y));
            var location = new Point(start.X < endLocation.X ? start.X : endLocation.X, start.Y < endLocation.Y ? start.Y : endLocation.Y);
            graphics.DrawEllipse(new Pen(MainForm.CurrentColor, MainForm.CurrentLineWidth), new Rectangle(location, size));
            pictureBox1.Image = bitmap;
        }

        private void DrawStar(Point endLocation)
        {
            bitmap = new Bitmap(startBitmap);
            saved = false;
            Graphics graphics = Graphics.FromImage(bitmap);
            int countPoints = MainForm.CurrentStarCount;
            var size = new Size(Math.Abs(endLocation.X - start.X), Math.Abs(endLocation.Y - start.Y));
            Point location = new Point(start.X < endLocation.X ? start.X : endLocation.X, start.Y < endLocation.Y ? start.Y : endLocation.Y);
            PointF[] points = new PointF[2 * countPoints + 1];
            double rotation = 50, additionRotate = Math.PI / countPoints, width, height;

            for (int k = 0; k < 2 * countPoints + 1; k++)
            {
                width = k % 2 == 0 ? size.Width : size.Width / 2;
                height = k % 2 == 0 ? size.Height : size.Height / 2;
                points[k] = new PointF((float)(location.X + width * Math.Cos(rotation)), (float)(location.Y + height * Math.Sin(rotation)));
                rotation += additionRotate;
            }
            graphics.DrawPolygon(new Pen(MainForm.CurrentColor, MainForm.CurrentLineWidth), points);
            pictureBox1.Image = bitmap;
        }
        #endregion



        #region Работа с файлами

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                DialogResult dialogResult = MessageBox.Show($"Вы хотите сохранить изменения в файле \"{this.Text}\"?", "Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dialogResult)
                {
                    case DialogResult.Cancel: { e.Cancel = true; } break;
                    case DialogResult.Yes: { if (String.IsNullOrEmpty(FilePath)) SaveAs(); else Save(); this.Dispose(); } break;
                    case DialogResult.No: { this.Dispose(); } break;
                }
            }
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(FilePath)) SaveAs();
            else
            {
                try
                {
                    Bitmap bmp1 = new Bitmap(bitmap);
                    if (System.IO.File.Exists(FilePath))
                        System.IO.File.Delete(FilePath);
                    bmp1.Save(FilePath);
                    bmp1.Dispose();
                    saved = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка при работе с файлом", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpg)|*.jpg";
            dlg.FileName = Text;
            ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(dlg.FileName, ff[dlg.FilterIndex - 1]);
                FilePath = dlg.FileName;
                Text = dlg.FileName;
                saved = true;
            }
            dlg.Dispose();

        }
        #endregion



        #region Увеличение, уменьшение изображения
        public void Zoom(object sender, MouseEventArgs e)
        {
            ResizeImage((double)e.Delta / 100.00);
        }

        public void ZoomImage()
        {
            ResizeImage(0.25);
        }

        public void ReduceImage()
        {
            ResizeImage(-0.25);
        }

        private void ResizeImage(double percentage)
        {
            int width = Convert.ToInt32(CanvasWidth + (CanvasWidth * percentage));
            int height = Convert.ToInt32(CanvasHeight + (CanvasHeight * percentage));

            if (width < 60 || height < 60)
            {
                width = Convert.ToInt32(60.00 * ratio);
                height = 60;
            }
            if (width > 8000 || height > 8000)
            {
                width = Convert.ToInt32(8000.00 * ratio);
                height = 8000;
            }

            var size = new Size(width, height);
            pictureBox1.Size = size;
            Bitmap tempBitmap = GetResizedBitmap(bitmap, size.Width, size.Height);
            bitmap = tempBitmap;
            pictureBox1.Image = bitmap;


        }

        private static Bitmap GetResizedBitmap(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            return destImage;
        }
        #endregion



        #region Отмена действия, восстановление
        public void Undo()
        {
            if(startBitmap != null)
            {
                var tmpBitmap = new Bitmap(bitmap);
                bitmap = startBitmap;
                startBitmap = tmpBitmap;
                pictureBox1.Image = bitmap;
            }
        }

        public void Redo()
        {
            if (startBitmap != null)
            {
                var tmpBitmap = new Bitmap(bitmap);
                bitmap = startBitmap;
                startBitmap = tmpBitmap;
                pictureBox1.Image = bitmap;
            }
        }
        #endregion

        private void DocumentForm_MouseLeave(object sender, EventArgs e)
        {
            ((MainForm)MdiParent).SetMouseLocation(string.Empty);
        }

    }
}
