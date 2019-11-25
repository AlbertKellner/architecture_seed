using System.Collections.Generic;

namespace ApiEndpoint.Models.Response
{
    public class LaboratorioResponseModel : BaseResponseModel
    {
        public string Nome { get; set; }

        public List<MedicoResponseModel> Medicos { get; set; }
        public List<FarmaciaResponseModel> Farmacias { get; set; }
    }
}