using System;
using System.Collections.Generic;

namespace TGS.SGV.Comun.Validador
{ 
    public interface IEntityValidator
    { 
        bool IsValid<TEntity>(TEntity item)
            where TEntity : class;
        
        IEnumerable<String> ObtenerMensajesError<TEntity>(TEntity item)
            where TEntity : class;
    }
}
