using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Strings;

namespace TGS.SGV.Negocio.Implementacion
{
    public class SolicitudNegocio: ISolicitudNegocio
    {
        private readonly ISolicitudAccesoDatos _ISolicitudesAccesoDatos;        
        private readonly IPerfilUsuarioNegocio _IPerfilUsuarioNegocio;
        private readonly IPlantillaNegocio _IPlantillaNegocio;
        private readonly IRolVacacionalNegocio _IRolVacacionalNegocio;        
        private readonly IPeriodoNegocio _IPeriodoNegocio;
        private readonly ITrabajadorNegocio _ITrabajadorNegocio;
        private readonly IEmpresaNegocio _IEmpresaNegocio;
        private readonly ICorreoNegocio _ICorreoNegocio;        
        private readonly IParametroNegocio _IParametroNegocio;        

        public SolicitudNegocio(ISolicitudAccesoDatos solicitudesAccesoDatos, 
            IPerfilUsuarioNegocio iPerfilUsuarioNegocio,
            IPlantillaNegocio iPlantillaNegocio,
            IPeriodoNegocio iPeriodoNegocio,
            IEmpresaNegocio iEmpresaNegocio,
            ITrabajadorNegocio iTrabajadorNegocio,
            IRolVacacionalNegocio iRolVacacionalNegocio,
            IParametroNegocio iParametroNegocio,
            ICorreoNegocio iCorreoNegocio            
            )
        {
            _ISolicitudesAccesoDatos = solicitudesAccesoDatos;            
            _IRolVacacionalNegocio = iRolVacacionalNegocio;
            _IParametroNegocio = iParametroNegocio;
            _IPlantillaNegocio = iPlantillaNegocio;
            _ITrabajadorNegocio = iTrabajadorNegocio;
            _ICorreoNegocio = iCorreoNegocio;
            _IPerfilUsuarioNegocio = iPerfilUsuarioNegocio;
            _IPeriodoNegocio = iPeriodoNegocio;
            _IEmpresaNegocio = iEmpresaNegocio;                        
        }

        #region Solicitud

        public SolicitudResponse ListarSolicitud(SolicitudRequest solicitudRequest)
        {
            return _ISolicitudesAccesoDatos.ListarSolicitud(solicitudRequest);
        }

        public ComboFeriadoResponse ListarComboFeriado(ComboFeriadoRequest comboFeriadoRequest)
        {
            return _ISolicitudesAccesoDatos.ListarComboFeriado(comboFeriadoRequest);
        }
        
        public SolicitudResponse ObtenerSolicitudPorCodigo(string codigoSolicitud)
        {
            return _ISolicitudesAccesoDatos.ObtenerSolicitudPorCodigo(codigoSolicitud);
        }

        public Solicitud ActualizaSolicitudTrabajador(string codigoSolicitud, string codigoUsuario)
        {
            return _ISolicitudesAccesoDatos.ActualizaSolicitudTrabajador(codigoSolicitud, codigoUsuario);
        }
                
        public SolicitudResponse ObtenerSolicitudRegistrada(SolicitudRequest solicitudRequest)
        {
            return _ISolicitudesAccesoDatos.ObtenerSolicitudRegistrada(solicitudRequest);
        }

