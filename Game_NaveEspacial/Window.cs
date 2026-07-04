using System.Drawing;

namespace NaveEspacial
{
    internal class Window
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ConsoleColor Color { get; set; }
        public Point UpperLimit { get; set; }
        public Point LowerLimit { get; set; }

        public Window(int width, int height, ConsoleColor color, Point upperLimit, Point lowerLimit)
        {
            Width = width;
            Height = height;
            Color = color;
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;
            Init();
        }

        private void Init()
        {
            Console.SetWindowSize(Width, Height);
            Console.Title = "Space Shooter";
            Console.CursorVisible = false;
            Console.BackgroundColor = Color;
            Console.Clear();
        }

        public void DrawBorders()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for(int i = UpperLimit.X; i <= LowerLimit.X; i++)
            {
                Console.SetCursorPosition(i, UpperLimit.Y);
                Console.Write('=');
                Console.SetCursorPosition(i, LowerLimit.Y);
                Console.Write('=');
            }

            for (int i = UpperLimit.Y; i <= LowerLimit.Y; i++)
            {
                Console.SetCursorPosition(UpperLimit.X, i);
                Console.Write('║');
                Console.SetCursorPosition(LowerLimit.X, i);
                Console.Write('║');
            }

            Console.SetCursorPosition(UpperLimit.X, UpperLimit.Y);
            Console.Write('╔');
            Console.SetCursorPosition(UpperLimit.X, LowerLimit.Y);
            Console.Write('╚');
            Console.SetCursorPosition(LowerLimit.X, UpperLimit.Y);
            Console.Write('╗');
            Console.SetCursorPosition(LowerLimit.X, LowerLimit.Y);
            Console.Write('╝');
        }
    }
}