using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.AccesoDatos
{
    public class SGVContext:  DbContext
    {
        public SGVContext()
            : base("Name=OracleDbContext")
        {  
            Database.SetInitializer<SGVContext>(null);
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }
       
    }
}
