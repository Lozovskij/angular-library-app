﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.Entities;
public class DemoInfo : BaseEntity
{
    public Patron Patron { get; set; }
    public List<Book> Books { get; set; }
    public List<Author> Authors { get; set; }
}
