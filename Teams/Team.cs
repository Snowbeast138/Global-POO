using static System.Console;


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