using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyApi.Authorization.Roles;
using MyApi.Authorization.Users;
using MyApi.MultiTenancy;
using MyApi.Customers;
using MyApi.EmailCodes;
using MyApi.Informacoes;
using System.Net;
using MyApi.Informacoes;
using MyApi.Exercicios;
using MyApi.Treinos;
using MyApi.ExercicioTreinos;
using MyApi.UsuarioDados;

namespace MyApi.EntityFrameworkCore
{
    public class MyApiDbContext : AbpZeroDbContext<Tenant, Role, User, MyApiDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmailCode> EmailCodes { get; set; }
        public DbSet<Informacao> Informacoes { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<ExercicioTreino> ExercicioTreinos { get; set; }
        public DbSet<UsuarioDado> UsuarioDados { get; set; }
        public MyApiDbContext(DbContextOptions<MyApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .HasOne(e => e.Informacao)
            .WithOne(a => a.User)
            .HasForeignKey<Informacao>(a => a.UserId);

            modelBuilder.Entity<Treino>()
          .HasOne(e => e.Exercicio)
          .WithMany(a => a.Treinos)
          .HasForeignKey(a => a.ExercicioId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(d => d.UsuarioDado)
                .WithOne(d => d.User)
                .HasForeignKey<UsuarioDado>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
