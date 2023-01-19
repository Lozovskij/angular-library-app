using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.Entities;

public abstract class BaseEntity
{
    public virtual int Id { get; set; }
}

public abstract class BaseDemoEntity : BaseEntity
{
    public virtual DemoInfo Demo { get; set; }
    public virtual int DemoId { get; set; }
}
