using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testAPI.Models
{
    public class Digimon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }

        public Digimon(string name, string description, int id, string type)
        {
            this.Name = name;
            this.Description = description;
            this.Id = id;
            this.Type = type;
        }
    }
}