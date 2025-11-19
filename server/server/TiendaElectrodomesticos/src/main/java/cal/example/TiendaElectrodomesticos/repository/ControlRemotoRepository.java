package cal.example.TiendaElectrodomesticos.repository;

import cal.example.TiendaElectrodomesticos.model.ControlRemoto;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface ControlRemotoRepository extends JpaRepository<ControlRemoto, Integer> {
    Optional<ControlRemoto> findByNumeroReferencia(Integer numeroReferencia);

    @Query("""
        SELECT c FROM ControlRemoto c 
        JOIN FETCH c.televisor t 
        WHERE c.marca = :marca
    """)
    List<ControlRemoto> findControlesPorMarcaConTelevisor(String marca);
}
