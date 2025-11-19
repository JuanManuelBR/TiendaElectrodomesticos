package cal.example.TiendaElectrodomesticos.clients;

import cal.example.TiendaElectrodomesticos.dtos.TvBoxDTO;
import org.springframework.stereotype.Service;
import org.springframework.web.client.RestTemplate;
import org.springframework.http.*;
import java.util.Arrays;
import java.util.List;
import java.util.Collections;
import java.util.Map;

@Service
public class TvBoxClientService {

    private final RestTemplate restTemplate;
    private final String base = "http://localhost:8091/api/tvbox";

    public TvBoxClientService(RestTemplate restTemplate) {
        this.restTemplate = restTemplate;
    }

    // Crear (con o sin televisor)
    public TvBoxDTO createTvBox(TvBoxDTO payload) {

        // =============================
        // Validar televisor asociado
        // =============================
        if (payload.getTelevisorNumeroReferencia() != null) {

            String urlTelevisor = "http://localhost:8090/televisor/"
                    + payload.getTelevisorNumeroReferencia();

            try {
                // Intenta obtener el televisor
                restTemplate.getForObject(urlTelevisor, Object.class);
            } catch (Exception e) {
                // Si falla → el televisor NO existe
                throw new RuntimeException(
                        "No existe un televisor con número de referencia: "
                                + payload.getTelevisorNumeroReferencia()
                );
            }
        }

        // =============================
        // Crear TVBOX SI TODO ESTÁ BIEN
        // =============================
        return restTemplate.postForObject(base, payload, TvBoxDTO.class);
    }


    // Obtener por id
    public TvBoxDTO getById(Integer id) {
        try {
            return restTemplate.getForObject(base + "/" + id, TvBoxDTO.class);
        } catch (Exception e) { return null; }
    }

    // Obtener por numeroReferencia
    public TvBoxDTO getByNumeroReferencia(Integer numeroReferencia) {
        try {
            return restTemplate.getForObject(base + "/numero/" + numeroReferencia, TvBoxDTO.class);
        } catch (Exception e) { return null; }
    }

    // Listar todos
    public List<TvBoxDTO> getAll() {
        try {
            TvBoxDTO[] arr = restTemplate.getForObject(base, TvBoxDTO[].class);
            if (arr == null) return Collections.emptyList();
            return Arrays.asList(arr);
        } catch (Exception e) { return Collections.emptyList(); }
    }

    // Actualizar por numeroReferencia (envía todo el objeto; el micro C guarda)
    public TvBoxDTO updateByNumeroReferencia(Integer numeroReferencia, TvBoxDTO payload) {
        restTemplate.put(base + "/referencia/" + numeroReferencia, payload);
        return getByNumeroReferencia(numeroReferencia);
    }

    public void deleteByNumeroReferencia(Integer numeroReferencia) {
        restTemplate.delete(base + "/numero/" + numeroReferencia);
    }

    public List<Map<String, Object>> getAllTvBoxes() {
        try {
            ResponseEntity<List> response =
                    restTemplate.getForEntity(base, List.class);

            return response.getBody();
        } catch (Exception e) {
            return Collections.emptyList();
        }
    }

    // Obtener TVBoxes por marca
    public List<TvBoxDTO> getByMarca(String marca) {
        try {
            TvBoxDTO[] arr = restTemplate.getForObject(base + "/marca/" + marca, TvBoxDTO[].class);
            if (arr == null) return Collections.emptyList();
            return Arrays.asList(arr);
        } catch (Exception e) {
            return Collections.emptyList();
        }
    }

    public TvBoxDTO asignarTelevisorATvBox(Integer numeroReferenciaTvBox, Integer numeroReferenciaTelevisor) {
        try {
            // Endpoint del MS TVBox
            String url = base + "/asignarTelevisor/" + numeroReferenciaTvBox + "/" + numeroReferenciaTelevisor;

            // Hacer PUT (no retorna objeto directamente)
            restTemplate.put(url, null);

            // Retornar el TVBox actualizado
            return getByNumeroReferencia(numeroReferenciaTvBox);

        } catch (Exception e) {
            throw new RuntimeException("No se pudo asignar el TVBox: " + e.getMessage());
        }
    }

    // Desasignar un televisor de un TVBox
    public TvBoxDTO desasignarTelevisorDeTvBox(Integer numeroReferenciaTvBox) {
        try {
            // Endpoint del MS TVBox para desasignar
            String url = base + "/desasignar/" + numeroReferenciaTvBox;

            // Hacer PUT al endpoint de desasignación
            restTemplate.put(url, null);

            // Retornar el TVBox actualizado
            return getByNumeroReferencia(numeroReferenciaTvBox);

        } catch (Exception e) {
            throw new RuntimeException("No se pudo desasignar el TVBox: " + e.getMessage());
        }
    }





}
