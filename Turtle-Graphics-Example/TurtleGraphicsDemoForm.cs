using Nakov.TurtleGraphics;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Turtle_Graphics_Example
{
    public partial class TurtleGraphicsDemoForm : Form
    {
        public TurtleGraphicsDemoForm()
        {
            InitializeComponent();
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            // Assign a delay to visualize the drawing process
            Turtle.Delay = 150;

            // Draw a equilateral triangle
            Turtle.PenColor = Color.Green;
            Turtle.Rotate(30);
            Turtle.Forward(200);
            Turtle.Rotate(120);
            Turtle.Forward(200);
            Turtle.Rotate(120);
            Turtle.Forward(200);

            // Draw a line in the triangle
            Turtle.Rotate(-30);
            Turtle.PenUp();
            Turtle.Backward(50);
            Turtle.PenDown();
            Turtle.PenColor = Color.Red;
            Turtle.PenSize = 5;
            Turtle.Backward(100);
            Turtle.PenColor = Turtle.DefaultColor;
            Turtle.PenSize = Turtle.DefaultPenSize;
            Turtle.PenUp();
            Turtle.Forward(150);
            Turtle.PenDown();
            Turtle.Rotate(30);
        }

        private void buttonDrawSpiral_Click(object sender, EventArgs e)
        {
            Turtle.PenColor = Color.Red;
            Turtle.Delay = 50;
            for (int i = 0; i < 25; i++)
            {
                Turtle.Forward(i * 5);
                Turtle.Rotate(30 + i);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Turtle.Reset();
        }

        private void buttonShowHideTurtle_Click(object sender, EventArgs e)
        {
            if (Turtle.ShowTurtle)
            {
                Turtle.ShowTurtle = false;
                this.buttonShowHideTurtle.Text = "Show Turtle";
            }
            else
            {
                Turtle.ShowTurtle = true;
                this.buttonShowHideTurtle.Text = "Hide Turtle";
            }
        }
    }
}
