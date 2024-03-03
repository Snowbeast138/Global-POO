using System.Text.Json;
using static System.Console;

public class TeamMenu
{
    public void ShowMenu()
    {
        WriteLine("1-Mostrar Equipos\n2-Crear Equipo\n3-Generar Equipo Random\n4-Modificar Equipo\n5-Eliminar Equipo");
        int res;
        while (!int.TryParse(ReadLine(), out res) || res > 5 || res < 1)
        {
            WriteLine("Esa no es una opcion");
        }
        switch (res)
        {
            case 1:
                {
                    ShowTeams();
                }
                break;
            case 2:
                {
                    WriteLine("Vamos a crear un equipo!");
                    CreateTeam(false);
                }
                break;
            case 3:
                {
                    CreateTeam(true);
                }
                break;
            case 4:
                {
                    UpdateTeam();
                }
                break;
            case 5:
                {
                    DeleteTeam();
                }
                break;
            default:
                {
                    WriteLine("Eso no es una opcion");
                }
                break;
        }
    }

    static void ShowTeams()
    {
        List<Team> teams = JSONS.DeserializeTeamsJson();


        if (teams.Count > 0)
        {
            foreach (var team in teams)
            {
                team.ShowTeam();
                WriteLine("------------------------");
            }
        }
        else
        {
            WriteLine("No hay Equipos Guardados");
        }
    }

    static void CreateTeam(bool randomTeam){
        List<Team> teams= JSONS.DeserializeTeamsJson();
        List<EntityPokemon> pokemons = JSONS.DeserializePokedexJson();
        
        Team newTeam = new();
        newTeam = newTeam.CreateTeam(teams, pokemons, randomTeam);
        
        teams.Add(newTeam);
        
        JSONS.SerializeTeamJSON(teams);

        WriteLine("Equipo creado Correctamente!");

        newTeam.ShowTeam();
     }

     static void UpdateTeam(){
        List<Team> teams= JSONS.DeserializeTeamsJson();
        List<EntityPokemon> pokemons = JSONS.DeserializePokedexJson();
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
        
        JSONS.SerializeTeamJSON(teams);
        
        teamToUpdate.ShowTeam();
    }

     static void DeleteTeam(){
        ShowTeams();
        List<Team> teams = JSONS.DeserializeTeamsJson();
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
        JSONS.SerializeTeamJSON(updatedTeams);
     }
}
