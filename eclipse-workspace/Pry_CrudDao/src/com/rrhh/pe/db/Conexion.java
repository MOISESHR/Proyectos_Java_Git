/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.rrhh.pe.db;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import javax.swing.JOptionPane;

/**
 *
 * @author MOISESHR
 */
public class Conexion {
    private static Connection conn = null;
    private static String login = "SYSTEM";
    private static String clave = "yourfriend_440";
    private static final String url = "jdbc:oracle:thin:@localhost:1521:orcl";
    
    public static Connection getConnection() {
        try {
            Class.forName("oracle.jdbc.driver.OracleDriver");
            conn = DriverManager.getConnection(url, login, clave);
            conn.setAutoCommit(false);
            if (conn != null) {
                System.out.println("Conexion exitosa");
            }
            else {
                System.out.println("Conexion ex erronea");
            }
            
        } 
        catch (ClassNotFoundException | SQLException e) {
            JOptionPane.showMessageDialog(null, "Conexion erronea" + e.getMessage());
        }
        return conn;
    }
    
    public void desconexion() {
        try {
            conn.close();
        }
        catch (Exception e)
        {
            System.out.println("Error al desconectar " + e.getMessage());
        }
    }
    
    public static void main(String[] args) {
        Conexion c = new Conexion();
        c.getConnection();
    }
}
