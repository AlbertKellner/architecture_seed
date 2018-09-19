namespace DataEntity.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Relations;

    [Table("Pacientes")]
    public class PacienteEntity : BaseEntity
    {
        public int UsuarioEntityId { get; set; }

        public string Nome { get; set; }

        public List<RelationFarmaciaPaciente> Farmacias { get; set; }

        public List<RelationMedicoPaciente> Medicos { get; set; }

        public bool IsValid() => true;
    }
}