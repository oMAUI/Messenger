using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using Mora_Messenger.Design.UIControls;

namespace Mora_Messenger.Design.Button
{
    public class UserCard : Control
    {
        StringFormat SF = new StringFormat();

        Color perimiterColor { get; set; }

        bool MouseEntered = false;
        bool MousePressed = false;

        public UserCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(1920 / 4 - 15, 100);

            BackColor = Color.FromArgb(41, 50, 65);
            ForeColor = Color.White;
            perimiterColor = Color.FromArgb(41, 50, 65);

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            Font = new Font("TimesNewRoman", 20F);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.Clear(Parent.BackColor);

            Rectangle rec = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle PhotoRec = new Rectangle(5, 5, Height - 10, Height - 10);

            GraphicsPath path = Rounding.RoundedRectangle(rec, 30);

            g.DrawPath(new Pen(perimiterColor), path);
            g.FillPath(new SolidBrush(BackColor), path);

            g.DrawEllipse(new Pen(Color.FromArgb(172, 176, 189)), PhotoRec);
            g.FillEllipse(new SolidBrush(Color.FromArgb(172, 176, 189)), PhotoRec);

            if (MouseEntered)
            {
                g.DrawPath(new Pen(Color.FromArgb(60, Color.White)), path);
                g.FillPath(new SolidBrush(Color.FromArgb(60, Color.White)), path);
            }

            if (MousePressed)
            {
                g.DrawPath(new Pen(Color.FromArgb(30, Color.White)), path);
                g.FillPath(new SolidBrush(Color.FromArgb(30, Color.White)), path);
            }

            g.DrawString(Text, Font, new SolidBrush(ForeColor), rec, SF);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;
            Invalidate();
        }
    }
}
