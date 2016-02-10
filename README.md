# TurtleGraphics.NET
C# Turtule Graphics library - for teaching kids to code

This library provides а **very simple "[turtle graphics](https://en.wikipedia.org/wiki/Turtle_graphics)" drawing interface for C#**, designed for kids, learning to code.

## TurtleGraphics.NET - Developer's Reference
The interface is intentionally simplified through a single static class (I know this is not a good practice in OOP), to enable kids start playing with the turtle with just few clicks, without knowing anything about "classes" and "objects". The class Turtle supports all major turtule graphics primitives in Windows Forms C# / .NET GUI applications:
 - `Init()` - initializes the turtle graphics system. Callers can specify the target Windows Forms control to hold the drawing surface (e.g. a Panel). If not specified, the currently active form is used as drawing surface. If not called, it will be called on demand with the first turtle move / draw command. Initially the turtle location is {0, 0} (the screen center) and the direction (angle) is 0 degrees (up).
 - `Dispose()` / `Reset()` - destroys the turtle graphics system and deallocates all resources, associated with it.
 - `Forward(distance)` - moves the turtle forward in the current direction by the specified distance. If the pen is down, the turtle draws a line, otherwise it just moves without leaving a track.
 - `Backward(distance)` - moves the turtle if the backward direction and draws a line if the pen is down.
 - `Rotate(angle)` - rotates the tutle relatively to the current direction. The angle is given in degrees (e.g. 45, -30, or 315).
 - `MoveTo(x, y)` - moves the turtle to the specified position and draws a line if the pen is down.
 - 

