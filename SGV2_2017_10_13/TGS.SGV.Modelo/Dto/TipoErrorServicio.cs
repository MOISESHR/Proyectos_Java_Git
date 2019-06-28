using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public enum TipoErrorServicio
    {
        [EnumMember]
        ErrorNoManejado = 0,
        [EnumMember]
        ErrorBaseDatos = 1,
        [EnumMember]
        AccesoDenegado = 2,
        [EnumMember]
        ErrorTiempoConexion = 3,
        [EnumMember]
        ErrorNegocio = 4,
        [EnumMember]
        ErrorSeguridad = 5,
        [EnumMember]
        ErrorConexionWs = 6,
        [EnumMember]
        ErrorProtocoloWs = 7
    }
}
