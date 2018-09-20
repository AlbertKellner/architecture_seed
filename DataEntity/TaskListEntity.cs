namespace DataEntity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TaskList")] //TODO:Renomear para TaskLists
    public class TaskListEntity : BaseEntity
    {
        public string Name { get; set; }

        public List<TaskEntity> Tasks { get; set; }
        public int UsuarioEntityId { get; set; }

        public bool IsValid() => true;
    }
}