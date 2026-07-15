using NaveEspacial;
using System.Drawing;

Window window;
Spaceship spaceship;
bool jugar = true;
bool FinalBoss = false;
Enemy enemy1;
Enemy enemy2;
Enemy boss;

void init()
{
    window = new Window(165, 53, ConsoleColor.Black, new Point(5, 3), new Point(160, 51));
    window.DrawBorders();
    spaceship = new Spaceship(new Point(80, 25), ConsoleColor.White, window);
    spaceship.Draw();
    enemy1 = new Enemy(new Point(50, 10), ConsoleColor.Cyan, window, EnemyType.Standard, spaceship);
    enemy1.Draw();
    enemy2 = new Enemy(new Point(120, 10), ConsoleColor.Red, window, EnemyType.Standard, spaceship);
    enemy2.Draw();
    boss = new Enemy(new Point(80, 10), ConsoleColor.Magenta, window, EnemyType.Boss, spaceship);

    spaceship.Enemies.Add(enemy1);
    spaceship.Enemies.Add(enemy2);
    spaceship.Enemies.Add(boss);
}

void game()
{
    while(jugar)
    {
        if (!enemy1.Living && !enemy2.Living)
        {
            FinalBoss = true;
        }
        if(FinalBoss)
        {
            boss.Move();
            boss.Information(140);
        }
        else 
        {
        enemy1.Move();
        enemy1.Information(100);
        enemy2.Move();
        enemy2.Information(120);
        }
        
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
//Console.ReadKey();