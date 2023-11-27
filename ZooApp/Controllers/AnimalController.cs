using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ZooApp.Data;
using ZooApp.Models;

namespace ZooApp.Controllers
{
    public class AnimalController
    {
        private readonly ZooContext _context;

        public AnimalController(ZooContext context)
        {
            _context = context;
        }

        public List<Animal> GetAnimalList()
        {
            return _context.Animals.Select(animal => new Animal
            {
                Name = animal.Name,
                Species = animal.Species,
                Habitat = animal.Habitat,
                Description = animal.Description
            }).ToList();
        }
        public void AddAnimal(string name, string species, string habitat, string description)
        {
            var newAnimal = new Animal
            {
                Name = name,
                Species = species,
                Habitat = habitat,
                Description = description
            };

            _context.Animals.Add(newAnimal);
            _context.SaveChanges();
        }
        public void RemoveAnimal(string name)
        {
            var animalToRemove = _context.Animals.FirstOrDefault(animal => animal.Name == name);

            if (animalToRemove != null)
            {
                _context.Animals.Remove(animalToRemove);
                _context.SaveChanges();
            }
        }
    }
}