﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class ExpedienteAlumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteAlumbradoId { get; set; }
        public DateTime? FechaAlta { get; set; }
        public string? DescripcionSolucion { get; set; }
        public string? Nomenclatura { get; set; }
        public string? Direccion { get; set; }

        public string? Localidad { get; set; }
        public bool Archivado { get; set; }
        public int? AlumbradoId { get; set; }
        public int? DepartamentoId { get; set; }
        //public int? AreaId { get; set; }

        [ForeignKey("AlumbradoId")]
        public Alumbrado? Alumbrado { get; set; }

        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }

        //[ForeignKey("AreaId")]
        //public Area? Area { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<EmpleadosAlumbrado>? EmpleadosAlumbrado { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<VehiculosAlumbrado>? VehiculosAlumbrado { get; set; }
    }
}
