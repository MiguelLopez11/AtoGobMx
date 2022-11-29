<template>
  <b-card class="m-3" align="center">
    <b-row cols="3">
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/AlumbradoPublico/list"
          style="border-size: 2px"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-lightning-charge-fill"
            viewBox="0 0 16 16"
          >
            <path
              d="M11.251.068a.5.5 0 0 1 .227.58L9.677 6.5H13a.5.5 0 0 1 .364.843l-8 8.5a.5.5 0 0 1-.842-.49L6.323 9.5H3a.5.5 0 0 1-.364-.843l8-8.5a.5.5 0 0 1 .615-.09z"
            />
          </svg>
          <p>Alumbrado Publico</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/CementeriosPublicos/list"
        >
          <svg
            id="Layer_1"
            data-name="Layer 1"
            width="140"
            height="150"
            fill="currentColor"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 50.46 50.46"
          >
            <path
              class="cls-1"
              d="M24.71,17.9a2.27,2.27,0,0,1,1.79-3.64h0a2.25,2.25,0,0,1,1.94,1.09,2.3,2.3,0,0,1,0,2.31,8.34,8.34,0,0,1-.52.89.5.5,0,0,0,0,.61l4.82,5.6a.49.49,0,0,0,.38.18h0a.53.53,0,0,0,.38-.18c2.27-2.66,2.66-3.86,2.67-3.92A10.77,10.77,0,1,0,17.81,23c.05.06,10.11,12.1,18.55,22.18a.46.46,0,0,0,.38.18h.08a.5.5,0,0,0,.39-.31l3-8.08a.53.53,0,0,0-.08-.5Z"
            />
            <path
              class="cls-1"
              d="M19.87,28.43a.46.46,0,0,0-.38-.18h0a.5.5,0,0,0-.39.18L12.4,36.5a.51.51,0,0,0-.09.5l3,8.08a.49.49,0,0,0,.38.31h.08a.5.5,0,0,0,.39-.18l8.6-10.29a.49.49,0,0,0,0-.64Z"
            />
          </svg>
          <p>Cementerios</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/AseoPublico/list"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-truck"
            viewBox="0 0 16 16"
          >
            <path
              d="M0 3.5A1.5 1.5 0 0 1 1.5 2h9A1.5 1.5 0 0 1 12 3.5V5h1.02a1.5 1.5 0 0 1 1.17.563l1.481 1.85a1.5 1.5 0 0 1 .329.938V10.5a1.5 1.5 0 0 1-1.5 1.5H14a2 2 0 1 1-4 0H5a2 2 0 1 1-3.998-.085A1.5 1.5 0 0 1 0 10.5v-7zm1.294 7.456A1.999 1.999 0 0 1 4.732 11h5.536a2.01 2.01 0 0 1 .732-.732V3.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5v7a.5.5 0 0 0 .294.456zM12 10a2 2 0 0 1 1.732 1h.768a.5.5 0 0 0 .5-.5V8.35a.5.5 0 0 0-.11-.312l-1.48-1.85A.5.5 0 0 0 13.02 6H12v4zm-9 1a1 1 0 1 0 0 2 1 1 0 0 0 0-2zm9 0a1 1 0 1 0 0 2 1 1 0 0 0 0-2z"
            />
          </svg>
          <p>Aseo Publico</p>
        </b-button>
      </b-col>
    </b-row>
  </b-card>
</template>

<script>
export default {}
</script>

<style></style>
