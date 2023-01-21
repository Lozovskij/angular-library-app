using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.Entities;
public class Patron : BaseDemoEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CardNumber { get; set; }
    // put password related data into owned type (but what are the benefits?)
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public List<BookInstance> BookInstances { get; set; }
    public Patron(string firstName, string lastName, string cardNumber, int demoId, string passwordHash, string passwordSalt)
    {
        FirstName = firstName;
        LastName = lastName;
        CardNumber = cardNumber;
        DemoId = demoId;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
}
