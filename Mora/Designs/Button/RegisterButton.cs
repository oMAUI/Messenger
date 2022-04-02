using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mora.Designs.Animations;
using Mora_Messenger.Design.UIControls;

namespace Mora.Designs.Button
{
    class RegisterButton : Control
    {
        StringFormat SF = new StringFormat();

        Animation anim = new Animation();

        Point clickLocation = new Point();

        bool MousePressed { get; set; }
        bool MouseEntered { get; set; }

        public RegisterButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(100, 50);

            BackColor = Color.FromArgb(41, 50, 65);
            ForeColor = Color.White;


            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            Font = new Font("Arial", 11.25F);

            MousePressed = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.Clear(Parent.BackColor);

            Rectangle rec = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle recCurtain = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle recRipple = new Rectangle(
                clickLocation.X - (int)anim.Value / 2 + 1,
                clickLocation.Y - (int)anim.Value / 2 + 1,
                (int)anim.Value,
                (int)anim.Value
                );

            GraphicsPath path = Rounding.RoundedRectangle(rec, 50);
            //GraphicsPath pathRecRipple = Rounding.RoundedRectangle(recRipple, 50);

            g.DrawPath(new Pen(BackColor), path);
            g.FillPath(new SolidBrush(BackColor), path);

            g.SetClip(path);

            if(anim.Value > 0 && anim.Value < anim.TargetValue)
            {
                g.DrawEllipse(new Pen(Color.FromArgb(30, Color.Black)), recRipple);
                g.FillEllipse(new SolidBrush(Color.FromArgb(30, Color.Black)), recRipple);
            }
            else if(anim.Value == anim.TargetValue)
            {
                anim.Value = 0;
            }

            g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(237, 246, 249)), rec, SF);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;
            clickLocation = e.Location;
            
            ButtonRippleAction();
        }

        private void ButtonRippleAction()
        {
            anim = new Animation("RegisterButton_" + Handle, Invalidate, 0, Width * 2);

            Animator.Request(anim, true);
            //Invalidate();
        }
    }
}
