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
    public class GoceVacacionalRequest  
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaDerecho{ get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaRol { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Periodo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Reprogramado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }
        

    }

    [DataContract]
    public class GoceVacacionalResponse : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public List<GoceVacacionalDto> GoceVacacionalDtoLista { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public GoceVacacionalDto GoceVacacionalDto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class GoceVacacionalDto 
    {
        [DataMember(EmitDefaultValue = false)]
        public string TipoGoce { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Situacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Dias { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Comentario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double CodigoPadre { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GeneraPago { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DescansoFisico { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GoceReprogramado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Activo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string BuyEnjoyMent { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Reprogramming { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasPagados { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MesInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MesFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PeriodoDetestado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string UsuarioCreacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaCreacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AdelantoVacaciones { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FlagPartidaVacaciones { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaSolicitud { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
