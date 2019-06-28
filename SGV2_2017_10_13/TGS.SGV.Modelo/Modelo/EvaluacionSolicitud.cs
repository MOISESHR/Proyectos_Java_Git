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
    public class EvaluacionSolicitud : Auditoria, IValidatableObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CIP { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Empleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Unidad { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Gerencia { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GerenciaCentral { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFin { get; set; }
        
        [DataMember(EmitDefaultValue = false)] 
        public string EstadoSolicitud { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CIP_Aprobador_1 { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string Aprobador_1 { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string Perfil_Aprobador_1 { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CIP_Aprobador_2 { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string Aprobador_2 { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string Perfil_Aprobador_2 { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string Tipo { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string Adelanto { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public int Dias { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEstado { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string MandoResponsable { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CIPMando { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CodigoTipo { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitud { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string TipConfig { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string TipEmpleado { get; set; } //char

        
        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CodigoCCR { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string NombreCCR { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string Hijos { get; set; } //char

        //[DataMember(EmitDefaultValue = false)]
        //public string CIP { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Nombres { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string APaterno { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string AMaterno { get; set; } //char

        //[DataMember(EmitDefaultValue = false)]
        //public string CodigoEstado { get; set; } //char

        //[DataMember(EmitDefaultValue = false)]
        //public string CodigoTipo { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitudInicio { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitudFin { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; } //char

        [DataMember(EmitDefaultValue = false)]
        public string CodigoPerfil { get; set; } //char


        [DataMember(EmitDefaultValue = false)]
        public string CIPUsuario { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string CodigoError { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipError { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MsgError { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
    }
}
