<template>
  <b-card class="m-3" align="center">
    <b-row cols="3">
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/Empleados/list"
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
          <p>Alumbrado publico</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/Departamentos/list"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-sign-intersection"
            viewBox="0 0 16 16"
          >
            <path
              d="M7.25 4v3.25H4v1.5h3.25V12h1.5V8.75H12v-1.5H8.75V4h-1.5Z"
            />
            <path
              d="M9.05.435c-.58-.58-1.52-.58-2.1 0L.436 6.95c-.58.58-.58 1.519 0 2.098l6.516 6.516c.58.58 1.519.58 2.098 0l6.516-6.516c.58-.58.58-1.519 0-2.098L9.05.435Zm-1.4.7a.495.495 0 0 1 .7 0l6.516 6.515a.495.495 0 0 1 0 .7L8.35 14.866a.495.495 0 0 1-.7 0L1.134 8.35a.495.495 0 0 1 0-.7L7.65 1.134Z"
            />
          </svg>
          <p>Cementerios</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/Areas/list"
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
