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
    public class PerfilUsuarioRequest : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public string CipUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GestorNotificacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ResponsableNotificacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
    [DataContract]
    public class PerfilUsuarioResponse : IPaginacion
    {
        [DataMember(EmitDefaultValue = false)]
        public List<PerfilUsuarioDto> PerfilUsuarioDtoLista { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Indice { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Tamanio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }

    [DataContract]
    public class PerfilUsuarioDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CipUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string NombreUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoPerfil { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string GestorNotificacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ResponsableNotificacion { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
