namespace DataEntity.Model.Relations
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class RelationFarmaciaPaciente
    {
        [Column(Order = 0)]
        public int FarmaciaId { get; set; }

        public FarmaciaEntity Farmacia { get; set; }

        [Column(Order = 1)]
        public int PacienteId { get; set; }

        public PacienteEntity Paciente { get; set; }
    }
}