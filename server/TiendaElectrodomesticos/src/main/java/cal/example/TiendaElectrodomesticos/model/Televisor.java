package cal.example.TiendaElectrodomesticos.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonManagedReference;
import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import lombok.Getter;
import lombok.Setter;
import java.time.LocalDateTime;
import java.util.HashSet;
import java.util.Set;
import com.fasterxml.jackson.annotation.JsonProperty;

@Getter
@Setter
@Entity
@Table(name = "televisores")
public class Televisor {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(updatable = false, nullable = false)
    @JsonProperty(access = JsonProperty.Access.READ_ONLY)

    private Integer id;

    @NotBlank(message = "El nombre del televisor es obligatorio")
    @Size(min = 3, max = 100, message = "El nombre debe tener entre 3 y 100 caracteres")
    private String nombre;

    @NotNull(message = "El número de referencia es obligatorio")
    @Positive(message = "El número de referencia debe ser un número positivo")
    @Column(name = "numero_referencia", unique = true)
    private Integer numeroReferencia;

    @Positive(message = "El precio debe ser mayor a 0")
    private double precio;

    @NotBlank(message = "La marca es obligatoria")
    private String marca;

    @NotNull(message = "La fecha de creación es obligatoria")
    private LocalDateTime fechaCreacion;

    @OneToMany(mappedBy = "televisor")
    @JsonIgnoreProperties({ "nombre", "numeroReferencia", "alcance", "marca", "fechaVenta", "Televisor"})
    private Set<ControlRemoto> controles = new HashSet<>();

    public Televisor() {}

    public Televisor(String nombre, int numeroReferencia, double precio, String marca, LocalDateTime fechaCreacion) {
        this.nombre = nombre;
        this.numeroReferencia = numeroReferencia;
        this.precio = precio;
        this.marca = marca;
        this.fechaCreacion = fechaCreacion;
    }

    public Integer getId() {
        return id;
    }
}
