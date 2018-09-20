namespace DataEntity
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tasks")]
    public class TaskEntity : BaseEntity
    {
        public string Description { get; set; }
        public int UsuarioEntityId { get; set; }

        public bool IsValid() => true;
    }
}