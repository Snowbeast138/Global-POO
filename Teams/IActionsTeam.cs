public interface IActionsTeam
{
    Team FindTeam(List<Team> teams, int id);

    Team
    CreateTeam(
        List<Team> teams, List<EntityPokemon> pokemons, bool randomTeam
    );

    Team ReplacePokemon(Team team, int id, PokemonTeam newPokemon);
}
