namespace DataTransferObject
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tasks")]
    public class TaskDto : BaseEntityDto
    {
        public string Description { get; set; }
    }
}