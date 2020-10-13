using Microsoft.EntityFrameworkCore;
using Solution.Autentica.Autoriza.Def.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Autentica.Autoriza.Def.API
{
    public class UsuarioDbContext: DbContext
    {
        
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) 
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("DefaultConnection");
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
