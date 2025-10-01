package cal.example.TiendaElectrodomesticos.config;


import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.web.SecurityFilterChain;

@Configuration
@EnableWebSecurity
public class SecurityConfig {

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
        http
                .csrf(csrf -> csrf.disable()) // Deshabilitar CSRF para APIs
                .authorizeHttpRequests(auth -> auth
                        .requestMatchers("/publico/**").permitAll() // Endpoints públicos
                        .anyRequest().authenticated()               // Todos los demás requieren autenticación
                )
                .httpBasic(customizer -> {}); // Habilitar Basic Auth (Spring usa usuario de properties)

        return http.build();
    }
}

