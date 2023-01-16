using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.Entities;
public class Patron: BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string LoginCode { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public int DemoId { get; private set; }
    public DemoInfo Demo { get; private set; }

    public Patron(string firstName, string lastName, string loginCode, int demoId, string passwordHash, string passwordSalt)
    {
        FirstName = firstName;
        LastName = lastName;
        LoginCode = loginCode;
        DemoId = demoId;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
}
