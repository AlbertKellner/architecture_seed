namespace ApiEndpoint.ViewModels.Request
{
    using System.Collections.Generic;

    public class TaskListRequestModel : BaseRequestModel
    {
        public string Name { get; set; }

        public List<TaskRequestModel> Tasks { get; set; }

        public bool IsValid() => true;
    }
}