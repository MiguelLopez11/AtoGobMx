﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class InventarioEstatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventarioEstatusId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Archivado { get; set; }
    }
}
