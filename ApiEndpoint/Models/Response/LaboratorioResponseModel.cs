namespace ApiEndpoint.ViewModels.Response
{
    using System.Collections.Generic;

    public class LaboratorioResponseModel : BaseResponseModel
    {
        public string Nome { get; set; }

        public List<MedicoResponseModel> Medicos { get; set; }
        public List<FarmaciaResponseModel> Farmacias { get; set; }
    }
}