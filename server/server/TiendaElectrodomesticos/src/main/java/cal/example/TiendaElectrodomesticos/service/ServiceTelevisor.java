package cal.example.TiendaElectrodomesticos.service;

import cal.example.TiendaElectrodomesticos.dtos.ControlRemotoDTO;
import cal.example.TiendaElectrodomesticos.dtos.TelevisorControlDTO;
import cal.example.TiendaElectrodomesticos.dtos.TelevisorDTO;
import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
import cal.example.TiendaElectrodomesticos.model.Televisor;
import cal.example.TiendaElectrodomesticos.repository.ControlRemotoRepository;
import cal.example.TiendaElectrodomesticos.repository.TelevisorRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.text.Normalizer;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class ServiceTelevisor {

    private final TelevisorRepository televisorRepository;
    private final ControlRemotoRepository controlRepository;

    public ServiceTelevisor(TelevisorRepository televisorRepository,
                            ControlRemotoRepository controlRepository) {
        this.televisorRepository = televisorRepository;
        this.controlRepository = controlRepository;
    }

    // ==================== TELEVISOR ====================
    @Transactional
    public TelevisorDTO addTelevisor(Televisor t) {
        if (t == null || t.getNumeroReferencia() == null)
            throw new IllegalArgumentException("El televisor o su número de referencia no pueden ser nulos.");

        televisorRepository.findByNumeroReferencia(t.getNumeroReferencia())
                .ifPresent(existing -> {
                    throw new IllegalArgumentException("⚠️ Ya existe un televisor con el número de referencia: "
                            + t.getNumeroReferencia());
                });

        Televisor saved = televisorRepository.save(t);
        return new TelevisorDTO(saved);
    }

    public List<TelevisorDTO> listTelevisores() {
        return televisorRepository.findAll()
                .stream()
                .map(TelevisorDTO::new)
                .collect(Collectors.toList());
    }

    public TelevisorDTO buscarTelevisor(int numeroReferencia) {
        return televisorRepository.findByNumeroReferencia(numeroReferencia)
                .map(TelevisorDTO::new)
                .orElse(null);
    }

    @Transactional
    public boolean deleteTelevisor(int numeroReferencia) {
        Optional<Televisor> opt = televisorRepository.findByNumeroReferencia(numeroReferencia);
        if (opt.isPresent()) {
            Televisor tv = opt.get();

            // Desvincular todos los controles
            for (ControlRemoto c : tv.getControles()) {
                c.setTelevisor(null);
                controlRepository.save(c);
            }

            televisorRepository.delete(tv);
            return true;
        }
        return false;
    }

    @Transactional
    public boolean updateTelevisor(int numeroReferencia, String nombre, double precio, String marca, LocalDateTime fechaCreacion) {
        Optional<Televisor> opt = televisorRepository.findByNumeroReferencia(numeroReferencia);
        if (opt.isPresent()) {
            Televisor t = opt.get();
            t.setNombre(nombre);
            t.setPrecio(precio);
            t.setMarca(marca);
            t.setFechaCreacion(fechaCreacion);
            televisorRepository.save(t);
            return true;
        }
        return false;
    }

    public List<String> getMarcasTelevisores() {
        List<String> marcas = new ArrayList<>();
        for (Televisor t : televisorRepository.findAll()) {
            if (t.getMarca() != null) {
                String normalizada = Normalizer.normalize(t.getMarca(), Normalizer.Form.NFD)
                        .replaceAll("\\p{M}", "")
                        .toLowerCase();
                if (marcas.stream().noneMatch(m -> m.equalsIgnoreCase(normalizada))) {
                    marcas.add(t.getMarca());
                }
            }
        }
        return marcas;
    }

    public List<TelevisorDTO> filtrarTelevisoresPorMarca(String marcaBuscada) {
        String normalizadaBuscada = Normalizer.normalize(marcaBuscada, Normalizer.Form.NFD)
                .replaceAll("\\p{M}", "")
                .toLowerCase();
        return televisorRepository.findAll()
                .stream()
                .filter(t -> t.getMarca() != null &&
                        Normalizer.normalize(t.getMarca(), Normalizer.Form.NFD)
                                .replaceAll("\\p{M}", "")
                                .toLowerCase()
                                .equals(normalizadaBuscada))
                .map(TelevisorDTO::new)
                .collect(Collectors.toList());
    }

    // ==================== CONTROL REMOTO ====================
    @Transactional
    public ControlRemotoDTO addControlRemoto(ControlRemoto c) {
        if (c == null || c.getNumeroReferencia() == null)
            throw new IllegalArgumentException("El control remoto o su número de referencia no pueden ser nulos.");

        controlRepository.findByNumeroReferencia(c.getNumeroReferencia())
                .ifPresent(existing -> {
                    throw new IllegalArgumentException("Ya existe un control remoto con el número de referencia: "
                            + c.getNumeroReferencia());
                });

        if (c.getTelevisor() != null && c.getTelevisor().getNumeroReferencia() != null) {
            Televisor tv = televisorRepository.findByNumeroReferencia(c.getTelevisor().getNumeroReferencia())
                    .orElseThrow(() -> new IllegalArgumentException(
                            "No existe un televisor con el número de referencia: " + c.getTelevisor().getNumeroReferencia()));
            c.setTelevisor(tv);
        }

        ControlRemoto saved = controlRepository.save(c);
        return new ControlRemotoDTO(saved);
    }

    public List<ControlRemotoDTO> listControles() {
        return controlRepository.findAll()
                .stream()
                .map(ControlRemotoDTO::new)
                .collect(Collectors.toList());
    }

    public ControlRemotoDTO buscarControlRemoto(int numeroReferencia) {
        return controlRepository.findByNumeroReferencia(numeroReferencia)
                .map(ControlRemotoDTO::new)
                .orElse(null);
    }

    @Transactional
    public boolean deleteControlRemoto(int numeroReferencia) {
        Optional<ControlRemoto> opt = controlRepository.findByNumeroReferencia(numeroReferencia);
        if (opt.isPresent()) {
            ControlRemoto c = opt.get();
            if (c.getTelevisor() != null) {
                Televisor tv = c.getTelevisor();
                tv.getControles().remove(c);
                c.setTelevisor(null);
                televisorRepository.save(tv);
            }
            controlRepository.delete(c);
            return true;
        }
        return false;
    }

    @Transactional
    public boolean updateControlRemoto(int numeroReferencia, String nombre, double alcance, String marca, LocalDateTime fechaVenta) {
        Optional<ControlRemoto> opt = controlRepository.findByNumeroReferencia(numeroReferencia);
        if (opt.isPresent()) {
            ControlRemoto c = opt.get();
            c.setNombre(nombre);
            c.setAlcance(alcance);
            c.setMarca(marca);
            c.setFechaVenta(fechaVenta);
            controlRepository.save(c);
            return true;
        }
        return false;
    }

    public List<String> getMarcasControles() {
        List<String> marcas = new ArrayList<>();
        for (ControlRemoto c : controlRepository.findAll()) {
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

    public List<ControlRemotoDTO> filtrarControlesPorMarca(String marcaBuscada) {
        String normalizadaBuscada = Normalizer.normalize(marcaBuscada, Normalizer.Form.NFD)
                .replaceAll("\\p{M}", "")
                .toLowerCase();

        return controlRepository.findAll()
                .stream()
                .filter(c -> c.getMarca() != null &&
                        Normalizer.normalize(c.getMarca(), Normalizer.Form.NFD)
                                .replaceAll("\\p{M}", "")
                                .toLowerCase()
                                .equals(normalizadaBuscada))
                .map(ControlRemotoDTO::new)
                .collect(Collectors.toList());
    }

    // ==================== ASIGNACIÓN ====================
    @Transactional
    public boolean asignarControlATelevisor(int numeroReferenciaTelevisor, int numeroReferenciaControl) {
        Televisor tv = televisorRepository.findByNumeroReferencia(numeroReferenciaTelevisor)
                .orElseThrow(() -> new IllegalArgumentException(
                        "No existe un televisor con el número de referencia: " + numeroReferenciaTelevisor));

        ControlRemoto control = controlRepository.findByNumeroReferencia(numeroReferenciaControl)
                .orElseThrow(() -> new IllegalArgumentException(
                        "No existe un control remoto con el número de referencia: " + numeroReferenciaControl));

        if (control.getTelevisor() != null) {
            throw new IllegalArgumentException("El control remoto ya está vinculado a otro televisor.");
        }

        control.setTelevisor(tv);
        tv.getControles().add(control);

        controlRepository.save(control);
        televisorRepository.save(tv);
        return true;
    }

    public List<ControlRemotoDTO> listControlesDisponibles() {
        return controlRepository.findAll()
                .stream()
                .filter(c -> c.getTelevisor() == null)
                .map(ControlRemotoDTO::new)
                .collect(Collectors.toList());
    }


    public List<ControlRemotoDTO> obtenerControlesPorMarcaConTelevisor(String marca) {
        return controlRepository.findControlesPorMarcaConTelevisor(marca)
                .stream()
                .map(ControlRemotoDTO::new)
                .collect(Collectors.toList());
    }

    @Transactional
    public boolean desasignarControlDeTelevisor(int numeroReferenciaTelevisor, int numeroReferenciaControl) {
        Televisor tv = televisorRepository.findByNumeroReferencia(numeroReferenciaTelevisor)
                .orElseThrow(() -> new IllegalArgumentException(
                        "No existe un televisor con el número de referencia: " + numeroReferenciaTelevisor));

        ControlRemoto control = controlRepository.findByNumeroReferencia(numeroReferenciaControl)
                .orElseThrow(() -> new IllegalArgumentException(
                        "No existe un control remoto con el número de referencia: " + numeroReferenciaControl));

        if (control.getTelevisor() == null || control.getTelevisor().getNumeroReferencia() != numeroReferenciaTelevisor) {
            throw new IllegalArgumentException("El control no está asignado a este televisor.");
        }

        // Quitar la relación en ambos sentidos
        control.setTelevisor(null);
        tv.getControles().remove(control);

        controlRepository.save(control);
        televisorRepository.save(tv);

        return true;
    }

    public List<TelevisorControlDTO> obtenerControlesPorMarca(String marca) {
        List<TelevisorControlDTO> resultado = televisorRepository.findControlesPorMarca(marca);

        if (resultado == null || resultado.isEmpty()) {
            throw new IllegalArgumentException("No se encontraron televisores con controles de la marca: " + marca);
        }

        return resultado;
    }


}
