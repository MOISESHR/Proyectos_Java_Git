using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.Modelo.Modelo
{
    [DataContract]
    public class Solicitud : Auditoria, IValidatableObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CalculoDias { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string EstadoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEstado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoAprobador { get; set; }
        
        [DataMember(EmitDefaultValue = false)] 
        public string FlagAdelantoVacaciones { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string TipoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
    }
}
