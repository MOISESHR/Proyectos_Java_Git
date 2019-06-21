using System;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;
using TGS.SGV.Modelo.Dto;

namespace TGS.SGV.Servicio.Error
{
    public class ErrorHandler : IErrorHandler
    { 
        public bool HandleError(Exception error)
        {
            if (!(error is FaultException))
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(error));
            }

            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message msg)
        {
            if (error is FaultException)
                return;

            ErrorServicio  faultDetail = new ErrorHelper().MostrarError(error);

            if (faultDetail != null)
            {
                Type faultType =
                    typeof(FaultException<>).MakeGenericType(faultDetail.GetType());

                FaultReason faultReason = new FaultReason(faultDetail.Mensaje);
                FaultCode faultCode = FaultCode.CreateReceiverFaultCode(new FaultCode(faultDetail.Codigo));

                FaultException faultException =
                    (FaultException)Activator.CreateInstance(faultType, faultDetail, faultReason, faultCode);

                MessageFault faultMessage = faultException.CreateMessageFault();
                msg = Message.CreateMessage(version, faultMessage, faultException.Action);
            }
        }
         
    }
}