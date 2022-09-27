<template>
  <div>
    <b-card align="center" class="m-3">
      <nav class="navbar navbar-expand-lg">
        <div class="container-fluid">
          <a class="navbar-brand" href="/">
            <img src="@/Images/Logo2.jpg" width="50" height="50" />
            AtoGobMx
          </a>
          <button
            class="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span class="navbar-toggler-icon"></span>
          </button>
          <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
              <li class="nav-item dropdown">
                <a
                  class="nav-link dropdown-toggle"
                  href="#"
                  id="navbarDropdown"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Recursos Humanos
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/Empleados/list"
                      >
                        Empleados
                      </router-link>
                    </a>
                  </li>
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/ExpedientesDigitales/list"
                      >
                        Expedientes Digitales
                      </router-link>
                    </a>
                  </li>
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/Areas/list"
                      >
                        Areas de trabajo
                      </router-link>
                    </a>
                  </li>
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/Departamentos/list"
                      >
                        Departamentos
                      </router-link>
                    </a>
                  </li>
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/PuestosTrabajos/list"
                      >
                        Puestos de trabajo
                      </router-link>
                    </a>
                  </li>
                </ul>
              </li>
              <li class="nav-item dropdown">
                <a
                  class="nav-link dropdown-toggle"
                  href="#"
                  id="navbarDropdown"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Servicios publicos
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/FallasAlumbrado/list"
                        >Reporte Fallas
                      </router-link>
                    </a>
                  </li>
                </ul>
              </li>
              <li class="nav-item dropdown">
                <a
                  class="nav-link dropdown-toggle"
                  href="#"
                  id="navbarDropdown"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Administrador
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/Usuarios/list"
                      >
                        Usuarios
                      </router-link>
                    </a>
                  </li>
                  <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/Roles/list"
                      >
                        Roles
                      </router-link>
                    </a>
                  </li>
                  <!-- <li>
                    <a class="dropdown-item">
                      <router-link
                        class="nav-link active text-dark"
                        aria-current="page"
                        to="/Areas/list"
                      >
                        Areas de trabajo
                      </router-link>
                    </a>
                  </li>-->
                </ul>
              </li>
              <li class="nav-item dropdown">
                <a
                  class="nav-link dropdown-toggle"
                  href="#"
                  id="navbarDropdown"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Dropdown
                </a>
                <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                  <li><a class="dropdown-item" href="#">Action</a></li>
                  <li><a class="dropdown-item" href="#">Another action</a></li>
                  <li><hr class="dropdown-divider" /></li>
                  <li>
                    <a class="dropdown-item" href="#">Something else here</a>
                  </li>
                </ul>
              </li>
              <li class="nav-item">
                <a class="nav-link disabled">Disabled</a>
              </li>
            </ul>
            <div class="d-flex">
              <b-button pressed variant="success" to="/Login" size="lg">
                <i class="bi bi-box-arrow-in-right"></i>
                Iniciar Sesion
              </b-button>
            </div>
          </div>
        </div>
      </nav>
    </b-card>
  </div>
  <router-view></router-view>
</template>
<script>
import { ref } from 'vue'
import LogoAtogobMx from '@/Images/LogoAtogobMx.jpg'

export default {
  setup () {
    const background = ref(LogoAtogobMx)
    return {
      background
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
body {
  background-color: #e5e8e8 !important;
}
nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
</style>
