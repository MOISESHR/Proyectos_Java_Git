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
    public class EmpresaRequest 
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

    }
    [DataContract]
    public class EmpresaResponse : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public List<EmpresaDto> EmpresaDtoLista { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public EmpresaDto EmpresaDto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class EmpresaDto
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
        public string DiaCorte { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaCorte { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool SinFechaCorte { get; set; }



        [DataMember(EmitDefaultValue = false)]
        public int Contador { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DiaCorte1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string DiaCorte2 { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public string Tabla { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Dato { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Valor { get; set; }


        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
