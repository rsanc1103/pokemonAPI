using System;
using System.Collections.Generic;

namespace testAPI.EntityModels;

public partial class Pokemon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Height { get; set; }

    public decimal Weight { get; set; }

    public int BaseXp { get; set; }

    public int Hp { get; set; }

    public int Attack { get; set; }

    public int SpecialAttack { get; set; }

    public int Defense { get; set; }

    public int SpecialDefense { get; set; }

    public int Speed { get; set; }

    public decimal Evasion { get; set; }

    public decimal Accuracy { get; set; }

    public int GenerationId { get; set; }

    public virtual ICollection<Evolution> Evolutions { get; set; } = new List<Evolution>();

    public virtual Sprite? Sprite { get; set; }

    public virtual ICollection<TrainersPokemon> TrainersPokemons { get; set; } = new List<TrainersPokemon>();

    public virtual ICollection<Ability> Abilities { get; set; } = new List<Ability>();

    public virtual ICollection<Move> Moves { get; set; } = new List<Move>();

    public virtual ICollection<Type> Types { get; set; } = new List<Type>();
}
