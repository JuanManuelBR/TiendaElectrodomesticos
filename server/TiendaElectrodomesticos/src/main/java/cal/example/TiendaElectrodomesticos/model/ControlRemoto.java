package cal.example.TiendaElectrodomesticos.model;

import com.fasterxml.jackson.annotation.JsonBackReference;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

@Setter
@Getter
public class ControlRemoto {

    // Getters y setters
    private int codigo;
    private double alcance;
    private String nombre;
    private LocalDateTime fechaVenta;
    private String marca;

    @JsonBackReference
    private Televisor televisor;

    public ControlRemoto() {
    }

    public ControlRemoto(int codigo, double alcance, String nombre, LocalDateTime fechaVenta, String marca) {
        this.codigo = codigo;
        this.alcance = alcance;
        this.nombre = nombre;
        this.fechaVenta = fechaVenta;
        this.marca = marca;
    }

}
