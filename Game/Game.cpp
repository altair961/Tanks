#include <SFML/Graphics.hpp>
using namespace sf;

int main()
{
    VideoMode vm(1920, 1080);
    RenderWindow window(vm, "Tanks", Style::Fullscreen);

    while (window.isOpen())
    {
        if (Keyboard::isKeyPressed(Keyboard::Escape))
        {
            window.close();
        }
        window.clear();
        window.display();
    }
}