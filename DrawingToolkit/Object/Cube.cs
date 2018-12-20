﻿using DrawingToolkit.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit.Object
{
    class Cube : AObject
    {
        //public Point from { get; set; }
        //public Point to { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Point aPoint { get; set; }
        public Point bPoint { get; set; }
        public Point cPoint { get; set; }
        private Pen p;

        public Cube()
        {
            this.p = new Pen(Color.Black);
            p.Width = 2;
        }

        public Cube(Point awal) : this()
        {
            this.from = awal;
            this.Width = 0;
            this.Height = 0;
        }

        public override void DrawObject()
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Math.Min(to.X, from.X),
                                                Math.Min(to.Y, from.Y),
                                                Math.Abs(to.X - from.X),
                                                Math.Abs(to.Y - from.Y));
            int helper1, helper2, helper3, helper4;
            helper1 = Math.Abs((to.X - from.X) / 2);
            helper2 = Math.Abs((to.Y - from.Y) / 2);
            if (to.X < from.X)
            {
                helper3 = to.X;
            }
            else helper3 = from.X;
            if (to.Y < from.Y)
            {
                helper4 = to.Y;
            }
            else helper4 = from.Y;
            Point a = new Point();
            Point b = new Point();
            Point c = new Point();
            Point d = new Point();
            Point e = new Point();
            Point f = new Point();
            a.X = helper3 + helper1;
            a.Y = helper4 - helper2;
            b.X = helper3 + (3 * (helper1));
            b.Y = a.Y;
            c.X = b.X;
            c.Y = helper4 + helper2;
            d.X = helper3;
            d.Y = helper4;
            e.X = helper3 + (2 * helper1);
            e.Y = d.Y;
            f.X = e.X;
            f.Y = helper4 + (2 * helper2);
            this.aPoint = a;
            this.bPoint = b;
            this.cPoint = c;
            Point[] myPoint1 = { d, a, b, e };
            Point[] myPoint2 = { e, b, c, f };
            this.getGraphics().SmoothingMode = SmoothingMode.AntiAlias;
            this.getGraphics().DrawRectangle(p, rect);
            this.getGraphics().DrawPolygon(p, myPoint1);
            this.getGraphics().DrawPolygon(p, myPoint2);
        }

        public override void DrawPreview()
        {
            this.p.Color = Color.Red;
            DrawObject();
        }

        public override void DrawEdit()
        {
            this.p.Color = Color.Blue;
            DrawObject();
        }

        public override void DrawStatic()
        {
            this.p.Color = Color.Black;
            DrawObject();
        }

        public override Boolean Select(Point posisi)
        {
            if ((posisi.X >= from.X && posisi.X <= from.X + Width) && (posisi.Y >= from.Y && posisi.Y <= from.Y + Height))
            {
                //System.Diagnostics.Debug.WriteLine("Kotak Terpilih");
                return true;
            }
            return false;
        }

        public override void Translate(int difX, int difY)
        {
            this.from = new Point(this.from.X + difX, this.from.Y + difY);
            this.to = new Point(this.to.X + difX, this.to.Y + difY);

            this.centerPoint = new Point(this.centerPoint.X + difX, this.centerPoint.Y + difY);
            notify();
        }

        public override Point GetHandlePoint(int value)
        {
            //System.Diagnostics.Debug.WriteLine(from);
            Point result = Point.Empty;
            if (value == 1)//pojok kiri
                result = new Point(from.X, from.Y);
            else if (value == 2)//tengah kiri
                result = new Point(from.X, from.Y + (Height / 2));
            else if (value == 3)//bawah kiri
                result = new Point(from.X, to.Y);
            else if (value == 4)
                result = new Point(from.X + (Width / 2), from.Y);
            else if (value == 5)
                result = new Point(from.X + (Width / 2), to.Y);
            else if (value == 6)
                result = new Point(to.X, from.Y);
            else if (value == 7)
                result = new Point(to.X, from.Y + (Height / 2));
            else if (value == 8)
                result = new Point(to.X, to.Y);
            return result;
        }

        public override void DrawHandle()
        {
            for (int i = 1; i < 9; i++)
            {
                Point point = GetHandlePoint(i);
                point.Offset(-2, -2);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(point.X, point.Y, 5, 5);
                this.getGraphics().SmoothingMode = SmoothingMode.AntiAlias;
                this.getGraphics().DrawRectangle(p, rect);
            }
        }

        public override int GetClickHandle(Point posisi)
        {
            for (int i = 1; i < 9; i++)
            {
                Point point = GetHandlePoint(i);
                point.Offset(-2, -2);
                if ((posisi.X >= point.X && posisi.X <= point.X + 5) && (posisi.Y >= point.Y && posisi.Y <= point.Y + 5))
                {
                    // System.Diagnostics.Debug.WriteLine("Berubah"+i);
                    return i;
                }
            }
            return -1;
        }

        public override void Resize(int posisiClick, Point posisi)
        {
            //System.Diagnostics.Debug.WriteLine(from);
            if (posisiClick == 1)//pojok kiri
            {
                this.from = posisi;
            }
            else if (posisiClick == 2)//tengah kiri
            {
                this.from = new Point(posisi.X, from.Y);
            }
            else if (posisiClick == 3)//bawah kiri
            {
                this.from = new Point(posisi.X, from.Y);
                this.to = new Point(to.X, posisi.Y);
            }
            else if (posisiClick == 4)
            {
                this.from = new Point(from.X, posisi.Y);
            }
            else if (posisiClick == 5)
            {
                this.to = new Point(to.X, posisi.Y);
            }
            else if (posisiClick == 6)
            {
                this.from = new Point(from.X, posisi.Y);
                this.to = new Point(posisi.X, to.Y);
            }
            else if (posisiClick == 7)
            {
                this.to = new Point(posisi.X, to.Y);
            }
            else if (posisiClick == 8)
            {
                this.to = posisi;
            }
            this.Width = Math.Abs(from.X - to.X);
            this.Height = Math.Abs(from.Y - to.Y);
        }
        
    }
}