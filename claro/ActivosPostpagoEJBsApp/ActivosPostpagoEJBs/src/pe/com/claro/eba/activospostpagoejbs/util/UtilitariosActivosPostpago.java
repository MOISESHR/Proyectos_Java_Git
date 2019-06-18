package pe.com.claro.eba.activospostpagoejbs.util;

import java.util.ArrayList;
import java.util.List;

import pe.com.claro.ebs.services.activospostpago.ws.types.ListaCamposAdicionalesType;

public class UtilitariosActivosPostpago {
    
    public static String obtenerPrimerValor(List<ListaCamposAdicionalesType.CampoAdicional> listaCampos, String key){
        
        for(ListaCamposAdicionalesType.CampoAdicional ad:listaCampos){
            if(key.equalsIgnoreCase(  ad.getNombreCampo() )){
                return ad.getValor();
            }
        }
        
        return null;
    }
    
     public static String obtenerUltimoValor(List<ListaCamposAdicionalesType.CampoAdicional> listaCampos, String key){
        
        int size= listaCampos.size();
        ListaCamposAdicionalesType.CampoAdicional ad=null;
        
        for(int i=size -1;i>=0;i--){
            ad= listaCampos.get(i);
            
            if(key.equalsIgnoreCase(  ad.getNombreCampo() )){
                return ad.getValor();
            }
        }
        
        return null;
    }
    
     public static String[] obtenerTodosValores(List<ListaCamposAdicionalesType.CampoAdicional> listaCampos, String key){
        
        List<String> lista= new ArrayList<String>();
        
        for(ListaCamposAdicionalesType.CampoAdicional ad:listaCampos){
            if(key.equalsIgnoreCase(  ad.getNombreCampo() )){
                lista.add( ad.getValor() );
            }
        }
        
        return lista.toArray( new String [ lista.size() ] );
    }
    
}
