namespace ApiEndpoint.Models.Response
{
    using System.Collections.Generic;
    using ApiEndpoint.ViewModels.Response;

    public class TaskListRequestModel : BaseResponseModel
    {
        public string Name { get; set; }

        public List<TaskResponseModel> Tasks { get; set; }

        public bool IsValid() => true;
    }
}