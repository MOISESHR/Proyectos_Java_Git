using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Comun.Paginacion;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class EvaluacionSolicitudRequest : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipEmpleado { get; set; }

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
        public string EstadoSolicitud { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CIP_Aprobador_1 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Aprobador_1 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Perfil_Aprobador_1 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CIP_Aprobador_2 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Aprobador_2 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Perfil_Aprobador_2 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Tipo { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Adelanto { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public int Dias { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEstado { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string MandoResponsable { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CIPMando { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoTipo { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitud { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string TipConfig { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string TipEmpleado { get; set; } 



        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoCCR { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string NombreCCR { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Hijos { get; set; } 
        
        [DataMember(EmitDefaultValue = false)]
        public string Nombres { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string APaterno { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string AMaterno { get; set; } 
        

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitudInicio { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CompraDisfrute { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitudFin { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoPerfil { get; set; } 



        [DataMember(EmitDefaultValue = false)]
        public string CipUsuario { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaDerecho { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ComentarioSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string RequiereBonos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPago { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoPadre { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DescansoFisico { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Reprogramado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaPago { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoSolicitud { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }

    }

    [DataContract]
    public class EvaluacionSolicitudResponse : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public List<EvaluacionSolicitudDto> ListarEvaluacionSolicitudDto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Contador { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IntervaloFecha { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoError { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipError { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MsgError { get; set; }


        [DataMember]
        public TipoResultado TipoRespuesta { get; set; }

        [DataMember]
        public IEnumerable<string> Errores { get; set; }



        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class EvaluacionSolicitudDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipEmpleado { get; set; }

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
        public string EstadoSolicitud { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CIP_Aprobador_1 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Aprobador_1 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Perfil_Aprobador_1 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CIP_Aprobador_2 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Aprobador_2 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Perfil_Aprobador_2 { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Tipo { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Adelanto { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public int Dias { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEstado { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string MandoResponsable { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CIPMando { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoTipo { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitud { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string TipConfig { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string TipEmpleado { get; set; } 

        

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoCCR { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string NombreCCR { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string Hijos { get; set; } 

        //[DataMember(EmitDefaultValue = false)]
        //public string CIP { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Nombres { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string APaterno { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string AMaterno { get; set; } 

        //[DataMember(EmitDefaultValue = false)]
        //public string CodigoEstado { get; set; } 

        //[DataMember(EmitDefaultValue = false)]
        //public string CodigoTipo { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitudInicio { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitudFin { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public string CodigoPerfil { get; set; } 


        [DataMember(EmitDefaultValue = false)]
        public string CipUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
