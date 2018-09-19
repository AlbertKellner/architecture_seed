namespace ApiEndpoint
{
    using System;
    using DataEntity;
    using DataEntity.Model;
    using Microsoft.AspNetCore.Identity;
    using Repository;

    public class IdentityInitializer
    {
        private readonly OnCareContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public IdentityInitializer(OnCareContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_context.Database.EnsureCreated())
                return;

            if (!_roleManager.RoleExistsAsync(Roles.Admin).Result)
            {
                var resultado = _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).Result;

                if (!resultado.Succeeded)
                    throw new Exception($"Role creation error {Roles.Admin}.");
            }

            CreateUser(new AppUser {UserName = "admin_oncare", Email = "AlbertKa@kalunga.com.br", EmailConfirmed = true}, "aA!1234u778", Roles.Admin);

            CreateUser(new AppUser {UserName = "usrSemRole", Email = "usrinvalido@teste.com.br", EmailConfirmed = true}, "aA!1234u778");
        }

        private void CreateUser(AppUser user, string password, string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result != null)
                return;

            var resultado = _userManager.CreateAsync(user, password).Result;

            if (resultado.Succeeded && !string.IsNullOrWhiteSpace(initialRole))
                _userManager.AddToRoleAsync(user, initialRole).Wait();
        }
    }
}