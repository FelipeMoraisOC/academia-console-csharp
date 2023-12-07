

using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    public enum Duracao
    {
        Mensal,
        Semestral,
        Anual
    }

    [Table("Planos")]
    public class Plano
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? PlanoId { get; set; }
        public string Nome {get; set; }
        public string Descricao {get;set;}
        public decimal Preco {get;set;}
        public Duracao Duracao {get;set;}
        public bool EhMensal {get;set;}
        public Plano()
        {
            
        }
        public Plano(int? planoId, string nome, string descricao, decimal preco, Duracao duracao, bool ehMensal, AlunoPlano alunoPlano )
        {
            PlanoId = planoId;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Duracao = duracao;
            EhMensal = ehMensal;
        }

        public override string ToString()
        {
            return $"Aluno: Id={PlanoId}, Nome={Nome}, Descricao={Descricao},  Preco={Preco}, Duracao={Duracao}, EhMensal={EhMensal}";
        }

    }
}