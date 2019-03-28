
package com.rrhh.pe.test;

import com.rrhh.pe.bo.EmpleadoBO;
import com.rrhh.pe.dao.EmpleadoDAO;
import com.rrhh.pe.entity.Empleado;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author MOISESHR
 */
public class Test {
    EmpleadoBO ebo = new EmpleadoBO();
    Empleado emp = new Empleado();
    String mensaje = "";
    
    public void insertar() {
        emp.setNombres("JOSUE");
        emp.setApellidos("VILLA RAMOS");
        emp.setDni("41229380");
        emp.setEstadoCivil('S');
        emp.setGenero('M');
        emp.setEdad(37);
        mensaje = ebo.agregar(emp);
        System.out.println(mensaje);
    }
    
    public void modificar() {
        emp.setId_Empleado(17);
        emp.setNombres("PEDRO");
        emp.setApellidos("CALDERON ORTIZ");
        emp.setDni("555555");
        emp.setEstadoCivil('S');
        emp.setGenero('M');
        emp.setEdad(30);
        mensaje = ebo.modificar(emp);
        System.out.println(mensaje);
    }
    
    public void eliminar() {
        mensaje = ebo.eliminar(18);
        System.out.println(mensaje);
    }
    public void obtenerLista() {
        List<Empleado> listaCliente= new ArrayList<Empleado>();
        listaCliente = ebo.obtenerLista();
        System.out.println("Listo");
    }
    public static void main(String[] args) {
        Test test = new Test();
        test.obtenerLista();
    }
    
}
