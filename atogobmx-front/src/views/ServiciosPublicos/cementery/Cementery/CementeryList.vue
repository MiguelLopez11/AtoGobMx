<template>
  <b-card class="m-3">
    <b-row align-h="end" class="mb-3 mr-1">
      <b-form-input
        size="lg"
        style="width: 350px"
        v-model="searchvalue"
        type="search"
        placeholder="Buscar Cementerios..."
      ></b-form-input>
      <b-button
        variant="primary"
        style="
            height: 50px;
            width: auto;
            font-size: 18px;
            margin-right: 15px;
            margin-left: 20px;
        "
        v-b-modal.modal-cementery
        type="submit"
      >
        <i class="bi bi-person-plus-fill"></i>
        Cementerios
      </b-button>
    </b-row>
    <EasyDataTable
      rows-per-page-message="registro por pagina"
      empty-message="No se encontro ningun registro"
      table-class-name="customize-table"
      buttons-pagination
      border-cell
      :loading="isloading"
      :headers="fields"
      :items="cementeryService"
      :rows-per-page="5"
      :search-field="searchField"
      :search-value="searchValue"
      :tbale-height="330"
    >
      <template #header-actions="header">
        {{ header.text }}
      </template>
      <template #item-actions="items">
        <b-button
          @click="RemoveCementeryService(items.cementeriosId)"
          class="m-1"
          variant="outline-danger"
          ><i class="bi bi-trash3"></i
        ></b-button>
        <b-button
          class="m-1"
          variant="outline-warning"
          :to="{
            name: 'Cementerios-Edit',
            params: { CementeriosId: items.cementeriosId }
          }"
          ><i class="bi bi-pencil-square"></i
        ></b-button>
      </template>
    </EasyDataTable>

    <b-modal
      id="modal-cementery"
      tittle="Agregar Cementerio"
      size="xl"
      hide-footer
      button-size="lg"
      lazy
    >
      <Form @submit="addCementeryService">
        <b-row cols="2">
          <b-col>
            <b-form-group class="mt-3" label="Nombre del propietario">
              <Field name="PropietaryField" :rules="validatePropietary">
                <b-form-input
                  v-modal="CementeryServiceFields.nombrePropietario"
                  :state="PropietaryState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="PropietaryField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>

            <b-form-group class="mt-3" label="Numero de espacios">
              <Field name="SpacesField" :rules="validateSpaces">
                <b-form-input
                  v-modal="CementeryServiceFields.numeroEspacios"
                  :state="SpacesState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="SpacesField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>

            <b-form-group class="mt-3" label="Metros que le corresponden">
              <Field name="MeterField" :rules="validateMeter">
                <b-form-input
                  v-modal="CementeryServiceFields.metrosCorrespondientes"
                  :state="MeterState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="MeterField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>

            <b-form-group class="mt-3" label="Espacios Disponibles">
              <Field name="AvailableField" :rules="validateAvailable">
                <b-form-input
                  v-modal="CementeryServiceFields.espaciosDisponibles"
                  :state="AvailableState"
                >
                </b-form-input>
              </Field>
              <ErrorMessage name="AvailableField">
                <span>Este campo es requerido</span>
                <i class="bi bi-exclamation-circle" />
              </ErrorMessage>
            </b-form-group>
          </b-col>
        </b-row>

        <b-row align-h="end">
          <b-button
            class="w-auto m-2 text-white"
            variant="primary"
            v-b-modal.modal-cementery
          >
            Cancelar
          </b-button>
          <b-button class="w-auto m-2" variant="success" type="submit">
            Guardar
          </b-button>
        </b-row>
      </Form>
    </b-modal>
  </b-card>
</template>

<script>

</script>

<style></style>
