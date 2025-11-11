package cal.example.TiendaElectrodomesticos.controller;

import cal.example.TiendaElectrodomesticos.dtos.ControlRemotoDTO;
import cal.example.TiendaElectrodomesticos.dtos.TelevisorControlDTO;
import cal.example.TiendaElectrodomesticos.dtos.TelevisorDTO;
import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
import cal.example.TiendaElectrodomesticos.model.Televisor;
import cal.example.TiendaElectrodomesticos.service.ServiceTelevisor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/televisor")
@CrossOrigin(origins = "http://localhost:5173", allowCredentials = "true")
public class ControllerTelevisor {

    private final ServiceTelevisor serviceTelevisor;

    public ControllerTelevisor(ServiceTelevisor serviceTelevisor) {
        this.serviceTelevisor = serviceTelevisor;
    }

    // ==========================================================
    // 🩺 HEALTH CHECK
    // ==========================================================
    @GetMapping("/healthCheck")
    public String healthCheck() {
        return "✅ Televisor microservice running OK!";
    }

    // ==========================================================
    // 📺 TELEVISOR
    // ==========================================================

    @PostMapping("")
    public ResponseEntity<?> addTelevisor(@RequestBody Televisor televisor) {
        try {
            TelevisorDTO nuevo = serviceTelevisor.addTelevisor(televisor);
            return ResponseEntity.status(HttpStatus.CREATED).body(nuevo);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @GetMapping("")
    public List<TelevisorDTO> listTelevisores() {
        return serviceTelevisor.listTelevisores();
    }

    @GetMapping("/{codigo}")
    public ResponseEntity<?> buscarTelevisor(@PathVariable int codigo) {
        TelevisorDTO t = serviceTelevisor.buscarTelevisor(codigo);
        if (t != null) {
            return ResponseEntity.ok(t);
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No se encontró un televisor con el código: " + codigo);
        }
    }

    @DeleteMapping("/{codigo}")
    public ResponseEntity<?> eliminarTelevisor(@PathVariable int codigo) {
        try {
            boolean eliminado = serviceTelevisor.deleteTelevisor(codigo);
            if (eliminado) {
                return ResponseEntity.ok("Televisor eliminado y control des-asignado correctamente");
            } else {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("No se encontró el televisor con ese código.");
            }
        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @PutMapping("/{codigo}")
    public ResponseEntity<?> updateTelevisor(
            @PathVariable int codigo,
            @RequestBody Televisor televisorDatos) {

        boolean actualizado = serviceTelevisor.updateTelevisor(
                codigo,
                televisorDatos.getNombre(),
                televisorDatos.getPrecio(),
                televisorDatos.getMarca(),
                televisorDatos.getFechaCreacion()
        );

        if (actualizado) {
            return ResponseEntity.ok("Televisor actualizado correctamente.");
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No se encontró un televisor con código " + codigo);
        }
    }

    @GetMapping("/marcas")
    public List<String> getMarcasTelevisores() {
        return serviceTelevisor.getMarcasTelevisores();
    }

    @GetMapping("/filtrar/{marca}")
    public List<TelevisorDTO> filtrarTelevisoresPorMarca(@PathVariable String marca) {
        return serviceTelevisor.filtrarTelevisoresPorMarca(marca);
    }

    // ==========================================================
    // 🎮 CONTROL REMOTO
    // ==========================================================

    @PostMapping("/control")
    public ResponseEntity<?> addControlRemoto(@RequestBody ControlRemoto c) {
        try {
            ControlRemotoDTO nuevo = serviceTelevisor.addControlRemoto(c);
            return ResponseEntity.status(HttpStatus.CREATED).body(nuevo);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @GetMapping("/control")
    public List<ControlRemotoDTO> listControles() {
        return serviceTelevisor.listControles();
    }

    @GetMapping("/control/{codigo}")
    public ResponseEntity<?> buscarControlRemoto(@PathVariable int codigo) {
        ControlRemotoDTO c = serviceTelevisor.buscarControlRemoto(codigo);
        if (c != null) {
            return ResponseEntity.ok(c);
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No se encontró un control remoto con el código: " + codigo);
        }
    }

    @DeleteMapping("/control/{codigo}")
    public ResponseEntity<?> eliminarControl(@PathVariable int codigo) {
        try {
            boolean eliminado = serviceTelevisor.deleteControlRemoto(codigo);
            if (eliminado) {
                return ResponseEntity.ok("Control eliminado correctamente. Si estaba asignado, fue desasignado del televisor.");
            } else {
                return ResponseEntity.status(HttpStatus.NOT_FOUND)
                        .body("No se encontró el control remoto con ese código.");
            }
        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @PutMapping("/control/{codigo}")
    public ResponseEntity<?> updateControlRemoto(
            @PathVariable int codigo,
            @RequestBody ControlRemoto controlDatos) {

        boolean actualizado = serviceTelevisor.updateControlRemoto(
                codigo,
                controlDatos.getNombre(),
                controlDatos.getAlcance(),
                controlDatos.getMarca(),
                controlDatos.getFechaVenta()
        );

        if (actualizado) {
            return ResponseEntity.ok("Control remoto actualizado correctamente.");
        } else {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No se encontró un control remoto con código " + codigo);
        }
    }

    @GetMapping("/control/marcas")
    public List<String> getMarcasControles() {
        return serviceTelevisor.getMarcasControles();
    }

    @GetMapping("/control/filtrar/{marca}")
    public List<ControlRemotoDTO> filtrarControlesPorMarca(@PathVariable String marca) {
        return serviceTelevisor.filtrarControlesPorMarca(marca);
    }

    // ==========================================================
    // 🔗 RELACIÓN TELEVISOR - CONTROL
    // ==========================================================

    @PutMapping("/asignarControl/{codigoTelevisor}/{codigoControl}")
    public ResponseEntity<?> asignarControlATelevisor(
            @PathVariable int codigoTelevisor,
            @PathVariable int codigoControl) {
        try {
            boolean asignado = serviceTelevisor.asignarControlATelevisor(codigoTelevisor, codigoControl);
            if (asignado) {
                return ResponseEntity.ok("Control remoto asignado correctamente al televisor.");
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                        .body("No se pudo asignar el control remoto.");
            }
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @GetMapping("/control/disponibles")
    public List<ControlRemotoDTO> listControlesDisponibles() {
        return serviceTelevisor.listControlesDisponibles();
    }

    // ==========================================================
    //  CONSULTAS PERSONALIZADAS
    // ==========================================================


    // Mostrar controles por marca y su televisor asociado
    @GetMapping("/control/marca/{marca}")
    public List<ControlRemotoDTO> obtenerControlesPorMarcaConTelevisor(@PathVariable String marca) {
        return serviceTelevisor.obtenerControlesPorMarcaConTelevisor(marca);
    }

    @PutMapping("/desasignarControl/{codigoTelevisor}/{codigoControl}")
    public ResponseEntity<?> desasignarControlDeTelevisor(
            @PathVariable int codigoTelevisor,
            @PathVariable int codigoControl) {
        try {
            boolean desasignado = serviceTelevisor.desasignarControlDeTelevisor(codigoTelevisor, codigoControl);
            if (desasignado) {
                return ResponseEntity.ok("Control remoto desasignado correctamente del televisor.");
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                        .body("No se pudo desasignar el control remoto.");
            }
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    // ==========================================================
//  Controles por marca
// ==========================================================
    @GetMapping("/controles/marca/{marca}")
    public ResponseEntity<?> obtenerControlesPorMarca(@PathVariable String marca) {
        try {
            List<TelevisorControlDTO> controles = serviceTelevisor.obtenerControlesPorMarca(marca);
            return ResponseEntity.ok(controles);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(ex.getMessage());
        }
    }


}
