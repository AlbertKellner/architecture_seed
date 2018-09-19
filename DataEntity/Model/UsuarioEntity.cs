namespace DataEntity.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Usuarios")]
    public class UsuarioEntity : BaseEntity
    {
        public UsuarioEntity()
        {
            //Laboratorios = new List<LaboratorioEntity>();
            //Farmacias = new List<FarmaciaEntity>();
            //Medicos = new List<MedicoEntity>();
            //Pacientes = new List<PacienteEntity>();
        }
       
        [Required]
        public string IdentityId { get; set; }

        [NotMapped]
        public AppUser Identity { get; set; }

        //public ICollection<LaboratorioEntity> Laboratorios { get; set; }
        //public ICollection<FarmaciaEntity> Farmacias { get; set; }
        //public ICollection<MedicoEntity> Medicos { get; set; }
        //public ICollection<PacienteEntity> Pacientes { get; set; }

        public bool IsValid()
        {
            ValidationErrors = new List<string>();

            //if (string.IsNullOrEmpty(Password))
            //    ValidationErrors.Add("Senha Inválida");

            //if (!HelperMethods.IsValidEmail(UserName))
            //	ValidationErrors.Add("E-mail inválido");

            if (string.IsNullOrEmpty(IdentityId))
                ValidationErrors.Add("Usuario nao possui identidade");

            return !ValidationErrors.Any();
        }
    }
}