package cal.example.TiendaElectrodomesticos.controller;

import cal.example.TiendaElectrodomesticos.dtos.ControlRemotoDTO;
import cal.example.TiendaElectrodomesticos.dtos.TelevisorControlDTO;
import cal.example.TiendaElectrodomesticos.dtos.TelevisorDTO;
import cal.example.TiendaElectrodomesticos.dtos.TvBoxDTO;
import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
import cal.example.TiendaElectrodomesticos.model.Televisor;
import cal.example.TiendaElectrodomesticos.service.ServiceTelevisor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;


@RestController
@RequestMapping("/televisor")
@CrossOrigin(origins = "http://localhost:5173", allowCredentials = "true")
public class ControllerTelevisor {
    private final cal.example.TiendaElectrodomesticos.clients.TvBoxClientService tvBoxClientService;

    private final ServiceTelevisor serviceTelevisor;

    public ControllerTelevisor(ServiceTelevisor serviceTelevisor,  cal.example.TiendaElectrodomesticos.clients.TvBoxClientService tvBoxClientService) {
        this.serviceTelevisor = serviceTelevisor;
        this.tvBoxClientService = tvBoxClientService;
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
        // 1. obtener televisores desde el service
        List<TelevisorDTO> televisores = serviceTelevisor.listTelevisores();

        // 2. obtener todos los tvboxes del microservicio C (cliente)
        List<TvBoxDTO> allBoxes = tvBoxClientService.getAll();

        // 3. agrupar por numeroReferencia del televisor
        Map<Integer, List<Integer>> mapTelevisorToBoxes = allBoxes.stream()
                .filter(b -> b.getTelevisorNumeroReferencia() != null)
                .collect(Collectors.groupingBy(
                        TvBoxDTO::getTelevisorNumeroReferencia,
                        Collectors.mapping(TvBoxDTO::getNumeroReferencia, Collectors.toList())
                ));

        // 4. llenar cada TelevisorDTO con su lista de tvboxes (numeroReferencia)
        for (TelevisorDTO t : televisores) {
            List<Integer> boxNums = mapTelevisorToBoxes.get(t.getNumeroReferencia());
            if (boxNums != null) t.setTvboxNumeroReferencias(boxNums);
            else t.setTvboxNumeroReferencias(List.of());
        }

        return televisores;
    }

    @GetMapping("/{codigo}")
    public ResponseEntity<?> buscarTelevisor(@PathVariable int codigo) {
        TelevisorDTO t = serviceTelevisor.buscarTelevisor(codigo);

        if (t != null) {
            // Obtener todos los TVBoxes
            List<TvBoxDTO> allBoxes = tvBoxClientService.getAll();

            // Filtrar solo los asignados a este televisor
            List<Integer> boxNums = allBoxes.stream()
                    .filter(b -> b.getTelevisorNumeroReferencia() != null)
                    .filter(b -> b.getTelevisorNumeroReferencia() == codigo)
                    .map(TvBoxDTO::getNumeroReferencia)
                    .toList();

            // Asignar la lista al DTO
            t.setTvboxNumeroReferencias(boxNums);

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
        // 1. Obtener televisores filtrados desde el service
        List<TelevisorDTO> televisores = serviceTelevisor.filtrarTelevisoresPorMarca(marca);

        // 2. Obtener todos los TVBoxes del microservicio C
        List<TvBoxDTO> allBoxes = tvBoxClientService.getAll();

        // 3. Agrupar por numeroReferencia del televisor
        Map<Integer, List<Integer>> mapTelevisorToBoxes = allBoxes.stream()
                .filter(b -> b.getTelevisorNumeroReferencia() != null)
                .collect(Collectors.groupingBy(
                        TvBoxDTO::getTelevisorNumeroReferencia,
                        Collectors.mapping(TvBoxDTO::getNumeroReferencia, Collectors.toList())
                ));

        // 4. Llenar cada TelevisorDTO con su lista de TVBoxes (numeroReferencia)
        for (TelevisorDTO t : televisores) {
            List<Integer> boxNums = mapTelevisorToBoxes.get(t.getNumeroReferencia());
            if (boxNums != null) t.setTvboxNumeroReferencias(boxNums);
            else t.setTvboxNumeroReferencias(List.of());
        }

        return televisores;
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
    // ==========================================================
//  TVBox
// ==========================================================

    @PostMapping("/tvbox")
    public ResponseEntity<?> crearTvBoxLibre(@RequestBody TvBoxDTO dto) {
        try {
            TvBoxDTO creado = tvBoxClientService.createTvBox(dto);
            return ResponseEntity.status(HttpStatus.CREATED).body(creado);
        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Error creando TVBox: " + ex.getMessage());
        }
    }

    @GetMapping("/tvbox/numero/{numeroReferencia}")
    public ResponseEntity<?> getByNumeroReferencia(@PathVariable Integer numeroReferencia) {
        try {
            TvBoxDTO tv = tvBoxClientService.getByNumeroReferencia(numeroReferencia);

            if (tv == null) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND)
                        .body("No existe un TVBox con numero_referencia: " + numeroReferencia);
            }

            return ResponseEntity.ok(tv);

        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("Error consultando TVBox: " + ex.getMessage());
        }
    }

