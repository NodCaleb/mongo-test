// See https://aka.ms/new-console-template for more information
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoConsole;
using System.Xml.Linq;

const string _databaseName = "Zoo";
const string _collectionName = "Animals";

Console.WriteLine("Trying Mongo!");

var connectionString = "mongodb://user:password123@localhost:32768";

var client = new MongoClient(connectionString);

var databaseNames = client.ListDatabaseNames().ToList();

foreach (var name in databaseNames)
{
    Console.WriteLine(name);
}

var database = client.GetDatabase(_databaseName);

if (databaseNames.All(n => n != _databaseName))
{
    SeedDatabase(database);
}

ListAnimals(database);

void SeedDatabase(IMongoDatabase database)
{
    var animals = Animal.GenerateRandomAnimals(100);

    var animalsCollection = database.GetCollection<Animal>(_collectionName);

    animalsCollection.InsertMany(animals);
}

void ListAnimals(IMongoDatabase database)
{
    var animalsCollection = database.GetCollection<Animal>(_collectionName);

    Console.WriteLine("Full list of animals:");
    foreach (var animal in animalsCollection.Find(a => true).ToList())
    {
        Console.WriteLine(animal.Description);
    }

    Console.WriteLine();
    Console.WriteLine("List of 10 most old animals:");
    foreach (var animal in animalsCollection.Find(a => true).SortBy(a => a.DateOfBirth).Limit(10).ToList())
    {
        Console.WriteLine(animal.Description);
    }

    Console.WriteLine();
    Console.WriteLine("Rabbits in alphabetical order:");
    foreach (var animal in animalsCollection.Find(a => a.Species == "Rabbit").SortBy(a => a.Name).ToList())
    {
        Console.WriteLine(animal.Description);
    }

    Console.WriteLine();
    Console.WriteLine("Animals, who's name starts with 'M', sorted by species:");
    foreach (var animal in animalsCollection.Find(a => a.Name.StartsWith("M")).SortBy(a => a.Species).ToList())
    {
        Console.WriteLine(animal.Description);
    }
}