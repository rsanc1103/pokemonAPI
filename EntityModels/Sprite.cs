using System;
using System.Collections.Generic;

namespace testAPI.EntityModels;

public partial class Sprite
{
    public int PokemonId { get; set; }

    public string Normal { get; set; } = null!;

    public string? Shiny { get; set; }

    public virtual Pokemon Pokemon { get; set; } = null!;
}
