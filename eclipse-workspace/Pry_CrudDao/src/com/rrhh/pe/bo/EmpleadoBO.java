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
            conn.rollback();
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
            conn.rollback();
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
            conn.rollback();
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
    public void listar() {
        
    }
}
