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
    public class ComboFeriadoRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoEmpresa { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }
    }

    [DataContract]
    public class ComboFeriadoResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public List<ComboFeriadoDto> ComboFeriadoDtoLista { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public ComboFeriadoDto ComboFeriadoDto { get; set; }
    }

    [DataContract]
    public class ComboFeriadoDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string FeriadoAnio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FeriadoComentario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FeriadoEstado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FeriadoDiaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FeriadoDiaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ObtieneCombo { get; set; } 

    }

}
