using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Nakov.TurtleGraphics
{
    public static class Turtle
    {
        private static float x;
        public static float X
        {
            get
            {
                InitOnDemand();
                return x;
            }
            set
            {
                InitOnDemand();
                x = value;
            }
        }

        private static float y;
        public static float Y
        {
            get
            {
                InitOnDemand();
                return y;
            }
            set
            {
                InitOnDemand();
                y = value;
            }
        }

        private static float angle;
        public static float Angle
        {
            get
            {
                InitOnDemand();
                return angle;
            }
            set
            {
                InitOnDemand();
                angle = value % 360;
                if (angle < 0)
                {
                    angle += 360;
                }
            }
        }

        public static Color PenColor
        {
            get
            {
                InitOnDemand();
                return drawPen.Color;
            }
            set
            {
                InitOnDemand();
                drawPen.Color = value;
            }
        }

        public static float PenSize
        {
            get
            {
                InitOnDemand();
                return drawPen.Width;
            }
            set
            {
                InitOnDemand();
                drawPen.Width = value;
            }
        }

        private static bool penVisible;
        public static bool PenVisible
        {
            get
            {
                InitOnDemand();
                return penVisible;
            }
            set
            {
                InitOnDemand();
                penVisible = value;
            }
        }

        public static bool ShowTurtle
        {
            get
            {
                InitOnDemand();
                return turtleHeadImage.Visible;
            }
            set
            {
                InitOnDemand();
                turtleHeadImage.Visible = value;
            }
        }

        private static int delay;
        public static int Delay
        {
            get
            {
                InitOnDemand();
                return delay;
            }
            set
            {
                InitOnDemand();
                delay = value;
            }
        }

        public const int DrawAreaSize = 10000;
        public static readonly Color DefaultColor = Color.Blue;
        public const int DefaultPenSize = 7;        

        private static Control drawControl;
        private static Image drawImage;
        private static Graphics drawGraphics;
        private static Pen drawPen;
        private static PictureBox turtleHeadImage;

        public static void Init(Control targetControl = null)
        {
            // Dispose all resources if already allocated
            Dispose();

            // Initialize the drawing control (sufrace)
            drawControl = targetControl;
            if (drawControl == null)
            {
                // If no target control is provided, use the currently active form
                drawControl = Form.ActiveForm;
            }
            SetDoubleBuffered(drawControl);

            // Create an empty graphics area to be used by the turtle
            drawImage = new Bitmap(DrawAreaSize, DrawAreaSize); 
            drawControl.Paint += DrawControl_Paint;
            drawControl.ClientSizeChanged += DrawControl_ClientSizeChanged;
            drawGraphics = Graphics.FromImage(drawImage);
            drawGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Initialize the pen size and color
            drawPen = new Pen(DefaultColor, DefaultPenSize);
            drawPen.StartCap = LineCap.Round;
            drawPen.EndCap = LineCap.Round;

            // Initialize the turtle position and other settings
            X = 0;
            Y = 0;
            Angle = 0;
            PenVisible = true;
            // Delay = 0;  // Intentionally preserve the "Delay" settings

            // Initialize the turtle head image
            turtleHeadImage = new PictureBox();
            turtleHeadImage.BackColor = Color.Transparent;
            drawControl.Controls.Add(turtleHeadImage);
        }

        public static void Dispose()
        {
            if (drawControl != null)
            {
                // Remove the associated events for the drawing control
                drawControl.Paint -= DrawControl_Paint;
                drawControl.ClientSizeChanged -= DrawControl_ClientSizeChanged;

                // Release the pen object
                drawPen.Dispose();
                drawPen = null;

                // Release the graphic object
                drawGraphics.Dispose();
                drawGraphics = null;

                // Release the draw surface (image) object
                drawImage.Dispose();
                drawImage = null;

                // Release the turtle (head) image
                drawControl.Controls.Remove(turtleHeadImage);
                turtleHeadImage.Dispose();
                turtleHeadImage = null;

                // Release the drawing control
                drawControl.Invalidate();
                drawControl = null;
            }
        }

        public static void Reset()
        {
            Dispose();
        }

        public static void Forward(float distance = 10)
        {
            var angleRadians = Angle * Math.PI / 180;
            var newX = X + (float)(distance * Math.Sin(angleRadians));
            var newY = Y + (float)(distance * Math.Cos(angleRadians));
            MoveTo(newX, newY);
        }

        public static void Backward(float distance = 10)
        {
            Forward(-distance);
        }

        public static void MoveTo(float newX, float newY)
        {
            InitOnDemand();
            var fromX = DrawAreaSize / 2 + X;
            var fromY = DrawAreaSize / 2 - Y;
            X = newX;
            Y = newY;
            if (PenVisible)
            {
                var toX = DrawAreaSize / 2 + X;
                var toY = DrawAreaSize / 2 - Y;
                drawGraphics.DrawLine(drawPen, fromX, fromY, toX, toY);
            }
            DrawTurtle();
            PaintAndDelay();
        }

        public static void Rotate(float angleDelta)
        {
            InitOnDemand();
            Angle += angleDelta;
            DrawTurtle();
            PaintAndDelay();
        }
        
        public static void RotateTo(float newAngle)
        {
            InitOnDemand();
            Angle = newAngle;
            DrawTurtle();
            PaintAndDelay();
        }

        public static void PenUp()
        {
            PenVisible = false;
        }

        public static void PenDown()
        {
            PenVisible = true;
        }

        private static void SetDoubleBuffered(Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private static void InitOnDemand()
        {
            // Create the drawing surface if it does not already exist
            if (drawControl == null)
            {
                Init();
            }
        }

        private static void DrawTurtle()
        {
            if (ShowTurtle)
            {
                var turtleImg = Nakov.TurtleGraphics.Properties.Resources.Turtle;
                turtleImg = RotateImage(turtleImg, angle);
                var turtleImgSize = Math.Max(turtleImg.Width, turtleImg.Height);
                turtleHeadImage.BackgroundImage = turtleImg;
                turtleHeadImage.Width = turtleImg.Width;
                turtleHeadImage.Height = turtleImg.Height;

                var turtleX = 1 + drawControl.ClientSize.Width / 2 + X - turtleHeadImage.Width / 2;
                var turtleY = 1 + drawControl.ClientSize.Height / 2 - Y - turtleHeadImage.Height / 2;

                turtleHeadImage.Left = (int)Math.Round(turtleX);
                turtleHeadImage.Top = (int)Math.Round(turtleY);
            }
        }

        private static Bitmap RotateImage(Bitmap bmp, float angleDegrees)
        {
            Bitmap rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                // Set the rotation point as the center into the matrix
                g.TranslateTransform(bmp.Width / 2, bmp.Height / 2);

                // Rotate
                g.RotateTransform(angleDegrees);

                // Restore the rotation point into the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);

                // Draw the image on the new bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }
            bmp.Dispose();

            return rotatedImage;
        }

        private static void PaintAndDelay()
        {
            drawControl.Invalidate();
            if (Delay == 0)
            {
                // No delay -> invalidate the control, so it will be repainted later
            }
            else
            {
                // Immediately paint the control and them delay
                drawControl.Update();
                Thread.Sleep(Delay);
                Application.DoEvents();
            }
        }

        private static void DrawControl_ClientSizeChanged(object sender, EventArgs e)
        {
            drawControl.Invalidate();
            DrawTurtle();
        }

        private static void DrawControl_Paint(object sender, PaintEventArgs e)
        {
            if (drawControl != null)
            {
                var top = (drawControl.ClientSize.Width - DrawAreaSize) / 2;
                var left = (drawControl.ClientSize.Height - DrawAreaSize) / 2;
                // TODO: needs a fix -> does not work correctly when drawControl has AutoScroll
                e.Graphics.DrawImage(drawImage, top, left);
            }
        }
    }
}
