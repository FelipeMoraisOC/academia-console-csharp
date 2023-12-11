

using System.ComponentModel.DataAnnotations.Schema;


namespace EFCore
{
    [Table("Treinos")]
    public class Treino
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TreinoId { get; set; }
        public string Nome {get; set;}
        public string Descricao {get; set; }
        public string CriadoPor {get; set;}
        public ICollection<AlunoTreino> AlunosTreinos { get; set; }

        public ICollection<TreinoExercicio> TreinosExercicios {get; set;}
        public Treino()
        {
            
        }
        public Treino(int? treinoId, string nome, string descricao, string criadoPor)
        {
            TreinoId = treinoId;
            Nome = nome;
            Descricao = descricao;
            CriadoPor = criadoPor;
        }

        public override string ToString()
        {
            return $"Id: {TreinoId}, Nome: {Nome}, Descricao: {Descricao}, CriadoPor: {CriadoPor}";
        }
    }
}