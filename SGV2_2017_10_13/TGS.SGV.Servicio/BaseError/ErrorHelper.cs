using System;
using System.Data.SqlClient;
using System.Net;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Strings;

namespace TGS.SGV.Servicio.Error
{
    internal class ErrorHelper
    {
        public ErrorServicio  MostrarError(Exception error)
        {
            ErrorServicio  ErrExcepcion = new ErrorServicio();

            if (error is SqlException)
            {
                ErrExcepcion.Codigo = Errores.CONBD;
                ErrExcepcion.Mensaje = Errores.CONBD;
                ErrExcepcion.Tipo = TipoErrorServicio.ErrorBaseDatos;
            } 
            else if (error.GetType() == typeof(TimeoutException))
            {
                ErrExcepcion.Codigo = Errores.TIMEOUTWS;
                ErrExcepcion.Mensaje = Errores.TIMEOUTWS;
                ErrExcepcion.Tipo = TipoErrorServicio.ErrorTiempoConexion;
            }        
            else if (error is WebException)
            {
                switch ((error as WebException).Status)
                {                  
                    case WebExceptionStatus.ConnectFailure:
                        ErrExcepcion.Codigo = Errores.CONWS;
                        ErrExcepcion.Mensaje = Errores.CONWS;
                        ErrExcepcion.Tipo = TipoErrorServicio.ErrorConexionWs;
                        break;
                    case WebExceptionStatus.ProtocolError:
                        ErrExcepcion.Codigo = Errores.CONWSPT;
                        ErrExcepcion.Mensaje = Errores.CONWSPT;
                        ErrExcepcion.Tipo = TipoErrorServicio.ErrorProtocoloWs;
                        break;
                    default:
                        ErrExcepcion.Codigo = Errores.NOMANEJADOWS;
                        ErrExcepcion.Mensaje = Errores.NOMANEJADOWS;
                        ErrExcepcion.Tipo = TipoErrorServicio.ErrorNoManejado;
                        break;
                }
            }
            else
            {
                ErrExcepcion.Codigo = Errores.NOMANEJADOWS;
                ErrExcepcion.Mensaje = Errores.NOMANEJADOWS;
                ErrExcepcion.Tipo = TipoErrorServicio.ErrorNoManejado;
            }

            return ErrExcepcion;
        }
    }
}
