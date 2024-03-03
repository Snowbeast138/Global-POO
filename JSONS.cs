using System.Text.Json;
using static System.Console;

public class JSONS {
    public static List<Team> DeserializeTeamsJson(){
        string path = "./Teams.Json";
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            if(jsonString!=""){
                List<Team> teams =
                    JsonSerializer.Deserialize<List<Team>>(jsonString);
                return teams;
            }
            else{
                List<Team> teams = new();
                return teams;
            }
        }
        else
        {
            WriteLine("El archivo Teams.Json no existe o a Sido Borrado.");
            return null;
        }
    }

    public static List<EntityPokemon> DeserializePokedexJson()
    {
        string path = "./Pokedex.Json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            List<EntityPokemon> pokemons =
                JsonSerializer.Deserialize<List<EntityPokemon>>(jsonString);

            return pokemons;
        }
        else
        {
            WriteLine("El archivo Pokedex.Json no existe o a Sido Borrado.");
            return null;
        }
    }
 
    public static void SerializeTeamJSON(List<Team> teams){
        string json = JsonSerializer.Serialize(teams, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Teams.Json",json);
    }

    public static void SerializePokedexJSON(List<EntityPokemon> pokemons){
        string json = JsonSerializer.Serialize(pokemons, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Pokedex.Json",json);
    }
 
 }
