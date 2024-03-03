using static System.Console;

public class PokemonMenu
{
    public static void AddPokemon()
    {
        List<EntityPokemon> pokemons = JSONS.DeserializePokedexJson();
        EntityPokemon newPokemon = new EntityPokemon();
        newPokemon.Create();

        pokemons.Add (newPokemon);

        JSONS.SerializePokedexJSON (pokemons);

        WriteLine("Pokemon Agregado Correctamente!");
    }

    public static void findPokemon()
    {
        WriteLine("Que pokemon Quieres buscar");
        string name = ReadLine();
        EntityPokemon pokemon =new();
        pokemon = pokemon.findPokemon(JSONS.DeserializePokedexJson(), name);
        pokemon.ShowStats();
    }

   public static void ShowPokedex()
    {
        List<EntityPokemon> pokemons= JSONS.DeserializePokedexJson();
        foreach (var pokemon in pokemons)
        {
            WriteLine("");
            Write($"ID: {pokemon.id}  Pokemon:{pokemon.name}  Tipos:");
            foreach (var type in pokemon.type)
            {
                Write($" -{type}");
            }
        }
    }
}
