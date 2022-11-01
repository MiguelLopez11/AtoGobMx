<template>
  <b-card class="m-2">
    <b-avatar variant="light" size="250" src="https://media.istockphoto.com/vectors/shield-with-hand-block-icon-in-flat-style-with-shadow-vector-id867726980?k=20&m=867726980&s=612x612&w=0&h=ge5fg-rTBLy_OVAyhCiawo4Ou9bHcIQ9WtVORH20N8U=" />
    <div class="text-wrapper">
      <div class="title" data-content="404">
        Acceso denegado
      </div>

      <div class="subtitle">
        No tiene permiso para acceder a esta ruta.
      </div>

      <div class="buttons" style="cursor: pointer;">
        <a href="/">Regresar a inicio</a>
      </div>
    </div>
  </b-card>
</template>

<script>
export default {}
</script>

<style scoped>
@import url(https://fonts.googleapis.com/css?family=Raleway:700);

*,
*:before,
*:after {
  box-sizing: border-box;
}
html {
  height: 100%;
}
body {
  font-family: 'Raleway', sans-serif;
  background-color: #342643;
  height: 100%;
  padding: 10px;
}

a {
  color: #ee4b5e !important;
  text-decoration: none;
}
a:hover {
  color: #ffffff !important;
  text-decoration: none;
}

.text-wrapper {
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.title {
  font-size: 5em;
  font-weight: 700;
  color: #ee4b5e;
}

.subtitle {
  font-size: 40px;
  font-weight: 700;
  color: #1fa9d6;
}
.isi {
  font-size: 18px;
  text-align: center;
  margin: 30px;
  padding: 20px;
  color: white;
}
.buttons {
  margin: 30px;
  font-weight: 700;
  border: 2px solid #ee4b5e;
  text-decoration: none;
  padding: 15px;
  text-transform: uppercase;
  color: #ee4b5e;
  border-radius: 26px;
  transition: all 0.2s ease-in-out;
  display: inline-block;
}
.buttons:hover {
  background-color: #ee4b5e;
  color: white;
  transition: all 0.2s ease-in-out;
}
</style>
