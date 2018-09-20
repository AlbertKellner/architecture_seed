namespace DataTransferObject
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TaskLists")]
    public class TaskListDto : BaseEntityDto
    {
        public string Name { get; set; }

        public List<TaskDto> Tasks { get; set; }

        public bool IsValid() => true;
    }
}