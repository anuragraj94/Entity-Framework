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
        public ModelContext() : base("Data Source=AETELELINK-PC;Initial Catalog=codeFirst;User ID=sa;Password=sa")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<ModelContext>());
        }
        public DbSet<student> students { get; set; }
        
    }
}
