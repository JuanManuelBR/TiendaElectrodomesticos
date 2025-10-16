package cal.example.TiendaElectrodomesticos.service;

import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
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
    private List<ControlRemoto> controles = new ArrayList<>();
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

    public ControlRemoto addControlRemoto(ControlRemoto c) {
        for (ControlRemoto existente : controles) {
            if (existente.getCodigo() == c.getCodigo()) {
                throw new IllegalArgumentException("Ya existe un control con el código: " + c.getCodigo());
            }
        }
        controles.add(c);
        return c;
    }

    public List<ControlRemoto> listControles() {
        return controles;
    }

    public ControlRemoto buscarControlRemoto(int codigo) {
        for (ControlRemoto c : controles) {
            if (c.getCodigo() == codigo) {
                return c;
            }
        }
        return null;
    }

    public boolean deleteControlRemoto(int codigo) {
        ControlRemoto c = buscarControlRemoto(codigo);
        if (c != null) {
            controles.remove(c);
            return true;
        }
        return false;
    }

    public boolean putControlRemoto(
            int codigo,
            String nombre,
            double alcance,
            String marca,
            LocalDateTime fechaVenta
    ) {
        for (ControlRemoto c : controles) {
            if (c.getCodigo() == codigo) {
                c.setNombre(nombre);
                c.setAlcance(alcance);
                c.setMarca(marca);
                c.setFechaVenta(fechaVenta);
                return true;
            }
        }
        return false;
    }

    public List<String> getMarcasControles() {
        List<String> marcas = new ArrayList<>();
        for (ControlRemoto c : controles) {
            if (c.getMarca() != null) {
                String normalizada = Normalizer.normalize(c.getMarca(), Normalizer.Form.NFD)
                        .replaceAll("\\p{M}", "")
                        .toLowerCase();
                if (marcas.stream().noneMatch(m -> m.equalsIgnoreCase(normalizada))) {
                    marcas.add(c.getMarca());
                }
            }
        }
        return marcas;
    }

    public List<ControlRemoto> filtrarControlesPorMarca(String marcaBuscada) {
        List<ControlRemoto> filtrados = new ArrayList<>();

        String normalizadaBuscada = Normalizer.normalize(marcaBuscada, Normalizer.Form.NFD)
                .replaceAll("\\p{M}", "")
                .toLowerCase();

        for (ControlRemoto c : controles) {
            String normalizada = Normalizer.normalize(c.getMarca(), Normalizer.Form.NFD)
                    .replaceAll("\\p{M}", "")
                    .toLowerCase();

            if (normalizada.equals(normalizadaBuscada)) {
                filtrados.add(c);
            }
        }

        return filtrados;
    }

    public boolean asignarControlATelevisor(int codigoTelevisor, int codigoControl) {
        // Buscar televisor
        Electrodomestico e = buscarElectrodomestico(codigoTelevisor);
        if (e == null || !(e instanceof Televisor)) {
            throw new IllegalArgumentException("No existe un televisor con el código " + codigoTelevisor);
        }

        Televisor tv = (Televisor) e;

        // Verificar si ya tiene un control asignado
        if (tv.getControlRemoto() != null) {
            throw new IllegalArgumentException("El televisor ya tiene un control remoto asignado.");
        }

        // Buscar control remoto
        ControlRemoto control = buscarControlRemoto(codigoControl);
        if (control == null) {
            throw new IllegalArgumentException("No existe un control remoto con el código " + codigoControl);
        }

        // Verificar si el control ya está vinculado
        if (control.getTelevisor() != null) {
            throw new IllegalArgumentException("El control remoto ya está vinculado a otro televisor.");
        }

        // Asignar vínculos
        tv.setControlRemoto(control);
        control.setTelevisor(tv);

        return true;
    }

    public List<ControlRemoto> listControlesDisponibles() {
        List<ControlRemoto> disponibles = new ArrayList<>();
        for (ControlRemoto c : controles) {
            if (c.getTelevisor() == null) {
                disponibles.add(c);
            }
        }
        return disponibles;
    }
}
