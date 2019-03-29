/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.rrhh.pe.bo;

import com.rrhh.pe.dao.EmpleadoDAO;
import com.rrhh.pe.db.Conexion;
import com.rrhh.pe.entity.Empleado;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JTable;

/**
 *
 * @author MOISESHR
 */
public class EmpleadoBO {
    private String mensaje = "";
    private EmpleadoDAO edao = new EmpleadoDAO();
    
    public String agregar(Empleado emp) {
        Connection conn = Conexion.getConnection();        
        try {
            mensaje = edao.agregar(conn, emp);
            //conn.rollback();
        } catch (Exception e) {
            mensaje = mensaje + " " + e.getMessage();
        }finally{
            try {
                if (conn != null) {
                    conn.close();
                }
            } catch (Exception e) {
                mensaje = mensaje + " " + e.getMessage();
            }
        }
        
        return mensaje;
    }
    public String modificar(Empleado emp) {
        Connection conn = Conexion.getConnection();        
        try {
            mensaje = edao.modificar(conn, emp);
            //conn.rollback();
        } catch (Exception e) {
            mensaje = mensaje + " " + e.getMessage();
        }finally{
            try {
                if (conn != null) {
                    conn.close();
                }
            } catch (Exception e) {
                mensaje = mensaje + " " + e.getMessage();
            }
        }
        
        return mensaje;
    }
    public String eliminar(int id) {
        Connection conn = Conexion.getConnection();        
        try {
            mensaje = edao.eliminar(conn, id);
            
        } catch (Exception e) {
            mensaje = mensaje + " " + e.getMessage();
        }finally{
            try {
                if (conn != null) {
                    conn.close();
                }
            } catch (Exception e) {
                mensaje = mensaje + " " + e.getMessage();
            }
        }
        
        return mensaje;
    }
    public List<Empleado> obtenerLista() {
        Connection conn = Conexion.getConnection();
        List<Empleado> listaCliente= new ArrayList<Empleado>();
        
        try {
            listaCliente = edao.obtenerLista(conn);
            conn.close();
        } catch (Exception ex) {
            //Logger.getLogger(EmpleadoBO.class.getName()).log(Level.SEVERE, null, ex);
            System.out.println(ex.getMessage());
        }
        return listaCliente;
    }
    public void listar(JTable tabla) {
        Connection conn = Conexion.getConnection();
        edao.listar(conn, tabla);
        try {
            conn.close();
        } catch (SQLException ex) {
            //Logger.getLogger(EmpleadoBO.class.getName()).log(Level.SEVERE, null, ex);
            System.out.println(ex.getMessage());
        }
    }
    
    public int getMaxID() {
        Connection conn = Conexion.getConnection();
        int id = edao.getMaxID(conn);
        try {
            conn.close();
        } catch (SQLException ex) {            
            System.out.println(ex.getMessage());
        }
        return id;
    }
}
