//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace AtoGobMx.Models
//{
//    public class PAT_Teclado
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int TecladoId { get; set; }
//        public string? Marca { get; set; }
//        public string? TipoConexion { get; set; }
//        public int? EquipoComputoId { get; set; }
//        public bool Archivado { get; set; }

//        [ForeignKey("EquipoComputoId")]
//        public PAT_EquipoComputo? EquipoComputo { get; set; }
//    }
//}
