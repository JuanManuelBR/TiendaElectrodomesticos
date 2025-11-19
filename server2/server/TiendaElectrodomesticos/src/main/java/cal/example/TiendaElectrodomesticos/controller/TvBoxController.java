package cal.example.TiendaElectrodomesticos.controller;

import cal.example.TiendaElectrodomesticos.model.TvBox;
import cal.example.TiendaElectrodomesticos.service.TvBoxService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import jakarta.validation.Valid;
import java.util.List;

@RestController
@RequestMapping("/api/tvbox")
@CrossOrigin(origins = "*")
public class TvBoxController {

    private final TvBoxService service;

    public TvBoxController(TvBoxService service) {
        this.service = service;
    }

    @GetMapping("/health")
    public String health() { return "TVBox service OK"; }

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody TvBox t) {
        try {
            TvBox created = service.create(t);
            return ResponseEntity.status(201).body(created);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.badRequest().body(ex.getMessage());
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<?> getOne(@PathVariable Integer id) {
        TvBox t = service.getOne(id);
        if (t == null) return ResponseEntity.notFound().build();
        return ResponseEntity.ok(t);
    }

    @GetMapping("/numero/{numeroReferencia}")
    public ResponseEntity<?> getByNumero(@PathVariable Integer numeroReferencia) {
        TvBox t = service.getByNumeroReferencia(numeroReferencia);
        if (t == null) return ResponseEntity.notFound().build();
        return ResponseEntity.ok(t);
    }

    @GetMapping
    public List<TvBox> getAll() { return service.getAll(); }

    @PutMapping("/referencia/{numeroReferencia}")
    public ResponseEntity<?> update(
            @PathVariable Integer numeroReferencia,
            @Valid @RequestBody TvBox payload
    ) {
        TvBox updated = service.update(numeroReferencia, payload);
        return ResponseEntity.ok(updated);
    }


    @DeleteMapping("/numero/{numeroReferencia}")
    public ResponseEntity<?> deleteByNumeroReferencia(@PathVariable Integer numeroReferencia) {
        try {
            service.deleteByNumeroReferencia(numeroReferencia);
            return ResponseEntity.noContent().build(); // 204
        } catch (RuntimeException ex) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body(ex.getMessage());
        }
    }

    @GetMapping("/all")
    public ResponseEntity<?> getAllTvBoxes() {
        return ResponseEntity.ok(service.getAll());
    }

    @GetMapping("/marca/{marca}")
    public ResponseEntity<?> getByMarca(@PathVariable String marca) {
        List<TvBox> tvBoxes = service.getAllByMarca(marca);
        if (tvBoxes.isEmpty()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No se encontraron TVBoxes para la marca: " + marca);
        }
        return ResponseEntity.ok(tvBoxes);
    }

    @PutMapping("/asignarTelevisor/{numeroReferenciaTvBox}/{numeroReferenciaTelevisor}")
    public ResponseEntity<?> asignarTelevisor(
            @PathVariable Integer numeroReferenciaTvBox,
            @PathVariable Integer numeroReferenciaTelevisor
    ) {
        try {
            TvBox tvBox = service.asignarTelevisor(numeroReferenciaTvBox, numeroReferenciaTelevisor);
            return ResponseEntity.ok(tvBox);
        } catch (RuntimeException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @PutMapping("/desasignar/{numeroReferencia}")
    public ResponseEntity<?> desasignarTvBox(@PathVariable int numeroReferencia) {
        try {
            boolean success = service.desasignarTvBox(numeroReferencia);
            if (success) {
                return ResponseEntity.ok("TVBox desasignado correctamente.");
            } else {
                return ResponseEntity.status(HttpStatus.NOT_FOUND)
                        .body("No se encontró un TVBox con el número de referencia: " + numeroReferencia);
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Error al desasignar TVBox: " + e.getMessage());
        }
    }




}
