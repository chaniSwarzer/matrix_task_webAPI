using matrix_task.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Helpers
{
    public class HeroDBcontext : DbContext
    {
        public HeroDBcontext(DbContextOptions<HeroDBcontext> options)
           : base(options)
        {
        }

        public virtual DbSet<Trainer> Trainer { get; set; }
        public virtual DbSet<Hero> Hero { get; set; }
        public virtual DbSet<TrainerHero> TrainerHero { get; set; }
        public virtual DbSet<Ability> Ability { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.HasKey(e => e.TrainerId);
                entity.Property(x => x.PasswordHash).HasColumnType("BINARY(64)");
                entity.Property(x => x.PasswordSalt).HasColumnType("BINARY(128)");

            });

            modelBuilder.Entity<Hero>(entity =>
            {
                entity.HasKey(e => e.HeroId);

                entity.HasOne(d => d.Ability)
                .WithMany(p => p.Heroes)
                .HasForeignKey(d => d.AbilityId);

                entity.Property(x => x.CurrentPower).HasColumnType("decimal(18,4)");
                entity.Property(x => x.StartingPower).HasColumnType("decimal(18,4)");


            });

            modelBuilder.Entity<TrainerHero>(entity =>
            {
                entity.HasKey(e => e.TrainerHeroId);

                entity.HasOne(d => d.Trainer)
                 .WithMany(p => p.TrainerHeroes)
                 .HasForeignKey(d => d.TrainerId);

                entity.HasOne(d => d.Hero)
                    .WithMany(p => p.TrainerHeroes)
                    .HasForeignKey(d => d.HeroId);
            });

            modelBuilder.Entity<Ability>(entity =>
            {
                entity.HasKey(e => e.AbilityId);

            });

            base.OnModelCreating(modelBuilder);


        }
    
        
    }
}
