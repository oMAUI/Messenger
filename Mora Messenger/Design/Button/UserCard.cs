using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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

            g.DrawRectangle(new Pen(perimiterColor), rec);
            g.FillRectangle(new SolidBrush(BackColor), rec);

            g.DrawEllipse(new Pen(Color.FromArgb(172, 176, 189)), PhotoRec);
            g.FillEllipse(new SolidBrush(Color.FromArgb(172, 176, 189)), PhotoRec);

            if (MouseEntered)
            {
                g.DrawRectangle(new Pen(Color.FromArgb(60, Color.White)), rec);
                g.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.White)), rec);
            }

            if (MousePressed)
            {
                g.DrawRectangle(new Pen(Color.FromArgb(30, Color.White)), rec);
                g.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rec);
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
