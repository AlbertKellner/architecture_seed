namespace ApiEndpoint.Models.Response
{
    using System.Collections.Generic;

    public class TaskListResponseModel : BaseResponseModel
    {
        public string Name { get; set; }

        public List<TaskResponseModel> Tasks { get; set; }

        public bool IsValid() => true;
    }
}