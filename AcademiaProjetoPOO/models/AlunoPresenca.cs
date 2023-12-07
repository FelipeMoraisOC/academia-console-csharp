
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    [Table("AlunosPresencas")]
    public class AlunoPresenca
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 1)]
        public int? AlunoPresencaId { get; set; }

        [Column(Order = 3)]
        public DateTime Data {get; set;}

        [Column(Order = 2)]
        public int AlunoId  {get; set;}
        public Aluno Aluno {get; set;} = null!;

        public AlunoPresenca()
        {

        }
        public AlunoPresenca(int alunoPresencaId,int alunoId, DateTime data)
        {
            AlunoPresencaId = alunoPresencaId;
            AlunoId = alunoId;
            Data = data;
        }
    }
}