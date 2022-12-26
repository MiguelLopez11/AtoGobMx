﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class ArchivosAseo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArchivosAseoId { get; set; }
        public string Nombre { get; set; }
        public string TipoArchivo { get; set; }
        public bool Archivado { get; set; }
        public int AseoId { get; set; }

        [ForeignKey("AseoId")]
        public Aseo? Aseo { get; set; }
    }
}