    // Crear TVBox y asignarlo al televisor
    @PostMapping("/{numeroReferenciaTelevisor}/tvbox")
    public ResponseEntity<?> crearTvBoxParaTelevisor(@PathVariable int numeroReferenciaTelevisor,
                                                     @RequestBody TvBoxDTO dto) {
        TelevisorDTO tv = serviceTelevisor.buscarTelevisor(numeroReferenciaTelevisor);
        if (tv == null) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("No existe el televisor con numeroReferencia: " + numeroReferenciaTelevisor);
        }

        dto.setTelevisorNumeroReferencia(numeroReferenciaTelevisor);
        try {
            TvBoxDTO creado = tvBoxClientService.createTvBox(dto);
            return ResponseEntity.status(HttpStatus.CREATED).body(creado);
        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Error creando TVBox: " + ex.getMessage());
        }
    }

    // Listar TVBoxes de un televisor (por numeroReferencia)
    @GetMapping("/{numeroReferenciaTelevisor}/tvbox")
    public ResponseEntity<?> listarTvBoxesDelTelevisor(@PathVariable int numeroReferenciaTelevisor) {
        List<TvBoxDTO> all = tvBoxClientService.getAll();
        if (all == null) return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("No se pudo conectar con TVBox service");

        List<TvBoxDTO> filtradas = all.stream()
                .filter(t -> t.getTelevisorNumeroReferencia() != null && t.getTelevisorNumeroReferencia().equals(numeroReferenciaTelevisor))
                .toList();

        return ResponseEntity.ok(filtradas);
    }

    // Obtener TVBox por ID (sin validar televisor)
    @GetMapping("/tvbox/{id}")
    public ResponseEntity<?> obtenerTvBoxPorId(@PathVariable int id) {
        TvBoxDTO t = tvBoxClientService.getById(id);
        if (t == null) return ResponseEntity.status(HttpStatus.NOT_FOUND).body("No se encontró TVBox con ese id");
        return ResponseEntity.ok(t);
    }

    // Obtener TVBox por numeroReferencia (y validar que pertenezca a un televisor dado)
    @GetMapping("/{numeroReferenciaTelevisor}/tvbox/numero/{numeroReferenciaTvBox}")
    public ResponseEntity<?> obtenerTvBoxPorNumero(@PathVariable int numeroReferenciaTelevisor,
                                                   @PathVariable int numeroReferenciaTvBox) {
        TvBoxDTO t = tvBoxClientService.getByNumeroReferencia(numeroReferenciaTvBox);
        if (t == null) return ResponseEntity.status(HttpStatus.NOT_FOUND).body("No se encontró TVBox con ese número");
        if (t.getTelevisorNumeroReferencia() == null || !t.getTelevisorNumeroReferencia().equals(numeroReferenciaTelevisor)) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("La TVBox no pertenece al televisor indicado.");
        }
        return ResponseEntity.ok(t);
    }

    // Editar TVBox por id (no requiere enviar televisor; conserva la asignación actual salvo que el body la cambie)
    // Editar TVBox por número de referencia (no requiere enviar televisor)
    @PutMapping("/tvbox/referencia/{numeroReferencia}")
    public ResponseEntity<?> editarTvBox(
            @PathVariable int numeroReferencia,
            @RequestBody TvBoxDTO dto
    ) {
        TvBoxDTO original = tvBoxClientService.getByNumeroReferencia(numeroReferencia);
        if (original == null) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No existe TVBox con ese número de referencia");
        }


        try {
            TvBoxDTO actualizado = tvBoxClientService.updateByNumeroReferencia(numeroReferencia, dto);
            return ResponseEntity.ok(actualizado);

        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("Error actualizando TVBox: " + ex.getMessage());
        }
    }


    // Eliminar TVBox por número de referencia
    @DeleteMapping("/tvbox/referencia/{numeroReferencia}")
    public ResponseEntity<?> eliminarTvBoxPorNumeroReferencia(@PathVariable int numeroReferencia) {

        TvBoxDTO t = tvBoxClientService.getByNumeroReferencia(numeroReferencia);
        if (t == null) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No existe TVBox con ese número de referencia");
        }

        try {
            tvBoxClientService.deleteByNumeroReferencia(numeroReferencia);
            return ResponseEntity.noContent().build(); // 204 No Content
        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("Error eliminando TVBox: " + ex.getMessage());
        }
    }




    // Desasignar TVBox (quitar enlace al televisor)
    @PutMapping("/tvbox/{id}/desasignar")
    public ResponseEntity<?> desasignarTvBox(@PathVariable int id) {
        TvBoxDTO box = tvBoxClientService.getById(id);
        if (box == null) return ResponseEntity.status(HttpStatus.NOT_FOUND).body("No existe TVBox con ese id");

        box.setTelevisorNumeroReferencia(null);
        try {
            TvBoxDTO actualizado = tvBoxClientService.updateByNumeroReferencia(id, box);
            return ResponseEntity.ok(actualizado);
        } catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Error desasignando TVBox: " + ex.getMessage());
        }
    }
    @GetMapping("/tvbox/all")
    public ResponseEntity<?> getAllTvBoxes() {

        try {
            List<Map<String, Object>> data = tvBoxClientService.getAllTvBoxes();
            return ResponseEntity.ok(data);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body("Error obteniendo lista de TVBoxes: " + e.getMessage());
        }
    }

    @GetMapping("tvbox/marca/{marca}")
    public ResponseEntity<?> getTvBoxesByMarca(@PathVariable String marca) {
        List<TvBoxDTO> tvBoxes = tvBoxClientService.getByMarca(marca);
        if (tvBoxes.isEmpty()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body("No se encontraron TVBoxes para la marca: " + marca);
        }
        return ResponseEntity.ok(tvBoxes);
    }

    @PutMapping("/tvbox/asignarTelevisor/{numeroReferenciaTvBox}/{numeroReferenciaTelevisor}")
    public ResponseEntity<?> asignarTelevisor(
            @PathVariable Integer numeroReferenciaTvBox,
            @PathVariable Integer numeroReferenciaTelevisor
    ) {
        try {
            TvBoxDTO updated = tvBoxClientService.asignarTelevisorATvBox(numeroReferenciaTvBox, numeroReferenciaTelevisor);
            return ResponseEntity.ok(updated);
        } catch (RuntimeException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    // ==============================
    // Desasignar un televisor de un TVBox
    // ==============================
    @PutMapping("/tvbox/desasignar/{numeroReferenciaTvBox}")
    public ResponseEntity<?> desasignarTelevisor(@PathVariable Integer numeroReferenciaTvBox) {
        try {
            TvBoxDTO tvBoxActualizado = tvBoxClientService.desasignarTelevisorDeTvBox(numeroReferenciaTvBox);
            return ResponseEntity.ok(tvBoxActualizado);
        } catch (Exception e) {
            return ResponseEntity.badRequest().body(e.getMessage());
        }
    }






}
