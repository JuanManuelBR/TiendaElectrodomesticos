package cal.example.TiendaElectrodomesticos.dtos;

public class TelevisorControlDTO {
    private Integer idTelevisor;
    private String nombreTelevisor;
    private String marcaTelevisor;
    private Integer numeroReferenciaControl;
    private String marcaControl;
    private double alcance;

    public TelevisorControlDTO(Integer idTelevisor, String nombreTelevisor, String marcaTelevisor,
                               Integer numeroReferenciaControl, String marcaControl, double alcance) {
        this.idTelevisor = idTelevisor;
        this.nombreTelevisor = nombreTelevisor;
        this.marcaTelevisor = marcaTelevisor;
        this.numeroReferenciaControl = numeroReferenciaControl;
        this.marcaControl = marcaControl;
        this.alcance = alcance;
    }

    // Getters
    public Integer getIdTelevisor() { return idTelevisor; }
    public String getNombreTelevisor() { return nombreTelevisor; }
    public String getMarcaTelevisor() { return marcaTelevisor; }
    public Integer getNumeroReferenciaControl() { return numeroReferenciaControl; }
    public String getMarcaControl() { return marcaControl; }
    public double getAlcance() { return alcance; }
}
