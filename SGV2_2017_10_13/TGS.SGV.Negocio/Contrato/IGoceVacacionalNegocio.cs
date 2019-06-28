using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Base;


namespace TGS.SGV.Negocio.Contrato
{
    public interface IGoceVacacionalNegocio: IDisposable    
    {
        GoceVacacionalResponse ListarObtenerGoce(GoceVacacionalRequest goceVacacionalRequest);     
    }
}
