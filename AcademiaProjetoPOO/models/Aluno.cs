using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    [Table("Alunos")]
    public class Aluno
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? AlunoId { get; set; }
        public string Email {get; set;}
        public string Nome {get; set;}
        public string Sobrenome {get;set;}
        public string CPF {get; set;}
        public int Idade {get; set;}

        public ICollection<AlunoPresenca> AlunoPresencas { get; } = new List<AlunoPresenca>();
        public ICollection<AlunoTreino> AlunosTreinos {get; set;}

        public Aluno()
        {

        }
        public Aluno(int? alunoId, string email, string nome, string sobrenome, string cpf, int idade)
        {
            AlunoId = alunoId;
            Email = email;
            Nome = nome;
            Sobrenome = sobrenome;
            CPF = cpf;
            Idade = idade;
        }

        public override string ToString()
        {
            return $"Aluno: Id:{AlunoId}, Nome:{Nome}, Sobrenome:{Sobrenome}, E-mail:{Email}, CPF:{CPF}, Idade:{Idade}";
        }
    }
}