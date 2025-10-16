package cal.example.TiendaElectrodomesticos.controller;


import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
import cal.example.TiendaElectrodomesticos.model.Electrodomestico;
import cal.example.TiendaElectrodomesticos.model.Televisor;
import cal.example.TiendaElectrodomesticos.service.ServiceElectrodomestico;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/electrodomestico")
public class ControllerElectrodomestico {

    ServiceElectrodomestico serviceElectrodomestico = ServiceElectrodomestico.getInstance();


    public ControllerElectrodomestico(ServiceElectrodomestico serviceElectrodomestico) {
        this.serviceElectrodomestico = serviceElectrodomestico;
    }

    @GetMapping(value = "/healthCheck")
    public String healthCheck() {
        return "Status Ok!";
    }



    @PostMapping("")
    public ResponseEntity<?> addElectrodomestico(@RequestBody Electrodomestico e) {
        try {
            Electrodomestico nuevo = serviceElectrodomestico.addElectrodomestico(e);
            return ResponseEntity.status(HttpStatus.CREATED).body(nuevo);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }


    @GetMapping("")
    public List<Televisor> listTelevisores() {
        return serviceElectrodomestico.listTelevisores();
    }



    // Buscar electrodoméstico por código
    @GetMapping("/{codigo}")
    public ResponseEntity<?> buscarElectrodomestico(@PathVariable int codigo) {
        Electrodomestico e = serviceElectrodomestico.buscarElectrodomestico(codigo);
        if (e != null) {
            return ResponseEntity.ok(e);
        } else {
            return ResponseEntity.status(404)
                    .body("No se encontró un electrodoméstico con el código: " + codigo);
        }
    }


    @DeleteMapping("/{codigo}")
    public ResponseEntity<?> deleteElectrodomestico(@PathVariable int codigo) {
        boolean eliminado = serviceElectrodomestico.deleteElectrodomestico(codigo);

        if (eliminado) {
            return ResponseEntity.ok("Electrodoméstico con código " + codigo + " eliminado correctamente");
        } else {
            return ResponseEntity.status(404)
                    .body("No se encontró un electrodoméstico con el código: " + codigo);
        }
    }

    @PutMapping("/{codigo}")
    public ResponseEntity<?> updateTelevisor(
            @PathVariable int codigo,
            @RequestBody Televisor televisorDatos) {

        boolean actualizado = serviceElectrodomestico.putTelevisor(
                codigo,
                televisorDatos.getNombre(),
                televisorDatos.getPrecio(),
                televisorDatos.getMarca(),
                televisorDatos.getFechaCreacion()
        );

        if (actualizado) {
            return ResponseEntity.ok("Televisor actualizado correctamente");
        } else {
            return ResponseEntity.status(404).body("No se encontró un televisor con código " + codigo);
        }
    }

    @GetMapping("/marcasTelevisores")
    public List<String> getMarcasTelevisores() {
        return serviceElectrodomestico.getMarcasTelevisores();
    }

    @GetMapping("/filtrarTelevisores/{marca}")
    public List<Televisor> filtrarTelevisoresPorMarca(@PathVariable String marca) {
        return serviceElectrodomestico.filtrarTelevisoresPorMarca(marca);
    }

    @PostMapping("/control")
    public ResponseEntity<?> addControlRemoto(@RequestBody ControlRemoto c) {
        try {
            ControlRemoto nuevo = serviceElectrodomestico.addControlRemoto(c);
            return ResponseEntity.status(HttpStatus.CREATED).body(nuevo);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @GetMapping("/control")
    public List<ControlRemoto> listControles() {
        return serviceElectrodomestico.listControles();
    }

    @GetMapping("/control/{codigo}")
    public ResponseEntity<?> buscarControlRemoto(@PathVariable int codigo) {
        ControlRemoto c = serviceElectrodomestico.buscarControlRemoto(codigo);
        if (c != null) {
            return ResponseEntity.ok(c);
        } else {
            return ResponseEntity.status(404)
                    .body("No se encontró un control remoto con el código: " + codigo);
        }
    }

    @DeleteMapping("/control/{codigo}")
    public ResponseEntity<?> deleteControlRemoto(@PathVariable int codigo) {
        boolean eliminado = serviceElectrodomestico.deleteControlRemoto(codigo);
        if (eliminado) {
            return ResponseEntity.ok("Control remoto con código " + codigo + " eliminado correctamente");
        } else {
            return ResponseEntity.status(404)
                    .body("No se encontró un control remoto con el código: " + codigo);
        }
    }

    @PutMapping("/control/{codigo}")
    public ResponseEntity<?> updateControlRemoto(
            @PathVariable int codigo,
            @RequestBody ControlRemoto controlDatos) {

        boolean actualizado = serviceElectrodomestico.putControlRemoto(
                codigo,
                controlDatos.getNombre(),
                controlDatos.getAlcance(),
                controlDatos.getMarca(),
                controlDatos.getFechaVenta()
        );

        if (actualizado) {
            return ResponseEntity.ok("Control remoto actualizado correctamente");
        } else {
            return ResponseEntity.status(404)
                    .body("No se encontró un control remoto con código " + codigo);
        }
    }

    @GetMapping("/control/marcas")
    public List<String> getMarcasControles() {
        return serviceElectrodomestico.getMarcasControles();
    }

    @GetMapping("/control/filtrar/{marca}")
    public List<ControlRemoto> filtrarControlesPorMarca(@PathVariable String marca) {
        return serviceElectrodomestico.filtrarControlesPorMarca(marca);
    }


    @PutMapping("/asignarControl/{codigoTelevisor}/{codigoControl}")
    public ResponseEntity<?> asignarControlATelevisor(
            @PathVariable int codigoTelevisor,
            @PathVariable int codigoControl) {
        try {
            boolean asignado = serviceElectrodomestico.asignarControlATelevisor(codigoTelevisor, codigoControl);
            if (asignado) {
                return ResponseEntity.ok("Control remoto asignado correctamente al televisor.");
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("No se pudo asignar el control remoto.");
            }
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }
    @GetMapping("/control/disponibles")
    public List<ControlRemoto> listControlesDisponibles() {
        return serviceElectrodomestico.listControlesDisponibles();
    }

}
