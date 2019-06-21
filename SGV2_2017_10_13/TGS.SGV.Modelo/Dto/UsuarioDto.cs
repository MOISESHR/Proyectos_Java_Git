using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Comun.Paginacion;

namespace TGS.SGV.Modelo.Dto
{

    [DataContract]
    public class UsuarioRequest  
    {
        [DataMember(EmitDefaultValue = false)]
        public string Cip { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }
    }


    [DataContract]
    public class UsuarioDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string Login { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Password { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public List<PerfilDto> PerfilDtoLista { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public string Nombres { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NuevoPassword { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoEmpleado { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public string Ambito { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PerfilAdicional { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoEmpresa { get; set; }         

        [DataMember(EmitDefaultValue = false)]
        public int? CantidadPerfil { get; set; }
    }
}
