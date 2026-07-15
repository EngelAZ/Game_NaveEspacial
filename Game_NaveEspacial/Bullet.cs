using System.Drawing;

namespace NaveEspacial
{
    public enum BulletType
    {
        Standard, Special, Enemy,
    }
    internal class Bullet
    {
        public Point Position { get; set; }
        public ConsoleColor Color { get; set; }
        public BulletType Type { get; set; }
        public List<Point> PositionsBullet { get; set; }
        private DateTime _Time;

        public Bullet(Point position, ConsoleColor color, BulletType type)
        {
            Position = position;
            Color = color;
            Type = type;
            _Time = DateTime.Now;

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

                case BulletType.Enemy:
                    Console.SetCursorPosition(x, y);
                    Console.Write("█");
                    PositionsBullet.Add(new Point(x, y));
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

        public bool Move(int speed, int limit, List<Enemy>  enemies)
        {
            if(DateTime.Now > _Time.AddMilliseconds(20))
            {
                Erase();

                switch(Type)
                {
                    case BulletType.Standard:
                        Position = new Point(Position.X, Position.Y - speed);
                        if (Position.Y <= limit)
                            return true;

                        foreach(Enemy enemy in enemies)
                        {
                            foreach(Point positionE in enemy.EnemyPosition)
                            {
                                if(positionE.X == Position.X && positionE.Y == Position.Y)
                                {
                                    enemy.Health -= 7;
                                    if(enemy.Health <= 0)
                                    {
                                        enemy.Health = 0;
                                        enemy.Living = false;
                                        enemy.Death();
                                    }
                                    return true;
                                }
                            }
                        }

                        break;

                    case BulletType.Special:
                        Position = new Point(Position.X, Position.Y - speed);
                        if (Position.Y <= limit)
                            return true;

                        foreach(Enemy enemy in enemies)
                        {
                            foreach(Point positionE in enemy.EnemyPosition)
                            {
                                foreach(Point positionB in PositionsBullet)
                                {
                                    if(positionE.X == positionB.X && positionE.Y == positionB.Y)
                                    {
                                        enemy.Health -= 40;
                                        if(enemy.Health <= 0)
                                        {
                                            enemy.Health = 0;
                                            enemy.Living = false;
                                            enemy.Death();
                                        }
                                        return true;
                                    }
                                }
                            }
                        }

                        break;
                }
                Draw();
                _Time = DateTime.Now;
            }

            return false;
        }

        public bool Move(int speed, int limit, Spaceship spaceship)
        {
            if (DateTime.Now > _Time.AddMilliseconds(20))
            {
                Erase();
                Position = new Point(Position.X, Position.Y + speed);
                if (Position.Y >= limit)
                    return true;

                foreach(Point positionS in spaceship.PositionsSpaceship)
                {
                    if(positionS.X == Position.X && positionS.Y == Position.Y)
                    {
                        spaceship.Healt -= 5;
                        spaceship.ColorAssistance = Color;
                        spaceship.CollisionTime = DateTime.Now;
                        return true;
                    }
                }
                Draw();
                _Time = DateTime.Now;
            }

            return false;
        }
    }
}
