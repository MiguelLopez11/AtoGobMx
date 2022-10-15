<template>
  <b-card class="m-3">
    <section class="vh-auto">
      <div class="container-fluid h-custom">
        <div class="row d-flex justify-content-center align-items-center h-80">
          <div class="col-md-9 col-lg-6 col-xl-5">
            <img
              src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.webp"
              class="img-fluid"
              alt="Sample image"
            />
          </div>
          <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
            <form>
              <div class="form-outline mb-4">
                <label class="form-label" for="form3Example3"
                  >Nombre de usuario</label
                >
                <input
                  type="email"
                  id="form3Example3"
                  class="form-control form-control-lg"
                  placeholder="Ingresa un usuario válido"
                />
              </div>
              <div class="form-outline mb-3">
                <label class="form-label" for="form3Example4">Contraseña</label>
                <input
                  type="password"
                  id="form3Example4"
                  class="form-control form-control-lg"
                  placeholder="Ingresa tu contreseña"
                />
              </div>
              <div class="text-center text-lg-start mt-4 pt-2">
                <button
                  type="button"
                  class="btn btn-lg"
                  style="padding-left: 2.5rem; background-color: rgb(94,80,238); color:white; padding-right: 2.5rem;"
                >
                  Iniciar Sesión
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <!-- <div
    class="d-flex flex-column flex-md-row text-center text-md-start justify-content-between py-4 px-4 px-xl-5 bg-primary">
    <div class="text-white mb-3 mb-md-0">
      Copyright © 2020. All rights reserved.
    </div>
    <div>
      <a href="#!" class="text-white me-4">
        <i class="fab fa-facebook-f"></i>
      </a>
      <a href="#!" class="text-white me-4">
        <i class="fab fa-twitter"></i>
      </a>
      <a href="#!" class="text-white me-4">
        <i class="fab fa-google"></i>
      </a>
      <a href="#!" class="text-white">
        <i class="fab fa-linkedin-in"></i>
      </a>
    </div>
  </div> -->
    </section>
  </b-card>
</template>

<script>
import { ref } from 'vue'
export default {
  setup () {
    const user = ref({
      userName: '',
      password: ''
    })
    return {
      user
    }
  }
}
</script>

<style scoped>
.divider:after,
.divider:before {
  content: '';
  flex: 1;
  height: 1px;
  background: #eee;
}
.h-custom {
  height: calc(100% - 73px);
}
@media (max-width: 450px) {
  .h-custom {
    height: 100%;
  }
}
</style>
