using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class LudoContext : DbContext
    {
        //public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<GameSession> GameSessions { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Pawn> Pawns { get; set; }

        public LudoContext(DbContextOptions<LudoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameSession>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}