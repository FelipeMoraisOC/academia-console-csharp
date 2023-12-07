using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    [Table("AlunosTreinos")]
    public class AlunoTreino
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AlunoTreinoId { get; set; }

        [ForeignKey("AlunoId")]
        public Aluno Aluno {get;} = null!;
        public int AlunoId {get; set;}
        
        [ForeignKey("TreinoId")]
        public Treino Treino {get;} = null!;
        public int TreinoId{get; set;}

        public string DiaSemana {get; set;}

        public AlunoTreino(int? alunoTreinoId, int alunoId, int treinoId, string diaSemana)
        {
            AlunoTreinoId = alunoTreinoId;
            AlunoId = alunoId;
            TreinoId = treinoId;
            DiaSemana = diaSemana;
        }

    }
}