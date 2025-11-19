package cal.example.TiendaElectrodomesticos.model;

import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import java.time.LocalDateTime;
import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Entity
@Table(name = "tvboxes")
public class TvBox {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(updatable = false, nullable = false)
    @JsonProperty(access = JsonProperty.Access.READ_ONLY)
    private Integer id;

    @NotNull(message = "El número de referencia es obligatorio")
    @Positive(message = "El número de referencia debe ser positivo")
    @Column(name = "numero_referencia", unique = true)
    private Integer numeroReferencia;

    @NotBlank(message = "El nombre es obligatorio")
    @Size(min = 2, max = 100)
    private String nombre;

    @Positive(message = "El precio debe ser mayor a 0")
    private double precio;

    @NotBlank(message = "La marca es obligatoria")
    private String marca;

    @NotNull(message = "La fecha de creación es obligatoria")
    private LocalDateTime fechaCreacion;

    /**
     * No hay relación JPA directa porque TVBox vive en otra BD.
     * Guardamos aquí el numeroReferencia del Televisor al que pertenece (si aplica).
     */
    private Integer televisorNumeroReferencia;

    public TvBox() {}
}
