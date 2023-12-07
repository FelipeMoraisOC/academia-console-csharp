using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<AlunoPlano> AlunoPlanos { get; set; }
        public DbSet<PagamentoAlunoPlano> PagamentoAlunosPlanos { get; set; }
        public DbSet<AlunoPresenca> AlunosPresencas { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<AlunoTreino> AlunosTreinos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public AppDbContext()
        {
            Usuarios = Set<Usuario>();
            Alunos = Set<Aluno>();
            Planos = Set<Plano>();
            AlunoPlanos = Set<AlunoPlano>();
            PagamentoAlunosPlanos = Set<PagamentoAlunoPlano>();
            AlunosPresencas = Set<AlunoPresenca>();
            Treinos = Set<Treino>();
            AlunosTreinos = Set<AlunoTreino>();
            Exercicios = Set<Exercicio>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=academia_poo", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Carregando...");
                Console.ResetColor();
                
                // modelBuilder.Entity<Usuario>().ToTable("Usuarios");
                // modelBuilder.Entity<Aluno>().ToTable("Alunos");
                // modelBuilder.Entity<Plano>().ToTable("Planos");
                // modelBuilder.Entity<AlunoPlano>().ToTable("AlunosPlanos");
                // modelBuilder.Entity<PagamentoAlunoPlano>().ToTable("PagamentoAlunosPlanos");
                // modelBuilder.Entity<AlunoPresenca>().ToTable("AlunosPresencas");
                // modelBuilder.Entity<Treino>().ToTable("Treinos");
                // modelBuilder.Entity<Exercicio>().ToTable("Exercicios");

                //Setar chaves primárias
                modelBuilder.Entity<Usuario>().HasKey(a => a.UsuarioId);
                modelBuilder.Entity<Aluno>().HasKey(a => a.AlunoId);
                modelBuilder.Entity<Plano>().HasKey(a => a.PlanoId);
                modelBuilder.Entity<AlunoPlano>().HasKey(a => a.AlunoPlanoId);
                modelBuilder.Entity<PagamentoAlunoPlano>().HasKey(a => a.PagamentoAlunoPlanoId);
                modelBuilder.Entity<Treino>().HasKey(a => a.TreinoId);
                modelBuilder.Entity<AlunoPresenca>().HasKey(a => a.AlunoPresencaId);
                modelBuilder.Entity<Exercicio>().HasKey(a => a.ExercicioId);
                modelBuilder.Entity<AlunoTreino>().HasKey(a => a.AlunoTreinoId);


                //Para enums e costumização de dados
                modelBuilder.Entity<Usuario>().Property(p => p.UsuarioTipo).HasConversion(typeof(string));
                modelBuilder.Entity<Plano>().Property(p => p.Duracao).HasConversion(typeof(string));

                modelBuilder.Entity<Aluno>()
                .HasMany(e => e.AlunoPresencas)
                .WithOne(e => e.Aluno)
                .HasForeignKey(e => e.AlunoId)
                .IsRequired();

                //Relacionamento muitos para muitos entre alunos e treinos
                // modelBuilder.Entity<AlunoTreino>()
                // .HasKey(e => new { e.AlunoId, e.TreinoId });

                modelBuilder.Entity<AlunoTreino>()
                .HasOne(at => at.Aluno)
                .WithMany(a => a.AlunosTreinos)
                .HasForeignKey(at => at.AlunoId);

                modelBuilder.Entity<AlunoTreino>()
                .HasOne(at => at.Treino)
                .WithMany(t => t.AlunosTreinos)
                .HasForeignKey(at => at.TreinoId);

                // Relacionamento muitos para muitos entre Treino e Exercicio
                modelBuilder.Entity<TreinoExercicio>()
                    .HasKey(te => new { te.TreinoId, te.ExercicioId });

                modelBuilder.Entity<TreinoExercicio>()
                    .HasOne(te => te.Treino)
                    .WithMany(t => t.TreinosExercicios)
                    .HasForeignKey(te => te.TreinoId);

                modelBuilder.Entity<TreinoExercicio>()
                    .HasOne(te => te.Exercicio)
                    .WithMany(e => e.TreinosExercicios)
                    .HasForeignKey(te => te.ExercicioId);

                // Configurações adicionais, se necessário

            base.OnModelCreating(modelBuilder);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
