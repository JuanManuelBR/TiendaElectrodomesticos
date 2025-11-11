package cal.example.TiendaElectrodomesticos.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import lombok.Getter;
import lombok.Setter;
import java.time.LocalDateTime;
import com.fasterxml.jackson.annotation.JsonProperty;

@Getter
@Setter
@Entity
@Table(name = "controles_remotos")
public class ControlRemoto {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(updatable = false, nullable = false)
    @JsonProperty(access = JsonProperty.Access.READ_ONLY)
    private Integer id;

    @NotNull(message = "El número de referencia es obligatorio")
    @Positive(message = "El número de referencia debe ser un número positivo")
    @Column(name = "numero_referencia", unique = true)
    private Integer numeroReferencia;

    @NotBlank(message = "El nombre del control es obligatorio")
    private String nombre;

    @PositiveOrZero(message = "El alcance no puede ser negativo")
    private double alcance;

    @NotBlank(message = "La marca es obligatoria")
    private String marca;

    @NotNull(message = "La fecha de venta es obligatoria")
    private LocalDateTime fechaVenta;

    @ManyToOne
    @JoinColumn(name = "televisor_id", nullable = true)
    @JsonIgnoreProperties({"controles", "nombre", "numeroReferencia", "precio", "marca", "fechaCreacion"})
    private Televisor televisor;

    public ControlRemoto() {}

    public ControlRemoto(Integer numeroReferencia, String nombre, double alcance, String marca, LocalDateTime fechaVenta) {
        this.numeroReferencia = numeroReferencia;
        this.nombre = nombre;
        this.alcance = alcance;
        this.marca = marca;
        this.fechaVenta = fechaVenta;
    }

    public Integer getId() {
        return id;
    }
}
