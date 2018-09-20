namespace ApiEndpoint.ViewModels.Request
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class TaskRequestModel : BaseRequestModel
    {
        public string Description { get; set; }

        public bool IsValid() => true;
    }
}