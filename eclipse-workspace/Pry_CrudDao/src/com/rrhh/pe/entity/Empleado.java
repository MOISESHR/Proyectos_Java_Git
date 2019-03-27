/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package com.rrhh.pe.entity;

/**
 *
 * @author MOISESHR
 */
public class Empleado {
    private int id_Empleado;
    private String nombres;
    private String apellidos;
    private String dni;
    private char estadoCivil;
    private char genero;
    private int edad;

    public Empleado() {
    }

    public Empleado(int id_Empleado, String nombres, String apellidos, String dni, char estadoCivil, char genero, int edad) {
        this.id_Empleado = id_Empleado;
        this.nombres = nombres;
        this.apellidos = apellidos;
        this.dni = dni;
        this.estadoCivil = estadoCivil;
        this.genero = genero;
        this.edad = edad;
    }

    public int getId_Empleado() {
        return id_Empleado;
    }

    public void setId_Empleado(int id_Empleado) {
        this.id_Empleado = id_Empleado;
    }

    public String getNombres() {
        return nombres;
    }

    public void setNombres(String nombres) {
        this.nombres = nombres;
    }

    public String getApellidos() {
        return apellidos;
    }

    public void setApellidos(String apellidos) {
        this.apellidos = apellidos;
    }

    public String getDni() {
        return dni;
    }

    public void setDni(String dni) {
        this.dni = dni;
    }

    public char getEstadoCivil() {
        return estadoCivil;
    }

    public void setEstadoCivil(char estadoCivil) {
        this.estadoCivil = estadoCivil;
    }

    public char getGenero() {
        return genero;
    }

    public void setGenero(char genero) {
        this.genero = genero;
    }

    public int getEdad() {
        return edad;
    }

    public void setEdad(int edad) {
        this.edad = edad;
    }

    @Override
    public String toString() {
        return "Empleado{" + "id_Empleado=" + id_Empleado + ", nombres=" + nombres + ", apellidos=" + apellidos + ", dni=" + dni + ", estadoCivil=" + estadoCivil + ", genero=" + genero + ", edad=" + edad + '}';
    }

    
}
