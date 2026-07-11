using NaveEspacial;
using System.Drawing;

Window window;
Spaceship spaceship;
bool jugar = true;
Enemy enemy1;
Enemy enemy2;
Enemy boss;

void init()
{
    window = new Window(165, 53, ConsoleColor.Black, new Point(5, 3), new Point(160, 51));
    window.DrawBorders();
    spaceship = new Spaceship(new Point(80, 25), ConsoleColor.White, window);
    spaceship.Draw();
    enemy1 = new Enemy(new Point(50, 10), ConsoleColor.Cyan, window, EnemyType.Standard);
    enemy1.Draw();
    enemy2 = new Enemy(new Point(120, 10), ConsoleColor.Red, window, EnemyType.Standard);
    enemy2.Draw();
    boss = new Enemy(new Point(80, 10), ConsoleColor.Magenta, window, EnemyType.Boss);
}

void game()
{
    while(jugar)
    {
        enemy1.Move();
        enemy2.Move();
        spaceship.Move(3);
        spaceship.Shoot();
        if(spaceship.Healt <= 0)
        {
            jugar = false;
            spaceship.Death();
        }
    }
}

init();
game();
Console.ReadKey();