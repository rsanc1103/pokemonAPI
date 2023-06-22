using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testAPI.Models;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("digimon")]
    public class DigimonController : ControllerBase
    {
        public static List<Digimon> digimonList = new List<Digimon>()
        {
            new Digimon("Agumon", "Agumon is a reptile Digimon with fiery orange fur and large, clawed hands.", 1, "Vaccine"),
            new Digimon("Gabumon", "Gabumon is a reptile Digimon with a fur-covered body and a horn on its head.", 2, "Data"),
            new Digimon("Patamon", "Patamon is a mammal Digimon with large ears and small wings on its back.", 3, "Vaccine"),
            new Digimon("Gatomon", "Gatomon is a feline Digimon with a long tail and large, clawed paws.", 4, "Data"),
            new Digimon("Biyomon", "Biyomon is a bird Digimon with pink feathers and a beak.", 5, "Vaccine"),
            new Digimon("Tentomon", "Tentomon is an insect Digimon with a green exoskeleton and large mandibles.", 6, "Vaccine"),
            new Digimon("Palmon", "Palmon is a plant Digimon with a flower on its head and vine-like arms.", 7, "Data"),
            new Digimon("Gomamon", "Gomamon is a sea lion Digimon with blue fur and a fish tail.", 8, "Vaccine"),
            new Digimon("Patamon", "Patamon is a mammal Digimon with large ears and small wings on its back.", 9, "Vaccine"),
            new Digimon("Veemon", "Veemon is a dragon-like Digimon with blue skin and yellow wings.", 10, "Vaccine"),
            new Digimon("Hawkmon", "Hawkmon is a bird Digimon with green feathers and a beak.", 11, "Vaccine"),
            new Digimon("Armadillomon", "Armadillomon is an armadillo Digimon with a hard shell and large claws.", 12, "Data"),
            new Digimon("Wormmon", "Wormmon is an insect Digimon with green skin and a segmented body.", 13, "Data"),
            new Digimon("Guilmon", "Guilmon is a dinosaur-like Digimon with red skin and large claws.", 14, "Virus"),
            new Digimon("Renamon", "Renamon is a fox Digimon.", 15, "Virus")

        };


        [HttpGet("")]
        public List<Digimon> GetDigimonList()
        {
            return digimonList;
        }

        [HttpPost("addDigimon")]
        public ActionResult<Digimon> AddDigimon([FromBody] Digimon newDigimon)
        {
            Digimon digimon = digimonList.FirstOrDefault(d => d.Id == newDigimon.Id);
            if (digimon == null)
            {

            }
            else
            {
                digimon.Name = newDigimon.Name;
                digimon.Description = newDigimon.Description;
                digimon.Type = newDigimon.Type;
            }
            return Ok(newDigimon);
        }

        [HttpPost("deleteDigimon/{id}")]
        public ActionResult<List<Digimon>> DeleteDigimon(int id)
        {
            Digimon digimonFound = digimonList.FirstOrDefault(d => d.Id == id);
            if (digimonFound != null)
            {
                digimonList.Remove(digimonFound);
                return Ok(digimonList);
            }
            else
            {
                return BadRequest("Digimon not found");
            }
        }

        [HttpGet("{name}")]
        public Digimon GetDigimonByName(string name)
        {
            Digimon digimon = digimonList.First(d => d.Name.ToLower() == name.ToLower());
            return digimon;
        }

        [HttpGet("type/{type}")]
        public List<Digimon> GetDigimonByType(string type)
        {
            return digimonList.FindAll(Digimon => Digimon.Type.ToLower() == type.ToLower());
        }

        [HttpGet("type")]
        public List<string> GetTypes()
        {
            return digimonList.Select(digimon => digimon.Type).Distinct().ToList();
        }
    }
}