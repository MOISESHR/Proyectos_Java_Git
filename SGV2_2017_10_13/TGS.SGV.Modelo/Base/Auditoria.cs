using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Modelo.Base
{
    [DataContract]
    public abstract class Auditoria : IAuditoria
    {
        [DataMember]
        public DateTime FechaRegistro { get; set; }

        [DataMember]
        public DateTime? FechaModifica { get; set; }

        [DataMember]
        public int UsuarioRegistro { get; set; }

        [DataMember]
        public int? UsuarioModifica { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}
