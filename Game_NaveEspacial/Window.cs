using System.Drawing;

namespace NaveEspacial
{
    internal class Window
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ConsoleColor Color { get; set; }
        public Point LimiteSuperior { get; set; }
        public Point LimiteInferior { get; set; }

        public Window(int width, int height, ConsoleColor color, Point limiteSuperior, Point limiteInferior)
        {
            Width = width;
            Height = height;
            Color = color;
            LimiteSuperior = limiteSuperior;
            LimiteInferior = limiteInferior;
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
            for(int i = LimiteSuperior.X; i <= LimiteInferior.X; i++)
            {
                Console.SetCursorPosition(i, LimiteSuperior.Y);
                Console.Write('=');
                Console.SetCursorPosition(i, LimiteInferior.Y);
                Console.Write('=');
            }

            for (int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++)
            {
                Console.SetCursorPosition(LimiteSuperior.X, i);
                Console.Write('║');
                Console.SetCursorPosition(LimiteInferior.X, i);
                Console.Write('║');
            }

            Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
            Console.Write('╔');
            Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
            Console.Write('╚');
            Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
            Console.Write('╗');
            Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
            Console.Write('╝');
        }
    }
}