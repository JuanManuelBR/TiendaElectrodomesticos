package cal.example.TiendaElectrodomesticos.repository;

import cal.example.TiendaElectrodomesticos.dtos.TelevisorControlDTO;
import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
import cal.example.TiendaElectrodomesticos.model.Televisor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface TelevisorRepository extends JpaRepository<Televisor, Integer> {
    Optional<Televisor> findByNumeroReferencia(Integer numeroReferencia);

    @Query("""
    SELECT new cal.example.TiendaElectrodomesticos.dtos.TelevisorControlDTO(
        t.id, t.nombre, t.marca, c.numeroReferencia, c.marca, c.alcance
    )
    FROM Televisor t
    JOIN ControlRemoto c ON t.id = c.televisor.id
    WHERE t.marca = c.marca
""")
    List<TelevisorControlDTO> findControlesPorMarca(String marca);

}
