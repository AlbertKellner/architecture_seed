namespace DataEntity.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Relations;

    [Table("Farmacias")]
    public class FarmaciaEntity : BaseEntity
    {
        public int UsuarioEntityId { get; set; }

        public string Nome { get; set; }

        public ICollection<RelationLaboratorioFarmacia> Laboratorios { get; set; }

        public ICollection<RelationFarmaciaMedico> Medicos { get; set; }

        public ICollection<RelationFarmaciaPaciente> Pacientes { get; set; }

        public bool IsValid() => true;
    }
}