﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class SERMED_Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime FechaVencimiento { get; set; }
        public int? CantidadDisponible { get; set; }
        public int? CantidadFaltante { get; set; }
        public bool Archivado { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<SERMED_ProductosReceta>? ProductosReceta { get; set; }
    }
}
