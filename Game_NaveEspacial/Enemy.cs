using System.Drawing;

namespace NaveEspacial
{
    public enum EnemyType
    {
        Standard,
        Boss,
    }

    internal class Enemy
    {
        public bool Living { get; set; }
        public float Health { get; set; }
        public Point Position { get; set; }
        public Window WindowN { get; set; }
        public ConsoleColor Color { get; set; }
        public EnemyType EnemyTypeE { get; set; }
        public List<Point> EnemyPosition { get; set; }

        public Enemy(Point position, ConsoleColor color, Window window, EnemyType enemyType)
        {
            Position = position;
            Color = color;
            WindowN = window;
            EnemyTypeE = enemyType;
            Health = 100;
            Living = true;
            EnemyPosition = new List<Point>();
        }

        public void Draw()
        {
            switch(EnemyTypeE)
            {
                case EnemyType.Standard:
                    StandardDrawing();
                    break;

                case EnemyType.Boss:
                    BossDrawing();
                    break;
            }
        }

        public void StandardDrawing()
        {
            Console.ForegroundColor = Color;

            int x = Position.X;
            int y = Position.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            EnemyPosition.Clear();

            EnemyPosition.Add(new Point(x + 1, y));
            EnemyPosition.Add(new Point(x + 2, y));
            EnemyPosition.Add(new Point(x, y + 1));
            EnemyPosition.Add(new Point(x + 1, y + 1));
            EnemyPosition.Add(new Point(x + 2, y + 1));
            EnemyPosition.Add(new Point(x + 3, y + 1));
            EnemyPosition.Add(new Point(x, y + 2));
            EnemyPosition.Add(new Point(x + 3, y + 2));
        }

        public void BossDrawing()
        {
            Console.ForegroundColor = Color;

            int x = Position.X;
            int y = Position.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("█▄▄▄▄█");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("██ ██ ██");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("█▀▀▀▀▀▀█");

            EnemyPosition.Clear();

            EnemyPosition.Add(new Point(x + 1, y));
            EnemyPosition.Add(new Point(x + 2, y));
            EnemyPosition.Add(new Point(x + 3, y));
            EnemyPosition.Add(new Point(x + 4, y));
            EnemyPosition.Add(new Point(x + 5, y));
            EnemyPosition.Add(new Point(x + 6, y));

            EnemyPosition.Add(new Point(x, y + 1));
            EnemyPosition.Add(new Point(x + 1, y + 1));
            EnemyPosition.Add(new Point(x + 3, y + 1));
            EnemyPosition.Add(new Point(x + 4, y + 1));
            EnemyPosition.Add(new Point(x + 6, y + 1));
            EnemyPosition.Add(new Point(x + 7, y + 1));

            EnemyPosition.Add(new Point(x, y + 2));
            EnemyPosition.Add(new Point(x + 1, y + 2));
            EnemyPosition.Add(new Point(x + 2, y + 2));
            EnemyPosition.Add(new Point(x + 3, y + 2));
            EnemyPosition.Add(new Point(x + 4, y + 2));
            EnemyPosition.Add(new Point(x + 5, y + 2));
            EnemyPosition.Add(new Point(x + 6, y + 2));
            EnemyPosition.Add(new Point(x + 7, y + 2));
        }

        public void Erase()
        {
            foreach(Point item in EnemyPosition)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }
    }
}