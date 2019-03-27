
package com.rrhh.pe.test;

import com.rrhh.pe.bo.EmpleadoBO;
import com.rrhh.pe.dao.EmpleadoDAO;
import com.rrhh.pe.entity.Empleado;

/**
 *
 * @author MOISESHR
 */
public class Test {
    EmpleadoBO ebo = new EmpleadoBO();
    Empleado emp = new Empleado();
    String mensaje = "";
    
    public void insertar() {
        emp.setNombres("MOISES");
        emp.setApellidos("HUAMAN RAMOS");
        emp.setDni("41229380");
        emp.setEstadoCivil('S');
        emp.setGenero('M');
        emp.setEdad(37);
        mensaje = ebo.agregar(emp);
        System.out.println(mensaje);
    }
    
    public void modificar() {
        emp.setId_Empleado(2);
        emp.setNombres("MOISES");
        emp.setApellidos("HUAMAN RAMOS");
        emp.setDni("41229380");
        emp.setEstadoCivil('S');
        emp.setGenero('M');
        emp.setEdad(37);
        mensaje = ebo.modificar(emp);
        System.out.println(mensaje);
    }
    
    public static void main(String[] args) {
        Test test = new Test();
        test.insertar();
    }
    
}
