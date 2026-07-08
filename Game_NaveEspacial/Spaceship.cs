using System.Drawing;

namespace NaveEspacial
{
    internal class Spaceship
    {
        public float Healt { get; set; }
        public Point Position { get; set; }
        public ConsoleColor Color { get; set; }
        public Window WindowN { get; set; }
        public List<Point> PositionsSpaceship { get; set; }
        public List<Bullet> Bullets { get; set; }

        public Spaceship( Point position, ConsoleColor color, Window window)
        {
            Healt = 100;
            Position = position;
            Color = color;
            WindowN = window;
            PositionsSpaceship = new List<Point>();
            Bullets = new List<Bullet>();
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            Console.SetCursorPosition(x + 3, y);
            Console.Write('A');
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("<{x}>");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("± W W ±");

            PositionsSpaceship.Clear();

            PositionsSpaceship.Add(new Point(x + 3, y));

            PositionsSpaceship.Add(new Point(x + 1, y + 1));
            PositionsSpaceship.Add(new Point(x + 2, y + 1));
            PositionsSpaceship.Add(new Point(x + 3, y + 1));
            PositionsSpaceship.Add(new Point(x + 4, y + 1));
            PositionsSpaceship.Add(new Point(x + 5, y + 1));

            PositionsSpaceship.Add(new Point(x, y + 2));
            PositionsSpaceship.Add(new Point(x + 2, y + 2));
            PositionsSpaceship.Add(new Point(x + 4, y + 2));
            PositionsSpaceship.Add(new Point(x + 6, y + 2));
        }

        public void Erase()
        {
            foreach (Point item in PositionsSpaceship)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }

        public void Keyboard(ref Point distance, int speed)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.W)
                distance = new Point(0, -1);
            if (key.Key == ConsoleKey.S)
                distance = new Point(0, 1);
            if (key.Key == ConsoleKey.A)
                distance = new Point(-1, 0);
            if (key.Key == ConsoleKey.D)
                distance = new Point(1, 0);

            distance.X *= speed;
            distance.Y *= speed;

            if (key.Key == ConsoleKey.RightArrow)
            {
                Bullet bullet = new Bullet(new Point(Position.X + 6, Position.Y + 2), 
                    ConsoleColor.White, BulletType.Standard);
                Bullets.Add(bullet);
            }
            if (key.Key == ConsoleKey.LeftArrow)
            {
                Bullet bullet = new Bullet(new Point(Position.X, Position.Y + 2), 
                    ConsoleColor.White, BulletType.Standard);
                Bullets.Add(bullet);
            }
            if (key.Key == ConsoleKey.UpArrow)
            {
                Bullet bullet = new Bullet(new Point(Position.X + 2, Position.Y - 2), 
                    ConsoleColor.White, BulletType.Special);
                Bullets.Add(bullet);
            }
        }

        public void Collisions(Point distance)
        {
            Point positionAssistance = new Point(Position.X + distance.X, Position.Y + distance.Y);

            if (positionAssistance.X <= WindowN.UpperLimit.X)
                positionAssistance.X = WindowN.UpperLimit.X + 1;
            if (positionAssistance.X + 6 >= WindowN.LowerLimit.X)
                positionAssistance.X = WindowN.LowerLimit.X - 7;
            if (positionAssistance.Y <= WindowN.UpperLimit.Y)
                positionAssistance.Y = WindowN.UpperLimit.Y + 1;
            if (positionAssistance.Y + 2 >= WindowN.LowerLimit.Y)
                positionAssistance.Y = WindowN.LowerLimit.Y - 3;

            Position = positionAssistance;
        }

        public void Move(int speed)
        {
            if(Console.KeyAvailable)
            {
                Erase();
                Point distance = new Point();
                Keyboard(ref distance, speed);
                Collisions(distance);
                Draw();
            }
        }

        public void Shoot()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i].Move(1, WindowN.UpperLimit.Y))
                {
                    Bullets.RemoveAt(i);
                }
            }
        }

    }
}
