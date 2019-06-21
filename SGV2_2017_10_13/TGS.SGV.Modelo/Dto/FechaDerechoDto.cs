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
    public class FechaDerechoDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string MaximaFechaDerecho { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaDerechoOrdenar { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaDerecho { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasComprados { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasPendientes { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public double Reprogramacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string VacacionesPerdidas { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Periodo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasGozados { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasPagados { get; set; }

    }

    [DataContract]
    public class FechaDerechoResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public List<FechaDerechoDto> FechaDerechoDtoLista { get; set; }
    }

    [DataContract]
    public class FechaDerechoRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }        
    }
}