        public Resultado<Solicitud> InsertaSolicitudIndividual(Solicitud solicitud)
        {
            var respuestaEnvioSolicitud = _ISolicitudesAccesoDatos.InsertaSolicitudIndividual(solicitud);
            if (respuestaEnvioSolicitud.Equals(Constantes.Generales.ValorCero))
            {
                return new Resultado<Solicitud>(TipoResultado.Ok, string.Empty);
            }
            else
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, respuestaEnvioSolicitud);
            }
        }

        public Resultado<Solicitud> ValidacionesCombosVacacionales(Solicitud solicitud)
        {
            var resultadoTrabajador = _ITrabajadorNegocio.ObtenerDatosTrabajador(solicitud.CodigoEmpleado);
            
            var oComboFeriadoRequest = new ComboFeriadoRequest()
            {
                TipoEmpresa=resultadoTrabajador.TrabajadorDto.CodigoEmpresa,
                FechaInicio=solicitud.FechaInicio,
                FechaFinal=solicitud.FechaFinal
            }; 

            var resultadoComboFeriado = _ISolicitudesAccesoDatos.ObtenerComboFeriado(oComboFeriadoRequest);

            if (resultadoComboFeriado.ComboFeriadoDto.ObtieneCombo!= Convert.ToDouble(Constantes.Generales.ValorCero))
            {
                IndicadorTrabajadorRequest oValidaIndicadorTrabajador = new IndicadorTrabajadorRequest()
                {
                    CodigoUsuario = solicitud.CodigoEmpleado,
                    Fecha_Inicio = solicitud.FechaInicio,
                };

                var resultadoValidaIndicadorTrabajador = _IParametroNegocio.ValidarIndicadorTrabajador(oValidaIndicadorTrabajador);

                if (resultadoValidaIndicadorTrabajador.Tipo.Equals(TipoResultado.Invalido))
                {
                    return new Resultado<Solicitud>(TipoResultado.Invalido, resultadoValidaIndicadorTrabajador.Errores);
                }
            }
            
            var resultadoValidaFechaGoce = _IParametroNegocio.ValidarFechaGoce(solicitud);

            if (resultadoValidaFechaGoce.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, resultadoValidaFechaGoce.Errores);
            }
            
            var oParametroRequest = new ParametroRequest()
            {
                Empresa = resultadoTrabajador.TrabajadorDto.CodigoEmpresa,
                Codigo = solicitud.TipoPerfil,
                CodigoOpcion = Constantes.Parametros.DiasMinimosGoce,
            };

            var resultadoParametro = _IParametroNegocio.ParametroPorOpcion(oParametroRequest);

            string respuestaVacaciones = string.Empty;

            if (resultadoParametro.ParametroDto.Vacaciones.Equals(Constantes.Generales.ValorPositivo))
            {
                respuestaVacaciones = ReglasNegocio.RespuestaVacaciones;
            }
            else
            {
                respuestaVacaciones = string.Empty;
            }
            
            string vRolesFechaDerecho=string.Empty;

            var oFechaDerecho = new FechaDerechoRequest()
                {
                    CodigoUsuario = solicitud.CodigoEmpleado,
                };
            var resultadoRolesFechaDerecho = _IRolVacacionalNegocio.ObtenerFechaDerecho(oFechaDerecho);

            for (int i = 0; i < resultadoRolesFechaDerecho.FechaDerechoDtoLista.Count; i++)
            {
                if (vRolesFechaDerecho.Equals(string.Empty))
                {
                    if (Convert.ToInt32(resultadoRolesFechaDerecho.FechaDerechoDtoLista[i].DiasPendientes) > 0 & (Convert.ToInt32(resultadoRolesFechaDerecho.FechaDerechoDtoLista[i].VacacionesPerdidas) == 0))
                    {
                        vRolesFechaDerecho = Convert.ToString(resultadoRolesFechaDerecho.FechaDerechoDtoLista[i].FechaDerecho);
                    }
                }
            }
            vRolesFechaDerecho = Convert.ToString((vRolesFechaDerecho.Substring(0, 6)) + Convert.ToString(DateTime.Now.Year));


            var oParametroTipoPago = new ParametroRequest()
            {
                Codigo = solicitud.CodigoEmpleado,
            };

            var resultadoParametroTipoPago = _IParametroNegocio.ParametroTipoPago(oParametroTipoPago);

            if (resultadoTrabajador.TrabajadorDto.CodigoEmpresa.Equals(Constantes.Empresas.TelefonicaDelPeru))
            {
                var resultadoFechaGoce = _IParametroNegocio.ObtenerGoceFuturo(solicitud.CodigoEmpleado);
                var resultadoIndicadorTrabajador = _IParametroNegocio.ObtenerIndicadoresFuturo(solicitud.CodigoEmpleado, solicitud.FechaInicio);
                double vTotalTrunco = (resultadoIndicadorTrabajador.IndicadorTrabajadorDto.Pendientes + resultadoIndicadorTrabajador.IndicadorTrabajadorDto.Truncos + resultadoIndicadorTrabajador.IndicadorTrabajadorDto.Vencidos + resultadoIndicadorTrabajador.IndicadorTrabajadorDto.GoceFuturo - resultadoIndicadorTrabajador.IndicadorTrabajadorDto.DiasPendienteAprobacion - resultadoIndicadorTrabajador.IndicadorTrabajadorDto.DiasPendienteGoce);
                double vTotalTruncoCompensa = (resultadoIndicadorTrabajador.IndicadorTrabajadorDto.Pendientes + 30 + resultadoIndicadorTrabajador.IndicadorTrabajadorDto.Vencidos + resultadoIndicadorTrabajador.IndicadorTrabajadorDto.GoceFuturo - resultadoIndicadorTrabajador.IndicadorTrabajadorDto.DiasPendienteAprobacion - resultadoIndicadorTrabajador.IndicadorTrabajadorDto.DiasPendienteGoce);

                var vDiasCalculados = Funciones.RestarFechas(Convert.ToDateTime(solicitud.FechaInicio), Convert.ToDateTime(solicitud.FechaFinal));
                var vDias = Convert.ToDouble(vDiasCalculados) - Convert.ToDouble(vTotalTruncoCompensa);

                if (Convert.ToDouble(vDiasCalculados) > 0 & Convert.ToDouble(vDiasCalculados) <= Convert.ToDouble(vTotalTruncoCompensa))
                {
                    if (Convert.ToDouble(vDiasCalculados) <= Convert.ToDouble(vTotalTrunco))
                    {
                    }
                    else
                    {
                        if (Funciones.ComparaFechas(solicitud.FechaInicio, vRolesFechaDerecho))
                        {
                            if (Convert.ToDouble(vTotalTrunco) < 0)
                            {
                                vTotalTrunco = 0;
                            }
                            return new Resultado<Solicitud>(TipoResultado.Invalido, string.Format(ReglasNegocio.SolicitarDiasTruncos, vTotalTrunco, vTotalTruncoCompensa, resultadoFechaGoce.FechaGoceDto.FechadeDerecho));
                        }
                    }
                }
                else
                {
                    return new Resultado<Solicitud>(TipoResultado.Invalido, string.Format(ReglasNegocio.ExcenderDiasDisponibles, vTotalTruncoCompensa, solicitud.FechaInicio));
                }
            }            

            return new Resultado<Solicitud>(TipoResultado.Ok, string.Empty);
        }

        public Resultado<Solicitud> RegistraSolicitudTrabajador(Solicitud solicitud)
        {
            var resultadoTrabajador = _ITrabajadorNegocio.ObtenerDatosTrabajador(solicitud.CodigoEmpleado);

            var respuestaValidaComboVacacionales=ValidacionesCombosVacacionales(solicitud);

            if (respuestaValidaComboVacacionales.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, respuestaValidaComboVacacionales.Errores);
            }

            SolicitudParametroRequest solicitudParametroRequest = new SolicitudParametroRequest()
            {
                CodigoUsuario = solicitud.CodigoEmpleado,
                FechaInicio = solicitud.FechaInicio,
                FechaFinal = solicitud.FechaFinal,
                CalculoDias = solicitud.CalculoDias
            };

            var validaFeriadoCombo = _IParametroNegocio.ValidarFeriadoCombo(solicitudParametroRequest);

            if (validaFeriadoCombo.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaFeriadoCombo.Errores);
            }

            var validaComboSigue = _IParametroNegocio.ValidarComboSigue(solicitudParametroRequest);

            if (validaComboSigue.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaComboSigue.Errores);
            }

            var validaDiasDisponible = _IParametroNegocio.ValidarDiasDisponible(solicitudParametroRequest);

            if (validaDiasDisponible.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaDiasDisponible.Errores);
            }

            var validaRol = _IRolVacacionalNegocio.ValidarRol(solicitudParametroRequest);

            if (validaRol.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaRol.Errores);
            }

            var validaDiasMayor = _IParametroNegocio.ValidarDiasMayor(solicitudParametroRequest);

            if (validaDiasMayor.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaDiasMayor.Errores);
            }

            var validaCruceSolicitud = ValidarCruceSolicitud(solicitudParametroRequest);

            if (validaCruceSolicitud.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaCruceSolicitud.Errores);
            }

            var validaCruceGoce = _IRolVacacionalNegocio.ValidarCruceGoce(solicitudParametroRequest);

            if (validaCruceGoce.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaCruceGoce.Errores);
            }

            var validaLicencia = _IParametroNegocio.ValidarLicencia(solicitudParametroRequest);

            if (validaLicencia.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaLicencia.Errores);
            }

            var validaMando = _IParametroNegocio.ValidarMando(solicitudParametroRequest);

            if (validaMando.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaMando.Errores);
            }

            var validaRolActivo = _IRolVacacionalNegocio.ValidarRolActivo(solicitudParametroRequest);

            if (validaRolActivo.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaRolActivo.Errores);
            }

            var osolicitud = new Solicitud
            {
                CodigoEmpleado = solicitud.CodigoEmpleado,
                CalculoDias =solicitud.CalculoDias,
                FechaInicio = solicitud.FechaInicio,
                FechaFinal = solicitud.FechaFinal,                
                FlagAdelantoVacaciones= Constantes.Generales.ValorCero,
                TipoSolicitud = Constantes.TipoEstadoSolicitud.Individual,
                CodigoAprobador = resultadoTrabajador.TrabajadorDto.CodigoJefe,                
            };
           
            var respuestaRegistro = _ISolicitudesAccesoDatos.RegistraSolicitudTrabajador(solicitud);

            if (respuestaRegistro.Equals(Constantes.Generales.ValorCero))
            {

                var oSolicitudRequest = new SolicitudRequest()
                {
                    CodigoEmpleado = solicitud.CodigoEmpleado
                };

                var respuestaSolicitud = ObtenerSolicitudRegistrada(oSolicitudRequest);

                var vCodigoSolicitud = respuestaSolicitud.SolicitudDto.CodigoSolicitud;

                var oEnvioSolicitud = new Solicitud()
                {
                    CodigoSolicitud = vCodigoSolicitud,
                    CodigoEmpleado = solicitud.CodigoEmpleado
                };

                var respuestaEnvioSolicitud = InsertaSolicitudIndividual(oEnvioSolicitud);

                var oCorreoRequest = new CorreoRequest()
                {
                    CorreoEnvio = Constantes.Generales.ParametroCorreoIndividual,
                    CodigoUsuario = solicitud.CodigoEmpleado,
                    Empresa = resultadoTrabajador.TrabajadorDto.CodigoEmpresa,
                    TipoSolicitud=Constantes.TipoEstadoSolicitud.Individual,
                    CodigoMando = resultadoTrabajador.TrabajadorDto.CodigoJefe,
                    Usuario = solicitud.CodigoEmpleado,
                    CodigoFrom=solicitud.CodigoEmpleado,
                    CodigoTo= resultadoTrabajador.TrabajadorDto.CodigoJefe,
                    CodigoCC=string.Empty
                };

                var respuestaObtenerDatoCorreo = _ICorreoNegocio.ObtenerDatoCorreo(oCorreoRequest);

                var vCorreoPara = string.Empty;

                var vCorreoCopia = string.Empty;

                var oParametroInterno = new ParametroRequest()
                {
                    Codigo= Constantes.ParametrosInternos.Empresas,
                    Empresa= resultadoTrabajador.TrabajadorDto.CodigoEmpresa
                };

                var respuestaParametroInterno = _IParametroNegocio.ObtenerParametroInterno(oParametroInterno);

                var vEmpresaEmpleado = resultadoTrabajador.TrabajadorDto.CodigoEmpresa;

                var vEmpresasConfiguradas= respuestaParametroInterno.ParametroDto.ValorParametro;

                int vEncontrado = vEmpresasConfiguradas.IndexOf(vEmpresaEmpleado,0);

                if (resultadoTrabajador.TrabajadorDto.CodigoEmpresa != Constantes.TipoEmpleado.Directivo || resultadoTrabajador.TrabajadorDto.CodigoEmpresa != Constantes.TipoEmpleado.Desplazado)
                {
                    if (vEncontrado >= 0 & (resultadoTrabajador.TrabajadorDto.CodigoJefe == null || resultadoTrabajador.TrabajadorDto.CodigoJefe.Equals(string.Empty)))
                    {
                        vCorreoPara = respuestaObtenerDatoCorreo.CorreoDto.CorreoParaEmpresa;
                    }
                    else
                    {
                        vCorreoPara = respuestaObtenerDatoCorreo.CorreoDto.CorreoParaPersona;
                    }
                }
                else
                {
                    if (vEncontrado >= 0 & (resultadoTrabajador.TrabajadorDto.CodigoJefe == null || resultadoTrabajador.TrabajadorDto.CodigoJefe.Equals(string.Empty)))
                    {
                        vCorreoPara = respuestaObtenerDatoCorreo.CorreoDto.CorreoParaEmpresa;
                    }
                }
                if (oCorreoRequest.CorreoEnvio == Constantes.Generales.ParametroCorreoIndividual)
                {
                    if (respuestaObtenerDatoCorreo.CorreoDto.ContadorAdm > 0)
                    {
                        if (respuestaObtenerDatoCorreo.CorreoDto.CuentaCorreo.Length > 0)
                        {
                            vCorreoCopia = respuestaObtenerDatoCorreo.CorreoDto.CuentaCorreo;
                        }
                    }
                }
                else
                {
                    if (oCorreoRequest.CodigoCC.Length>0)
                    { 
                        vCorreoCopia = respuestaObtenerDatoCorreo.CorreoDto.CorreoCopia;
                    }
                }

                 var datoscorreo = new DatosCorreo
                {
                    Asunto = respuestaObtenerDatoCorreo.CorreoDto.CorreoAsunto,
                    Cuerpo = respuestaObtenerDatoCorreo.CorreoDto.CorreoCuerpo,
                    Destinatarios = vCorreoPara,
                    CorreoEnvia = respuestaObtenerDatoCorreo.CorreoDto.CuentaCorreo,
                    ConCopia = vCorreoCopia                    
                };
                Funciones.EnviarCorreo(datoscorreo);

                return new Resultado<Solicitud>(TipoResultado.Ok, string.Empty);
            }
            else
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, respuestaRegistro);
            }
            
        }
                
        public Resultado<SolicitudParametroRequest> ValidarCruceSolicitud(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoCruceSolicitud = _ISolicitudesAccesoDatos.ValidarCruceFechaSolicitud(solicitudParametroRequest);

            if (resultadoCruceSolicitud.SolicitudParametroDto.CruceGoce > 0)
            {                
                if (resultadoCruceSolicitud.SolicitudParametroDto.CodigoTipoEmpleado == Constantes.TipoEmpleado.Directivo || resultadoCruceSolicitud.SolicitudParametroDto.CodigoTipoEmpleado == Constantes.TipoEmpleado.Desplazado)
                {
                    if (resultadoCruceSolicitud.SolicitudParametroDto.TipoCruce.Equals(Constantes.TipoEstadoSolicitud.Individual))
                    {
                        return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.CruceSolicitudIndividual, resultadoCruceSolicitud.SolicitudParametroDto.CodigoCip)); 
                    }
                    if (resultadoCruceSolicitud.SolicitudParametroDto.TipoCruce.Equals(Constantes.TipoEstadoSolicitud.Regularizacion))
                    {
                        return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.CruceSolicitudRegularizacion, resultadoCruceSolicitud.SolicitudParametroDto.CodigoCip));
                    }
                    if (resultadoCruceSolicitud.SolicitudParametroDto.TipoCruce.Equals(Constantes.TipoEstadoSolicitud.Reprogramacion))
                    {
                        return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.CruceSolicitudReprogramacion, resultadoCruceSolicitud.SolicitudParametroDto.CodigoCip));
                    }
                }

                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.CruceSolicitudRegistrada, resultadoCruceSolicitud.SolicitudParametroDto.CodigoCip));
            }

                return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
               

         

        
        #endregion






        #region Evaluacion de Solicitud

        public SolicitudResponse ObtenerSolicitud(string codigoSolicitud)
        {
            return _ISolicitudesAccesoDatos.ObtenerSolicitud(codigoSolicitud);
        }
        public EvaluacionSolicitudResponse ListarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest)
        {
            return _ISolicitudesAccesoDatos.ListarEvaluacionSolicitud(ESRequest);
        }
        /// <summary>
        /// Listar Evaluacion Solicitud Desplazado
        /// </summary>
        /// <param name="ESRequest"></param>
        /// <returns></returns>
        public EvaluacionSolicitudResponse ListarEvaluacionSolicitudDPZ(EvaluacionSolicitudRequest ESRequest)
        {
            return _ISolicitudesAccesoDatos.ListarEvaluacionSolicitudDPZ(ESRequest);
        }
        
        public EvaluacionSolicitudResponse ProcesoAprobarEvaluacionSolicitudFutura(EvaluacionSolicitudRequest evaluacion)
        {
            EvaluacionSolicitudResponse respuesta = new EvaluacionSolicitudResponse();
            
            var tipoSeleccion = evaluacion.TipoSolicitud;
            if (tipoSeleccion != Constantes.TipoSolicitud.Reprogramacion && tipoSeleccion != Constantes.TipoSolicitud.Regularización && tipoSeleccion != Constantes.TipoSolicitud.Estapecial)
            {
                //APROB   INDIVIDUALES, ESPECIALES
                //Envio Masivo de solicitudes: Individuales y especiales
                var solicitudesIndividuales = string.Empty;
                var solicitudesEspeciales = string.Empty;
                var cipIndividuales = string.Empty;
                var cipEspeciales = string.Empty;

                var datosSolicitud = evaluacion.CodigoSolicitud.Split(',');
                foreach (var item in datosSolicitud)
                {
                    var datos = item.Split('|');
                    var codigoSolicitud = datos[0];
                    var cipEmpleado = datos[1];
                    var tipoSolicitud = datos[2];

                    if (tipoSolicitud.Equals(Constantes.TipoSolicitud.Individual))
                    {
                        if (string.IsNullOrEmpty(solicitudesIndividuales))
                        {
                            solicitudesIndividuales = codigoSolicitud;
                        }
                        else
                        {
                            solicitudesIndividuales += "|" + codigoSolicitud;
                        }

                        if (cipIndividuales == string.Empty)
                        {
                            cipIndividuales = cipEmpleado;
                        }
                        else
                        {
                            cipIndividuales += "|" + cipEmpleado;
                        }
                    }
                    if (tipoSolicitud.Equals(Constantes.TipoSolicitud.Estapecial))
                    {
                        if (string.IsNullOrEmpty(solicitudesEspeciales))
                        {
                            solicitudesEspeciales = codigoSolicitud;
                        }
                        else
                        {
                            solicitudesEspeciales += "|" + codigoSolicitud;
                        }

                        if (string.IsNullOrEmpty(cipEspeciales))
                        {
                            cipEspeciales = cipEmpleado;
                        }
                        else
                        {
                            cipEspeciales += "|" + cipEmpleado;
                        }                            
                    }
                }

                string estadoCorreo = string.Empty;
                string mensaje = string.Empty;

                if (!string.IsNullOrEmpty(solicitudesIndividuales))
                {
                    var aprobacion = AprobarSolicitudes(solicitudesIndividuales, evaluacion.CodigoUsuario, evaluacion.CodigoPerfil);

                    if (aprobacion.TipoRespuesta == TipoResultado.Ok)
                    {
                        var arrayCip = cipIndividuales.Split('|');
                        var arraySol = solicitudesIndividuales.Split('|');

                        var envioMail = true;

                        for (int i = 0; i < arrayCip.Length; i++)
                        {
                            PlantillaRequest plantilla = new PlantillaRequest();
                            plantilla.CodigoCorreo = Constantes.Generales.CodigoCorreo;
                            plantilla.Empleado = arrayCip[i];
                            plantilla.CodigoEmpresa = evaluacion.CodigoEmpresa;
                            plantilla.CodigoSolicitud = arraySol[i];
                            plantilla.TipoSolicitud = tipoSeleccion;
                            plantilla.Mando = string.Empty;
                            plantilla.AdmEmp = string.Empty;
                            plantilla.CodigoUsuario = evaluacion.CodigoUsuario;
                            plantilla.CipFrom = evaluacion.CodigoUsuario;
                            plantilla.CipTo = arrayCip[i];
                            plantilla.CipCC = string.Empty;

                            var resProc = _IPlantillaNegocio.EnvioCorreoAprobacionSolictud(plantilla);

                            if (resProc.Tipo != TipoResultado.Ok)
                            {
                                envioMail = false;
                            }
                            else
                            {
                                estadoCorreo = Constantes.TipoEstadoCorreo.Activo;
                            }
                        }

                        if (estadoCorreo.Equals(Constantes.TipoEstadoCorreo.Activo))
                        {
                            if (envioMail)
                            {
                                mensaje = ReglasNegocio.AprobacionSolicitudesEnvioMail;
                            }
                            else
                            {
                                mensaje = ReglasNegocio.AprobacionSolicitudesEnvioMailIncompleto;
                            }
                        }
                        else
                        {
                            mensaje = ReglasNegocio.AprobacionSolicitudes;
                        }
                    }
                    else
                    {
                        respuesta.Errores = aprobacion.Errores;
                        mensaje = string.Format(ReglasNegocio.AprobacionSolicitudesErrorI, aprobacion.MsgError);
                    }
                }

                if (!string.IsNullOrEmpty(solicitudesEspeciales))
                {                   
                    var rsResult = AprobarSolicitudesRegularizacion(solicitudesEspeciales, evaluacion.CodigoUsuario, evaluacion.CodigoPerfil);
                    if (rsResult.TipoRespuesta == TipoResultado.Ok)
                    {
                        var arrayCip = cipEspeciales.Split('|');
                        var arraySol = solicitudesEspeciales.Split('|');

                        var envioMail = true;

                        for (int i = 0; i < arrayCip.Length; i++)
                        {
                            PlantillaRequest plantilla = new PlantillaRequest();
                            plantilla.CodigoCorreo = Constantes.Generales.CodigoCorreo;
                            plantilla.Empleado = arrayCip[i];
                            plantilla.CodigoEmpresa = evaluacion.CodigoEmpresa;
                            plantilla.CodigoSolicitud = arraySol[i];
                            plantilla.TipoSolicitud = tipoSeleccion;
                            plantilla.Mando = string.Empty;
                            plantilla.AdmEmp = string.Empty;
                            plantilla.CodigoUsuario = evaluacion.CodigoUsuario;
                            plantilla.CipFrom = evaluacion.CodigoUsuario;
                            plantilla.CipTo = arrayCip[i];
                            plantilla.CipCC = string.Empty;

                            var resProc = _IPlantillaNegocio.EnvioCorreoAprobacionSolictud(plantilla);

                            if (resProc.Tipo != TipoResultado.Ok)
                            {
                                envioMail = false;
                            }
                            else
                            {
                                estadoCorreo = Constantes.TipoEstadoCorreo.Activo;
                            }
                        }

                        if (estadoCorreo.Equals(Constantes.TipoEstadoCorreo.Activo))
                        {
                            if (envioMail)
                            {
                                mensaje = ReglasNegocio.AprobacionSolicitudesEnvioMail;
                            }
                            else
                            {
                                mensaje = ReglasNegocio.AprobacionSolicitudesEnvioMailIncompleto;
                            }
                        }
                        else
                        {
                            mensaje = ReglasNegocio.AprobacionSolicitudes;
                        }
                    }
                    else
                    {
                        List<string> listaErrores = rsResult.Errores.ToList();
                        foreach (var errorMsg in listaErrores)
                        {
                            respuesta.Errores.ToList().Add(errorMsg);
                        }
                        mensaje = string.Format(ReglasNegocio.AprobacionSolicitudesErrorS, rsResult.MsgError);
                    }
                }
                respuesta.MsgError = mensaje;
            }
            return respuesta;
        }

        public EvaluacionSolicitudResponse ProcesoModificarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest)
        {
            EvaluacionSolicitudResponse respuesta = new EvaluacionSolicitudResponse();
            // por implementar es para el boton Modificar
            return respuesta;
        }
        public EvaluacionSolicitudResponse ProcesoRechazarEvaluacionSolicitud(EvaluacionSolicitudRequest ESRequest)
        {
            return null; // por implementar es para el boton rechazar
        }
        public EvaluacionSolicitudResponse AprobarSolicitudes(string cadenaCodigosSolicitud, string cipUsuario, string codigoPerfil)
        {
            EvaluacionSolicitudResponse respesta = new EvaluacionSolicitudResponse();
            List<string> listaErrores = new List<string>();
            string[] strCodigSolicitud = cadenaCodigosSolicitud.Split('|');

            foreach (var codigoSolicitud in strCodigSolicitud)
            {
                SolicitudResponse datosSolicitud = ObtenerSolicitud(codigoSolicitud);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    Resultado<Solicitud> validaAprobar = ValidarAprobarSolicitud(datosSolicitud, cipUsuario, codigoPerfil);
                    respesta.TipoRespuesta = validaAprobar.Tipo;
                    if (validaAprobar.Tipo == TipoResultado.Invalido)
                    {
                        listaErrores.Add(validaAprobar.Errores.ToList()[0]);
                    }

                    if (validaAprobar.Tipo == TipoResultado.Ok)
                    {
                        SolicitudRequest oParam = new SolicitudRequest()
                        {
                            CodigoSolicitud = datosSolicitud.SolicitudDto.CodigoSolicitud,
                            EstadoSolicitud = datosSolicitud.SolicitudDto.EstadoSolicitud,
                            CodUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                        };
                        ActualizarAprobado(oParam);
                        ActualizarSolicitudMasiva(oParam);
                    }
                    scope.Complete();
                }
            }
            respesta.Errores = listaErrores;
            return respesta;
        }
        public Resultado<Solicitud> ValidarAprobarSolicitud(SolicitudResponse solicitud, string cipUsuario, string codigoPerfil)
        {
            SolicitudParametroRequest solicitudParametroRequest = new SolicitudParametroRequest()
            {
                CodigoUsuario = solicitud.SolicitudDto.CodigoEmpleado,
                FechaInicio = solicitud.SolicitudDto.FechaInicio,
                FechaFinal = solicitud.SolicitudDto.FechaFinal,
                CalculoDias = solicitud.SolicitudDto.CalculoDias
            };
            var validaFeriado = ValidarFechaFinFeriado(solicitudParametroRequest);
            if (validaFeriado.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaFeriado.Errores);
            }
            var validarCombo = _IParametroNegocio.ValidarComboSigueVacacional(solicitudParametroRequest);
            if (validarCombo.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarCombo.Errores);
            }
            var validarFecCorte = ValidarFechaCorte(solicitud, codigoPerfil);
            if (validarFecCorte.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarFecCorte.Errores);
            }
            var validarDiasDisponibles = ValidarDiasDisponiblesTrabajoPago(solicitud);
            if (validarDiasDisponibles.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarDiasDisponibles.Errores);
            }
            var validarCruceGoce = ValidarCruceGoceProgramado(solicitud);
            if (validarCruceGoce.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarCruceGoce.Errores);
            }
            var validarLicencia = ValidarLicenciaProgramada(solicitud);
            if (validarLicencia.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarLicencia.Errores);
            }

            var validarGeneracionGoce = ValidarGeneracionGoce(solicitud, cipUsuario, codigoPerfil);
            if (validarGeneracionGoce.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarGeneracionGoce.Errores);
            }
            var validarModificarFechaPago = ValidarModificarFechaPago(solicitud);
            if (validarModificarFechaPago.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarModificarFechaPago.Errores);
            }

            return new Resultado<Solicitud>(TipoResultado.Ok, string.Empty);
        }
        public Resultado<SolicitudParametroRequest> ValidarFechaFinFeriado(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultado = _IParametroNegocio.ValidarFeriadoComboParametro(solicitudParametroRequest);
            var valorCombo = Constantes.Generales.ValorNegativo;

            if (resultado.SolicitudParametroDto.ValorComboInicio > 0 && resultado.SolicitudParametroDto.ValorComboFinal > 0)
            {
                valorCombo = Constantes.Generales.ValorPositivo;
            }

            if (resultado.SolicitudParametroDto.ValorDiasFestivo > 0 && valorCombo.Equals(Constantes.Generales.ValorNegativo))
            {
                var diasFestivo = 0;

                if (resultado.SolicitudParametroDto.DiasFindeSemana > resultado.SolicitudParametroDto.DiasFeriado)
                {
                    diasFestivo = Convert.ToInt16(resultado.SolicitudParametroDto.DiasFindeSemana);
                }
                else
                {
                    diasFestivo = Convert.ToInt16(resultado.SolicitudParametroDto.DiasFeriado);
                }
                if (diasFestivo > 0)
                {
                    SolicitudParametroRequest oSolicitudParametroRequest = new SolicitudParametroRequest()
                    {
                        FechaInicio = solicitudParametroRequest.FechaInicio,
                        FechaFinal = solicitudParametroRequest.FechaFinal,
                        DiasFest = Convert.ToDouble(diasFestivo)
                    };
                    var validaFeriado = _IParametroNegocio.ValidarFeriadoFestivo(oSolicitudParametroRequest);

                    if (validaFeriado.SolicitudParametroDto.DiasFindeSemana > validaFeriado.SolicitudParametroDto.DiasFeriado)
                    {
                        diasFestivo = Convert.ToInt16(validaFeriado.SolicitudParametroDto.DiasFindeSemana);
                    }
                    else
                    {
                        diasFestivo = Convert.ToInt16(validaFeriado.SolicitudParametroDto.DiasFeriado);
                    }
                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaFechaFin, validaFeriado.SolicitudParametroDto.CodigoCip));
                }

                SolicitudParametroRequest rSolicitudParametroRequest = new SolicitudParametroRequest()
                {
                    CodigoUsuario = solicitudParametroRequest.CodigoUsuario,
                    FechaInicio = solicitudParametroRequest.FechaInicio,
                    FechaFinal = solicitudParametroRequest.FechaFinal,
                };
                var resultado3 = _IParametroNegocio.ValidarComboContinuadoParametro(rSolicitudParametroRequest);

                if (resultado3.SolicitudParametroDto.ValorContinuoCombo.Equals(Constantes.Generales.ValorPositivo))
                {
                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaFechaInicio, resultado3.SolicitudParametroDto.CodigoCip));
                }
            }
            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        /// <summary>
        /// Validando la fecha de corte para el Mando Responsable (R) o Asistente Administrativo (M)
        /// y que la solicitud sea del tipo Individual (I) o Especial (S), no Reprogramado ni Regularización
        /// </summary>
        /// <param name="solicitud"></param>
        /// <param name="codigoPerfil"></param>
        /// <returns></returns>
        public Resultado<SolicitudParametroRequest> ValidarFechaCorte(SolicitudResponse solicitud, string codigoPerfil)
        {
            if (!codigoPerfil.Equals(Constantes.TipoPerfil.AdministradorEmpresa) && 
                (solicitud.SolicitudDto.TipoSolicitud.Equals(Constantes.TipoSolicitud.Individual) 
                || solicitud.SolicitudDto.TipoSolicitud.Equals(Constantes.TipoSolicitud.Estapecial)))
            {
                EmpresaRequest parametro = new EmpresaRequest() {                    
                    CodigoEmpleado = solicitud.SolicitudDto.CodigoEmpleado,
                    FechaInicio = solicitud.SolicitudDto.FechaInicio,
                };
                var fechaCorte = _IEmpresaNegocio.ObtenerFechaCorte(parametro);

                if (fechaCorte.EmpresaDtoLista.Count > 0)
                {
                    if (DateTime.Today > Convert.ToDateTime(fechaCorte.EmpresaDtoLista[0].FechaCorte))
                    {
                        return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaFechaCorte, solicitud.SolicitudDto.CodigoEmpleado));
                    }
                }
            }
            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        public Resultado<SolicitudParametroRequest> ValidarDiasDisponiblesTrabajoPago(SolicitudResponse solicitud)
        {
            RolVacacionalRequest oParam = new RolVacacionalRequest()
            {
                CipEmpleado = solicitud.SolicitudDto.CodigoEmpleado,
                FechaInicio = solicitud.SolicitudDto.FechaInicio,
                FechaFinal = solicitud.SolicitudDto.FechaFinal
            };
            var datos = _IRolVacacionalNegocio.ObtenerDiasDisponiblesPagos(oParam);
            if (!datos.RolVacacionalDto.NumeroMaximoGoce.Equals(Constantes.Generales.NumeroCero))
            {
                datos.RolVacacionalDto.DiasDisponibles = datos.RolVacacionalDto.NumeroMaximoGoce;
                datos.RolVacacionalDto.DiasDisponiblesPago = datos.RolVacacionalDto.NumeroMaximoGoce;
                if (datos.RolVacacionalDto.EmpresaEmpleado.Equals(Constantes.Empresas.TelefonicaDelPeru))
                {
                    if (datos.RolVacacionalDto.ValorGoceFuturo.Equals(Constantes.Generales.NumeroCero))
                    {
                        datos.RolVacacionalDto.DiasDisponibles = datos.RolVacacionalDto.DiasDisponiblesHab;
                        datos.RolVacacionalDto.DiasDisponiblesPago = datos.RolVacacionalDto.DiasDisponiblesHab;
                    }
                    else
                    {
                        datos.RolVacacionalDto.DiasDisponibles = datos.RolVacacionalDto.DiasDisponiblesFut;
                        datos.RolVacacionalDto.DiasDisponiblesPago = datos.RolVacacionalDto.DiasDisponiblesFut;
                    }
                }
                else
                {
                    datos.RolVacacionalDto.DiasDisponibles = datos.RolVacacionalDto.DiasDisponiblesFut;
                    datos.RolVacacionalDto.DiasDisponiblesPago = datos.RolVacacionalDto.DiasDisponiblesFut;
                }
            }

            if (datos.RolVacacionalDto.DiasDisponibles < solicitud.SolicitudDto.CalculoDias)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaDiasMayorDisponible, solicitud.SolicitudDto.CodigoEmpleado));
            }
            if (datos.RolVacacionalDto.DiasDisponiblesPago < solicitud.SolicitudDto.CalculoDias)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaDiasMayorDisponiblePago, solicitud.SolicitudDto.CodigoEmpleado));
            }
            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public Resultado<SolicitudParametroRequest> ValidarDiasDisponiblesRegularizacion(SolicitudResponse solicitud)
        {
            RolVacacionalRequest oParam = new RolVacacionalRequest()
            {
                CipEmpleado = solicitud.SolicitudDto.CodigoEmpleado,
                FechaInicio = solicitud.SolicitudDto.FechaInicio,
                FechaFinal = solicitud.SolicitudDto.FechaFinal
            };
            var datosDias = _IRolVacacionalNegocio.ObtenerDiasDisponiblesPagos(oParam);

            if (datosDias.RolVacacionalDto.DiasDisponibles < solicitud.SolicitudDto.CalculoDias)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaDiasMayorDisponible, solicitud.SolicitudDto.CodigoEmpleado));
            }
            if (datosDias.RolVacacionalDto.DiasDisponiblesPago < solicitud.SolicitudDto.CalculoDias)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaDiasMayorDisponiblePago, solicitud.SolicitudDto.CodigoEmpleado));
            }
            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public Resultado<SolicitudParametroRequest> ValidarCruceGoceProgramado(SolicitudResponse solicitud)
        {
            SolicitudParametroRequest oParam = new SolicitudParametroRequest()
            {
                CodigoUsuario = solicitud.SolicitudDto.CodigoEmpleado,
                FechaInicio = solicitud.SolicitudDto.FechaInicio,
                FechaFinal = solicitud.SolicitudDto.FechaFinal
            };
            var goceVacacional = _IRolVacacionalNegocio.ValidarCruceGoceVacacional(oParam);

            if (goceVacacional.SolicitudParametroDto.CruceGoce > 0)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaCruceProgramado, solicitud.SolicitudDto.CodigoEmpleado));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        public Resultado<SolicitudParametroRequest> ValidarLicenciaProgramada(SolicitudResponse solicitud)
        {
            SolicitudParametroRequest oParam = new SolicitudParametroRequest()
            {
                CodigoUsuario = solicitud.SolicitudDto.CodigoEmpleado,
                FechaInicio = solicitud.SolicitudDto.FechaInicio,
                FechaFinal = solicitud.SolicitudDto.FechaFinal
            };
            var fechaLicencia = _IParametroNegocio.ValidarCruceFechaLicencia(oParam);

            if (fechaLicencia.SolicitudParametroDto.ContadorLicencia != Constantes.Generales.NumeroCero)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaCruceLicenciaFechas, solicitud.SolicitudDto.CodigoEmpleado));
            }
            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        public Resultado<SolicitudParametroRequest> ValidarGeneracionGoce(SolicitudResponse datoSolicitud, string cipUsuario, string codigoPerfil)
        {
            string tipoPago = string.Empty;
            DateTime? fechaInicioNuevo = null;
            DateTime? fechaFinalNuevo = null;
            int numeroDiasPendientes = Constantes.Generales.NumeroCero;
            var codigoError = Constantes.Generales.NumeroCero;

            var Periodo = _IPeriodoNegocio.ObtenerPeriodo(datoSolicitud.SolicitudDto.CodigoEmpleado);
            RolVacacionalRequest parametro = new RolVacacionalRequest()
            {
                CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                Periodo = Periodo.PeriodoDto.Periodo
            };
            var listaFechaDerecho = _IRolVacacionalNegocio.ObtenerFechaDerecho(parametro);
            var contador = Constantes.Generales.NumeroCero;
            var contadorFD = Constantes.Generales.NumeroCero;
            foreach (var item in listaFechaDerecho.RolVacacionalDtoLista)
            {
                contadorFD++;
                if (contador == Constantes.Generales.NumeroCero)
                {
                    if (datoSolicitud.SolicitudDto.CalculoDias <= item.DiasDisponibles)
                    {
                        if (datoSolicitud.SolicitudDto.CalculoDias > item.DiasDisponiblesPago)
                        {
                            tipoPago = Constantes.TipoPago.SinPago;
                        }
                        else
                        {
                            tipoPago = Constantes.TipoPago.ConPago;
                        }

                        EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                        {
                            CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                            FechaDerecho = item.FechaDerecho,
                            CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                            FechaSolicitudInicio = datoSolicitud.SolicitudDto.FechaInicio,
                            FechaSolicitudFin = datoSolicitud.SolicitudDto.FechaFinal,
                            RequiereBonos = Constantes.Generales.ValorNegativo,
                            ComentarioSolicitud = string.Format(ReglasNegocio.AprobacionComentarioSolicitudConsignada, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal),
                            CipUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                            TipoPago = tipoPago,
                            CodigoPadre = Constantes.Generales.ValorCero,
                            DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                            Reprogramado = Constantes.Generales.ValorCero,
                            Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                            FechaPago = string.Empty,
                            CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                            TipoSolicitud = datoSolicitud.SolicitudDto.TipoSolicitud,
                        };
                        codigoError = CreaGocePrev(oBE);

                        break;
                    }
                    else
                    {
                        fechaFinalNuevo = Convert.ToDateTime(datoSolicitud.SolicitudDto.FechaInicio).AddDays(item.DiasDisponibles - 1);
                        numeroDiasPendientes = datoSolicitud.SolicitudDto.CalculoDias - item.DiasDisponibles;

                        if (item.DiasDisponibles > item.DiasDisponiblesPago)
                        {
                            tipoPago = Constantes.TipoPago.SinPago;
                        }
                        else
                        {
                            tipoPago = Constantes.TipoPago.ConPago;
                        }
                            
                        EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                        {
                            CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                            FechaDerecho = item.FechaDerecho,
                            CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                            FechaSolicitudInicio = datoSolicitud.SolicitudDto.FechaInicio,
                            FechaSolicitudFin = fechaFinalNuevo != null ? Funciones.FormatoFecha(Convert.ToDateTime(fechaFinalNuevo)) : string.Empty,
                            RequiereBonos = Constantes.Generales.ValorNegativo,
                            ComentarioSolicitud = string.Format(ReglasNegocio.AprobacionComentarioSolicitudParte, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal),
                            CipUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                            TipoPago = tipoPago,
                            CodigoPadre = Constantes.Generales.ValorCero,
                            DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                            Reprogramado = Constantes.Generales.ValorCero,
                            Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                            FechaPago = string.Empty,
                            CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                            TipoSolicitud = datoSolicitud.SolicitudDto.TipoSolicitud,
                        };
                        codigoError = CreaGocePrev(oBE);
                        contador++;
                        fechaInicioNuevo = Convert.ToDateTime(fechaFinalNuevo).AddDays(1);

                    }
                }
                else
                {
                    if (numeroDiasPendientes <= item.DiasDisponibles)
                    {
                        if (numeroDiasPendientes > item.DiasDisponiblesPago)
                        {
                            tipoPago = Constantes.TipoPago.SinPago;
                        }
                        else
                        {
                            tipoPago = Constantes.TipoPago.ConPago;
                        }
                        EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                        {
                            CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                            FechaDerecho = item.FechaDerecho,
                            CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                            FechaSolicitudInicio = fechaInicioNuevo != null ? Funciones.FormatoFecha(Convert.ToDateTime(fechaInicioNuevo)) : string.Empty,
                            FechaSolicitudFin = datoSolicitud.SolicitudDto.FechaFinal,
                            RequiereBonos = Constantes.Generales.ValorNegativo,
                            ComentarioSolicitud = string.Format(ReglasNegocio.AprobacionComentarioSolicitudFinal, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal),
                            CipUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                            TipoPago = tipoPago,
                            CodigoPadre = Constantes.Generales.ValorCero,
                            DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                            Reprogramado = Constantes.Generales.ValorCero,
                            Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                            FechaPago = string.Empty,
                            CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                            TipoSolicitud = datoSolicitud.SolicitudDto.TipoSolicitud,
                        };
                        codigoError = CreaGocePrev(oBE);
                        numeroDiasPendientes = Constantes.Generales.NumeroCero;
                        break;
                    }
                    else
                    {
                        if (numeroDiasPendientes > Constantes.Generales.NumeroCero)
                        {
                            numeroDiasPendientes = numeroDiasPendientes - item.DiasDisponibles;
                            fechaFinalNuevo = Convert.ToDateTime(fechaInicioNuevo).AddDays(item.DiasDisponibles - Constantes.Generales.NumeroUno);

                            if (numeroDiasPendientes > item.DiasDisponiblesPago)
                            {
                                tipoPago = Constantes.TipoPago.SinPago;
                            }
                            else
                            {
                                tipoPago = Constantes.TipoPago.ConPago;
                            }

                            EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                            {
                                CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                                FechaDerecho = item.FechaDerecho,
                                CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                                FechaSolicitudInicio = fechaInicioNuevo != null ? Funciones.FormatoFecha(Convert.ToDateTime(fechaInicioNuevo)) : string.Empty,
                                FechaSolicitudFin = fechaFinalNuevo != null ? Funciones.FormatoFecha(Convert.ToDateTime(fechaFinalNuevo)) : string.Empty,
                                RequiereBonos = Constantes.Generales.ValorNegativo,
                                ComentarioSolicitud = string.Format(ReglasNegocio.AprobacionComentarioSolicitudFinal, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal),
                                CipUsuario = cipUsuario,
                                CodigoPerfil = codigoPerfil,
                                TipoPago = tipoPago,
                                CodigoPadre = Constantes.Generales.ValorCero,
                                DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                                Reprogramado = Constantes.Generales.ValorCero,
                                Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                                FechaPago = string.Empty,
                                CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                                TipoSolicitud = datoSolicitud.SolicitudDto.TipoSolicitud,
                            };
                            codigoError = CreaGocePrev(oBE);
                            fechaInicioNuevo = Convert.ToDateTime(fechaFinalNuevo).AddDays(1);

                        }
                    }
                }
            }
            if (codigoError < Constantes.Generales.NumeroCero)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaGeneracionGoce, datoSolicitud.SolicitudDto.CodigoEmpleado));
            }

            if (listaFechaDerecho.RolVacacionalDtoLista.Count == Constantes.Generales.NumeroCero)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaDiasDisponibles, datoSolicitud.SolicitudDto.CodigoEmpleado));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        private string ObtenerFechaPago(string codigoEmpleado)
        {
            string fechaPago = string.Empty;
            var calculoFechaPago = _IEmpresaNegocio.ObtenerFechaPagoCalculo(codigoEmpleado);
            if (calculoFechaPago.EmpresaDto.Contador > Constantes.Generales.NumeroCero)
            {
                if (DateTime.Today.Day > Constantes.Empresas.DiaQuince)
                {
                    fechaPago = Constantes.Empresas.DiaPago + Funciones.FormatoFechaMesAnio(DateTime.Today.AddMonths(1));
                }
                else
                {
                    fechaPago = Constantes.Empresas.DiaPago + Funciones.FormatoFechaMesAnio(DateTime.Today);
                }
            }
            else
            {
                fechaPago = Constantes.Empresas.DiaPago + Funciones.FormatoFechaMesAnio(DateTime.Today);
                if (DateTime.Today.Day > Convert.ToInt32(calculoFechaPago.EmpresaDto.DiaCorte2))
                {
                    fechaPago = Constantes.Empresas.DiaPago + Funciones.FormatoFechaMesAnio(DateTime.Today);
                }
                if (DateTime.Today.Day > Convert.ToInt32(calculoFechaPago.EmpresaDto.DiaCorte1))
                {
                    fechaPago = Constantes.Empresas.DiaPago + Funciones.FormatoFechaMesAnio(DateTime.Today.AddMonths(1));
                }
            }
            return fechaPago;
        }
        public Resultado<SolicitudParametroRequest> ValidarGeneracionGoceRegulariza(SolicitudResponse datoSolicitud, string cipUsuario, string codigoPerfil)
        {
            DateTime? fechaInicioNuevo = null;
            DateTime? fechaFinalNuevo = null;
            int numDiasPendientes = 0;
            var codigoError = Constantes.Generales.NumeroCero;
            string comentarioSolicitud = string.Empty;
            string fechaPago = ObtenerFechaPago(datoSolicitud.SolicitudDto.CodigoEmpleado);

            var listaFechaDerecho = _IRolVacacionalNegocio.ObtenerFechaDerechoRegulariza(datoSolicitud.SolicitudDto.CodigoEmpleado);
            var contador = Constantes.Generales.NumeroCero;
            var contadorFD = Constantes.Generales.NumeroCero;
            foreach (var item in listaFechaDerecho.RolVacacionalDtoLista)
            {
                contadorFD++;
                if (contador == Constantes.Generales.NumeroCero)
                {
                    if (datoSolicitud.SolicitudDto.CalculoDias <= item.DiasDisponibles)
                    {
                        if (datoSolicitud.SolicitudDto.TipoSolicitud == Constantes.TipoSolicitud.Regularización)
                        {
                            comentarioSolicitud = string.Format(ReglasNegocio.AprobacionRegularizacionFechasConsignadas, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal);
                        }
                        else
                        {
                            comentarioSolicitud = string.Format(ReglasNegocio.AprobacionRegularizacionIndividualFueraFecha, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal);
                        }
                        EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                        {
                            CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                            FechaDerecho = item.FechaDerecho,
                            CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                            FechaSolicitudInicio = datoSolicitud.SolicitudDto.FechaInicio,
                            FechaSolicitudFin = datoSolicitud.SolicitudDto.FechaFinal,
                            RequiereBonos = Constantes.Generales.ValorNegativo,
                            ComentarioSolicitud = comentarioSolicitud,
                            CipUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                            TipoPago = Constantes.TipoPago.ConPago,
                            CodigoPadre = Constantes.Generales.ValorCero,
                            DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                            Reprogramado = Constantes.Generales.ValorCero,
                            Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                            FechaPago = fechaPago,
                            CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                            TipoSolicitud = Constantes.TipoSolicitud.Regularización
                        };
                        codigoError = CreaGocePrev(oBE);
                        if (codigoError < Constantes.Generales.NumeroCero)
                        {
                            return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionRegularizacionNoAprobado, datoSolicitud.SolicitudDto.CodigoEmpleado));
                        }
                        
                        break;
                    }
                    else
                    {
                        fechaFinalNuevo = Convert.ToDateTime(datoSolicitud.SolicitudDto.FechaInicio).AddDays(item.DiasDisponibles - 1);
                        numDiasPendientes = datoSolicitud.SolicitudDto.CalculoDias - item.DiasDisponibles;

                        if (datoSolicitud.SolicitudDto.TipoSolicitud == Constantes.TipoSolicitud.Regularización)
                        {
                            comentarioSolicitud = string.Format(ReglasNegocio.AprobacionRegularizacionGrabarParte, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal);
                        }
                        else
                        {
                            comentarioSolicitud = string.Format(ReglasNegocio.AprobacionRegularizacionGrabarParteFueraFecha, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal);
                        }

                        EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                        {
                            CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                            FechaDerecho = item.FechaDerecho,
                            CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                            FechaSolicitudInicio = datoSolicitud.SolicitudDto.FechaInicio,
                            FechaSolicitudFin = fechaFinalNuevo.ToString(),
                            RequiereBonos = Constantes.Generales.ValorNegativo,
                            ComentarioSolicitud = comentarioSolicitud,
                            CipUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                            TipoPago = Constantes.TipoPago.ConPago,
                            CodigoPadre = Constantes.Generales.ValorCero,
                            DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                            Reprogramado = Constantes.Generales.ValorCero,
                            Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                            FechaPago = fechaPago,
                            CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                            TipoSolicitud = Constantes.TipoSolicitud.Regularización
                        };
                        codigoError = CreaGocePrev(oBE);
                        if (codigoError < Constantes.Generales.NumeroCero)
                        {
                            return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionRegularizacionNoAprobado, datoSolicitud.SolicitudDto.CodigoEmpleado));
                        }
                        contador++;
                        fechaInicioNuevo = Convert.ToDateTime(fechaFinalNuevo).AddDays(1);
                    }
                }
                else
                {
                    if (numDiasPendientes <= item.DiasDisponibles)
                    {
                        if (datoSolicitud.SolicitudDto.TipoSolicitud == Constantes.TipoSolicitud.Regularización)
                        {
                            comentarioSolicitud = string.Format(ReglasNegocio.AprobacionRegularizacionGrabaParticionFinal, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal);
                        }
                        else
                        {
                            comentarioSolicitud = string.Format(ReglasNegocio.AprobacionRegularizacionGrabaParticionFinalFueraFecha, datoSolicitud.SolicitudDto.FechaInicio, datoSolicitud.SolicitudDto.FechaFinal);
                        }

                        EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                        {
                            CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                            FechaDerecho = item.FechaDerecho,
                            CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                            FechaSolicitudInicio = fechaInicioNuevo.ToString(),
                            FechaSolicitudFin = datoSolicitud.SolicitudDto.FechaFinal,
                            RequiereBonos = Constantes.Generales.ValorNegativo,
                            ComentarioSolicitud = comentarioSolicitud,
                            CipUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                            TipoPago = Constantes.TipoPago.ConPago,
                            CodigoPadre = Constantes.Generales.ValorCero,
                            DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                            Reprogramado = Constantes.Generales.ValorCero,
                            Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                            FechaPago = fechaPago,
                            CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                            TipoSolicitud = Constantes.TipoSolicitud.Regularización
                        };
                        codigoError = CreaGocePrev(oBE);
                        if (codigoError < Constantes.Generales.NumeroCero)
                        {
                            return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionRegularizacionNoAprobado, datoSolicitud.SolicitudDto.CodigoEmpleado));
                        }
                        numDiasPendientes = Constantes.Generales.NumeroCero;
                        break;
                    }
                    else
                    {
                        if (numDiasPendientes > Constantes.Generales.NumeroCero)
                        {
                            numDiasPendientes = numDiasPendientes - item.DiasDisponibles;
                            fechaFinalNuevo = Convert.ToDateTime(fechaInicioNuevo).AddDays(item.DiasDisponibles - 1);
                            
                            EvaluacionSolicitudRequest oBE = new EvaluacionSolicitudRequest()
                            {
                                CipEmpleado = datoSolicitud.SolicitudDto.CodigoEmpleado,
                                FechaDerecho = item.FechaDerecho,
                                CompraDisfrute = Constantes.TipoCompraDisfrute.TipoG,
                                FechaSolicitudInicio = fechaInicioNuevo.ToString(),
                                FechaSolicitudFin = fechaFinalNuevo.ToString(),
                                RequiereBonos = Constantes.Generales.ValorNegativo,
                                ComentarioSolicitud = comentarioSolicitud,
                                CipUsuario = cipUsuario,
                                CodigoPerfil = codigoPerfil,
                                TipoPago = Constantes.TipoPago.ConPago,
                                CodigoPadre = Constantes.Generales.ValorCero,
                                DescansoFisico = Constantes.TipoDescansoFisico.SiTomado,
                                Reprogramado = Constantes.Generales.ValorCero,
                                Adelanto = datoSolicitud.SolicitudDto.FlagAdelantoVacaciones,
                                FechaPago = fechaPago,
                                CodigoSolicitud = datoSolicitud.SolicitudDto.CodigoSolicitud,
                                TipoSolicitud = Constantes.TipoSolicitud.Regularización
                            };
                            codigoError = CreaGocePrev(oBE);
                            if (codigoError < Constantes.Generales.NumeroCero)
                            {
                                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionRegularizacionNoAprobado, datoSolicitud.SolicitudDto.CodigoEmpleado));
                            }
                            fechaInicioNuevo = Convert.ToDateTime(fechaFinalNuevo).AddDays(1);

                        }
                    }
                }
            }
            
            if (listaFechaDerecho.RolVacacionalDtoLista.Count == Constantes.Generales.NumeroCero)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaDiasDisponibles, datoSolicitud.SolicitudDto.CodigoEmpleado));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        /// <summary>
        /// Valida y Modifica la FechaPago
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public Resultado<SolicitudParametroRequest> ValidarModificarFechaPago(SolicitudResponse solicitud)
        {
            RolVacacionalRequest oParam = new RolVacacionalRequest()
            {
                CipEmpleado = solicitud.SolicitudDto.CodigoEmpleado,
                FechaInicio = solicitud.SolicitudDto.FechaInicio,
                FechaFinal = solicitud.SolicitudDto.FechaFinal
            };
            var resultado = _IRolVacacionalNegocio.ValidarModificarFechaPago(oParam);

            if (resultado < Constantes.Generales.NumeroCero)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaTrasladoRegularizacion, solicitud.SolicitudDto.CodigoEmpleado));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        public int CreaGocePrev(EvaluacionSolicitudRequest oRequest)
        {
            return _ISolicitudesAccesoDatos.CreaGocePrev(oRequest);
        }
        /// <summary>
        /// Actualizar Solicitud Estado Aprobado
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public int ActualizarAprobado(SolicitudRequest solicitud)
        {
            return _ISolicitudesAccesoDatos.ActualizarAprobado(solicitud);
        }
        /// <summary>
        /// Actualizar Solicitud Regularizacion Estado Aprobado
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public int ActualizarAprobadoRegula(SolicitudRequest solicitud)
        {
            return _ISolicitudesAccesoDatos.ActualizarAprobadoRegula(solicitud);
        }
        public int ActualizarSolicitudMasiva(SolicitudRequest solicitud)
        {
            return _ISolicitudesAccesoDatos.ActualizarSolicitudMasiva(solicitud);
        }
        public EvaluacionSolicitudResponse AprobarSolicitudesRegularizacion(string CadenaCodigosSolicitud, string cipUsuario, string codigoPerfil)
        {
            EvaluacionSolicitudResponse respesta = new EvaluacionSolicitudResponse();
            List<string> oListaError = new List<string>();
            string[] listaCodigSolicitud = CadenaCodigosSolicitud.Split('|');

            foreach (var codigoSolicitud in listaCodigSolicitud)
            {
                SolicitudResponse datosSolicitud = ObtenerSolicitud(codigoSolicitud);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    Resultado<Solicitud> validaAprobar = ValidarAprobarSolicitudRegularizacion(datosSolicitud, cipUsuario, codigoPerfil);
                    respesta.TipoRespuesta = validaAprobar.Tipo;
                    respesta.Errores = validaAprobar.Errores;
                    if (validaAprobar.Tipo == TipoResultado.Invalido)
                    {
                        oListaError.Add(validaAprobar.Errores.ToList()[0]);
                    }
                    if (validaAprobar.Tipo == TipoResultado.Ok)
                    {
                        SolicitudRequest oParam = new SolicitudRequest()
                        {
                            CodigoSolicitud = datosSolicitud.SolicitudDto.CodigoSolicitud,
                            TipoSolicitud = datosSolicitud.SolicitudDto.TipoSolicitud,
                            CodUsuario = cipUsuario,
                            CodigoPerfil = codigoPerfil,
                        };
                        ActualizarAprobadoRegula(oParam);
                        ActualizarSolicitudMasiva(oParam);
                    }
                    scope.Complete();
                }                
            }
            respesta.Errores = oListaError;
            return respesta;
        }
        public Resultado<Solicitud> ValidarAprobarSolicitudRegularizacion(SolicitudResponse solicitud, string cipUsuario, string codigoPerfil)
        {
            SolicitudParametroRequest solicitudParametroRequest = new SolicitudParametroRequest()
            {
                CodigoUsuario = solicitud.SolicitudDto.CodigoEmpleado,
                FechaInicio = solicitud.SolicitudDto.FechaInicio,
                FechaFinal = solicitud.SolicitudDto.FechaFinal,
                CalculoDias = solicitud.SolicitudDto.CalculoDias
            };
            var validaFeriado = ValidarFechaFinFeriado(solicitudParametroRequest);
            if (validaFeriado.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validaFeriado.Errores);
            }
            var validarCombo = _IParametroNegocio.ValidarComboSigueVacacional(solicitudParametroRequest);
            if (validarCombo.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarCombo.Errores);
            }
            
            var validarDiasDisponibles = ValidarDiasDisponiblesRegularizacion(solicitud);
            if (validarDiasDisponibles.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarDiasDisponibles.Errores);
            }            //
            var validarCruceGoce = ValidarCruceGoceProgramado(solicitud);
            if (validarCruceGoce.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarCruceGoce.Errores);
            }
            var validarLicencia = ValidarLicenciaProgramada(solicitud);
            if (validarLicencia.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarLicencia.Errores);
            }

            var validarGeneracionGoce = ValidarGeneracionGoceRegulariza(solicitud, cipUsuario, codigoPerfil);
            if (validarGeneracionGoce.Tipo.Equals(TipoResultado.Invalido))
            {
                return new Resultado<Solicitud>(TipoResultado.Invalido, validarGeneracionGoce.Errores);
            }

            return new Resultado<Solicitud>(TipoResultado.Ok, string.Empty);
        }
        #endregion


        public void Dispose()
        {
            _ISolicitudesAccesoDatos.Dispose();
            _IPerfilUsuarioNegocio.Dispose();
            _IPlantillaNegocio.Dispose();
            _IRolVacacionalNegocio.Dispose();
            _IPeriodoNegocio.Dispose();
            _ITrabajadorNegocio.Dispose();
            _IEmpresaNegocio.Dispose();
            _ICorreoNegocio.Dispose();
            _IParametroNegocio.Dispose();
            
        }
    }
}
