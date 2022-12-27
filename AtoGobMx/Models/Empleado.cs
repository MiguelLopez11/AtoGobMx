
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AtoGobMx.Models
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpleadoId { get; set; }
        [Required]
        [MaxLength(200)]
        public string? NombreCompleto { get; set; }
        public string? CódigoEmpleado { get; set; }
        public string? Estatus { get; set; }
        public string? Antigüedad { get; set; }
        public float? SueldoQuincenal { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        [Required]
        public bool Archivado { get; set; }
        public bool TieneExpediente { get; set; }
        public bool TieneExpedienteMédico { get; set; }
        public int? DepartamentoId { get; set; }
        public int? PuestoTrabajoId { get; set; }
        [ForeignKey("DepartamentoId")]
        public Departamentos? Departamentos { get; set; }
        [ForeignKey("PuestoTrabajoId")]
        public PuestoTrabajo? PuestoTrabajo { get; set; }


        [JsonIgnore]
        public virtual IEnumerable<PAT_Armeria>? Armería { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<EmpleadosAlumbrado>? EmpleadosAlumbrado { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<PROV_ControlVales>? ControlDeVales { get; set; }
        [JsonIgnore]
        public virtual IEnumerable<SERMED_ExpedienteMedico>? ExpedienteMedico { get; set; }
    }
}
