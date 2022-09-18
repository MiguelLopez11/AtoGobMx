<template>
  <section class="vh-auto">
    <div class="container py-5 h-auto">
      <div class="row d-flex align-items-center justify-content-center h-auto">
        <div class="col-md-6 col-lg-5 col-xl-4">
          <img
            src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.svg"
            class="img-fluid"
          />
        </div>
        <div class="col-md-5 col-lg-5 col-xl-5 offset-xl-1">
          <b-img
            thumbnail
            fluid
            src="https://www.atotonilco.gob.mx/templates/it_headlines/custom/images/logo_h.png"
            alt="Image 1"
            width="600"
            height="800"
            class="m-5"
            center
          ></b-img>
          <form>
            <!-- Email input -->
            <div class="form-outline mb-4">
              <input
                placeholder="Nombre de Usuario"
                class="form-control form-control-lg"
              />
              <!-- <label class="form-label" for="form1Example13">Email address</label> -->
            </div>

            <!-- Password input -->
            <div class="form-outline mb-4">
              <input
                type="password"
                placeholder="Contraseña"
                class="form-control form-control-lg"
              />
            </div>

            <div class="d-flex justify-content-around align-items-center mb-4">
              <!-- Checkbox -->
              <div class="form-check">
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  id="form1Example3"
                  checked
                />
                <label class="form-check-label" for="form1Example3">
                  Remember me
                </label>
              </div>
              <a href="#!">Forgot password?</a>
            </div>

            <!-- Submit button -->
            <button type="submit" class="btn btn-primary btn-lg btn-block">
              Sign in
            </button>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {}
</script>

<style scoped>
</style>
