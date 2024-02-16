using System.Diagnostics;

using static System.Console;

class TabladeTipos : Program
{
    static void getWeakness(Pokemon pokemon)
    {
        foreach (var type in pokemon.type)
        {
            switch (type)
            {
                case "Fire":
                    {
                    }
                    break;
                case "Water":
                    {
                    }
                    break;
                case "Grass":
                    {
                    }
                    break;
                case "Normal":
                    {
                    }
                    break;
                case "Fly":
                    {
                    }
                    break;
                case "Electric":
                    {
                    }
                    break;
                case "Ice":
                    {
                    }
                    break;
                case "Fighting":
                    {
                    }
                    break;
                case "Poison":
                    {
                    }
                    break;
                case "Ground":
                    {
                    }
                    break;
                case "Psychic":
                    {
                    }
                    break;
                case "Bug":
                    {
                    }
                    break;
                case "Rock":
                    {
                    }
                    break;
                case "Ghost":
                    {
                    }
                    break;
                case "Dragon":
                    {
                    }
                    break;
                case "Dark":
                    {
                    }
                    break;
                case "Steel":
                    {
                    }
                    break;
                case "Fairy":
                    {
                    }
                    break;
                default:
                    {
                        WriteLine("Ese no es un tipo existente");
                    }
                    break;
            }
        }
    }
}
