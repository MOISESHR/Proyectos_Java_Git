package pe.com.claro.eba.activospostpagoejbs.service;

import pe.com.claro.ebs.services.activospostpago.beans.ActivosPostpagoBean;
import pe.com.claro.ejb.commons.exception.ClaroBaseException;

public interface GestionTxEaiService {
    
    void procesarTransaccionesEAI(ActivosPostpagoBean activosBean) throws ClaroBaseException;
    void procesarArchivamientoTxEAI(ActivosPostpagoBean activosBean) throws ClaroBaseException;
}
