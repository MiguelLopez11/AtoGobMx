﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ExpedienteAlumbrado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteAlumbradoId { get; set; }
        //public string Tarea { get; set; }
        public DateTime? FechaAlta { get; set; }
        //public DateTime FechaBaja { get; set; }
        //public string Domicilio { get; set; }
        //public string DescripcionDomicilio { get; set; }
        //departamento, areas
        public string? DescripcionSolucion { get; set; }
        public string? LugarPublico { get; set; }
        public string? Localidad { get; set; }
        public bool Archivado { get; set; }
        public int? AlumbradoId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? AreaId { get; set; }

        [ForeignKey("AlumbradoId")]
        public Alumbrado? Alumbrado { get; set; }

        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }

        [ForeignKey("AreaId")]
        public Area? Area { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<EmpleadosAlumbrado>? EmpleadosAlumbrado { get; set; }
    }
}
