using System;
using System.Collections.Generic;

namespace testAPI.EntityModels;

public partial class AllTrainersPokemonTeam
{
    public string? TrainerName { get; set; }

    public string? PokemonSpecies { get; set; }

    public string? PokemonName { get; set; }

    public bool? IsShiny { get; set; }
}
