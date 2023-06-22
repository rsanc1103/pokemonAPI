using System;
using System.Collections.Generic;

namespace testAPI.EntityModels;

public partial class Trainer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TrainersPokemon> TrainersPokemons { get; set; } = new List<TrainersPokemon>();
}
