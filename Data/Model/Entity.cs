using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCrud.Data.Model
{
    public abstract class Entity : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public virtual void NewId() {
            Id = Guid.NewGuid();
        }
    }
}
