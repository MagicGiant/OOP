using BusinessLayer.Entities;
using PresentationLayer.Menu;

class Program
{
    public static void Main()
    {
        AuthorizationMenu authorizationMenu = new ();
        authorizationMenu.RunMenu();
    }
}