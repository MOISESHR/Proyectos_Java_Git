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
    public class SolicitudParametroRequest 
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoUsuario { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FechaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasFest { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int CalculoDias { get; set; }
    }

    [DataContract]
    public class SolicitudParametroResponse
    {

        [DataMember(EmitDefaultValue = false)]
        public SolicitudParametroDto SolicitudParametroDto { get; set; }

    }

    [DataContract]
    public class SolicitudParametroDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CodigoTipoEmpleado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double CodigoPeriodo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoSalida { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoResponsable { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string CodigoCip { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ComboInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ComboFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasFindeSemana { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasFeriado { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double DiasDisponibleCip { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime ComboFechaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaCruzadaInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaCruzadaFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime FechaFinalFestivo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ValorDiasFestivo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ValorComboInicio { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ValorComboFinal { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorContinuoCombo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValorSigueCombo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ValorGoceFuturo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ValidaRol { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TipoCruce { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double CruceGoce { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ContadorLicencia { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string IntervaloFecha { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double ContenidoIntervalo { get; set; }
    }
}
