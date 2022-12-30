﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtoGobMx.Models
{
    public class ArchivosVehiculos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivoVehiculoId { get; set; }
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [MaxLength(100)]
        public string? TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int VehiculoId { get; set; }

        [ForeignKey("VehiculoId")]
        public PAT_Vehiculo? Vehiculo { get; set; }
    }
}
