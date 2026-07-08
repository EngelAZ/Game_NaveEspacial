using System.Drawing;

namespace NaveEspacial
{
    public enum BulletType
    {
        Standard, Special
    }
    internal class Bullet
    {
        public Point Position { get; set; }
        public ConsoleColor Color { get; set; }
        public BulletType Type { get; set; }
        public List<Point> PositionsBullet { get; set; }

        public Bullet(Point position, ConsoleColor color, BulletType type)
        {
            Position = position;
            Color = color;
            Type = type;

            PositionsBullet = new List<Point>();    
        }

       public void Draw()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            PositionsBullet.Clear();

            switch (Type)
            {
                case BulletType.Standard:
                    Console.SetCursorPosition(x, y);
                    Console.Write("o");
                    PositionsBullet.Add(new Point(x, y));
                    break;

                case BulletType.Special:
                    Console.SetCursorPosition(x + 1, y);
                    Console.Write("-");
                    Console.SetCursorPosition(x, y + 1);
                    Console.Write("( )");
                    Console.SetCursorPosition(x + 1, y + 2);
                    Console.Write("W");

                    PositionsBullet.Add(new Point(x + 1, y));
                    PositionsBullet.Add(new Point(x, y + 1));
                    PositionsBullet.Add(new Point(x + 2, y + 1));
                    PositionsBullet.Add(new Point(x + 1, y + 2));

                    break;
            }
        }

        public void Erase()
        {
            foreach(Point item in PositionsBullet)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }

        public bool Move(int speed, int limit)
        {
            Erase();

            switch(Type)
            {
                case BulletType.Standard:
                    Position = new Point(Position.X, Position.Y - speed);
                    if (Position.Y <= limit)
                        return true;
                    break;

                case BulletType.Special:
                    Position = new Point(Position.X, Position.Y - speed);
                    if (Position.Y <= limit)
                        return true;
                    break;
            }
            Draw();
            return false;
        }
    }
}
