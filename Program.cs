using System.Text.Json;

using static System.Console;

public class Pokemon
{
    public int id { get; set; }

    public name name { get; set; }

    public List<string> type { get; set; }

    public BaseStats BaseStats { get; set; }
}

public class name
{
    public string english { get; set; }
}

public class BaseStats
{
    public int HP { get; set; }

    public int Attack { get; set; }

    public int Defense { get; set; }

    public int SpAttack { get; set; }

    public int SpDefense { get; set; }

    public int Speed { get; set; }
}

class Program
{
    static void Main()
    {
        string path = "./Pokedex.Json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            List<Pokemon> pokemons =
                JsonSerializer.Deserialize<List<Pokemon>>(jsonString);

            ShowPokedex (pokemons);
        }
        else
        {
            WriteLine("El archivo Pokedex.Json no existe.");
        }
    }

    static void ShowPokedex(List<Pokemon> pokemons)
    {
        foreach (var pokemon in pokemons)
        {
            WriteLine("----------------------------------");
            WriteLine($"ID: {pokemon.id}");

            WriteLine($"Pokemon: {pokemon.name.english}");

            WriteLine("Tipos:");
            foreach (var type in pokemon.type)
            {
                WriteLine($"- {type}");
            }
            // WriteLine();
            // WriteLine("Estadísticas base:");
            // WriteLine($"HP: {pokemon.BaseStats.HP}");
            // WriteLine($"Ataque: {pokemon.BaseStats.Attack}");
            // WriteLine($"Defensa: {pokemon.BaseStats.Defense}");
            // WriteLine($"Ataque Especial: {pokemon.BaseStats.SpAttack}");
            // WriteLine($"Defensa Especial: {pokemon.BaseStats.SpDefense}");
            // WriteLine($"Velocidad: {pokemon.BaseStats.Speed}");
            // WriteLine("----------------------------------");
            // WriteLine();
        }
    }
}
