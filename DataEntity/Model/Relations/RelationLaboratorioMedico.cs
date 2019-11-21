namespace DataEntity.Model.Relations
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class RelationLaboratorioMedico
    {
        [Column(Order = 0)]
        public int LaboratorioId { get; set; }

        public LaboratorioEntity Laboratorio { get; set; }

        [Column(Order = 1)]
        public int MedicoId { get; set; }
    }
}