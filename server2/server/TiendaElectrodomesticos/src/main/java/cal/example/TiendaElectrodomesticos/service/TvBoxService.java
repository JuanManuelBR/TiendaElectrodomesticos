package cal.example.TiendaElectrodomesticos.service;

import cal.example.TiendaElectrodomesticos.dtos.TvBoxListDTO;
import cal.example.TiendaElectrodomesticos.model.TvBox;
import cal.example.TiendaElectrodomesticos.repository.TvBoxRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class TvBoxService {

    private final TvBoxRepository repo;

    public TvBoxService(TvBoxRepository repo) {
        this.repo = repo;
    }

    public TvBox create(TvBox t) {
        repo.findByNumeroReferencia(t.getNumeroReferencia())
                .ifPresent(x -> { throw new IllegalArgumentException("Número de referencia ya existe: " + t.getNumeroReferencia());});
        return repo.save(t);
    }

    public TvBox getOne(Integer id) {
        return repo.findById(id).orElse(null);
    }

    public TvBox getByNumeroReferencia(Integer numeroReferencia) {
        return repo.findByNumeroReferencia(numeroReferencia).orElse(null);
    }

    public List<TvBox> getAll() {
        return repo.findAll();
    }

    @Transactional
    public TvBox update(Integer numeroReferencia, TvBox payload) {

        // Obtener el registro original por numeroReferencia
        TvBox original = repo.findByNumeroReferencia(numeroReferencia)
                .orElseThrow(() -> new RuntimeException("No existe TvBox con referencia: " + numeroReferencia));
        // Si viene null → desasignar
        if (payload.getTelevisorNumeroReferencia() == null) {
            original.setTelevisorNumeroReferencia(null);
        }


        // Mantener el ID original
        payload.setId(original.getId());

        // Asegurar que NO se cambie el numero de referencia
        payload.setNumeroReferencia(numeroReferencia);

        return repo.save(payload);
    }

    @Transactional
    public TvBox desasignarTelevisor(Integer numeroReferenciaTvBox) {

        // Buscar tvbox por su número de referencia
        TvBox tvBox = repo.findByNumeroReferencia(numeroReferenciaTvBox)
                .orElseThrow(() -> new RuntimeException(
                        "No existe TVBox con número de referencia: " + numeroReferenciaTvBox
                ));

        // Quitar la asignación
        tvBox.setTelevisorNumeroReferencia(null);

        // Guardar cambios
        return repo.save(tvBox);
    }
    public void deleteByNumeroReferencia(Integer numeroReferencia) {

        TvBox tvBox = repo.findByNumeroReferencia(numeroReferencia)
                .orElseThrow(() -> new RuntimeException(
                        "No existe un TVBox con número de referencia: " + numeroReferencia
                ));

        repo.delete(tvBox);
    }
    public List<TvBox> getAllByMarca(String marca) {
        return repo.findAllByMarca(marca);
    }

    @Transactional
    public TvBox asignarTelevisor(Integer numeroReferenciaTvBox, Integer numeroReferenciaTelevisor) {

        TvBox tvBox = repo.findByNumeroReferencia(numeroReferenciaTvBox)
                .orElseThrow(() -> new RuntimeException(
                        "No existe TVBox con número de referencia: " + numeroReferenciaTvBox
                ));

        // Verificar si el TVBox ya tiene un televisor asignado
        if (tvBox.getTelevisorNumeroReferencia() != null) {
            throw new RuntimeException("El TVBox " + numeroReferenciaTvBox + " ya está asignado a otro televisor.");
        }

        tvBox.setTelevisorNumeroReferencia(numeroReferenciaTelevisor);

        return repo.save(tvBox);
    }

    // Desasignar el TVBox de cualquier televisor
    public boolean desasignarTvBox(int numeroReferencia) {
        Optional<TvBox> optionalBox = repo.findByNumeroReferencia(numeroReferencia);
        if (optionalBox.isPresent()) {
            TvBox box = optionalBox.get();
            box.setTelevisorNumeroReferencia(null); // Desasignar
            repo.save(box);
            return true;
        } else {
            return false;
        }
    }



}
