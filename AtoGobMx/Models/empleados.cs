﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class empleados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int idEmpleado { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Nombre { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ApellidoPaterno { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ApellidoMaterno { get; set; }
        [Required]
        public DateTime? FechaNacimiento { get; set; }
        [Required]
        public string? Direccion { get; set; }
        [Required]
        public string CURP { get; set; }
        [Required]
        public string RFC { get; set; }
        [Required]
        public DateTime FechaAlta { get; set; }
        [Required]
        public DateTime? FechaBaja { get; set; }
        [Required]
        [ForeignKey("Areas")]
        public int? AreaTrabajoId { get; set; }
        [Required]
        public bool Archivado { get; set; }

        [JsonIgnore]
        public virtual Areas? Areas { get; set; }

    }
}
