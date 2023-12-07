
using System.Drawing;
using System.Security.AccessControl;
using ConsoleTables;
using EFCore;
using Enums;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using EFCore;
namespace Services;

public class UsuarioService
{
    private UsuarioRepository _usuarioRepository = new UsuarioRepository();
    
    public Usuario AdicionarUsuario()
    {
        string nome, sobrenome, email, senha, cpf;
        UsuarioTipo usuarioTipo = new UsuarioTipo();

        Console.WriteLine("--CADASTRO DE UM NOVO USUÁRIO--");
        while(true)
        {
            try{
                Console.WriteLine("Digite o nome: ");
                nome = Console.ReadLine();
                if(string.IsNullOrEmpty(nome)) throw new ArgumentException("Nome não pode estar vazio! Digite novamente.");

                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        while(true)
        {
            try{
                Console.WriteLine("Digite o Sobrenome: ");
                sobrenome = Console.ReadLine();

                if(string.IsNullOrEmpty(sobrenome)) throw new ArgumentException("Sobrenome não pode estar vazio! Digite novamente.");

                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            
            
        }
        while(true)
        {
            try{
                Console.WriteLine("Digite o e-mail: ");
                email = Console.ReadLine();

                if(string.IsNullOrEmpty(email)) throw new ArgumentException("E-mail não pode estar vazio! Digite novamente.");
                
                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            
            
        }
        while(true)
        {
            try{
                Console.WriteLine("Digite o CPF: ");
                cpf = Console.ReadLine();

                if(string.IsNullOrEmpty(cpf)) throw new ArgumentException("CPF não pode estar vazio! Digite novamente.");

                if(_usuarioRepository.ExisteNaBaseDeDados("Usuarios", "CPF", cpf) != null) 
                throw new ArgumentException("Usuario com esse CPF já existente na base de dados! Digite novamente.");

                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        while(true)
        {
            try{
                Console.WriteLine("Digite a Senha: ");
                senha = Console.ReadLine();

                if(string.IsNullOrEmpty(senha)) throw new ArgumentException("Senha não pode estar vazio! Digite novamente.");
                
                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            
            
        }
        while(true)
        {
           
            int ut = 0;
            Console.WriteLine("Escolha o Tipo de Usuario que ele será: ");
            Console.WriteLine("1 - Admin\n2 -Professor\n3 - Atendente\nDigite o numero de sua escolha:");
            Checkbox cbUsuarios = new Checkbox(
                $"Selecione o Tipo de Usuário que ele será:", 
                "Admin", 
                "Professor",
                "Atendente"
            );
            foreach (var checkboxReturn in cbUsuarios.Select())
            {
                ut = checkboxReturn.Index;
            }
            switch(ut)
            {
                case 0: 
                    usuarioTipo = UsuarioTipo.Admin;
                break;
                case 1:
                    usuarioTipo = UsuarioTipo.Professor;
                break;
                case 2:
                    usuarioTipo = UsuarioTipo.Atendente;
                break;
            }
            break;
            
        }

        try
        {
            Usuario usuario = new Usuario(null, nome, sobrenome, email, senha, cpf, usuarioTipo);
            _usuarioRepository.Inserir(usuario);

            return usuario;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
             throw;
        }
        
    }

    public Usuario Logar()
    {
        string cpf, senha;
        Console.WriteLine("--FAÇA O LOGIN--");
        while(true)
        {
            try{
                Console.WriteLine("Digite o CPF: ");
                cpf = Console.ReadLine();

                if(string.IsNullOrEmpty(cpf)) throw new ArgumentException("CPF não pode estar vazio! Digite novamente.");
                
                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        while(true)
        {
            try{
                Console.WriteLine("Digite a Senha: ");
                senha = Console.ReadLine();

                if(string.IsNullOrEmpty(senha)) throw new ArgumentException("Senha não pode estar vazio! Digite novamente.");
                
                break;
            } 
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: " + ex.Message);
                Console.ResetColor();
            }  
        }
        try
        {
            return _usuarioRepository.logarUsuario(cpf, senha);


        } catch(Exception ex)
        {
            throw new ArgumentException("Login inválido!");

        }

    }

    public string ExibirListaUsuarios()
    {
        List<Usuario> usuarios = _usuarioRepository.ObterTodos();
        var table = new ConsoleTable("UsuarioId", "Nome", "Sobrenome", "Email", "Senha", "Tipo");
    
        foreach(var u in usuarios)
        {
           table.AddRow(u.UsuarioId, u.Nome, u.Sobrenome, u.Email, u.Senha, u.UsuarioTipo);
        }

        return table.ToStringAlternative();

    }

    public void AlterarUsuarioPorId()
    {
        string usuarioId = "";
        string campo = "";

        try
        {
            
            Console.WriteLine("Digite o Id do Usuario que deseja alterar: ");
            usuarioId = Console.ReadLine();

            if(_usuarioRepository.ExisteNaBaseDeDados("Usuarios", "UsuarioId", usuarioId) == null) throw new ArgumentException("Usuario Id inexistente");

    
            while(true)
            {
                try
                {
                    int opcao = 0;
                    Checkbox cbUsuarios = new Checkbox(
                        $"PÁGINA USUÁRIO > ALTERAR USUÁRIO\n\nQual campo deseja alterar?\n", 
                        "Voltar", 
                        "Nome",
                        "Sobrenome",
                        "E-mail",
                        "Senha"
                    );
                    foreach (var checkboxReturn in cbUsuarios.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }
                        
                    if(opcao == 0) break;
                    else if(opcao == 1) campo = "Nome";
                    else if(opcao == 2) campo = "Sobrenome";
                    else if(opcao == 3) campo = "Email";
                    else if(opcao == 4) campo = "Senha";

                    Console.WriteLine("Digite o novo valor: ");
                    string valor = Console.ReadLine();
                    
                    _usuarioRepository.AtualizarCampo(filtro: e => e.UsuarioId == Int32.Parse(usuarioId), campo, valor);
                    
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine("Dado atualizado com sucesso!");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: " + ex.Message);
                    Console.ResetColor();
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: " + ex.Message);
            Console.ResetColor();
        }
    }

    public void RemoverUsuarioPorId()
    {
        int usuarioId;

        Console.WriteLine("PÁGINA USUÁRIO > REMOVER USUÁRIO");
        try
        {
            
            Console.WriteLine("Digite o Id do Usuário que deseja remover: ");
            usuarioId = Int32.Parse(Console.ReadLine());

            if(_usuarioRepository.ExisteNaBaseDeDados("Usuarios", "UsuarioId", usuarioId.ToString()) == null) throw new ArgumentException("Usuario Id inexistente");

    
            while(true)
            {
                try
                {
                    int opcao = 0;
                        
                    Console.WriteLine("Tem certeza que deseja exculir?\n");
                    Checkbox cbUsuarios = new Checkbox(
                        $"PÁGINA USUÁRIO > REMOVER USUÁRIO\n\nTem certeza que deseja exculir?\n", 
                        "Não",
                        "Sim"
                    );
                    foreach (var checkboxReturn in cbUsuarios.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }
                    
                    if(opcao == 1) _usuarioRepository.Excluir(usuarioId);
                    else break;
                    
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine("Usuario removido com sucesso!");
                    Console.ResetColor();
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: " + ex.Message);
                    Console.ResetColor();
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: " + ex.Message);
            Console.ResetColor();
        }
    }
}