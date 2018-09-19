namespace DataEntity.Model.Relations
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class RelationFarmaciaMedico
    {
        [Column(Order = 0)]
        public int FarmaciaId { get; set; }

        public FarmaciaEntity Farmacia { get; set; }

        [Column(Order = 1)]
        public int MedicoId { get; set; }

        public MedicoEntity Medico { get; set; }
    }
}