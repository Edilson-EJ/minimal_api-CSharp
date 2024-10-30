using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Infraestrutura.Db
{
    public class DbContexto : DbContext
    {
        private readonly IConfiguration _configurationAppSettings;

        public DbContexto(IConfiguration configurationAppSettings)
        {
            _configurationAppSettings= configurationAppSettings;
        }

        public DbSet<Administrador> Administradores { get; set; } = default;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>().HasData(
                new Administrador
                {
                    Email = "administrador@teste.com",
                    Senha = "123456",
                    Perfil = "Adm"
                }
            );
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConexao = _configurationAppSettings.GetConnectionString(("mysql")?.ToString());

            if (!string.IsNullOrEmpty(stringConexao))
            {
                optionsBuilder.UseMySql("mysql", 
                    ServerVersion.AutoDetect("mysql")
                
                );
            }
            
            
        }
    }
}
