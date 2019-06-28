using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Base
{
    [DataContract]
    public class Resultado<T>
    {
        [DataMember]
        public TipoResultado Tipo { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public IEnumerable<string> Errores { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public T Data { get; set; }

        public Resultado(T data)
        {
            Data = data;
            Tipo = TipoResultado.Ok;
            Errores = new List<string>();
        }

        public Resultado(TipoResultado tipo, IEnumerable<string> errors)
        {
            Tipo = tipo;
            Errores = errors;
        }

        public Resultado(TipoResultado tipo, string error)
        {
            Tipo = tipo;
            Errores = new List<string> { error };
        }
        public Resultado(TipoResultado tipo, int id)
        {
            Tipo = tipo;
            Id = id;
        }
        public Resultado(TipoResultado tipo, string error, string mensaje)
        {
            Tipo = tipo;
            Errores = new List<string> { error };
            Mensaje = mensaje;
        }

    }
}
