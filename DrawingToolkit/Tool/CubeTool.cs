using DrawingToolkit.Interface;
using DrawingToolkit.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit.Tool
{
    class CubeTool : ATool
    {
        public bool isActive { set; get; }
        private Cube cubeObject;
        public override bool MouseClick(object sender, MouseEventArgs e, LinkedList<AObject> listObject)
        {
            System.Diagnostics.Debug.WriteLine("Click");
            return true;
        }

        public override void MouseDown(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            cubeObject = new Cube(e.Location);
            cubeObject.to = e.Location;
            cubeObject.setGraphics(panel1.CreateGraphics());
            cubeObject.Draw();
            panel1.Invalidate();
        }

        public override void MouseMove(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            cubeObject.to = e.Location;
            cubeObject.Width = Math.Abs(e.X - cubeObject.from.X);
            cubeObject.Height = Math.Abs(e.Y - cubeObject.from.Y);
            cubeObject.Draw();
        }

        public override AObject MouseUp(object sender, MouseEventArgs e, Panel panel1, LinkedList<AObject> listObject)
        {
            cubeObject.to = e.Location;
            cubeObject.Width = Math.Abs(e.X - cubeObject.from.X);
            cubeObject.Height = Math.Abs(e.Y - cubeObject.from.Y);
            //cubeObject.DrawEdit();
            //cubeObject.Select();
            cubeObject.centerPoint = new System.Drawing.Point(Math.Abs(cubeObject.from.X - cubeObject.to.X) / 2, Math.Abs(cubeObject.from.Y - cubeObject.to.Y) / 2);
            cubeObject.Deselect();
            cubeObject.Draw();
            return cubeObject;
        }

        public override void KeyUp(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void KeyDown(object sender, KeyEventArgs e, Panel panel1)
        {
            throw new NotImplementedException();
        }
    }
}