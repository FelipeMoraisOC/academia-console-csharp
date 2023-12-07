using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    [Table("AlunosPlanos")]
    public class AlunoPlano
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AlunoPlanoId {get; set;}

        [ForeignKey("AlunoId")]
        public Aluno Aluno {get; set;}
        public int? AlunoId{get; set;}

        [ForeignKey("PlanoId")]
        public Plano Plano {get; set;}
        public int? PlanoId{get;set;}
        public DateTime DataInicio {get;set;}
        public bool Ativo {get; set;}
        
        public AlunoPlano(int? alunoPlanoId, int? alunoId, int? planoId, DateTime dataInicio)
        {
            AlunoPlanoId = alunoPlanoId;
            AlunoId = alunoId;
            PlanoId = planoId;
            DataInicio = dataInicio;

        }
    }
}