package pe.com.claro.eba.activospostpagoejbs.util;

import java.util.HashMap;
import java.util.Map;

import org.apache.log4j.Logger;

import org.springframework.beans.BeansException;
import org.springframework.beans.factory.BeanFactory;
import org.springframework.beans.factory.access.BeanFactoryLocator;
import org.springframework.beans.factory.access.SingletonBeanFactoryLocator;
import org.springframework.context.ConfigurableApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import org.springframework.core.io.support.ResourcePatternResolver;
import org.springframework.core.io.support.ResourcePatternUtils;


public class MyContextSingletonBeanFactoryLocator extends SingletonBeanFactoryLocator {


    @Override
    protected BeanFactory createDefinition(String resourceLocation,
                                           String factoryKey) {

        final ClassPathXmlApplicationContext cpxac =
            new ClassPathXmlApplicationContext(new String[] { resourceLocation },
                                               false);
        final ClassLoader classLoader = this.getClass().getClassLoader();
        cpxac.setClassLoader(classLoader);
        return cpxac;
    }

    private static final String BEANS_REFS_XML_NAME =
        "classpath*:beanRefContext.xml";

    // the keyed singleton instances
    private static Map instances = new HashMap();


    /**
     * Returns an instance which uses the default "classpath*:beanRefContext.xml", as
     * the name of the definition file(s). All resources returned by the current
     * thread's context classloader's getResources() method with this name will be
     * combined to create a definition, which is just a BeanFactory.
     */
    public static BeanFactoryLocator getInstance() throws BeansException {
        return getInstance(BEANS_REFS_XML_NAME);
    }

    /**
     * Returns an instance which uses the the specified selector, as the name of the
     * definition file(s). In the case of a name with a Spring "classpath*:" prefix,
     * or with no prefix, which is treated the same, the current thread's context class
     * loader's <code>getResources</code> method will be called with this value to get
     * all resources having that name. These resources will then be combined to form a
     * definition. In the case where the name uses a Spring "classpath:" prefix, or
     * a standard URL prefix, then only one resource file will be loaded as the
     * definition.
     * @param selector the name of the resource(s) which will be read and combine to
     * form the definition for the SingletonBeanFactoryLocator instance. The one file
     * or multiple fragments with this name must form a valid ApplicationContext
     * definition.
     */
    public static BeanFactoryLocator getInstance(String selector) throws BeansException {
        // For backwards compatibility, we prepend "classpath*:" to the selector name if there
        // is no other prefix (i.e. "classpath*:", "classpath:", or some URL prefix).
        if (!ResourcePatternUtils.isUrl(selector)) {
            selector =
                    ResourcePatternResolver.CLASSPATH_ALL_URL_PREFIX + selector;
        }

        synchronized (instances) {
            if (logger.isDebugEnabled()) {
                logger.debug("ContextSingletonBeanFactoryLocator.getInstance(): instances.hashCode=" +
                             instances.hashCode() + ", instances=" +
                             instances);
            }
            BeanFactoryLocator bfl =
                (BeanFactoryLocator)instances.get(selector);
            if (bfl == null) {
                bfl = new MyContextSingletonBeanFactoryLocator(selector);
                instances.put(selector, bfl);
            }
            return bfl;
        }
    }


    /**
     * Constructor which uses the default "classpath*:beanRefContext.xml", as the name of the
     * definition file(s). All resources returned by the definition classloader's
     * getResources() method with this name will be combined to create a definition.
     */
    protected MyContextSingletonBeanFactoryLocator() {
        super(BEANS_REFS_XML_NAME);
    }

    /**
     * Constructor which uses the the specified name as the name of the
     * definition file(s). All resources returned by the definition classloader's
     * getResources() method with this name will be combined to create a definition.
     */
    protected MyContextSingletonBeanFactoryLocator(String resourceName) {
        super(resourceName);
    }

    /**
     * Overrides default method to initialize definition object, which this method
     * assumes is a ConfigurableApplicationContext. This implementation simply invokes
     * {@link ConfigurableApplicationContext#refresh ConfigurableApplicationContext.refresh()}.
     */
    protected void initializeDefinition(BeanFactory groupDef) throws BeansException {
        if (groupDef instanceof ConfigurableApplicationContext) {
            ((ConfigurableApplicationContext)groupDef).refresh();
        }
    }

    /**
     * Overrides default method to work with ApplicationContext
     */
    protected void destroyDefinition(BeanFactory groupDef,
                                     String resourceName) throws BeansException {
        if (groupDef instanceof ConfigurableApplicationContext) {
            // debugging trace only
            if (logger.isDebugEnabled()) {
                logger.debug("ContextSingletonBeanFactoryLocator group with resourceName '" +
                             resourceName +
                             "' being released, as there are no more references");
            }
            ((ConfigurableApplicationContext)groupDef).close();
        }
    }

}
