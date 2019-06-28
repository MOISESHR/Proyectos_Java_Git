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
    public class TrabajadorDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoTrabajador { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreTrabajador { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Unidad { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipConfig { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreLider { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUnidad { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoJefe { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoPuesto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoCategoria { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoUbicacionFisica { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoOficinaAdm { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoTipoPago { get; set; }
    }

    [DataContract]
    public class TrabajadorRequest  
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Nombre { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CipMando { get; set; }
    }

    [DataContract]
    public class TrabajadorResponse  
    {
        [DataMember(EmitDefaultValue = false)]
        public TrabajadorDto TrabajadorDto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<TrabajadorDto> TrabajadorDtoLista { get; set; }
    }

}
