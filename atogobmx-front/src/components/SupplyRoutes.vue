<template>
  <b-card class="m-3" align="center">
    <b-row cols="3">
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/ControlVale/list"
          style="border-size: 2px"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-clipboard-pulse"
            viewBox="0 0 16 16"
          >
            <path
              fill-rule="evenodd"
              d="M10 1.5a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h3a.5.5 0 0 0 .5-.5v-1Zm-5 0A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5v1A1.5 1.5 0 0 1 9.5 4h-3A1.5 1.5 0 0 1 5 2.5v-1Zm-2 0h1v1H3a1 1 0 0 0-1 1V14a1 1 0 0 0 1 1h10a1 1 0 0 0 1-1V3.5a1 1 0 0 0-1-1h-1v-1h1a2 2 0 0 1 2 2V14a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2V3.5a2 2 0 0 1 2-2Zm6.979 3.856a.5.5 0 0 0-.968.04L7.92 10.49l-.94-3.135a.5.5 0 0 0-.895-.133L4.232 10H3.5a.5.5 0 0 0 0 1h1a.5.5 0 0 0 .416-.223l1.41-2.115 1.195 3.982a.5.5 0 0 0 .968-.04L9.58 7.51l.94 3.135A.5.5 0 0 0 11 11h1.5a.5.5 0 0 0 0-1h-1.128L9.979 5.356Z"
            />
          </svg>
          <p>Control de vales</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/EstatusVale/list"
          style="border-size: 2px"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-graph-up"
            viewBox="0 0 16 16"
          >
            <path
              fill-rule="evenodd"
              d="M0 0h1v15h15v1H0V0Zm14.817 3.113a.5.5 0 0 1 .07.704l-4.5 5.5a.5.5 0 0 1-.74.037L7.06 6.767l-3.656 5.027a.5.5 0 0 1-.808-.588l4-5.5a.5.5 0 0 1 .758-.06l2.609 2.61 4.15-5.073a.5.5 0 0 1 .704-.07Z"
            />
          </svg>
          <p>Estatus</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/Producto/list"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-box-seam"
            viewBox="0 0 16 16"
          >
            <path
              d="M8.186 1.113a.5.5 0 0 0-.372 0L1.846 3.5l2.404.961L10.404 2l-2.218-.887zm3.564 1.426L5.596 5 8 5.961 14.154 3.5l-2.404-.961zm3.25 1.7-6.5 2.6v7.922l6.5-2.6V4.24zM7.5 14.762V6.838L1 4.239v7.923l6.5 2.6zM7.443.184a1.5 1.5 0 0 1 1.114 0l7.129 2.852A.5.5 0 0 1 16 3.5v8.662a1 1 0 0 1-.629.928l-7.185 2.874a.5.5 0 0 1-.372 0L.63 13.09a1 1 0 0 1-.63-.928V3.5a.5.5 0 0 1 .314-.464L7.443.184z"
            />
          </svg>
          <p>Productos</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/Proveedor/list"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-buildings"
            viewBox="0 0 16 16"
          >
            <path
              d="M14.763.075A.5.5 0 0 1 15 .5v15a.5.5 0 0 1-.5.5h-3a.5.5 0 0 1-.5-.5V14h-1v1.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V10a.5.5 0 0 1 .342-.474L6 7.64V4.5a.5.5 0 0 1 .276-.447l8-4a.5.5 0 0 1 .487.022ZM6 8.694 1 10.36V15h5V8.694ZM7 15h2v-1.5a.5.5 0 0 1 .5-.5h2a.5.5 0 0 1 .5.5V15h2V1.309l-7 3.5V15Z"
            />
            <path
              d="M2 11h1v1H2v-1Zm2 0h1v1H4v-1Zm-2 2h1v1H2v-1Zm2 0h1v1H4v-1Zm4-4h1v1H8V9Zm2 0h1v1h-1V9Zm-2 2h1v1H8v-1Zm2 0h1v1h-1v-1Zm2-2h1v1h-1V9Zm0 2h1v1h-1v-1ZM8 7h1v1H8V7Zm2 0h1v1h-1V7Zm2 0h1v1h-1V7ZM8 5h1v1H8V5Zm2 0h1v1h-1V5Zm2 0h1v1h-1V5Zm0-2h1v1h-1V3Z"
            />
          </svg>
          <p>Proveedores</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/TipoVale/list"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="140"
            height="150"
            fill="currentColor"
            class="bi bi-activity"
            viewBox="0 0 16 16"
          >
            <path
              fill-rule="evenodd"
              d="M6 2a.5.5 0 0 1 .47.33L10 12.036l1.53-4.208A.5.5 0 0 1 12 7.5h3.5a.5.5 0 0 1 0 1h-3.15l-1.88 5.17a.5.5 0 0 1-.94 0L6 3.964 4.47 8.171A.5.5 0 0 1 4 8.5H.5a.5.5 0 0 1 0-1h3.15l1.88-5.17A.5.5 0 0 1 6 2Z"
            />
          </svg>
          <p>Tipo de vale</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/DetalleVale/list"
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
            <polygon
              class="cls-1"
              points="14.35 34.66 14.35 33.09 31.65 33.09 31.65 31.71 14.35 31.71 14.35 15.17 12.98 15.17 12.98 31.71 11.52 31.71 11.52 33.09 12.98 33.09 12.98 34.66 14.35 34.66"
            />
            <path
              class="cls-1"
              d="M17.15,22.86a1.21,1.21,0,0,0-1.21,1.21v6h2.42v-6A1.21,1.21,0,0,0,17.15,22.86Z"
            />
            <path
              class="cls-1"
              d="M21.29,17.69a1.21,1.21,0,0,0-1.21,1.21V30.06H22.5V18.9A1.21,1.21,0,0,0,21.29,17.69Z"
            />
            <path
              class="cls-1"
              d="M25.44,13.17a1.21,1.21,0,0,0-1.21,1.21V30.06h2.42V14.38A1.21,1.21,0,0,0,25.44,13.17Z"
            />
            <path
              class="cls-1"
              d="M29.58,19.17a1.21,1.21,0,0,0-1.21,1.21v9.68h2.42V20.38A1.21,1.21,0,0,0,29.58,19.17Z"
            />
            <path
              class="cls-1"
              d="M29.32,40.26H11.26a2.34,2.34,0,0,1-2.34-2.34V8.26a2.34,2.34,0,0,1,2.34-2.34H30.65l0,6.07a1.06,1.06,0,0,0,1.08,1c.38,0,3,0,5.25,0v19.3h.25a8,8,0,0,1,1.58.16l-.07-21.06v-.09L32,4.13,32,4.06H11.26a4.2,4.2,0,0,0-4.2,4.2V37.92a4.2,4.2,0,0,0,4.2,4.2H29.54a7.93,7.93,0,0,1-.23-1.85ZM35.61,11H32.77l0-3.11Z"
            />
            <path
              class="cls-1"
              d="M37.27,34.14a6.13,6.13,0,1,0,6.13,6.13A6.13,6.13,0,0,0,37.27,34.14Zm2.48,5H38.11V39a1.19,1.19,0,0,0-.2-.77.64.64,0,0,0-.52-.23.66.66,0,0,0-.5.18.71.71,0,0,0-.18.52.67.67,0,0,0,.17.48,2.2,2.2,0,0,0,.73.41,8.89,8.89,0,0,1,.95.44,2.89,2.89,0,0,1,.65.48,1.9,1.9,0,0,1,.41.62,2.12,2.12,0,0,1,.14.81,1.91,1.91,0,0,1-.54,1.41,2.29,2.29,0,0,1-1.35.62v1h-1V44a2.22,2.22,0,0,1-2-2.37v-.14h1.65v.14a1.14,1.14,0,0,0,.23.78.84.84,0,0,0,.67.26.81.81,0,0,0,.57-.18.63.63,0,0,0,.2-.5.73.73,0,0,0-.18-.53,1.86,1.86,0,0,0-.68-.39,8.83,8.83,0,0,1-.91-.39,3,3,0,0,1-.68-.46,1.93,1.93,0,0,1-.45-.63,2.12,2.12,0,0,1-.16-.86,1.92,1.92,0,0,1,.55-1.41,2.27,2.27,0,0,1,1.33-.62v-1h1v1.05a2.1,2.1,0,0,1,1.28.73,2.46,2.46,0,0,1,.51,1.6Z"
            />
          </svg>
          <p>Detalle Vale</p>
        </b-button>
      </b-col>
      <b-col>
        <b-button
          variant="outline-secondary"
          class="border border-2 border-secundary w-75 mt-3"
          to="/ServiciosPublicos/DetalleProducto/list"
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
              d="M34.79,5.18l0,0H15.65l-8,10.41,0,0V38.73a4.58,4.58,0,0,0,4.55,4.59H38.27a4.58,4.58,0,0,0,4.55-4.59V15.58ZM26.46,7h7.43l6.44,8.54H26.46ZM16.57,7h8.08v8.54H10.21ZM41,38.73a2.75,2.75,0,0,1-2.74,2.76H12.19a2.75,2.75,0,0,1-2.74-2.76V17.35H41Z"
            />
            <path
              class="cls-1"
              d="M25.22,27.37h8.62a.75.75,0,0,0,0-1.5H25.22a.75.75,0,0,0,0,1.5Z"
            />
            <path
              class="cls-1"
              d="M25.22,23h8.62a.75.75,0,0,0,0-1.5H25.22a.75.75,0,0,0,0,1.5Z"
            />
            <path
              class="cls-1"
              d="M16.26,31.78H33.84a.75.75,0,0,0,0-1.5H16.26a.75.75,0,0,0,0,1.5Z"
            />
            <path
              class="cls-1"
              d="M16.26,36.19H33.84a.75.75,0,0,0,0-1.5H16.26a.75.75,0,0,0,0,1.5Z"
            />
            <path
              class="cls-1"
              d="M16.26,28h5.52a.76.76,0,0,0,.75-.75V21.72a.75.75,0,0,0-.75-.75H16.26a.74.74,0,0,0-.74.75v5.51A.75.75,0,0,0,16.26,28ZM17,22.47h4v4H17Z"
            />
          </svg>
          <p>Detalle Producto</p>
        </b-button>
      </b-col>
    </b-row>
  </b-card>
</template>

<script>
export default {}
</script>

<style></style>
