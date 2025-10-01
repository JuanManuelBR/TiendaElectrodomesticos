package cal.example.TiendaElectrodomesticos.service;

import cal.example.TiendaElectrodomesticos.model.Electrodomestico;
import cal.example.TiendaElectrodomesticos.model.Televisor;
import org.springframework.stereotype.Service;

import java.text.Normalizer;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Service
public class ServiceElectrodomestico {

    private static ServiceElectrodomestico serviceElectrodomestico;
    private List<Electrodomestico> electrodomesticos = new ArrayList<>();

    private ServiceElectrodomestico(){}


    public synchronized static ServiceElectrodomestico getInstance() {

        if (serviceElectrodomestico == null) {
            serviceElectrodomestico = new ServiceElectrodomestico();
        }

        return serviceElectrodomestico;
    }



    public Electrodomestico addElectrodomestico(Electrodomestico e) {
        // Verificar si ya existe un electrodoméstico con el mismo id
        for (Electrodomestico existente : electrodomesticos) {
            if (existente.getCodigo() == e.getCodigo()) {
                throw new IllegalArgumentException("Ya existe un electrodoméstico con el id: " + e.getCodigo());
            }
        }

        electrodomesticos.add(e);
        return e;
    }


    public List<Televisor> listTelevisores() {
        List<Televisor> televisores = new ArrayList<>();

        for (Electrodomestico e : electrodomesticos) {

            if (e instanceof Televisor) {
                Televisor t = (Televisor) e;
                televisores.add(t);
            }
        }
        return televisores;
    }

    public Electrodomestico buscarElectrodomestico(int codigo) {
        for (Electrodomestico elc : electrodomesticos) {
            if (elc.getCodigo()==(codigo)) {
                return elc;
            }
        }
        return null;
    }


    public boolean deleteElectrodomestico(int codigo) {
        Electrodomestico e = (Electrodomestico) buscarElectrodomestico(codigo);
        if (e != null) {

            electrodomesticos.remove(e);
            return true;
        }
        return false;
    }

    public boolean putTelevisor(
            int codigo,
            String nombre,
            double precio,
            String marca,
            LocalDateTime fechaCreacion) {

        List<Televisor> televisores = listTelevisores();

        for (Televisor t : televisores) {
            if (t.getCodigo() == codigo) {

                t.setNombre(nombre);
                t.setPrecio(precio);
                t.setMarca(marca);
                t.setFechaCreacion(fechaCreacion);

                return true;
            }
        }
        return false;
    }

    public List<String> getMarcasTelevisores() {
        List<String> marcas = new ArrayList<>();

        for (Electrodomestico e : electrodomesticos) {
            if (e instanceof Televisor) {
                String marca = ((Televisor) e).getMarca();
                if (marca != null) {
                    // Normalizamos (quitamos tildes, case insensitive)
                    String normalizada = Normalizer.normalize(marca, Normalizer.Form.NFD)
                            .replaceAll("\\p{M}", "")
                            .toLowerCase();

                    // Evitar duplicados
                    if (marcas.stream().noneMatch(m -> m.equalsIgnoreCase(normalizada))) {
                        marcas.add(marca);
                    }
                }
            }
        }
        return marcas;
    }

    public List<Televisor> filtrarTelevisoresPorMarca(String marcaBuscada) {
        List<Televisor> filtrados = new ArrayList<>();

        String normalizadaBuscada = Normalizer.normalize(marcaBuscada, Normalizer.Form.NFD)
                .replaceAll("\\p{M}", "")
                .toLowerCase();

        for (Electrodomestico e : electrodomesticos) {
            if (e instanceof Televisor) {
                Televisor t = (Televisor) e;

                String normalizada = Normalizer.normalize(t.getMarca(), Normalizer.Form.NFD)
                        .replaceAll("\\p{M}", "")
                        .toLowerCase();

                if (normalizada.equals(normalizadaBuscada)) {
                    filtrados.add(t);
                }
            }
        }
        return filtrados;
    }





}
