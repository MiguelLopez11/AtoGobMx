//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace AtoGobMx.Models
//{
//    public class PROV_DetalleProducto
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int DetalleProductoId { get; set; }
       
//        public int? DetalleValeId { get; set; }
//        public float? Precio { get; set; }
//        public bool Archivado { get; set; }

       
//        [ForeignKey("DetalleValeId")]
//        public PROV_DetalleVale? PROV_DetalleVale { get; set; }
//    }
//}
