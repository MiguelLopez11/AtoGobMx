<template>
  <b-card class="m-3" align="center">
    <b-row cols="3">
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/Administrador/Usuarios/list"
          style="border-size: 2px"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-person-lock"
            viewBox="0 0 16 16"
          >
            <path
              d="M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0ZM8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4Zm0 5.996V14H3s-1 0-1-1 1-4 6-4c.564 0 1.077.038 1.544.107a4.524 4.524 0 0 0-.803.918A10.46 10.46 0 0 0 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664h5ZM9 13a1 1 0 0 1 1-1v-1a2 2 0 1 1 4 0v1a1 1 0 0 1 1 1v2a1 1 0 0 1-1 1h-4a1 1 0 0 1-1-1v-2Zm3-3a1 1 0 0 0-1 1v1h2v-1a1 1 0 0 0-1-1Z"
            />
          </svg>
          <p>Usuarios</p>
        </b-button>
      </b-col>
      <!-- <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="Administrador/Roles/list"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-person-vcard"
            viewBox="0 0 16 16"
          >
            <path
              d="M5 8a2 2 0 1 0 0-4 2 2 0 0 0 0 4Zm4-2.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4a.5.5 0 0 1-.5-.5ZM9 8a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 0 1h-4A.5.5 0 0 1 9 8Zm1 2.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5Z"
            />
            <path
              d="M2 2a2 2 0 0 0-2 2v8a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V4a2 2 0 0 0-2-2H2ZM1 4a1 1 0 0 1 1-1h12a1 1 0 0 1 1 1v8a1 1 0 0 1-1 1H8.96c.026-.163.04-.33.04-.5C9 10.567 7.21 9 5 9c-2.086 0-3.8 1.398-3.984 3.181A1.006 1.006 0 0 1 1 12V4Z"
            />
          </svg>
          <p>Roles</p>
        </b-button>
      </b-col> -->
    </b-row>
  </b-card>
</template>

<script>
export default {}
</script>

<style></style>
