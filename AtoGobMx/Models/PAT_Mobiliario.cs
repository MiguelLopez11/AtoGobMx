﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtoGobMx.Models
{
    public class PAT_Mobiliario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MobiliarioId { get; set; }
        public string CodigoInventario { get; set; }
        public string Descripción { get; set; }
        public int AreaId { get; set; }
        public int TipoMobiliarioId { get; set; }
        public int DepartamentoId { get; set; }
        public bool Archivado { get; set; }

        [ForeignKey("AreaId")]
        public Area? Area { get; set; }
        [ForeignKey("TipoMobiliarioId")]
        public PAT_TipoMobiliario? TipoMobiliario { get; set; }
        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }


    }
}
