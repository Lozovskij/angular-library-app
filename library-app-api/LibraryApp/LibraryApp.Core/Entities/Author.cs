﻿namespace LibraryApp.Core.Entities;
public class Author : BaseDemoEntity
{
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}
