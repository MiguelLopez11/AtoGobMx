import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/Login',
    name: 'Login',
    component: () => import('../views/LogIn/LogIn.vue'),
    meta: {
      requiresAuth: false
    }
  },
  {
    path: '/RecursosHumanos',
    name: 'RecursosHumanos',
    component: () => import('../components/ResourcesHumanRoutes'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/ServiciosPublicos',
    name: 'ServiciosPublicos',
    component: () => import('../components/PublicServicesRoutes'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/ObrasPublicas',
    name: 'ObrasPublicas',
    component: () => import('../components/PublickWorks'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/Proveeduria',
    name: 'Proveeduria',
    component: () => import('../components/SupplyRoutes'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/PatrimonioMunicipal',
    name: 'PatrimonioMunicipal',
    component: () => import('../components/PatrimonyMunicipalRoutes'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/ServiciosMedicosMunicipales',
    name: 'ServiciosMedicosMunicipales',
    component: () => import('../components/MedicalServicesRoutes'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/Administrador',
    name: 'Administrador',
    component: () => import('../components/AdministratorRoutes'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador'
    }
  },
  {
    path: '/PageNotAllowed',
    name: 'PageNotPermission',
    component: () => import('../views/PageNotPermission/PageNotPermission.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/',
    name: 'Home',
    component: () => import('../views/Home/HomePage.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/RecursosHumanos/Areas/List',
    name: 'Areas',
    component: () => import('../views/RecursosHumanos/areas/AreaList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'

    }
  },
  {
    path: '/RecursosHumanos/Areas/:AreaId',
    name: 'Area-Edit',
    component: () => import('../views/RecursosHumanos/areas/AreaEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/RecursosHumanos/Empleados/list',
    name: 'Empleados',
    component: () => import('@/views/RecursosHumanos/Employees/EmployeeList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/RecursosHumanos/Empleados/:EmpleadoId',
    name: 'Empleados-Edit',
    component: () => import('@/views/RecursosHumanos/Employees/EmployeeEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/RecursosHumanos/ExpedientesDigitales/list',
    name: 'ExpedientesDigitales',
    component: () => import('@/views/RecursosHumanos/ExpedienteDigital/ExpedientDigitalList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/RecursosHumanos/ExpedientesDigitales/:ExpedienteDigitalId',
    name: 'ExpedienteDigital-edit',
    component: () => import('@/views/RecursosHumanos/ExpedienteDigital/ExpedientDigitalEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  // modulos arturo
  {
    path: '/ServiciosPublicos/AlumbradoPublico/list',
    name: 'AlumbradoPublico',
    component: () => import('@/components/PublicLighting.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/ServiciosPublicos/CementeriosPublicos/list',
    name: 'CementeriosPublicos',
    component: () => import('@/components/PublicCementery.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/ServiciosPublicos/AseoPublico/list',
    name: 'AseoPublico',
    component: () => import('@/components/PublicCleaning.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado'
    }
  },
  {
    path: '/ServiciosPublicos/Alumbrado/list',
    name: 'Alumbrado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/publiclighting/PublicLightingList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/Alumbrado/:AlumbradoId',
    name: 'Alumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/publiclighting/EditLighting.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/EstatusAlumbrado/list',
    name: 'EstatusAlumbradorado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/statuslighting/StatusLightingList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/EstatusAlumbrado/:EstatusId',
    name: 'EstatusAlumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/statuslighting/EditStatusLighting.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/ExpedienteAlumbrado/list',
    name: 'ExpedienteAlumbradorado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/expedientlighting/ExpedientLightingList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/ExpedienteAlumbrado/:ExpedienteAlumbradoId',
    name: 'ExpedienteAlumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/expedientlighting/EditExpedientLighting.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  // {
  //   path: '/InventarioAlumbrado/list',
  //   name: 'InventarioAlumbradorado',
  //   component: () => import('@/views/ServiciosPublicos/streetLighting/inventorylighting/InventoryLightingList.vue'),
  //   meta: {
  //     requiresAuth: true
  //   }
  // },
  // {
  //   path: '/InventarioAlumbrado/:InventarioAlumbradoId',
  //   name: 'InventarioAlumbrado-Edit',
  //   component: () => import('@/views/ServiciosPublicos/streetLighting/inventorylighting/EditInventoryLighting.vue'),
  //   meta: {
  //     requiresAuth: true
  //   }
  // },
  {
    path: '/ServiciosPublicos/TareaTipoAlumbrado/list',
    name: 'TareaTipoAlumbradorado',
    component: () => import('@/views/ServiciosPublicos/streetLighting/typetasklighting/TaskTypeLightingList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/TareaTipoAlumbrado/:TareaTipoAlumbradoId',
    name: 'TareaTipoAlumbrado-Edit',
    component: () => import('@/views/ServiciosPublicos/streetLighting/typetasklighting/EditTaskTypeLighting.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/Cementerios/list',
    name: 'Cementerios',
    component: () => import('@/views/ServiciosPublicos/cementery/Cementery/CementeryList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/Cementerios/:CementeriosId',
    name: 'Cementerios-Edit',
    component: () => import('@/views/ServiciosPublicos/cementery/Cementery/EditCementery.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/DireccionCementerios/list',
    name: 'DireccionCementerios',
    component: () => import('@/views/ServiciosPublicos/cementery/addresscementery/AddressCementeryList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/DireccionCementerios/:DireccionId',
    name: 'DireccionCementerios-Edit',
    component: () => import('@/views/ServiciosPublicos/cementery/addresscementery/EditAddressCementery.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/Aseo/list',
    name: 'Aseo',
    component: () => import('@/views/ServiciosPublicos/cleanness/Cleanness/CleannessList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/Aseo/:AseoId',
    name: 'Aseo-Edit',
    component: () => import('@/views/ServiciosPublicos/cleanness/Cleanness/EditCleanness.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/Ruta/list',
    name: 'Ruta',
    component: () => import('@/views/ServiciosPublicos/cleanness/Road/RoadList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  {
    path: '/ServiciosPublicos/Ruta/:RutaId',
    name: 'Ruta-Edit',
    component: () => import('@/views/ServiciosPublicos/cleanness/Road/EditRoad.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Servicios Publicos'
    }
  },
  // {
  //   path: '/Zona/list',
  //   name: 'Zona',
  //   component: () => import('@/views/ServiciosPublicos/cleanness/Zone/ZoneList.vue'),
  //   meta: {
  //     requiresAuth: true
  //   }
  // },
  // {
  //   path: '/Zona/:ZonaId',
  //   name: 'Zona-Edit',
  //   component: () => import('@/views/ServiciosPublicos/cleanness/Zone/EditZone.vue'),
  //   meta: {
  //     requiresAuth: true
  //   }
  // },
  // {
  //   path: '/ServiciosPublicos/AlumbradoEmpleado/list',
  //   name: 'AlumbradoEmpleado',
  //   component: () => import('@/views/ServiciosPublicos/streetLighting/lightingEmployee/LightingEmployeeList.vue'),
  //   meta: {
  //     requiresAuth: true,
  //     rol: 'Empleado',
  //     departamento: 'Servicios Publicos'
  //   }
  // },
  {
    path: '/ServiciosPublicos/ObrasPublicas/list',
    name: 'ObrasPublicas',
    component: () => import('@/views/ServiciosPublicos/publicworks/publicWorks/PublicWorksList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Obras Publicas'
    }
  },
  {
    path: '/ServiciosPublicos/ObrasPublicas/:ObraId',
    name: 'ObrasPublicas-Edit',
    component: () => import('@/views/ServiciosPublicos/publicworks/publicWorks/EditPublicWorks.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Obras Publicas'
    }
  },
  {
    path: '/ServiciosPublicos/EstatusOP/list',
    name: 'EstatusOP',
    component: () => import('@/views/ServiciosPublicos/publicworks/worksStatus/WorksStatusList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Obras Publicas'
    }
  },
  {
    path: '/ServiciosPublicos/EstatusOP/:EstatusObraId',
    name: 'EstatusOP-Edit',
    component: () => import('@/views/ServiciosPublicos/publicworks/worksStatus/EditWorksStatus.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Obras Publicas'
    }
  },
  {
    path: '/ServiciosPublicos/DetalleProducto/list',
    name: 'DetalleProducto',
    component: () => import('@/views/Supply/detailproduct/DetailProductList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/DetalleProducto/:DetalleProductoId',
    name: 'DetalleProducto-Edit',
    component: () => import('@/views/Supply/detailproduct/EditDetailProduct.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/DetalleVale/list',
    name: 'DetalleVale',
    component: () => import('@/views/Supply/detailvoucher/DetailVoucherList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/DetalleVale/:DetalleValeId',
    name: 'DetalleVale-Edit',
    component: () => import('@/views/Supply/detailvoucher/EditDetailVoucher.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/Producto/list',
    name: 'Producto',
    component: () => import('@/views/Supply/productvoucher/ProductVoucherList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/Producto/:ProductoId',
    name: 'Producto-Edit',
    component: () => import('@/views/Supply/productvoucher/EditProductVoucher.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/Proveedor/list',
    name: 'Proveedor',
    component: () => import('@/views/Supply/provider/ProviderList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/Proveedor/:ProveedorId',
    name: 'Proveedor-Edit',
    component: () => import('@/views/Supply/provider/EditProvider.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/EstatusVale/list',
    name: 'EstatusVale',
    component: () => import('@/views/Supply/statusvoucher/StatusVoucherList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/EstatusVale/:EstatusValeId',
    name: 'EstatusVale-Edit',
    component: () => import('@/views/Supply/statusvoucher/EditStatusVoucher.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/TipoVale/list',
    name: 'TipoVale',
    component: () => import('@/views/Supply/typevoucher/TypeVoucherList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administardor',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/TipoVale/:TipoId',
    name: 'TipoVale-Edit',
    component: () => import('@/views/Supply/typevoucher/EditTypeVoucher.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/ControlVale/list',
    name: 'ControlVale',
    component: () => import('@/views/Supply/vouchercontrol/VoucherControlList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  {
    path: '/ServiciosPublicos/ControlVale/:ControlValeId',
    name: 'ControlVale-Edit',
    component: () => import('@/views/Supply/vouchercontrol/EditVoucherControl.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Control de vales'
    }
  },
  // termino modulos arturo
  {
    path: '/Administrador/Usuarios/list',
    name: 'Usuarios',
    component: () => import('@/views/Users/UserList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de Sistemas y Tecnologías de la Comunicación'
    }
  },
  {
    path: '/Usuarios/:usuarioId',
    name: 'Usuarios-Edit',
    component: () => import('@/views/Users/UserEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de Sistemas y Tecnologías de la Comunicación'
    }
  },
  {
    path: '/Administrador/Roles/list',
    name: 'Roles',
    component: () => import('@/views/Roles/RoleList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de Sistemas y Tecnologías de la Comunicación'
    }
  },
  {
    path: '/Administrador/Roles/:RoleId',
    name: 'Roles-Edit',
    component: () => import('@/views/Roles/RoleEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Dirección de Sistemas y Tecnologías de la Comunicación'
    }
  },
  {
    path: '/RecursosHumanos/Departamentos/list',
    name: 'Departamentos',
    component: () => import('@/views/RecursosHumanos/Departaments/DepartamentList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/RecursosHumanos/Departamentos/:DepartamentoId',
    name: 'Departamentos-Edit',
    component: () => import('@/views/RecursosHumanos/Departaments/DepartamentEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/RecursosHumanos/PuestosTrabajos/list',
    name: 'PuestosTrabajos',
    component: () => import('@/views/RecursosHumanos/WorkStation/WorkStationList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/RecursosHumanos/PuestosTrabajos/:puestoTrabajoId',
    name: 'PuestoTrabajo-Edit',
    component: () => import('@/views/RecursosHumanos/WorkStation/WorkStationEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Recursos Humanos'
    }
  },
  {
    path: '/PatrimonioMunicipal/EquiposComputo/list',
    name: 'EquiposComputo',
    component: () => import('@/views/Patrimony/Computers/Computer/ComputerList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/EquiposComputo/:EquipoComputoId',
    name: 'EquiposComputo-Edit',
    component: () => import('@/views/Patrimony/Computers/Computer/ComputerEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/EstatusEquipo/list',
    name: 'EstatusEquipo',
    component: () => import('@/views/Patrimony/Computers/StatusComputer/StatusComputerList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/EstatusEquipo/:EstatusEquipoId',
    name: 'EstatusEquipo-edit',
    component: () => import('@/views/Patrimony/Computers/StatusComputer/StatusComputerEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/Vehiculos/list',
    name: 'Vehiculos',
    component: () => import('@/views/Patrimony/Vehicles/VehiclesList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/Vehiculos/:VehiculoId',
    name: 'Vehiculos-Edit',
    component: () => import('@/views/Patrimony/Vehicles/VehicleEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/Mobiliarios/list',
    name: 'Mobiliarios',
    component: () => import('@/views/Patrimony/Furnitures/FurnitureList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/Mobiliarios/:MobiliarioId',
    name: 'Mobiliarios-Edit',
    component: () => import('@/views/Patrimony/Furnitures/FurnitureEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/CategoriasMobiliario/list',
    name: 'CategoriasMobiliario',
    component: () => import('@/views/Patrimony/Furnitures/TypeFurnitures/TypeFurnitureList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/CategoriasMobiliario/:TipoMobiliarioId',
    name: 'CategoriasMobiliario-Edit',
    component: () => import('@/views/Patrimony/Furnitures/TypeFurnitures/TypeFurnitureEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/EstatusVehiculo/list',
    name: 'EstatusVehiculo',
    component: () => import('@/views/Patrimony/Vehicles/StatusVehicles/StatusVehicleList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/EstatusVehiculo/:EstatusVehiculoId',
    name: 'EstatusVehiculo-Edit',
    component: () => import('@/views/Patrimony/Vehicles/StatusVehicles/StatusVehicleEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Administrador',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/Armeria/list',
    name: 'Armeria',
    component: () => import('@/views/Patrimony/Armory/ArmoryList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/PatrimonioMunicipal/Armeria/:ArmaId',
    name: 'Armeria-Edit',
    component: () => import('@/views/Patrimony/Armory/ArmoryEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Patrimonio'
    }
  },
  {
    path: '/ServiciosMedicos/Cita/list',
    name: 'ServiciosMedicos-Cita',
    component: () => import('@/views/MunicipalMedicalServices/MedicalAppointment/MedicalAppointmentList.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Direccion de servicios medicos municipales'
    }
  },
  {
    path: '/ServiciosMedicos/Cita/:CitaId',
    name: 'ServiciosMedicos-Cita-Edit',
    component: () => import('@/views/MunicipalMedicalServices/MedicalAppointment/MedicalAppointmentEdit.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Direccion de servicios medicos municipales'
    }
  },
  {
    path: '/ServiciosMedicos/Agenda',
    name: 'ServiciosMedicos-Agenda',
    component: () => import('@/views/MunicipalMedicalServices/Schedule/ScheduleMedical.vue'),
    meta: {
      requiresAuth: true,
      rol: 'Empleado',
      departamento: 'Direccion de servicios medicos municipales'
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})
router.beforeEach(async (to, from, next) => {
  const isLoggedIn = window.sessionStorage.getItem('isLogged')
  const role = window.sessionStorage.getItem('Role')
  const departamento = window.sessionStorage.getItem('Departamento')
  if (['Login'].includes(to.name) && isLoggedIn) {
    next({ name: 'Home' })
  } else if (to.meta.requiresAuth && !isLoggedIn) {
    next({
      path: '/Login',
      query: { redirect: to.fullPath }
    })
  } else if (to.meta.rol && to.meta.departamento && departamento !== to.meta.departamento && role !== 'Administrador') {
    next({ name: 'PageNotPermission' })
  } else {
    next()
  }
})
export default router
