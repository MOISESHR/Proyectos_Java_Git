package pe.com.claro.eba.activospostpagoejbs.util;

import java.lang.reflect.Method;


public class Utilitarios {

    public static Method obtainSetterMethod(String methodName,Class methodClass,
                                            Class paramClass) throws NoSuchMethodException{
      
        java.lang.reflect.Method method;
        method = methodClass.getMethod(methodName, paramClass);
        
        return method;
    }
    
}
