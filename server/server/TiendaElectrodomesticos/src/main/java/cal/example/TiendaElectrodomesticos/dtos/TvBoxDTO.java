package cal.example.TiendaElectrodomesticos.dtos;

import java.time.LocalDateTime;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class TvBoxDTO {
    private Integer id;
    private Integer numeroReferencia;
    private String nombre;
    private double precio;
    private String marca;
    private LocalDateTime fechaCreacion;
    private Integer televisorNumeroReferencia;
}
