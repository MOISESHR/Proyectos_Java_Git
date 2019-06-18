package pe.com.claro.eba.activospostpagoejbs.util;

public class ServiceLocatorException extends Exception {
    public ServiceLocatorException(Throwable throwable) {
        super(throwable);
    }

    public ServiceLocatorException(String string, Throwable throwable) {
        super(string, throwable);
    }

    public ServiceLocatorException(String string) {
        super(string);
    }

    public ServiceLocatorException() {
        super();
    }
}