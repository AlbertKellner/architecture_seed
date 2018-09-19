namespace DataEntity.Model
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Relations;

    [Table("Laboratorios")]
    public class LaboratorioEntity : BaseEntity
    {
        public int UsuarioEntityId { get; set; }

        public string Nome { get; set; }

        public ICollection<RelationLaboratorioFarmacia> Farmacias { get; set; }

        public ICollection<RelationLaboratorioMedico> Medicos { get; set; }

        public bool IsValid() => true;
    }
}