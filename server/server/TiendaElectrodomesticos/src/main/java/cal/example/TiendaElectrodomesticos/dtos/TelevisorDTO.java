package cal.example.TiendaElectrodomesticos.dtos;

import cal.example.TiendaElectrodomesticos.model.Televisor;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class TelevisorDTO {
    private Integer id;
    private Integer numeroReferencia;
    private String nombre;
    private String marca;
    private Double precio;
    private LocalDateTime fechaCreacion;
    private List<ControlRemotoDTO> controles; // lista de controles asociados (solo campos básicos)
    private List<Integer> tvboxNumeroReferencias = new ArrayList<>();

    public List<Integer> getTvboxNumeroReferencias() {
        return tvboxNumeroReferencias;
    }

    public void setTvboxNumeroReferencias(List<Integer> tvboxNumeroReferencias) {
        this.tvboxNumeroReferencias = tvboxNumeroReferencias;
    }
    public TelevisorDTO(Televisor t) {
        this.id = t.getId();
        this.numeroReferencia = t.getNumeroReferencia();
        this.nombre = t.getNombre();
        this.marca = t.getMarca();
        this.precio = t.getPrecio();
        this.fechaCreacion = t.getFechaCreacion();

        if (t.getControles() != null) {
            this.controles = t.getControles()
                    .stream()
                    .map(ControlRemotoDTO::new)
                    .collect(Collectors.toList());
        }
    }

    // getters y setters
    public Integer getId() { return id; }
    public void setId(Integer id) { this.id = id; }
    public Integer getNumeroReferencia() { return numeroReferencia; }
    public void setNumeroReferencia(Integer numeroReferencia) { this.numeroReferencia = numeroReferencia; }
    public String getNombre() { return nombre; }
    public void setNombre(String nombre) { this.nombre = nombre; }
    public String getMarca() { return marca; }
    public void setMarca(String marca) { this.marca = marca; }
    public Double getPrecio() { return precio; }
    public void setPrecio(Double precio) { this.precio = precio; }
    public LocalDateTime getFechaCreacion() { return fechaCreacion; }
    public void setFechaCreacion(LocalDateTime fechaCreacion) { this.fechaCreacion = fechaCreacion; }
    public List<ControlRemotoDTO> getControles() { return controles; }
    public void setControles(List<ControlRemotoDTO> controles) { this.controles = controles; }
}
