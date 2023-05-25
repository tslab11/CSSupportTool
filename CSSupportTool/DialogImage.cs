using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSSupportTool
{
    public partial class DialogImage : Form
    {
        private Bitmap bitmap = null;

        public DialogImage()
        {
            InitializeComponent();
        }

        public void SetImage(Image img)
        {
            this.bitmap?.Dispose();

            this.bitmap = (Bitmap)img.Clone();

            //this.pictureBox1.Size = this.bitmap.Size;
            Size borderSize = this.Size - this.ClientSize;
            this.Size = new Size(this.bitmap.Width + borderSize.Width, this.bitmap.Height + borderSize.Height);
            this.pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(this.bitmap, new PointF(0, 0));
        }
    }

    public static class BitmapExt
    {

        public static void Show(this Bitmap bmp, IWin32Window owner, string title = "no-title")
        {
            DialogImage dlg = new DialogImage();
            dlg.Text = title;
            dlg.SetImage(bmp);
            dlg.Show(owner);
        }

    }
}
