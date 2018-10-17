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
        Line line = new Line();
        bool lineToggle = false;
        bool circleToggle = false;
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
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            lineToggle = true;
            circleToggle = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
