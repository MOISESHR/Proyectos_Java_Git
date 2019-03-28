package com.rrhh.pe.dao;
        
import com.rrhh.pe.entity.Empleado;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import javax.swing.JOptionPane;
import javax.swing.JTable;
import javax.swing.table.DefaultTableModel;

/**
 *
 * @author MOISESHR
 */
public class EmpleadoDAO {
    private String mensaje = "";
    
    public String agregar(Connection con, Empleado emp) {
        PreparedStatement pst = null;
        String sql = "INSERT INTO EMPLEADO (ID_EMPLEADO,NOMBRES,APELLIDOS,DNI,ESTADO_CIVIL,GENERO,EDAD) " 
                + "VALUES(EMPLEADO_SEQ.NEXTVAL,?,?,?,?,?,?)";
        
        try
        {
            pst = con.prepareStatement(sql);
            pst.setString(1, emp.getNombres());
            pst.setString(2, emp.getApellidos());
            pst.setString(3, emp.getDni());
            pst.setString(4, emp.getEstadoCivil()+"");
            pst.setString(5, emp.getGenero()+"");
            pst.setInt(6, emp.getEdad());
            pst.execute();
            pst.close();
            
            
            mensaje = "GUARDADO CORRECTAMENTE";
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO GUARDAR CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
    public String modificar(Connection con, Empleado emp) {
        PreparedStatement pst = null;
        String sql = "UPDATE EMPLEADO SET NOMBRES=?,APELLIDOS=?,DNI=?,ESTADO_CIVIL=?,GENERO=?,EDAD=? "
                + "WHERE ID_EMPLEADO=?";
        
        try
        {
            pst = con.prepareStatement(sql);
            pst.setString(1, emp.getNombres());
            pst.setString(2, emp.getApellidos());
            pst.setString(3, emp.getDni());
            pst.setString(4, emp.getEstadoCivil()+"");
            pst.setString(5, emp.getGenero()+"");
            pst.setInt(6, emp.getEdad());
            pst.setInt(7, emp.getId_Empleado());
            mensaje = "GUARDADO CORRECTAMENTE";
            pst.execute();
            pst.close();
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO GUARDAR CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
    public String eliminar(Connection con, int id) {
        PreparedStatement pst = null;
        String sql = "DELETE FROM EMPLEADO WHERE ID_EMPLEADO=?";
        
        try
        {
            pst = con.prepareStatement(sql);
            pst.setInt(1, id);            
            mensaje = "ACTUALIZADO CORRECTAMENTE";
            pst.execute();
            pst.close();
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO ACTUALIZADAR CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
    public void listar(Connection con, JTable tabla) {    	
    	DefaultTableModel model;
        String[] columnas = {"ID_EMPLEADO","NOMBRES","APELLIDOS","DNI","ESTADO_CIVIL","GENERO","EDAD"};
        model = new DefaultTableModel(null, columnas);
        
        String sql = "SELECT * FROM EMPLEADO ORDER BY ID_EMPLEADO";
        
        String[] filas = new String[7];
        Statement st = null;
        ResultSet rs = null;
        
        try {
            st = con.createStatement();
            rs = st.executeQuery(sql);
            
            while (rs.next())
            {
                for (int i = 0; i < 7; i++) {
                    filas[i] = rs.getString(i+1);
                }
                model.addRow(filas);
            }
            tabla.setModel(model);
            
        } catch (Exception e) {        	
            JOptionPane.showMessageDialog(null, "no se puede listar");
        }
    }
}
