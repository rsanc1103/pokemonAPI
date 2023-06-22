using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testAPI.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private PokemonContext _context;
        public PokemonController(PokemonContext context)
        {
            _context = context;
        }
        [HttpGet("type")]
        public ActionResult<List<PokemonTypesView>> GetPokemonType([FromQuery] int? id)
        {
            if (id == null)
            {
                return Ok(_context.PokemonTypesViews.Distinct().ToList());
            }
            var existingTypes = _context.PokemonTypesViews.Where(x => x.Id == id);
            if (existingTypes.Any())
            {
                return Ok(existingTypes);
            }
            return BadRequest("Pokemon ID does not exist");
        }

        [HttpGet("")]
        public ActionResult<List<Pokemon>> GetPokemon([FromQuery] int? id)
        {
            if (id == null)
            {
                return Ok(_context.Pokemons.Include(x => x.Sprite));
            }
            var existingPokemon = _context.Pokemons.Where(x => x.Id == id);
            if (existingPokemon.Any())
            {
                return Ok(existingPokemon.Include(x => x.Sprite).Where(p => p.Id == id));
            }
            return BadRequest("Pokemon ID does not exist");
        }

        [HttpGet("")]
    }
}