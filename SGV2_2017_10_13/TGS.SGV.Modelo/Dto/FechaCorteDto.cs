using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Comun.Paginacion;
using TGS.SGV.Modelo.Modelo;


namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class FechaCorteRequest 
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }
    }

    [DataContract]
    public class FechaCorteResponse 
    {
        [DataMember(EmitDefaultValue = false)]
        public List<FechaCorteDto> FechaCorteDtoLista { get; set; }
    }

        [DataContract]
    public class FechaCorteDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoTabla { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoDato { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorFecha { get; set; }
    }
}
