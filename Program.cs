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

public class Team{
    public int TeamId{get;set;}
    public List<PokemonTeam> pokemonsTeam {get; set;} = new List<PokemonTeam>();
}

public class PokemonTeam{
    public PokemonTeam(int PokemonTeamId, Pokemon? pokemon)
    {
        this.PokemonTeamId = PokemonTeamId;
        this.pokemon = pokemon;
    }

    public int PokemonTeamId{ get; set;}
    public Pokemon pokemon{get; set;}
    // public int Id { get; }
}


class Program
{
    static void Main()
    {
        TeamMenu();
        
    }

    static List<Pokemon> DeserializePokedexJson(){
        string path = "./Pokedex.Json";

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            List<Pokemon> pokemons =
                JsonSerializer.Deserialize<List<Pokemon>>(jsonString);
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
                CreateTeam();
            }break;
            case 3:{
                CreateRandomTeam();
            }break;
            case 4:{

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
                WriteLine("----------------------------");
                WriteLine($"TeamId:{team.TeamId}");
                WriteLine("Equipo Pokemon:");
                foreach(var pokemon in team.pokemonsTeam){
                    WriteLine($"-{pokemon.pokemon.name.english}");
                    WriteLine("Type/s:");
                    foreach(var type in pokemon.pokemon.type){
                        WriteLine($" -{type}");
                    }
                    WriteLine();
                }
                WriteLine("------------------------");
            }
        }
        else{
            WriteLine("No hay Equipos Guardados");
        }
    }

    static void ShowTeam(Team team){
     WriteLine("----------------------------");
     WriteLine($"TeamId:{team.TeamId}");
     WriteLine("Equipo Pokemon:");
     foreach(var pokemon in team.pokemonsTeam){
        WriteLine($"-{pokemon.pokemon.name.english}");
        WriteLine("Type/s:");
        foreach(var type in pokemon.pokemon.type){
            WriteLine($" -{type}");
        }
        WriteLine();
        }
     WriteLine("------------------------");

    }


     static void CreateTeam(){
        List<Team> teams = DeserializeTeamsJson();
            
        Random random = new Random();
        int TeamId = random.Next(1,1000);
            
        while(teams.Any(x=> x.TeamId == TeamId)){
            TeamId = random.Next(1,1000);
        }
        
        Team team = new();
        team.TeamId = TeamId;

        for(int i = 1; i <= 6; i++){
            Write($"Pokemon {i}:");
            string res = ReadLine();
            PokemonTeam pokemonTeam = new(i, findPokemon(DeserializePokedexJson(), res));
            team.pokemonsTeam.Add(pokemonTeam);
        }

        teams.Add(team);

        string json = JsonSerializer.Serialize(teams, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Teams.Json",json);

        WriteLine("Equipo creado Correctamente!");
        ShowTeam(team);
     }

     static void CreateRandomTeam(){
        List<Team> teams = DeserializeTeamsJson();

        List<Pokemon> pokemons = DeserializePokedexJson();
       
        Team randomTeam = new();

        Random random = new Random();
        
        int TeamId = random.Next(1,1000);

        while(teams.Any(x=> x.TeamId == TeamId)){
            TeamId = random.Next(1,1000);
        }

        randomTeam.TeamId = TeamId;

        for(int i = 1; i<=6;i++){
            int id = random.Next(1,809);
            while(randomTeam.pokemonsTeam.Any(x=> x.pokemon.id == id)){
                id = random.Next(1,809);
            }
           PokemonTeam pokemon = new(i,pokemons.Find(x=>x.id == id));
           randomTeam.pokemonsTeam.Add(pokemon);
        }

        teams.Add(randomTeam);
        
        string json = JsonSerializer.Serialize(teams, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Teams.Json",json);
        
        WriteLine("Equipo creado Correctamente!");
        ShowTeam(randomTeam);
     }


     static void DeleteTeam(){
        ShowTeams();
        Write("Ingresa el TeamId del equipo que deseas eliminar:");
        int res;
        while(!int.TryParse(ReadLine(), out res)){
            WriteLine("Ese no es un TeamId");
        }
        Team teamToDelte = findTeam(res);
        List<Team> teams = DeserializeTeamsJson();
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

    static Team findTeam(int TeamId){
        List<Team> teams = DeserializeTeamsJson();
        if(teams.Find(x=> x.TeamId == TeamId)!=null){
            
            Team team = teams.Find(x=> x.TeamId == TeamId);
            return team;
        }else{
            int res;
            do{
            WriteLine("No existe ese team id");
             while(!int.TryParse(ReadLine(), out res)){
                WriteLine("Ese no es un TeamId");
             }
            }while(teams.Find(x=> x.TeamId == res)==null);
            Team team = teams.Find(x=> x.TeamId == res);
            return team;
        }
    }


    static Pokemon findPokemon(List<Pokemon> pokemons, string name)
    {
        Pokemon pokemon = new();

        name = name.ToLower();


        if (pokemons.Find(x => x.name.english == name) != null)
        {
            pokemon =  pokemons.Find(x => x.name.english == name);
            // ShowPokemon(pokemon);
            return pokemon;
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
            while (pokemons.Find(x => x.name.english == res) == null);
            pokemon = pokemons.Find(x => x.name.english == res);
            // ShowPokemon(pokemon);
            return pokemon;
        }
    }

    static void ShowPokemon(Pokemon pokemon)
    {
        WriteLine($"ID: {pokemon.id}");

        WriteLine($"Pokemon: {pokemon.name.english}");

        WriteLine("Tipos:");
        foreach (var type in pokemon.type)
        {
            WriteLine($"- {type}");
        }
        WriteLine();
        WriteLine("Estadísticas base:");
        WriteLine($"HP: {pokemon.BaseStats.HP}");
        WriteLine($"Ataque: {pokemon.BaseStats.Attack}");
        WriteLine($"Defensa: {pokemon.BaseStats.Defense}");
        WriteLine($"Ataque Especial: {pokemon.BaseStats.SpAttack}");
        WriteLine($"Defensa Especial: {pokemon.BaseStats.SpDefense}");
        WriteLine($"Velocidad: {pokemon.BaseStats.Speed}");
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
        }
    }
}
