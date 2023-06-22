using System;
using System.Collections.Generic;

namespace testAPI.EntityModels;

public partial class Type
{
    public int Id { get; set; }

    public string Type1 { get; set; } = null!;

    public virtual ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
}
