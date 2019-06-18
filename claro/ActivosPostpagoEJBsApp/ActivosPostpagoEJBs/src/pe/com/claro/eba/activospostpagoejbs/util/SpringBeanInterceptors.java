package pe.com.claro.eba.activospostpagoejbs.util;

import org.springframework.beans.factory.access.BeanFactoryLocator;
import org.springframework.ejb.interceptor.SpringBeanAutowiringInterceptor;

public class SpringBeanInterceptors extends SpringBeanAutowiringInterceptor {

    @Override
    protected BeanFactoryLocator getBeanFactoryLocator(Object target) {

        return MyContextSingletonBeanFactoryLocator.getInstance();
    }

}
