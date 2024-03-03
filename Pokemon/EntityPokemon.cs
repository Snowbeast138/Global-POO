using static System.Console;

interface IActionsPokemon
{
    EntityPokemon findPokemon(List<EntityPokemon> pokemons, string name);
}

public class EntityPokemon : Pokemon, IActionsPokemon
{
    public EntityPokemon findPokemon(List<EntityPokemon> pokemons, string name)
    {
        if (pokemons.Find(x => x.name == name) != null)
        {
            return pokemons.Find(x => x.name == name);
        }
        else
        {
            string res;
            do
            {
                WriteLine("No existe registro de ese pokemon asegurese de haber escrito bien su nombre o que sea menor a 8 GEN");
                Write("Vuelva a escribir el nombre del pokemon:");
                res = ReadLine();
                res = res.ToLower();
            }
            while (pokemons.Find(x => x.name == res) == null);
            return pokemons.Find(x => x.name == res);
        }
    }

    public override void ShowStats()
    {
        WriteLine("--------------------------------------------");
        WriteLine($"ID: {this.id}");
        WriteLine($"Pokemon: {this.name}");
        WriteLine("Tipos:");
        foreach (var type in this.type)
        {
            WriteLine($"- {type}");
        }
        WriteLine();
        WriteLine("Estadísticas base:");
        WriteLine($"HP: {this.BaseStats.HP}");
        WriteLine($"Ataque: {this.BaseStats.Attack}");
        WriteLine($"Defensa: {this.BaseStats.Defense}");
        WriteLine($"Ataque Especial: {this.BaseStats.SpAttack}");
        WriteLine($"Defensa Especial: {this.BaseStats.SpDefense}");
        WriteLine($"Velocidad: {this.BaseStats.Speed}");
        WriteLine("--------------------------------------------");
    }
}
