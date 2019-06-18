package pe.com.claro.eba.activospostpagoejbs.jms.clients;

import java.io.Serializable;

import java.util.Properties;

import javax.jms.JMSException;
import javax.jms.ObjectMessage;
import javax.jms.Queue;
import javax.jms.QueueConnection;
import javax.jms.QueueConnectionFactory;
import javax.jms.QueueSender;
import javax.jms.QueueSession;
import javax.jms.Session;

import javax.naming.InitialContext;
import javax.naming.NamingException;

import org.apache.log4j.Logger;

import pe.com.claro.eba.activospostpagoejbs.util.ServiceLocatorException;


public class JMSClient {

    private static Logger logger = Logger.getLogger(JMSClient.class);

    private InitialContext context = null;
    private QueueConnectionFactory connectionFactory;
    private String jndiConnectionFactory;
    private String jndiQueue;
    private String providerUrl;


    public JMSClient(Properties params, String jndiConnectionFactory,
                     String jndiQueue) throws ServiceLocatorException {
        this.jndiConnectionFactory = jndiConnectionFactory;
        this.jndiQueue = jndiQueue;
        setUp(params);
        this.providerUrl= params.getProperty("java.naming.provider.url");
    }

    public void sendMessage(Serializable message,String mensajeTx) throws ServiceLocatorException,
                                                         JMSException {

        String mensajeTransaccion= mensajeTx + "[sendMessage] ";
        QueueConnection connection = null;
        QueueSession session = null;
        QueueSender sender = null;
        Queue queue = null;

        try {
            connection = connectionFactory.createQueueConnection();
            connection.start();

            session =
                    connection.createQueueSession(false, Session.AUTO_ACKNOWLEDGE);

            queue = (Queue)lookup(jndiQueue);
            sender = session.createSender(queue);

            ObjectMessage objMsg = session.createObjectMessage(message);
            sender.send(objMsg);

        } catch (JMSException jmse) {
            logger.error(mensajeTransaccion + "Error enviando mensaje:" + jmse.getMessage());
            throw jmse;
        } catch (ServiceLocatorException ser) {
            logger.error(mensajeTransaccion + "Error enviando mensaje:" + ser.getMessage());
            throw ser;
        }

        try {
            sender.close();
            session.close();
            connection.close();
        } catch (Exception je) {
            logger.error(mensajeTransaccion + "Error cerrando conexiones JMS:" + je.getMessage());
        } finally {
        }
    }

    private void setUp(Properties params) throws ServiceLocatorException {

        try {
            context = new InitialContext(params);
        } catch (NamingException e) {
            throw new ServiceLocatorException("Error obteniendo Context de Servidor. URL:" +
                                              params.getProperty("java.naming.provider.url"),e);
        }

        connectionFactory =
                (QueueConnectionFactory)lookup(jndiConnectionFactory);
    }

    private Object lookup(String objectName) throws ServiceLocatorException {
        Object object = null;

        try {
            object = context.lookup(objectName);
        } catch (NamingException e) {
            throw new ServiceLocatorException("Error buscando objeto:" +
                                              objectName, e);
        }

        return object;
    }

    public String getJndiConnectionFactory() {
        return jndiConnectionFactory;
    }

    public String getJndiQueue() {
        return jndiQueue;
    }

    public String getProviderUrl() {
        return providerUrl;
    }
}
