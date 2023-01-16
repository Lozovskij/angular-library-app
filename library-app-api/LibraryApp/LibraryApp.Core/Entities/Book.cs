using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.Entities;
public class Book : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int YearOfPublication { get; set; }
    public string Description { get; set; }
    public ICollection<Author> Author { get; set; }
}
