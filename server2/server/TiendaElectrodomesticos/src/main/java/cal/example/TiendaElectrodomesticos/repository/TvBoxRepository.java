package cal.example.TiendaElectrodomesticos.repository;

import cal.example.TiendaElectrodomesticos.model.TvBox;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface TvBoxRepository extends JpaRepository<TvBox, Integer> {
    Optional<TvBox> findByNumeroReferencia(Integer numeroReferencia);
    List<TvBox> findAllByMarca(String marca);

}
