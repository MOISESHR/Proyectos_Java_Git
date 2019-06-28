using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.Comun.Constantes;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Strings;
using TGS.SGV.Comun.Funciones;
using TGS.SGV.Modelo.Base;
using TGS.SGV.Modelo.Modelo;

namespace TGS.SGV.Negocio.Implementacion
{
    public class ParametroNegocio : IParametroNegocio
    {
        private readonly IParametroAccesoDatos _IParametroAccesoDatos;
        public ParametroNegocio(IParametroAccesoDatos parametroAccesoDatos)
        {
            _IParametroAccesoDatos = parametroAccesoDatos;
        }
        
        public ParametroResponse ObtenerParametroInterno(ParametroRequest parametroRequest)
        {
            var respuesta = _IParametroAccesoDatos.ObtenerParametroInterno(parametroRequest);

            if (respuesta.ParametroDto == null)
            {
                return new ParametroResponse { ParametroDto = new ParametroDto() };
            }
            else
            {
                return respuesta;
            }
        }

        public ParametroResponse ObtenerParametro(ParametroRequest parametroRequest)
        {
            var respuesta= _IParametroAccesoDatos.ObtenerParametro(parametroRequest);

            if (respuesta.ParametroDto == null)
            {
                return new ParametroResponse { ParametroDto = new ParametroDto() };
            }
            else
            {
                return respuesta;
            }
        }

        public ParametroAccesoResponse ObtenerParametroAcceso(UsuarioRequest usuarioRequest)
        { 
            var parametroGoce = new ParametroRequest
            {
                Codigo = Constantes.ParametrosInternos.GoceFuturo,
                Empresa = usuarioRequest.CodigoEmpresa
            };

            var goceFuturo = ObtenerParametroInterno(parametroGoce).ParametroDto;

            var parametroPerfiles = new ParametroRequest
            {
                Codigo = Constantes.Parametros.PerfilesBloqueados
            };

            var perfilesDenegados = ObtenerParametro(parametroPerfiles).ParametroDto;
             
            var empresasSuccessFactor = new ParametroRequest
            {
                Codigo = Constantes.Parametros.EmpresaSuccessFactor,
                Empresa = usuarioRequest.CodigoEmpresa
            };

            var empresasFactor = ObtenerParametro(empresasSuccessFactor).ParametroDto;
             
            return new ParametroAccesoResponse { 
                ParametroAccesoDto = new ParametroAccesoDto
                {
                    GoceFuturo = (!string.IsNullOrEmpty(goceFuturo.ValorParametro) ? 1 : 0),
                    EmpresaSuccessFactor = (!string.IsNullOrEmpty(empresasFactor.ValorParametro)  ? Constantes.Generales.EsSuccessFactor : Constantes.Generales.NoSuccessFactor),
                    PerfilDenegado = perfilesDenegados.ValorParametro
                }
            };
        }

        public IndicadorTrabajadorResponse ObtenerIndicadoresFuturo(string codigoUsuario, string fechaInicio)
        {
            var respuesta = _IParametroAccesoDatos.ObtenerIndicadoresFuturo(codigoUsuario, fechaInicio);
            if (respuesta.IndicadorTrabajadorDto== null)
            {
                return new IndicadorTrabajadorResponse { IndicadorTrabajadorDto = new IndicadorTrabajadorDto() };
            }
            else
            {
                return respuesta;
            }
        }


        public Resultado<IndicadorTrabajadorRequest> ValidarIndicadorTrabajador(IndicadorTrabajadorRequest indicadorTrabajadorRequest)
        {
            var resultadoIndicadorTrabajador = ObtenerIndicadoresFuturo(indicadorTrabajadorRequest.CodigoUsuario, indicadorTrabajadorRequest.Fecha_Inicio);

            var vVencido = Math.Abs(0 + resultadoIndicadorTrabajador.IndicadorTrabajadorDto.Vencidos);

            if (vVencido > 0)
            {
                return new Resultado<IndicadorTrabajadorRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.ComboVacacionesVencidas, string.Empty));
            }

