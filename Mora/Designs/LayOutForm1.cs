using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mora_Messenger.Design
{
    class LayOutForm1
    {
        public LayOutForm1()
        {

        }

        public void BackColor(int r, int g, int b)
        {

        }

        public void Panel(Graphics G, int x, int y, int Width, int Height, int r, int g, int b)
        {
            G.DrawRectangle(new Pen(Color.FromArgb(r, g, b)), x, y, Width, Height);
            G.FillRectangle(new SolidBrush(Color.FromArgb(r, g, b)), x, y, Width, Height);
        }

        public void EllipseMsg(Graphics G, int x, int y, int Width, int Height, int r, int g, int b)
        {
            Rectangle rec = new Rectangle(x, y, Width, Height);
            GraphicsPath path = RoundedRectangle(rec, 50);

            G.DrawPath(new Pen(Color.FromArgb(r, g, b)), path);
            G.FillPath(new SolidBrush(Color.FromArgb(r, g, b)), path);
        }

        public static GraphicsPath RoundedRectangle(Rectangle rec, float RoudSize)
        {
            GraphicsPath GP = new GraphicsPath();

            GP.AddArc(rec.X, rec.Y, RoudSize, RoudSize, 180, 90);
            GP.AddArc(rec.X + rec.Width - RoudSize, rec.Y, RoudSize, RoudSize, 270, 90);
            GP.AddArc(rec.X + rec.Width - RoudSize, rec.Y + rec.Height - RoudSize, RoudSize, RoudSize, 0, 90);
            GP.AddArc(rec.X, rec.Y + rec.Height - RoudSize, RoudSize, RoudSize, 90, 90);

            GP.CloseFigure();

            return GP;
        }
    }
}