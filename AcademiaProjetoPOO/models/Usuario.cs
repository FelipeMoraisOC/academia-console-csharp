using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

namespace EFCore
{
    [Table("Usuarios")]
    public class Usuario
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public UsuarioTipo UsuarioTipo {get; set;}
        
        public Usuario(){

        }

        public Usuario(int? usuarioId, string nome, string sobrenome, string email, string senha, string cpf, UsuarioTipo usuarioTipo) 
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
            CPF = cpf;
            UsuarioTipo = usuarioTipo;
        }

        public override string ToString()
        {
            return $"Id:{UsuarioId}, Nome:{Nome}, Sobrenome:{Sobrenome}, E-mail:{Email}, CPF:{CPF}, UsuarioTipo:{UsuarioTipo}";
        }

    }
}
