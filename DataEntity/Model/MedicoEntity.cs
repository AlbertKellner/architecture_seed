namespace DataEntity.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Relations;

    [Table("Medicos")]
    public class TodoListEntity : BaseEntity
    {
        public int UsuarioEntityId { get; set; }

        public string Nome { get; set; }

        public List<RelationMedicoPaciente> Pacientes { get; set; }

        public List<RelationLaboratorioMedico> Laboratorios { get; set; }

        public List<RelationFarmaciaMedico> Farmacias { get; set; }

        public bool IsValid() => true;
    }
}