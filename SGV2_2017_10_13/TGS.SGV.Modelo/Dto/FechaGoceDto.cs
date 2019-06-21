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
    public class FechaGoceRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }
    }

    [DataContract]
    public class FechaGoceResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public FechaGoceDto FechaGoceDto { get; set; }
    }

    [DataContract]
    public class FechaGoceDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string FechaFinalCalendario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechadeDerecho { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorDiaVencido { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaDerechoVencido { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public string FechaDerechoPendiente { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasPlanificadoPendiente { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasPlanificadoVencido { get; set; }
    }
}
