using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI; 
using TGS.SGV.Comun.Enumerables; 
using TGS.SGV.Web.Utilitarios;

namespace TGS.SGV.Web.Reportes
{
    public partial class frmContenedor : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = ConstantesUI.UsuarioLogin;

            if (usuario == null)
            {
                Response.Redirect("~/Seguridad/Login/Index");                
            }
             
            if (!Page.IsPostBack)
            {
                string reporte = Request.Params["reporte"];

                if ((String.IsNullOrEmpty(reporte)))
                {
                    return;
                }

                Enumerables.Reportes rep = (Enumerables.Reportes)Enum.Parse(typeof(Enumerables.Reportes), reporte);

                switch (rep)
                {
                    case Enumerables.Reportes.Colaborador:
                        Colaborador();
                        break;
                 
                    default:
                        break;
                }
            }
        }

        private void  Colaborador()
        {
            string idPlan = Request.Params["idPlan"].Trim();
            string fechaCita = Request.Params["fechaCita"].Trim();
            string idPlanSedeClinica = Request.Params["idPlanSedeClinica"].Trim();
            string idEmpresa = Request.Params["idEmpresa"].Trim();

            fechaCita = (fechaCita.Equals(string.Empty) ? null : fechaCita);

            rvVisor.ServerReport.ReportServerUrl = new Uri(ConstantesUI.ServerReport);

            rvVisor.ServerReport.ReportPath = "/TGS.GEMO/CitasColaborador";

            ReportParameter[] parametros = new ReportParameter[4]; 

            parametros[0] = new ReportParameter();
            parametros[0].Name = "IDPLAN";
            parametros[0].Values.Add(idPlan);

            parametros[1] = new ReportParameter();
            parametros[1].Name = "FECHACITA";
            parametros[1].Values.Add(fechaCita);

            parametros[2] = new ReportParameter();
            parametros[2].Name = "IDPLANSEDECLINICA";
            parametros[2].Values.Add(idPlanSedeClinica);

            parametros[3] = new ReportParameter();
            parametros[3].Name = "IDEMPRESA";
            parametros[3].Values.Add(idEmpresa);

            rvVisor.ServerReport.SetParameters(parametros);

            rvVisor.ServerReport.Refresh();
        }

        

       
    }
}