package cal.example.TiendaElectrodomesticos.model;


import com.fasterxml.jackson.annotation.JsonManagedReference;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;


@Setter
@Getter
public class Televisor extends Electrodomestico{
    @JsonManagedReference
    private ControlRemoto controlRemoto;
    // Constructor vacío
    public Televisor() {
        super();
    }
    public Televisor(String nombre, int codigo, double precio, String marca, LocalDateTime fechaCreacion) {
        super(nombre, codigo, precio, marca, fechaCreacion);
    }


}