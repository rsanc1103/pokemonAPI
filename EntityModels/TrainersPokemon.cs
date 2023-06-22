using System;
using System.Collections.Generic;

namespace testAPI.EntityModels;

public partial class TrainersPokemon
{
    public int Id { get; set; }

    public int TrainerId { get; set; }

    public int PokemonId { get; set; }

    public int Level { get; set; }

    public int Exp { get; set; }

    public string? Nickname { get; set; }

    public bool? IsShiny { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;

    public virtual Trainer Trainer { get; set; } = null!;
}
