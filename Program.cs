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
            case 4:{
                AddPokemon();
            }break;
            default:
                {
                    WriteLine("Eso no es una opcion");
                }
                break;
        }
    }

    static void AddPokemon(){
        List<EntityPokemon> pokemons = DeserializePokedexJson();
        EntityPokemon newPokemon = new EntityPokemon();
        newPokemon.Create();

        pokemons.Add(newPokemon);

        string json = JsonSerializer.Serialize(pokemons, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("./Pokedex.Json",json);
        WriteLine("Pokemon Agregado Correctamente!");

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
