using NaveEspacial;
using System.Drawing;

Window window;
Spaceship spaceship;
bool jugar = true;

void init()
{
    window = new Window(165, 53, ConsoleColor.Black, new Point(5, 3), new Point(160, 51));
    window.DrawBorders();
    spaceship = new Spaceship(new Point(80, 25), ConsoleColor.White, window);
    spaceship.Draw();
}

void game()
{
    while(jugar)
    {
        spaceship.Move(3);
    }
}

init();
game();
Console.ReadKey();