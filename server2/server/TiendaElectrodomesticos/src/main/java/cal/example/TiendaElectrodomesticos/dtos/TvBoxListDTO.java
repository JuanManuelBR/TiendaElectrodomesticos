package cal.example.TiendaElectrodomesticos.dtos;

import lombok.Data;

@Data
public class TvBoxListDTO {
    private Integer id;

    private Integer numeroReferencia;
    private String nombre;
    private Double precio;
    private String marca;
    private String fechaCreacion;
    private Integer televisorNumeroReferencia;
}
