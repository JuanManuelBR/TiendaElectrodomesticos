package cal.example.TiendaElectrodomesticos.dtos;

import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
import java.time.LocalDateTime;

public class ControlRemotoDTO {
    private Integer id;
    private Integer numeroReferencia;
    private String nombre;
    private String marca;
    private Double alcance;
    private LocalDateTime fechaVenta;
    private TelevisorIdDTO televisor; // solo id del televisor

    public ControlRemotoDTO(ControlRemoto c) {
        this.id = c.getId();
        this.numeroReferencia = c.getNumeroReferencia();
        this.nombre = c.getNombre();
        this.marca = c.getMarca();
        this.alcance = c.getAlcance();
        this.fechaVenta = c.getFechaVenta();

        if (c.getTelevisor() != null) {
            this.televisor = new TelevisorIdDTO(c.getTelevisor().getId());
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
    public Double getAlcance() { return alcance; }
    public void setAlcance(Double alcance) { this.alcance = alcance; }
    public LocalDateTime getFechaVenta() { return fechaVenta; }
    public void setFechaVenta(LocalDateTime fechaVenta) { this.fechaVenta = fechaVenta; }
    public TelevisorIdDTO getTelevisor() { return televisor; }
    public void setTelevisor(TelevisorIdDTO televisor) { this.televisor = televisor; }
}
