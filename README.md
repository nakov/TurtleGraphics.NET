# Nakov.TurtleGraphics
C# Turtle Graphics library - for teaching kids to code.

![Nakov Turtle Graphics for C# / .NET - free open-source library](https://github.com/nakov/TurtleGraphics.NET/blob/master/Nakov.TurtleGraphics-Demo.gif "Nakov Turtle Graphics for C# / .NET - free open-source library")

This library provides а **very simple "[turtle graphics](https://en.wikipedia.org/wiki/Turtle_graphics)" drawing interface for C#**, designed for kids, learning to code, or teachers who want to illustrate programming visually.

## Getting Started - Creating a Windows Forms Turtle Graphics App
1. Create a **new empty Windows Form application** with Visual Studio.
2. Install the [**NuGet package "Nakov.TurtleGraphics"**](https://www.nuget.org/packages/Nakov.TurtleGraphics/) from the Package Management Console in Visual Studio:
```
Install-Package Nakov.TurtleGraphics
```
3. Put a **button [Draw]**. Handle the **`Click`** event and add this code:
```
// Assign a delay to visualize the drawing process
Turtle.Delay = 200;

// Draw a equilateral triangle
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
Turtle.Backward(100);
Turtle.PenUp();
Turtle.Forward(150);
Turtle.PenDown();
Turtle.Rotate(30);
```
4. Enjoy the application:

![Nakov Turtle Graphics for C# / .NET - free open-source library](https://github.com/nakov/TurtleGraphics.NET/blob/master/Nakov.TurtleGraphics-Demo.gif "Nakov Turtle Graphics for C# / .NET - free open-source library")

See the full source code here: https://github.com/nakov/TurtleGraphics.NET/tree/master/Turtle-Graphics-Example.

## TurtleGraphics.NET - Developer's Reference
The interface is intentionally simplified through a single static class (I know this is not a good practice in OOP), to enable kids start playing with the turtle with just few clicks, without knowing anything about "classes" and "objects". The class **`Turtle`** supports all major turtle graphics primitives in Windows Forms C# / .NET GUI applications:
 - `Init()` - initializes the turtle graphics system. Callers can specify the target Windows Forms control to hold the drawing surface (e.g. a Panel). If not specified, the currently active form is used as drawing surface. If not called, it will be called on demand with the first turtle move / draw command. Initially the turtle location is {0, 0} (the screen center) and the direction (angle) is 0 degrees (up).
 - `Dispose()` / `Reset()` - destroys the turtle graphics system and deallocates all resources, associated with it.
 - `Forward(distance)` - moves the turtle forward in the current direction by the specified distance. If the pen is down, the turtle draws a line from the current to the new position, otherwise it just moves without leaving a track.
 - `Backward(distance)` - moves the turtle in backward direction and draws a line if the pen is down.
 - `MoveTo(x, y)` - moves the turtle to the specified position and draws a line if the pen is down.
 - `Rotate(angle)` - rotates the turtle relatively to the current direction. The rotation angle is given in degrees (e.g. 45, -30, 315, ...).
 - `RotateTo(angle)` - rotates the turtle to the specified angle in degrees (e.g. 0, 45, 180, 315, ...).
 - `PenUp()` - moves the pen up (makes further moves invisible). Further calls to `Forward(distance)` / `Backward(distance)` / `MoveTo(x, y)` will move the turtle without drawing a line.
 - `PenDown()` - moves the pen down (makes further moves visible). Further calls to `Forward(distance)` / `Backward(distance)` / `MoveTo(x, y)` will draw a line from the current to the new position.
 - `X` - gets / sets the current turtle horizontal position. The initial turtle position is the screen center {0, 0}. Increasing X will move the turtle right.
 - `Y` - gets / sets the current turtle vertical position. The initial turtle position is the screen center {0, 0}. Increasing Y will move the turtle up.
 - `Angle` - gets / sets the current turtle direction (angle in degrees). The value of 0 means up, 90 means right, 180 means down and 270 means left. The `Angle` is always kept in the range [0...360). Initially the angle is 0.
 - `PenColor` - gets / sets the color of the pen. The default pen color is "blue".
 - `PenSize` - gets / sets the size of the pen (in pixels). The default pen size is 7.
 - `PenVisible` - gets / sets the visibility of the pen. The default pen size is true. True means the pen is down (draws a line when the turtle moves). False means the pen is up (no line is drawn when the turtle moves).
 - `ShowTurtle` - gets / sets whether the turtle is visible. By default it is visible.
 - `Delay` - gets / sets the turtle delay in milliseconds after moving / rotating. By default the delay is 0 (no delay). Setting the delay to 100-300 will simulate a pleasant "animation effect". `Delay` is preserved after `Reset()`.
