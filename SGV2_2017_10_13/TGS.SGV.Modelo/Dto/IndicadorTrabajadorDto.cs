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
    public class IndicadorTrabajadorRequest
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Fecha_Inicio { get; set; }
    }

    [DataContract]
    public class IndicadorTrabajadorResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public IndicadorTrabajadorDto IndicadorTrabajadorDto { get; set; }
    }

    [DataContract]
    public class IndicadorTrabajadorDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Pendientes { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Truncos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double Vencidos { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasPendienteAprobacion { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasPendienteGoce { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double GoceFuturo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinalFuturo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasDisponible { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorAdelanto { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorMaximoUtil { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorMaximoCale { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MinimoDias { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MaximoDias { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string MaximoDiasReprogramado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorParametroInterno { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoConfiguracion { get; set; }

    }
}
