using System.Text.Json;

using static System.Console;

partial class Program
{
    static void Main()
    {
        WriteLine("1-Mostrar Pokedex\n2-Buscar Pokemon\n3-Menu de Equipos\n4-Agregar Pokemon");
        int res;
        while (!int.TryParse(ReadLine(), out res) || res > 4 || res < 1)
        {
            WriteLine("Esa no es una opcion");
        }
        switch (res)
        {
            case 1:
                {
                    PokemonMenu.ShowPokedex();
                    
                }
                break;
            case 2:
                {
                    PokemonMenu.findPokemon();
                    
                }
                break;
            case 3:
                {
                    TeamMenu teamMenu = new();
                    teamMenu.ShowMenu();
                }
                break;
            case 4:
                {
                    PokemonMenu.AddPokemon();
                }break;
            default:
                {
                    WriteLine("Eso no es una opcion");
                }
                break;
        }
    }

}
