using System.Collections.Generic;

namespace ApiEndpoint.Models.Request
{
    public class TaskListRequestModel : BaseRequestModel
    {
        public string Name { get; set; }

        public List<TaskRequestModel> Tasks { get; set; }

        public bool IsValid() => true;
    }
}