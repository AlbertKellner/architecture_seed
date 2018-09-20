namespace DataEntity.Model.Relations
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class RelationMedicoPaciente
    {
        [Column(Order = 0)]
        public int MedicoId { get; set; }

        public TodoListEntity Medico { get; set; }

        [Column(Order = 1)]
        public int PacienteId { get; set; }

        public PacienteEntity Paciente { get; set; }
    }
}