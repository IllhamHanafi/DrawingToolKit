using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.MouseMove += new MouseEventHandler(this.myMouseMoveHandler);
            this.MouseDown += new MouseEventHandler(this.myMouseDownHandler); 
            this.MouseUp += new MouseEventHandler(this.myMouseUpHandler);
            InitializeComponent();
            g = panel1.CreateGraphics();
            g.Clear(Color.BlanchedAlmond);
            
        }
        Graphics g;
        Point a, b;
        Point[] cubeTop = new Point[4];
        Point[] cubeRight = new Point[4];
        Line line = new Line();
        bool lineToggle = false;
        bool circleToggle = false;
        bool cubeToggle = false;
        bool shouldDraw = false;
        public Pen myPen = new Pen(Color.Black, 2);
        class DrawingCanvas : Control
        {
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        }
        private void Tool()
        {

        }
        abstract class DrawingObject
        {
            public abstract void draw(PaintEventArgs e);
        }
        class Line : DrawingObject
        {
            public override void draw(PaintEventArgs e)
            {
                // e.Graphics.DrawLine(myPen, a, b);
            }
        }
        private void myMouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (shouldDraw == true)
            {
                b = e.Location;
                if (lineToggle == true)
                {
                    ControlPaint.DrawReversibleLine(panel1.PointToScreen(a), panel1.PointToScreen(b), Color.Black);
                }
                else if (circleToggle == true)
                {
                    g.DrawEllipse(myPen, a.X, a.Y, Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
                }
                else if (cubeToggle == true)
                {
                    g.DrawRectangle(myPen, a.X, a.Y, Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
                }
                this.Refresh();

            }
        }
        private void myMouseUpHandler(object sender, MouseEventArgs e)
        {
            if (shouldDraw == true)
            {
                shouldDraw = false;
                if (lineToggle == true)
                {
                    g.DrawLine(myPen, a, b);
                }
                else if (circleToggle == true)
                {
                    g.DrawEllipse(myPen, a.X, a.Y, Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
                }
                else if (cubeToggle == true)
                {
                    g.DrawRectangle(myPen, a.X, a.Y, Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
                    Point c1 = new Point { X = (a.X + (Math.Abs(a.X - b.X) / 2)), Y = (a.Y - (Math.Abs(a.Y - b.Y) / 2)) };
                    Point c2 = new Point { X = (b.X + (Math.Abs(a.X - b.X) / 2)), Y = (a.Y - (Math.Abs(a.Y - b.Y) / 2)) };
                    Point c3 = new Point { X = (b.X + (Math.Abs(a.X - b.X) / 2)), Y = (b.Y - (Math.Abs(a.Y - b.Y) / 2)) };
                    Point c4 = new Point { X = b.X, Y = a.Y };
                    cubeTop[0] = a;
                    cubeTop[1] = c1;
                    cubeTop[2] = c2;
                    cubeTop[3] = c4;
                    cubeRight[0] = c4;
                    cubeRight[1] = c2;
                    cubeRight[2] = c3;
                    cubeRight[3] = b;
                    g.DrawPolygon(myPen, cubeTop);
                    g.DrawPolygon(myPen, cubeRight);
                }
            }
        }

        private void myMouseDownHandler(object sender, MouseEventArgs e)
        {
            a = e.Location;
            shouldDraw = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            circleToggle = true;
            lineToggle = false;
            cubeToggle = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            lineToggle = true;
            circleToggle = false;
            cubeToggle = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            lineToggle = false;
            circleToggle = false;
            cubeToggle = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
