using Nakov.TurtleGraphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Turtle_Graphics_Example
{
    public partial class TurtleGraphicsDemoForm : Form
    {
        private List<Turtle> myTurtles;

        public TurtleGraphicsDemoForm()
        {
            myTurtles = new List<Turtle>();
            myTurtles.Add(new Turtle());
            InitializeComponent();
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            foreach (Turtle turtle in myTurtles)
            {
                DrawStuff(turtle);
            }
        }

        private void DrawStuff(Turtle turtle)
        {
            // Assign a delay to visualize the drawing process
            turtle.Delay = 150;

            // Draw a equilateral triangle
            turtle.PenColor = Color.Green;
            turtle.Rotate(30);
            turtle.Forward(200);
            turtle.Rotate(120);
            turtle.Forward(200);
            turtle.Rotate(120);
            turtle.Forward(200);

            // Draw a line in the triangle
            turtle.Rotate(-30);
            turtle.PenUp();
            turtle.Backward(50);
            turtle.PenDown();
            turtle.PenColor = Color.Red;
            turtle.PenSize = 5;
            turtle.Backward(100);
            turtle.PenColor = Turtle.DefaultColor;
            turtle.PenSize = Turtle.DefaultPenSize;
            turtle.PenUp();
            turtle.Forward(150);
            turtle.PenDown();
            turtle.Rotate(30);
        }

        private void buttonDrawSpiral_Click(object sender, EventArgs e)
        {
            foreach (Turtle turtle in myTurtles)
            {
                DrawSpiral(turtle);
            }
        }

        private void DrawSpiral(Turtle turtle)
        {
            turtle.PenColor = Color.Red;
            turtle.Delay = 50;
            for (int i = 0; i < 25; i++)
            {
                turtle.Forward(i * 5);
                turtle.Rotate(30 + i);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            foreach (Turtle turtle in myTurtles)
            {
                turtle.Reset();
            }
        }

        private void buttonShowHideTurtle_Click(object sender, EventArgs e)
        {
            foreach (Turtle turtle in myTurtles)
            {
                if (turtle.ShowTurtle)
                {
                    turtle.ShowTurtle = false;
                    this.buttonShowHideTurtle.Text = "Show Turtle";
                }
                else
                {
                    turtle.ShowTurtle = true;
                    this.buttonShowHideTurtle.Text = "Hide Turtle";
                }
            }
        }

        private void btnAddTurtle_Click(object sender, EventArgs e)
        {
            Turtle newTurtle = new Turtle();
            newTurtle.X = 50;
            newTurtle.Y = 50;
            myTurtles.Add(newTurtle);
        }
    }
}
