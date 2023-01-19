using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.Entities;
public class Book : BaseDemoEntity
{
    public string Title { get; set; }
    public int YearOfPublication { get; set; }
    public string Description { get; set; }
    public ICollection<Author> Authors { get; set; }
}
