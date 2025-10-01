package cal.example.TiendaElectrodomesticos.model;


import java.time.LocalDateTime;


public class Televisor extends Electrodomestico{

    // Constructor vacío
    public Televisor() {
        super();
    }
    public Televisor(String nombre, int codigo, double precio, String marca, LocalDateTime fechaCreacion) {
        super(nombre, codigo, precio, marca, fechaCreacion);
    }

}