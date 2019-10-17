using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWithCodeFirst.Model
{
    public class ModelContext:DbContext
    {
        public ModelContext() : base()
        {

        }
        public DbSet<student> students { get; set; }
    }
}
