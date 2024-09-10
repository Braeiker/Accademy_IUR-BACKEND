using IUR_Backend.Data;
using IUR_Backend.Settings;
using Microsoft.AspNetCore.Identity;

namespace IUR_Backend.Models
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;

        // Costruttore per iniettare le dipendenze necessarie
        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Metodo per inizializzare il database
        public void Initialize()
        {
            // Verifica se il ruolo "Admin" esiste
            if (_roleManager.FindByNameAsync("UniAdministrator").Result == null)
            {
                // Crea i ruoli "Admin" e "User" se non esistono
                _roleManager.CreateAsync(new Role() { Id = Guid.NewGuid().ToString(), Name = "UniAdministrator" }).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new Role() { Id = Guid.NewGuid().ToString(), Name = "Student" }).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new Role() { Id = Guid.NewGuid().ToString(), Name = "Teacher" }).GetAwaiter().GetResult();

            }
            else
            {
                // Esci dal metodo se i ruoli già esistono
                return;
            }
        }
    }
}