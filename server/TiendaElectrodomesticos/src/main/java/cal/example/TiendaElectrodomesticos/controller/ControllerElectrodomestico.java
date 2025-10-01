package cal.example.TiendaElectrodomesticos.controller;


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



    @PostMapping("/add")
    public ResponseEntity<?> addElectrodomestico(@RequestBody Electrodomestico e) {
        try {
            Electrodomestico nuevo = serviceElectrodomestico.addElectrodomestico(e);
            return ResponseEntity.status(HttpStatus.CREATED).body(nuevo);
        } catch (IllegalArgumentException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }


    @GetMapping("/getTelevisores")
    public List<Televisor> listTelevisores() {
        return serviceElectrodomestico.listTelevisores();
    }



    // Buscar electrodoméstico por código
    @GetMapping("/search/{codigo}")
    public ResponseEntity<?> buscarElectrodomestico(@PathVariable int codigo) {
        Electrodomestico e = serviceElectrodomestico.buscarElectrodomestico(codigo);
        if (e != null) {
            return ResponseEntity.ok(e);
        } else {
            return ResponseEntity.status(404)
                    .body("No se encontró un electrodoméstico con el código: " + codigo);
        }
    }


    @DeleteMapping("/delete/{codigo}")
    public ResponseEntity<?> deleteElectrodomestico(@PathVariable int codigo) {
        boolean eliminado = serviceElectrodomestico.deleteElectrodomestico(codigo);

        if (eliminado) {
            return ResponseEntity.ok("Electrodoméstico con código " + codigo + " eliminado correctamente");
        } else {
            return ResponseEntity.status(404)
                    .body("No se encontró un electrodoméstico con el código: " + codigo);
        }
    }

    @PutMapping("/updateTelevisor/{codigo}")
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




}
