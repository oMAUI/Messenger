using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mora_Messenger.Design.UIControls
{
    public class MsgBox : Control
    {
        StringFormat SF = new StringFormat();

        public string message { get; private set; }

        public MsgBox(string msg)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(400, 100);

            BackColor = Color.FromArgb(41, 50, 65);
            ForeColor = Color.White;
            //perimiterColor = Color.FromArgb(41, 50, 65);

            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Near;

            Font = new Font("TimesNewRoman", 20F);
            Text = msg;

            int remainder = Text.Length / 48;
            if (Text.Length > 48)
            {
                for (int i = 0; i < remainder; i++)
                {
                    Height += 50;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(Parent.BackColor);

            Rectangle rec = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath path = Rounding.RoundedRectangle(rec, 30);

            g.DrawPath(new Pen(BackColor), path);
            g.FillPath(new SolidBrush(BackColor), path);

            Rectangle strRec = new Rectangle(20, 20, Width - 20, Height - 20);
            g.DrawString(Text, Font, new SolidBrush(Color.White), strRec, SF);
        }
    }
}