            return new Resultado<IndicadorTrabajadorRequest>(TipoResultado.Ok, string.Empty);
        }

        public FechaGoceResponse ObtenerGoceFuturo(string codigoUsuario)
        {            
            var respuesta = _IParametroAccesoDatos.ObtenerGoceFuturo(codigoUsuario);
            if (respuesta.FechaGoceDto == null)
            {
                return new FechaGoceResponse { FechaGoceDto = new FechaGoceDto() };
            }
            else
            {
                return respuesta;
            }
        }

        public SolicitudParametroResponse ValidarFeriadoFestivo(SolicitudParametroRequest solicitudParametroRequest)
        {
            var respuesta = _IParametroAccesoDatos.ValidarFeriadoFestivo(solicitudParametroRequest); 
            if (respuesta.SolicitudParametroDto == null)
            {
                return new SolicitudParametroResponse { SolicitudParametroDto = new SolicitudParametroDto() };
            }
            else
            {
                return respuesta;
            }
        }

        public IndicadorTrabajadorResponse ObtenerIndicadores(string codigoUsuario)
        {
            var respuesta = _IParametroAccesoDatos.ObtenerIndicadores(codigoUsuario);
            if (respuesta.IndicadorTrabajadorDto == null)
            {
                return new IndicadorTrabajadorResponse { IndicadorTrabajadorDto = new IndicadorTrabajadorDto() };
            }
            else
            {
                return respuesta;
            }
        }
        public ParametroResponse ParametroPorOpcion(ParametroRequest parametroRequest)
        {
            return _IParametroAccesoDatos.ParametroPorOpcion(parametroRequest);
        }

        public ParametroResponse ParametroTipoPago(ParametroRequest parametroRequest)
        {
            return _IParametroAccesoDatos.ParametroTipoPago(parametroRequest);
        }
        
        public SolicitudParametroResponse ValidarComboSigueParametro(SolicitudParametroRequest solicitudParametroRequest)
        {            
            return  _IParametroAccesoDatos.ValidarComboSigueParametro(solicitudParametroRequest);            
        }

        public SolicitudParametroResponse ValidarFeriadoComboParametro(SolicitudParametroRequest solicitudParametroRequest)
        {            
            return _IParametroAccesoDatos.ValidarFeriadoComboParametro(solicitudParametroRequest);            
        }

                public Resultado<SolicitudParametroRequest> ValidarFeriadoCombo(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoFeriadoCombo = ValidarFeriadoComboParametro(solicitudParametroRequest);

            var ValorCombo = Constantes.Generales.ValorPositivo;

            if (resultadoFeriadoCombo.SolicitudParametroDto.ValorComboInicio >= 0 && resultadoFeriadoCombo.SolicitudParametroDto.ValorComboFinal >= 0)
            {
                ValorCombo = Constantes.Generales.ValorNegativo;
            }

            if (resultadoFeriadoCombo.SolicitudParametroDto.ValorDiasFestivo > 0 && ValorCombo.Equals(Constantes.Generales.ValorNegativo))
            {
                int DiasFestivo = 0;

                if (resultadoFeriadoCombo.SolicitudParametroDto.DiasFindeSemana > resultadoFeriadoCombo.SolicitudParametroDto.DiasFeriado)
                {
                    DiasFestivo = Convert.ToInt16(resultadoFeriadoCombo.SolicitudParametroDto.DiasFindeSemana);
                }
                else
                {
                    DiasFestivo = Convert.ToInt16(resultadoFeriadoCombo.SolicitudParametroDto.DiasFeriado);
                }
                if (DiasFestivo > 0)
                {
                    SolicitudParametroRequest oSolicitudParametroRequest = new SolicitudParametroRequest()
                    {
                        FechaInicio = solicitudParametroRequest.FechaInicio,
                        FechaFinal = solicitudParametroRequest.FechaFinal,
                        DiasFest = Convert.ToDouble(DiasFestivo)
                    };

                    var resultadoFeriadoFestivo = ValidarFeriadoFestivo(oSolicitudParametroRequest);
                    DiasFestivo = 0;

                    if (resultadoFeriadoFestivo.SolicitudParametroDto.DiasFindeSemana > resultadoFeriadoFestivo.SolicitudParametroDto.DiasFeriado)
                    {
                        DiasFestivo = Convert.ToInt16(resultadoFeriadoFestivo.SolicitudParametroDto.DiasFindeSemana);
                    }
                    else
                    {
                        DiasFestivo = Convert.ToInt16(resultadoFeriadoFestivo.SolicitudParametroDto.DiasFeriado);
                    }

                    var CodigoRespuesta = Convert.ToDateTime(resultadoFeriadoFestivo.SolicitudParametroDto.FechaFinalFestivo);

                    CodigoRespuesta = CodigoRespuesta.AddDays(Convert.ToDouble(DiasFestivo));

                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.DiasFestivosFindeSemana, CodigoRespuesta));                     
                }

                SolicitudParametroRequest rSolicitudParametroRequest = new SolicitudParametroRequest()
                {
                    CodigoUsuario = solicitudParametroRequest.CodigoUsuario,
                    FechaInicio = solicitudParametroRequest.FechaInicio,
                    FechaFinal = solicitudParametroRequest.FechaFinal,
                };

                var resultadoComboContinua = ValidarComboContinuadoParametro(rSolicitudParametroRequest);

                if (resultadoComboContinua.SolicitudParametroDto.ValorComboInicio > 0 && resultadoComboContinua.SolicitudParametroDto.ValorComboFinal > 0)
                {
                    ValorCombo = Constantes.Generales.ValorNegativo;
                }

                if (resultadoComboContinua.SolicitudParametroDto.ValorDiasFestivo > 0 && ValorCombo.Equals(Constantes.Generales.ValorNegativo))
                {
                    if (resultadoComboContinua.SolicitudParametroDto.ValorContinuoCombo.Equals(Constantes.Generales.ValorPositivo))
                    {                               
                        return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.ComboVacacionalPrevio, resultadoComboContinua.SolicitudParametroDto.CodigoCip));                     
                    }
                }
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        
        public SolicitudParametroResponse ValidarComboContinuadoParametro(SolicitudParametroRequest solicitudParametroRequest)
        {
            return _IParametroAccesoDatos.ValidarComboContinuadoParametro(solicitudParametroRequest);
        }

        public Resultado<SolicitudParametroRequest> ValidarComboSigueVacacional(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultado = ValidarComboSigueParametro(solicitudParametroRequest);
            var ValCombo = Constantes.Generales.ValorNegativo;

            if (resultado.SolicitudParametroDto.ValorComboInicio > 0 & resultado.SolicitudParametroDto.ValorComboFinal > 0)
            {
                ValCombo = Constantes.Generales.ValorPositivo;
            }

            if (resultado.SolicitudParametroDto.ValorDiasFestivo > 0 && ValCombo.Equals(Constantes.Generales.ValorPositivo))
            {
                if (resultado.SolicitudParametroDto.ValorContinuoCombo.Equals(Constantes.Generales.ValorPositivo))
                {
                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.AprobacionValidaFechaInicio, resultado.SolicitudParametroDto.CodigoCip));
                }
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);

        }

        public Resultado<SolicitudParametroRequest> ValidarComboSigue(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoComboSigue = ValidarComboSigueParametro(solicitudParametroRequest);

            var ValorCombo = Constantes.Generales.ValorNegativo;

            if (resultadoComboSigue.SolicitudParametroDto.ValorComboInicio >= 0 & resultadoComboSigue.SolicitudParametroDto.ValorComboFinal >= 0)
            {
                ValorCombo = Constantes.Generales.ValorPositivo;
            }

            if (resultadoComboSigue.SolicitudParametroDto.ValorDiasFestivo >= 0 && ValorCombo.Equals(Constantes.Generales.ValorPositivo))
            {
                if (resultadoComboSigue.SolicitudParametroDto.ValorSigueCombo.Equals(Constantes.Generales.ValorPositivo))
                {
                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.RegistroVacacionalPosterior, resultadoComboSigue.SolicitudParametroDto.CodigoCip));
                }
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public SolicitudParametroResponse ValidarDiasDisponibleParametro(SolicitudParametroRequest solicitudParametroRequest)
        {
            return _IParametroAccesoDatos.ValidarDiasDisponibleParametro(solicitudParametroRequest);
        }

        public Resultado<SolicitudParametroRequest> ValidarDiasDisponible(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoDiasDisponible = ValidarDiasDisponibleParametro(solicitudParametroRequest);

            if (resultadoDiasDisponible.SolicitudParametroDto.CodigoTipoEmpleado != Constantes.TipoEmpleado.Directivo & resultadoDiasDisponible.SolicitudParametroDto.CodigoTipoEmpleado != Constantes.TipoEmpleado.Desplazado)
            {
                if (resultadoDiasDisponible.SolicitudParametroDto.DiasDisponibleCip == 0)
                {
                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.DiasInferiorSolicitado, resultadoDiasDisponible.SolicitudParametroDto.CodigoCip));
                }
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public SolicitudParametroResponse ValidarDiasMayorParametros(SolicitudParametroRequest solicitudParametroRequest)
        {
            return _IParametroAccesoDatos.ValidarDiasMayorParametros(solicitudParametroRequest);
        }

        public Resultado<SolicitudParametroRequest> ValidarDiasMayor(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoDiasMayor = ValidarDiasMayorParametros(solicitudParametroRequest);

            if (resultadoDiasMayor.SolicitudParametroDto.CodigoTipoEmpleado != Constantes.TipoEmpleado.Directivo & resultadoDiasMayor.SolicitudParametroDto.CodigoTipoEmpleado != Constantes.TipoEmpleado.Desplazado)
            {
                if (solicitudParametroRequest.CalculoDias > resultadoDiasMayor.SolicitudParametroDto.DiasDisponibleCip)
                {
                    return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.DiasMayorDisponible, resultadoDiasMayor.SolicitudParametroDto.CodigoCip, solicitudParametroRequest.FechaInicio));
                }
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public Resultado<SolicitudParametroRequest> ValidarLicencia(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoLicencia = ValidarCruceFechaLicencia(solicitudParametroRequest);

            if (resultadoLicencia.SolicitudParametroDto.ContadorLicencia != 0)
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.CruceLicenciaRegistrada, resultadoLicencia.SolicitudParametroDto.CodigoCip));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }
        
        public SolicitudParametroResponse ValidarCruceFechaLicencia(SolicitudParametroRequest solicitudParametroRequest)
        {
            return _IParametroAccesoDatos.ValidarCruceFechaLicencia(solicitudParametroRequest);
        }

        public Resultado<FechaGoceRequest> ValidarFechaGoce(Solicitud solicitudRequest)
        {
            var resultadoFechaGoce = ObtenerGoceFuturo(solicitudRequest.CodigoEmpleado);

            if (resultadoFechaGoce.FechaGoceDto.ValorDiaVencido.Equals(Constantes.Generales.ValorPositivo))
            {
                var oIndicadores = ObtenerIndicadores(solicitudRequest.CodigoEmpleado);
                int vTotalPendiente = 0;
                string vFechaMaximoRegistro = string.Empty;
                string vFechaMaximoCalculado = string.Empty;
                if (Convert.ToInt32(oIndicadores.IndicadorTrabajadorDto.Vencidos) > 0)
                {
                    if (Convert.ToInt32(resultadoFechaGoce.FechaGoceDto.DiasPlanificadoVencido) < 30)
                    {
                        vTotalPendiente = vTotalPendiente + 30 - Convert.ToInt32(resultadoFechaGoce.FechaGoceDto.DiasPlanificadoVencido);
                        vFechaMaximoRegistro = resultadoFechaGoce.FechaGoceDto.FechaDerechoVencido;
                    }
                    else
                    {
                        if (Convert.ToInt32(oIndicadores.IndicadorTrabajadorDto.Pendientes) > 0)
                        {
                            if (Convert.ToInt32(resultadoFechaGoce.FechaGoceDto.DiasPlanificadoPendiente) < 30)
                            {
                                vTotalPendiente = vTotalPendiente + 30 - Convert.ToInt32(resultadoFechaGoce.FechaGoceDto.DiasPlanificadoPendiente);
                                vFechaMaximoRegistro = resultadoFechaGoce.FechaGoceDto.FechaDerechoVencido;
                            }
                            else
                            {
                                vFechaMaximoRegistro = resultadoFechaGoce.FechaGoceDto.FechaDerechoPendiente;
                            }
                        }
                        else
                        {
                            vFechaMaximoRegistro = resultadoFechaGoce.FechaGoceDto.FechaFinalCalendario;
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(oIndicadores.IndicadorTrabajadorDto.Pendientes) > 0)
                    {
                        if (Convert.ToInt32(resultadoFechaGoce.FechaGoceDto.DiasPlanificadoPendiente) < 30)
                        {
                            vTotalPendiente = vTotalPendiente + 30 - Convert.ToInt32(resultadoFechaGoce.FechaGoceDto.DiasPlanificadoPendiente);
                            vFechaMaximoRegistro = resultadoFechaGoce.FechaGoceDto.FechaDerechoVencido;
                        }
                        else
                        {
                            vFechaMaximoRegistro = resultadoFechaGoce.FechaGoceDto.FechaDerechoPendiente;
                        }
                    }
                    else
                    {
                        vFechaMaximoRegistro = resultadoFechaGoce.FechaGoceDto.FechaFinalCalendario;
                    }
                }

                var DiasVencidos = oIndicadores.IndicadorTrabajadorDto.Vencidos;
                var AdicionaDias = Funciones.RestarFechas(Convert.ToDateTime(solicitudRequest.FechaInicio), Convert.ToDateTime(solicitudRequest.FechaFinal));
                AdicionaDias = Convert.ToString(Convert.ToInt32(AdicionaDias) - vTotalPendiente);
                if (Convert.ToInt32(AdicionaDias) > 0)
                {
                    vFechaMaximoCalculado = Funciones.SumarFechas(vFechaMaximoRegistro, Convert.ToDouble(AdicionaDias));
                    if (Funciones.ComparaTresFechas(vFechaMaximoCalculado, solicitudRequest.FechaInicio, solicitudRequest.FechaFinal))
                    {
                        return new Resultado<FechaGoceRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.GoceVacacionesVencidas, string.Empty));
                    }
                }
            }

            return new Resultado<FechaGoceRequest>(TipoResultado.Ok, string.Empty);
        }

        public Resultado<SolicitudParametroRequest> ValidarMando(SolicitudParametroRequest solicitudParametroRequest)
        {
            var resultadoMando = ValidarMandoParametro(solicitudParametroRequest);

            if (resultadoMando.SolicitudParametroDto.CodigoResponsable.Equals(string.Empty))
            {
                return new Resultado<SolicitudParametroRequest>(TipoResultado.Invalido, string.Format(ReglasNegocio.NoTieneAprobadorAsignado, resultadoMando.SolicitudParametroDto.CodigoCip));
            }

            return new Resultado<SolicitudParametroRequest>(TipoResultado.Ok, string.Empty);
        }

        public SolicitudParametroResponse ValidarMandoParametro(SolicitudParametroRequest solicitudParametroRequest)
        {            
            return _IParametroAccesoDatos.ValidarMandoParametro(solicitudParametroRequest);            
        }




        public void Dispose()
        {
            _IParametroAccesoDatos.Dispose();
        }
    }
}
