namespace ApiEndpoint.ViewModels.Request
{
    public class TaskRequestModel : BaseRequestModel
    {
        public string Description { get; set; }

        public bool IsValid() => true;
    }
}