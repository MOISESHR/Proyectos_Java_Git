using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Servicio.Contrato
{
    [ServiceContract]
    public interface IParametroServicio
    {
        [OperationContract]
        ParametroResponse ObtenerParametroInterno(ParametroRequest parametroRequest);

        [OperationContract]
        ParametroResponse ObtenerParametro(ParametroRequest parametroRequest);

        [OperationContract]
        ParametroAccesoResponse ObtenerParametroAcceso(UsuarioRequest usuarioRequest);

        [OperationContract]
        IndicadorTrabajadorResponse ObtenerIndicadoresFuturo(string codigoUsuario, string fechaInicio);

        [OperationContract]
        FechaGoceResponse ObtenerGoceFuturo(string CodigoUsuario);

        [OperationContract]
        IndicadorTrabajadorResponse ObtenerIndicadores(string codigoUsuario);
    }
}
