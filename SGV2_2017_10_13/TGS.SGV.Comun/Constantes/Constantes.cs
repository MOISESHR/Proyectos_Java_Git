using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGS.SGV.Comun.Constantes
{
    public class Constantes
    {
        public struct Generales
        {  
            public const string ValorCero = "0";  //ejemplo
            public const int NumeroCero = 0;
            public const int NumeroUno = 1;
            public const string EsSuccessFactor = "S";
            public const string NoSuccessFactor = "N";
            public const string CodigoTipoEstado = "025";
            public const string CodigoTipos = "026";
            /// <summary>
            /// Variables generales de resultado "N" o "S", sólo aplica para resultados que nunca tendrán variación. 
            /// </summary>
            public const string ValorPositivo = "S";        
            public const string ValorNegativo = "N";
            public const string ParametroCorreoIndividual = "009";
            public const string CodigoCorreo = "006";
        }

        public struct TipoPerfil
        {
            public const string Directivo = "V";
            public const string Usuario = "U";
            public const string MandoResponsable = "R";
            public const string AdministradorDirectivos = "X";
            public const string AdministradorEmpresa = "E";
            public const string AprobadorDirectivos = "D";
        }
        public struct TipoEstadoSolicitud  
        {
            /// <summary>
            /// Tipo de estados de la tabla SGVTC_TABLA_GENERAL parametro codigo '026'
            /// </summary>
            public const string Individual = "I";
            public const string Regularizacion = "E";
            public const string Reprogramacion = "R";
        }
        public struct TipoSolicitud  // Tipo de estados de la tabla SGVTC_TABLA_GENERAL parametro codigo '026'
        {
            public const string Regularización = "E";
            public const string Individual = "I";
            public const string Masiva = "M";
            public const string Reprogramacion = "R";
            public const string Estapecial = "S";
        }
        public struct TipoEmpleado  
        {
            /// <summary>
            /// Tipos de empleado de la tabla m4sco_X_emp_type
            /// </summary>
            public const string Desplazado = "DPZ";
            public const string Directivo = "DIR";
            public const string Empleado = "EMP";
        }
        public struct TipoEstadoCorreo  
        {
            public const string Inactivo = "I";
            public const string Activo = "A";
        }
        public struct TipoPago
        {
            public const string SinPago = "0";
            public const string ConPago = "1";
        }
        public struct TipoDescansoFisico
        {
            public const string NoTomado = "0";
            public const string SiTomado = "1";
        }
        public struct TipoCompraDisfrute
        {
            public const string TipoG = "G";
        }
        public struct ParametrosInternos
        {
            public const string GoceFuturo = "08";
            public const string PerfilGoceFuturo = "09";
            public const string Empresas = "10";
            public const string GrupoTGSVacacionesFes = "19";
            public const string EmpresaFlujoReprogram = "26";
            public const string PeriodoAnteriorReprogram = "34";            
        }

        public struct Parametros
        { 
            public const string PerfilesBloqueados = "158";
            public const string EmpresaSuccessFactor = "156";
            /// <summary>
            /// Constante con el nombre original cstrNumDiasMinimosGoce registro de la tabla SGVTM_PARAMETROS SP SGVPC_SELECT_PARAMETROXOPCION
            /// 
            /// </summary>
            public const string DiasMinimosGoce = "G0004"; 
        }

        public struct RespuestaRolVacacional
        {
            /// <summary>
            /// " SP VALIDAR_ROL - Código respuesta 9 - No tiene ninguna fecha derecho activa"
            /// </summary>
            public const string RolVacacional = "9"; 
        }

        public struct Empresas
        {
            public const string TelefonicaDelPeru = "TDP";
            public const string DiaPago = "25";
            public const int DiaQuince = 15;

            public const string CorteMensual = "01";
            public const string CorteQuicenal = "02";
            public const string SinFechaCorte = "03";

        }
    }
}
