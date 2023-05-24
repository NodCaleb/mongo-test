using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConsole
{
    public class Animal
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Description => $"{Name} the {Species}, {Gender}, born {DateOfBirth.ToString("dd.MM.yyyy")}";

        public static List<Animal> GenerateRandomAnimals(int count)
        {
            List<Animal> animals = new List<Animal>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                Animal animal = new Animal
                {
                    Id = Guid.NewGuid(),
                    Species = GenerateRandomSpecies(),
                    Name = GenerateRandomName(),
                    Gender = GenerateRandomGender(),
                    DateOfBirth = GenerateRandomDateOfBirth()
                };

                animals.Add(animal);
            }

            return animals;
        }

        public static string GenerateRandomSpecies()
        {
            string[] species = { "Dog", "Cat", "Bird", "Rabbit", "Fish" };
            Random random = new Random();
            int index = random.Next(species.Length);
            return species[index];
        }

        public static string GenerateRandomName()
        {
            string[] names = { "Bella", "Max", "Charlie", "Luna", "Lucy", "Cooper", "Leo", "Oliver" };
            Random random = new Random();
            int index = random.Next(names.Length);
            return names[index];
        }

        public static string GenerateRandomGender()
        {
            string[] genders = { "Male", "Female" };
            Random random = new Random();
            int index = random.Next(genders.Length);
            return genders[index];
        }

        public static DateTime GenerateRandomDateOfBirth()
        {
            Random random = new Random();
            int year = random.Next(2000, 2020);
            int month = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            return new DateTime(year, month, day);
        }

    }
}
