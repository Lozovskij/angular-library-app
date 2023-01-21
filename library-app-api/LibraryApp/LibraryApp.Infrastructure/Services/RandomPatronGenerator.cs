using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Infrastructure.Data;
using RandomNameGeneratorLibrary;
using System;

namespace LibraryApp.Infrastructure.Services;
public class RandomPatronGenerator : IRandomPatronGenerator
{
    private readonly LibraryAppContext _context;

    public RandomPatronGenerator(LibraryAppContext context)
    {
        _context = context;
    }

    public Task<Patron> GenerateRandomPatronAsync()
    {
        //I do not need salt and hash, maybe in the future
        var passwordHashStr = "";
        var passwordSaltStr = "";
        string uniqueCode = GetUniqueCode();

        var personGenerator = new PersonNameGenerator();
        var firstName = personGenerator.GenerateRandomFirstName();
        var lastName = personGenerator.GenerateRandomLastName();

        var demoPatron = new Patron(firstName, lastName, uniqueCode, 1, passwordHashStr, passwordSaltStr);
        return Task.FromResult(demoPatron);
    }

    private string GetUniqueCode()
    {
        var minNum = 100;
        var maxNum = 999;
        var letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
        var r = new Random();
        var res = getRandomCode();
        var repeat = 10;
        while (_context.Patrons.Any(p => p.CardNumber == res))
        {
            res = getRandomCode();
            repeat--;
            if (repeat < 0) { throw new Exception("can't create unique code"); }
        }
        return res;

        string getRandomCode() => r.Next(minNum, maxNum).ToString() + letters[r.Next(0, letters.Length - 1)];
    }
}
