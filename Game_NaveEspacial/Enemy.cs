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
        enum Direction
        {
            Right, Left, Up, Down,
        }

        public bool Living { get; set; }
        public float Health { get; set; }
        public Point Position { get; set; }
        public Window WindowN { get; set; }
        public ConsoleColor Color { get; set; }
        public EnemyType EnemyTypeE { get; set; }
        public List<Point> EnemyPosition { get; set; }
        public List<Bullet> Bullets { get; set; }
        private Direction _Direction;
        private DateTime _DirectionTime;
        private Random _Random;
        private float _RandomDirectionTime;
        private DateTime _MovementTime;
        private DateTime _FiringTime;
        private float _RandomFiringTime;

        public Enemy(Point position, ConsoleColor color, Window window, EnemyType enemyType)
        {
            Position = position;
            Color = color;
            WindowN = window;
            EnemyTypeE = enemyType;
            Health = 100;
            Living = true;
            _Direction = Direction.Right;
            _DirectionTime = DateTime.Now;
            _Random = new Random();
            _RandomDirectionTime = 1000;
            _MovementTime = DateTime.Now;
            _FiringTime = DateTime.Now;
            _RandomFiringTime = 200;
            EnemyPosition = new List<Point>();
            Bullets = new List<Bullet>();
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

        public void Move()
        {
            int time = 30;

            if (EnemyTypeE == EnemyType.Boss)
                time = 20;

            if(DateTime.Now > _MovementTime.AddMilliseconds(time))
            {
                Erase();
                RandomDirection();
                Point positionAssistance = Position;
                Movement(ref positionAssistance);
                Collisions(positionAssistance);
                Draw();
                _MovementTime = DateTime.Now;
            }
            CreateBullets();
            Fire();
        }

        public void Collisions(Point positionAssistance)
        {
            int width = 3;
            if(EnemyTypeE == EnemyType.Boss)
                width = 7;

            if (positionAssistance.X <= WindowN.UpperLimit.X)
            {
                _Direction = Direction.Right;
                positionAssistance.X = WindowN.UpperLimit.X + 1;
            }

            if (positionAssistance.X + width >= WindowN.LowerLimit.X)
            {
                _Direction = Direction.Left;
                positionAssistance.X = WindowN.LowerLimit.X - 1 - width;
            }

            if (positionAssistance.Y <= WindowN.UpperLimit.Y)
            {
                _Direction = Direction.Down;
                positionAssistance.Y = WindowN.UpperLimit.Y + 1;
            }

            if (positionAssistance.Y + 2 >= WindowN.UpperLimit.Y + 20)
            {
                _Direction = Direction.Up;
                positionAssistance.Y = WindowN.UpperLimit.Y + 20 - 2;
            }

            Position = positionAssistance;     
        }

        public void Movement(ref Point positionAssistance)
        {
            switch(_Direction)
            {
                case Direction.Right:
                    positionAssistance.X += 1;
                    break;
                
                case Direction.Left:
                    positionAssistance.X -= 1;
                    break;

                case Direction.Up:
                    positionAssistance.Y -= 1;
                    break;

                case Direction.Down:
                    positionAssistance.Y += 1;
                    break;
            }
        }

        public void RandomDirection()
        {
            if(DateTime.Now > _DirectionTime.AddMilliseconds(_RandomDirectionTime)
                && (_Direction == Direction.Right || _Direction == Direction.Left))
            {
                int randomNum = _Random.Next(1, 5);
            
                switch(randomNum)
                {
                    case 1:
                        _Direction = Direction.Right;
                        break;

                    case 2:
                        _Direction = Direction.Left;
                        break;

                    case 3:
                        _Direction = Direction.Up;
                        break;

                    case 4:
                        _Direction = Direction.Down;
                        break;
                }

                _DirectionTime = DateTime.Now;
                _RandomDirectionTime = _Random.Next(1000, 2000);
            }

            if (DateTime.Now > _DirectionTime.AddMilliseconds(50)
                && (_Direction == Direction.Up || _Direction == Direction.Down))
            {
                int randomNum = _Random.Next(1, 3);

                switch (randomNum)
                {
                    case 1:
                        _Direction = Direction.Right;
                        break;

                    case 2:
                        _Direction = Direction.Left;
                        break;
                }

                _DirectionTime = DateTime.Now;
            }
        }

        public void CreateBullets()
        {
            if(DateTime.Now > _FiringTime.AddMilliseconds(_RandomFiringTime))
            {
                if (EnemyTypeE == EnemyType.Standard)
                {
                    Bullet bullet = new Bullet(new Point(Position.X + 1, Position.Y + 2),
                        Color, BulletType.Enemy);
                    Bullets.Add(bullet);
                    _RandomFiringTime = _Random.Next(200, 500);
                }

                if (EnemyTypeE == EnemyType.Boss)
                {
                    Bullet bullet = new Bullet(new Point(Position.X + 4, Position.Y + 2),
                        Color, BulletType.Enemy);
                    Bullets.Add(bullet);
                    _RandomFiringTime = _Random.Next(100, 150);
                }
                _FiringTime = DateTime.Now;
            }
        }

        public void Fire()
        {
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                if (Bullets[i].Move(1, WindowN.LowerLimit.Y))
                    Bullets.RemoveAt(i);   
            }
        }

    }
}