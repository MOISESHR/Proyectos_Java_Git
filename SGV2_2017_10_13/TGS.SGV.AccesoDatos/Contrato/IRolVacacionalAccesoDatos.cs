using System;
using System.Collections.Generic;
using System.Linq;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.AccesoDatos.Contrato
{
    public interface IRolVacacionalAccesoDatos : IDisposable
    {
        RolVacacionalResponse ObtenerFechaDerecho(RolVacacionalRequest parametro);
        RolVacacionalResponse ObtenerFechaDerechoRegulariza(string cipEmpleado);

        RolVacacionalResponse ObtenerDiasDisponiblesPagos(RolVacacionalRequest parametro);
        int ValidarModificarFechaPago(RolVacacionalRequest parametro);

        FechaDerechoResponse ListarRolesFechaDerecho(FechaDerechoRequest fechaDerechoRequest);
        FechaDerechoResponse ListarRolesFechaDerechoVacio(FechaDerechoRequest fechaDerechoRequest);
        SolicitudParametroResponse ValidarRolVacacional(SolicitudParametroRequest solicitudParametroRequest);
        SolicitudParametroResponse ValidarCruceGoceVacacional(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarRolActivoVacacional(SolicitudParametroRequest solicitudParametroRequest);
    }
}
