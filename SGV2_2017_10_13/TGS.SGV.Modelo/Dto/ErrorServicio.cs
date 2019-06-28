using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Dto
{
    [DataContract]
    public class ErrorServicio
    {
        [DataMember]
        public TipoErrorServicio Tipo { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public string Codigo { get; set; }

        [DataMember]
        public string CodigoReglaNegocio { get; set; }

        public ErrorServicio()
        {
            Fecha = DateTime.Now;
        }

        public ErrorServicio(string codigo, string mensaje)
        {
            this.CodigoReglaNegocio = codigo;
            this.Mensaje = mensaje;
        }
    }
}
