namespace ApiEndpoint.ViewModels.Response
{
    using System.Collections.Generic;

    public class UsuarioResponseModel : BaseResponseModel
    {
        public List<LaboratorioResponseModel> Laboratorios { get; set; }
        public List<FarmaciaResponseModel> Farmacias { get; set; }
        public List<MedicoResponseModel> Medicos { get; set; }
        public List<PacienteResponseModel> Pacientes { get; set; }
    }
}