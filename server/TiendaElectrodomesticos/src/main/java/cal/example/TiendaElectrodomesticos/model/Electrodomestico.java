package cal.example.TiendaElectrodomesticos.model;
import com.fasterxml.jackson.annotation.JsonSubTypes;
import com.fasterxml.jackson.annotation.JsonTypeInfo;
import java.time.LocalDateTime;

@JsonTypeInfo(
        use = JsonTypeInfo.Id.NAME,           // Usamos un "nombre" para identificar el tipo
        include = JsonTypeInfo.As.PROPERTY,   // Se incluye como un campo del JSON
        property = "tipo"                     // El campo se llamará "tipo"
)
@JsonSubTypes({
        @JsonSubTypes.Type(value = Televisor.class, name = "televisor")
})

public abstract class Electrodomestico {

    private String nombre;
    private int codigo;
    private double precio;
    private String marca;
    private LocalDateTime fechaCreacion;

    // Constructor vacío (necesario para frameworks como Spring, Hibernate, etc.)
    public Electrodomestico() {
    }

    // Constructor con todos los atributos
    public Electrodomestico(String nombre, int codigo, double precio, String marca, LocalDateTime fechaCreacion) {
        this.nombre = nombre;
        this.codigo = codigo;
        this.precio = precio;
        this.marca = marca;
        this.fechaCreacion = fechaCreacion;
    }

    // Getters y Setters
    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    public double getPrecio() {
        return precio;
    }

    public void setPrecio(double precio) {
        this.precio = precio;
    }

    public String getMarca() {
        return marca;
    }

    public void setMarca(String marca) {
        this.marca = marca;
    }

    public LocalDateTime getFechaCreacion() {
        return fechaCreacion;
    }

    public void setFechaCreacion(LocalDateTime fechaCreacion) {
        this.fechaCreacion = fechaCreacion;
    }

    // toString() para depuración


    @Override
    public String toString() {
        return "Electrodomestico{" +
                "nombre='" + nombre + '\'' +
                ", codigo='" + codigo + '\'' +
                ", precio=" + precio +
                ", marca='" + marca + '\'' +
                ", fechaCreacion=" + fechaCreacion +
                '}';
    }

}
