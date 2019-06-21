using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using TGS.SGV.Strings;
using TGS.SGV.Comun.Funciones;

namespace TGS.SGV.Comun.Funciones
{
    public class Funciones
    {
        public static string FormatoFecha(DateTime fecha)
        {
            CultureInfo Culture = CultureInfo.CreateSpecificCulture("es-PE");
            return String.Format(Culture, "{0:dd/MM/yyyy}", fecha);
        }

        public static string FormatoFechaHora(DateTime fecha)
        {
            CultureInfo Culture = CultureInfo.CreateSpecificCulture("es-PE");
            return String.Format(Culture, "{0:dd/MM/yyyy HH:mm}", fecha);
        }

        public static string FormatoHora(DateTime fecha)
        {
            CultureInfo Culture = CultureInfo.CreateSpecificCulture("es-PE");
            return String.Format(Culture, "{0:T}", fecha);
        }
        public static string FormatoFechaMesAnio(DateTime fecha)
        {
            CultureInfo Culture = CultureInfo.CreateSpecificCulture("es-PE");
            return String.Format(Culture, "{0:/MM/yyyy}", fecha);
        }
        public static string FormatoFechaAnioMesDia(DateTime fecha)
        {
            CultureInfo Culture = CultureInfo.CreateSpecificCulture("es-PE");
            return String.Format(Culture, "{0:yyyyMMdd}", fecha);
        }
        public static string RestarFechas(DateTime fecha_inicio, DateTime fecha_final)
        {
            var vrestar_fechas = "";
            CultureInfo Culture = CultureInfo.CreateSpecificCulture("es-PE");
            var vfechainicio = String.Format(Culture, "{0:dd/MM/yyyy}", fecha_inicio);            
            var vfechafinal = String.Format(Culture, "{0:dd/MM/yyyy}", fecha_final);            
            var v1 = 0;
            if ((Convert.ToString(fecha_inicio) == vfechainicio) & (Convert.ToString(fecha_final) == vfechafinal))
            {
                v1 = (Convert.ToDateTime(vfechafinal) - Convert.ToDateTime(vfechainicio)).Days;
                v1 = v1 + 1;
            }
            else
            {
                v1 = (fecha_final- fecha_inicio).Days;
                v1 = v1 + 1;
            };
            vrestar_fechas = Convert.ToString(v1);
            return vrestar_fechas;
        }

        public static string SumarFechas(string Fecha, double NumeroDias)
        {
            CultureInfo Culture = CultureInfo.CreateSpecificCulture("es-PE");            
            var FechaOrigen = Convert.ToDateTime(Fecha);
            var vsumafecha = Convert.ToString(FechaOrigen.AddDays(Convert.ToDouble(NumeroDias)));
            vsumafecha = Convert.ToDateTime(vsumafecha).ToShortDateString();
            vsumafecha = String.Format(Culture, "{0:dd/MM/yyyy}", vsumafecha);
            return vsumafecha;
        }
                
        public static bool ComparaFechas(string FechaInicio, string FechaFinal)
        {
            var vInicio = Convert.ToDateTime(FechaInicio).ToShortDateString();
            var vFinal = Convert.ToDateTime(FechaFinal).ToShortDateString();            
            var vComparaFechas = false;
            if (Convert.ToDateTime(vInicio) < Convert.ToDateTime(vFinal))
            {
                vComparaFechas = true;
            }
            return vComparaFechas;
        }


        public static bool ComparaTresFechas(string FechaMaximoDias, string FechaInicio, string FechaFinal)
        {
            var vRespuesta = false;
            var vFechaMaximoDias = Convert.ToDateTime(FechaMaximoDias).ToShortDateString();
            var vFechaInicio = Convert.ToDateTime(FechaInicio).ToShortDateString();
            var vFechaFin = Convert.ToDateTime(FechaFinal).ToShortDateString();
            if (Convert.ToDateTime(vFechaMaximoDias) < Convert.ToDateTime(vFechaInicio) || Convert.ToDateTime(vFechaMaximoDias) < Convert.ToDateTime(vFechaFin))
            {
                vRespuesta = true;
            }
            return vRespuesta;
        }

        public static string EnviarCorreo(DatosCorreo datosEnvio)
        {
            string respuesta = General.RespuestaOk;

            try
            {
                MailMessage Mensaje = new MailMessage();

                Mensaje.From = new MailAddress(datosEnvio.CorreoEnvia);

                foreach (string Correo in datosEnvio.Destinatarios.Split(','))
                {
                    Mensaje.To.Add(new MailAddress(Correo));
                }
                foreach (string Correo in datosEnvio.ConCopia.Split(','))
                {
                    Mensaje.CC.Add(new MailAddress(Correo));
                }
                Mensaje.Subject = datosEnvio.Asunto;
                Mensaje.Body = datosEnvio.Cuerpo;
                Mensaje.IsBodyHtml = true;

                SmtpClient SMTP = new SmtpClient(ConfigurationManager.AppSettings["Smtp_server"], Convert.ToInt32(ConfigurationManager.AppSettings["Smtp_port"]));
                SMTP.EnableSsl = false;
                System.Threading.Thread.Sleep(1000);
                SMTP.Send(Mensaje);
                SMTP.Dispose();
            }
            catch (Exception)
            {
                respuesta = General.Fallo;
            }

            return respuesta;
        }
    }
}
