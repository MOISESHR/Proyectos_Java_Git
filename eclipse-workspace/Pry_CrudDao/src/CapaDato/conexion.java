package CapaDato;

import java.sql.Connection;
import java.sql.DriverManager;

public class conexion {

	private static Connection conn = null;
    private static String login = "SYSTEM";
    private static String clave = "yourfriend_440";
    private static final String urlOracle = "jdbc:oracle:thin:@localhost:1521:orcl";
    private static final String urlSQL = "sqlserver:@localhost:1433:DatabaseName=";
    
	public Connection conectar() {
		Connection cn = null;
		try {			
			cn = DriverManager.getConnection(urlOracle);
			
		} catch (Exception e) {
			// TODO: handle exception
		}
		
		return cn;
	}
}
