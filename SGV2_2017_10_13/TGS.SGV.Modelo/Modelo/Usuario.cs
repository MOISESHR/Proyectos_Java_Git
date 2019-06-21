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
    public class Usuario: Auditoria , IValidatableObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Nombre { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ApellidoPaterno { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ApellidoMaterno { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaNacimiento { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
    }
}
