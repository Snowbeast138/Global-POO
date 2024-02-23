using System.Text.Json;

using static System.Console;

public abstract class Pokemon
{
    public abstract void ShowStats();

    public int id { get; set; }

    public string name { get; set; }

    public List<string> type { get; set; }

    public BaseStats BaseStats { get; set; }
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

public interface IActionsTeam
{
    Team FindTeam(List<Team> teams, int id);

    Team CreateTeam(List<Team> teams, List<EntityPokemon> pokemons, bool randomTeam);
    Team ReplacePokemon(Team team, int id, PokemonTeam newPokemon);
}

public class Team : IActionsTeam
{
    public int TeamId { get; set; }

    public List<PokemonTeam> pokemonsTeam
    { get; set;
    } = new List<PokemonTeam>();

    public Team ReplacePokemon(Team team, int id, PokemonTeam newPokemon)
    {
        int index = team.pokemonsTeam.FindIndex(x => x.PokemonTeamId == id);
        if (index != 1)
        {
            team.pokemonsTeam[index] = newPokemon;
        }
        return team;
    }

    public Team FindTeam(List<Team> teams, int id)
    {
        if (teams.Find(x => x.TeamId == id) != null)
        {
            Team team = teams.Find(x => x.TeamId == id); 
            team.ShowTeam();
            return team;
        }
        else
        {
            int res;
            do
            {
                WriteLine("No existe ese team id");
                while (!int.TryParse(ReadLine(), out res))
                {
                    WriteLine("Ese no es un TeamId");
                }
            }
            while (teams.Find(x => x.TeamId == res) == null);
            Team team =teams.Find(x => x.TeamId == res);
             team.ShowTeam();
            return team;
        }
    }

    public void ShowTeam(){
        WriteLine("----------------------------");
        WriteLine($"TeamId:{this.TeamId}");
        WriteLine("Equipo Pokemon:");
        foreach(var pokemonTeam in this.pokemonsTeam){
            WriteLine($"PokemonTeamID:{pokemonTeam.PokemonTeamId}");
            pokemonTeam.pokemon.ShowStats();
            WriteLine();
            }
    }

    public Team CreateTeam(List<Team> teams, List<EntityPokemon> pokemons, bool randomTeam){
        Random random = new Random();
        int TeamId = random.Next(1,1000);
        while(teams.Any(x=> x.TeamId == TeamId)){
            TeamId = random.Next(1,1000);
        }
        Team team = new();
        team.TeamId = TeamId;

        if(randomTeam == false){
            for(int i = 1; i <= 6; i++){
                Write($"Pokemon {i}:");
                string res = ReadLine();
                EntityPokemon pokemon = new();
                PokemonTeam pokemonTeam = new(i, pokemon.findPokemon(pokemons, res.ToLower()));
                team.pokemonsTeam.Add(pokemonTeam);
            }
        }
        else{
            for(int i = 1; i<=6;i++){
            int id = random.Next(1,809);
            while(team.pokemonsTeam.Any(x=> x.pokemon.id == id)){
                id = random.Next(1,809);
            }
            PokemonTeam pokemon = new(i,pokemons.Find(x=>x.id == id));
            team.pokemonsTeam.Add(pokemon);
        }
        }
        return team;
    }
}

public class PokemonTeam
{
    public PokemonTeam(int PokemonTeamId, EntityPokemon? pokemon)
    {
        this.PokemonTeamId = PokemonTeamId;
        this.pokemon = pokemon;
    }

    public int PokemonTeamId { get; set; }

    public EntityPokemon pokemon { get; set; }
}

partial class Program
{
    static void Main()
    {
        WriteLine("1-Mostrar Pokedex\n2-Buscar Pokemon\n3-Menu de Equipos");
        int res;
        while (!int.TryParse(ReadLine(), out res) || res > 3 || res < 1)
        {
            WriteLine("Esa no es una opcion");
        }
        switch (res)
        {
            case 1:
                {
                    ShowPokedex(DeserializePokedexJson());
                }
                break;
            case 2:
                {
                    findPokemon();
                }
                break;
            case 3:
                {
                    TeamMenu();
                }
                break;
            default:
                {
                    WriteLine("Eso no es una opcion");
                }
                break;
        }
    }

    static List<EntityPokemon> DeserializePokedexJson()
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

