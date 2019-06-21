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
    public class PeriodoRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string CidEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Periodo { get; set; }
        
        
    }
    [DataContract]
    public class PeriodoResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public PeriodoDto PeriodoDto { get; set; }
        public List<PeriodoDto> PeriodoDtoLista { get; set; }
        
    }

    [DataContract]
    public class PeriodoDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CipEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Periodo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
