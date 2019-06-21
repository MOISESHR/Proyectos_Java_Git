using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class ParametroRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string Codigo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Empresa { get; set; }

        /// <summary>
        /// parametro entrada del SP PARAMETRO_POR_OPCION invoca la tabla SGVTM_PARAMETROS
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string CodigoOpcion { get; set; }

    }

    [DataContract]
    public class ParametroResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public ParametroDto ParametroDto { get; set; }
    }

    [DataContract]
    public class ParametroDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string ValorParametro { get; set; }

        /// <summary>
        /// parametros del SP PARAMETRO_POR_OPCION invoca la tabla SGVTM_PARAMETROS
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string Codigo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Evento { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Vacaciones { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double TipoPago { get; set; }
        
    }
}
