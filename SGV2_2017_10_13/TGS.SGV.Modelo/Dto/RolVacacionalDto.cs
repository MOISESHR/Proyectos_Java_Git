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
    public class RolVacacionalRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string CipEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Periodo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }

    }
    [DataContract]
    public class RolVacacionalResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public List<RolVacacionalDto> RolVacacionalDtoLista { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public RolVacacionalDto RolVacacionalDto { get; set; }

    }

    [DataContract]
    public class RolVacacionalDto
    {
        [DataMember(EmitDefaultValue = false)]
        public int DiasDisponibles { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DiasDisponiblesPago { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaDerecho { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public int NumeroMaximoGoce { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EmpresaEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ValorGoceFuturo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DiasDisponiblesHab { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int DiasDisponiblesFut { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public int CodigoPeriodo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CodigoError { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