    static void TeamMenu(){
        WriteLine("1-Mostrar Equipos\n2-Crear Equipo\n3-Generar Equipo Random\n4-Modificar Equipo\n5-Eliminar Equipo");
        int res;
        while(!int.TryParse(ReadLine(), out res)|| res > 5 || res < 1){
            WriteLine("Esa no es una opcion");
        }
        switch (res)
        {
            case 1:{
                ShowTeams();
            }break;
            case 2:{
                WriteLine("Vamos a crear un equipo!");
                CreateTeam(false);
            }break;
            case 3:{
                CreateTeam(true);
            }break;
            case 4:{
                UpdateTeam();
            }break;
            case 5:{
                DeleteTeam();
            }break;
            default:{WriteLine("Eso no es una opcion");}break;
        }
    }
    static List<Team> DeserializeTeamsJson(){
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
    static void ShowTeams(){
        List<Team> teams = DeserializeTeamsJson();
        if(teams.Count > 0){
            foreach(var team in teams){
                team.ShowTeam();
                WriteLine("------------------------");
            }
        }
        else{
            WriteLine("No hay Equipos Guardados");
        }
    }
  
     static void CreateTeam(bool randomTeam){
        List<Team> teams = DeserializeTeamsJson();
        List<EntityPokemon> pokemons = DeserializePokedexJson();
        Team newTeam = new();
        newTeam = newTeam.CreateTeam(teams, pokemons, randomTeam);
        
        teams.Add(newTeam);
        string json = JsonSerializer.Serialize(teams, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Teams.Json",json);
        WriteLine("Equipo creado Correctamente!");

        newTeam.ShowTeam();
     }

    static void UpdateTeam(){
        List<Team> teams = DeserializeTeamsJson();
        List<EntityPokemon> pokemons = DeserializePokedexJson();
        ShowTeams();
        Write("Ingresa el TeamId del equipo que deseas Modificar:");
        int res;
        while(!int.TryParse(ReadLine(), out res)){
            WriteLine("Ese no es un TeamId");
        }
        Team teamToUpdate = new();
        teamToUpdate = teamToUpdate.FindTeam(teams, res);

        WriteLine("Que pokemon quieres modificar?:");
        int pokemonId;
        while(!int.TryParse(ReadLine(), out pokemonId)|| pokemonId < 1 || pokemonId > 6){
            WriteLine("Ese no es un PokemonTeamId");
        }

        WriteLine("Por cual pokemon deseas cambiarlo?:");
        string name = ReadLine();
        EntityPokemon pokemon = new();
        pokemon = pokemon.findPokemon(pokemons, name.ToLower());

        PokemonTeam pokemonTeam = new(pokemonId, pokemon);

       teamToUpdate=teamToUpdate.ReplacePokemon(teamToUpdate, pokemonId,pokemonTeam);
        
        List<Team> updatedTeams = new();
        foreach (var team in teams)
        {
            if(team.TeamId!=teamToUpdate.TeamId){
                updatedTeams.Add(team);
            }else{
                updatedTeams.Add(teamToUpdate);
            }
        }
        WriteLine($"Equipo {teamToUpdate.TeamId} Actualizado");
        string json = JsonSerializer.Serialize(updatedTeams, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Teams.Json",json);
        teamToUpdate.ShowTeam();
    }


     static void DeleteTeam(){
        ShowTeams();
        List<Team> teams = DeserializeTeamsJson();
        Write("Ingresa el TeamId del equipo que deseas eliminar:");
        int res;
        while(!int.TryParse(ReadLine(), out res)){
            WriteLine("Ese no es un TeamId");
        }
        Team teamToDelte = new();
        teamToDelte = teamToDelte.FindTeam(teams,res);
        
        List<Team> updatedTeams = new();
        foreach (var team in teams)
        {
            if(team.TeamId!=teamToDelte.TeamId){
                updatedTeams.Add(team);
            }
        }
        WriteLine($"Equipo {teamToDelte.TeamId} Eliminado");
        string json = JsonSerializer.Serialize(updatedTeams, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Teams.Json",json);
     }
    // static Team findTeam(int TeamId){
    //     List<Team> teams = DeserializeTeamsJson();
    //     if(teams.Find(x=> x.TeamId == TeamId)!=null){
    //         Team team = teams.Find(x=> x.TeamId == TeamId);
    //         return team;
    //     }else{
    //         int res;
    //         do{
    //         WriteLine("No existe ese team id");
    //          while(!int.TryParse(ReadLine(), out res)){
    //             WriteLine("Ese no es un TeamId");
    //          }
    //         }while(teams.Find(x=> x.TeamId == res)==null);
    //         Team team = teams.Find(x=> x.TeamId == res);
    //         return team;
    //     }
    // }
    static void findPokemon()
    {
        WriteLine("Que pokemon Quieres buscar");
        string name = ReadLine();
        EntityPokemon pokemon =new();
        pokemon = pokemon.findPokemon(DeserializePokedexJson(), name);
        pokemon.ShowStats();
    }
 
    static void ShowPokedex(List<EntityPokemon> pokemons)
    {
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
