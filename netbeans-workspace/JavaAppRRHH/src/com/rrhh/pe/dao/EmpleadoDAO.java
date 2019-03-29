package com.rrhh.pe.dao;
        
import com.rrhh.pe.entity.Empleado;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;
import javax.swing.JOptionPane;
import javax.swing.JTable;
import javax.swing.table.DefaultTableModel;

/**
 *
 * @author MOISESHR
 */
public class EmpleadoDAO {
    private String mensaje = "";
    
    public String agregar1(Connection con, Empleado emp) {
        
        Statement stm= null;
        
        String sql = "INSERT INTO EMPLEADO (ID_EMPLEADO,NOMBRES,APELLIDOS,DNI,ESTADO_CIVIL,GENERO,EDAD) " 
                + "VALUES(EMPLEADO_SEQ.NEXTVAL,'"+emp.getNombres()+"','"+emp.getApellidos()+"','"+emp.getDni()+"','"+emp.getEstadoCivil()+"','"+emp.getGenero()+"',"+emp.getEdad()+")";
        
        try
        {
            stm= con.createStatement();
            stm.execute(sql);
            stm.close();
            con.close();
            mensaje = "GUARDADO CORRECTAMENTE";
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO GUARDAR CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
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
        Statement stm= null;
        String sql = "UPDATE EMPLEADO SET NOMBRES='"+emp.getNombres()+"',APELLIDOS='"+emp.getApellidos()+"',DNI='"+emp.getDni()+"',ESTADO_CIVIL='"+emp.getEstadoCivil()+"',GENERO='"+emp.getGenero()+"',EDAD="+emp.getEdad()+" WHERE ID_EMPLEADO="+emp.getId_Empleado();
        
        try
        {
            stm=con.createStatement();
            stm.execute(sql);
            mensaje = "MODIFICADO CORRECTAMENTE";
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO MODIFICADO CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
    public String modificar2(Connection con, Empleado emp) {
        PreparedStatement pst = null;
        String sql = "UPDATE EMPLEADO SET NOMBRES=?,APELLIDOS=?,DNI=?,ESTADO_CIVIL=?,GENERO=?,EDAD=? WHERE ID_EMPLEADO=?";
        
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
            pst.execute();
            pst.close();
            mensaje = "MODIFICADO CORRECTAMENTE";
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO MODIFICADO CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
    public String eliminar1(Connection con, int id) {
        Statement stm= null;
        String sql = "DELETE EMPLEADO WHERE ID_EMPLEADO="+id;
        
        try
        {
            stm=con.createStatement();
            stm.execute(sql);
            stm.close();
            con.close();
            mensaje = "ELIMINADO CORRECTAMENTE";
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO ACTUALIZADAR CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
    public String eliminar(Connection con, int id) {
        PreparedStatement pst = null;
        String sql = "DELETE EMPLEADO WHERE ID_EMPLEADO=?";
        
        try
        {
            pst = con.prepareStatement(sql);
            pst.setInt(1, id);
            pst.execute();
            pst.close();
            mensaje = "ELIMINADO CORRECTAMENTE";
        }
        catch (SQLException e)
        {
            mensaje = "NO SE PUDO ACTUALIZADAR CORRECTAMENTE \n " + e.getMessage();
        }
        return mensaje;
    }
    public List<Empleado> obtenerLista(Connection con) {        
        Statement stm= null;
        ResultSet rs=null;

        String sql="SELECT * FROM EMPLEADO ORDER BY ID_EMPLEADO";

        List<Empleado> listaCliente= new ArrayList<Empleado>();

        try {
                stm=con.createStatement();
                rs=stm.executeQuery(sql);
                while (rs.next()) {
                        Empleado c = new Empleado();
                        c.setId_Empleado(rs.getInt(1));
                        c.setNombres(rs.getString(2));
                        c.setApellidos(rs.getString(3));
                        c.setDni(rs.getString(4));
                        c.setEstadoCivil(rs.getString(5).charAt(0));
                        c.setGenero(rs.getString(6).charAt(0));
                        c.setEdad(rs.getInt(7));
                        listaCliente.add(c);
                }
                stm.close();
                rs.close();
                con.close();
        } catch (SQLException e) {
                System.out.println("Error: Clase ClienteDaoImple, método obtener");
                e.printStackTrace();
        }

        return listaCliente;
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
    
    public int getMaxID(Connection con) {
        int id = 0;
        PreparedStatement pst = null;
        ResultSet rs = null;
        String sql = "SELECT MAX(ID_EMPLEADO)+1 FROM EMPLEADO";
        try {
            pst = con.prepareStatement(sql);
            rs = pst.executeQuery();
            if (rs.next()) {
                id = rs.getInt(1);
            }
            rs.close();
            pst.close();
            
        } catch (Exception e) {
            System.out.println("error al mostrar is" + e.getMessage());
        }
        
        return id;
    }
}
