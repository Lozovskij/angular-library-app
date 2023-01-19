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
    public string LoginCode { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }

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
