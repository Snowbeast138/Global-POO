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
