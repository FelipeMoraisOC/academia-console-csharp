using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    [Table("Exercicios")]
    public class Exercicio
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ExercicioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }

        public ICollection<TreinoExercicio> TreinosExercicios { get; set; } = new List<TreinoExercicio>();

        // Construtor padrão para Entity Framework Core
        public Exercicio()
        {
        }

        // Construtor personalizado
        public Exercicio(int? exercicioId, string nome, string descricao, int series, int repeticoes)
        {
            ExercicioId = exercicioId;
            Nome = nome;
            Descricao = descricao;
            Series = series;
            Repeticoes = repeticoes;
        }

        public override string ToString()
        {
            return $"Id: {ExercicioId}, Nome: {Nome}, Descricao: {Descricao}, Series: {Series}, Repeticoes: {Repeticoes}";
        }
    }
}