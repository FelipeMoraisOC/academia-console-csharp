

using System.ComponentModel;
using System.Diagnostics;
using EFCore;
using Enums;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

public class AdminClient : Client
{
    TreinoClientView treinoClientView;
    ExercicioClientView exercicioClientView;
    AlunoClientView alunoClientView;
    public AdminClient(Usuario usuario) : base(usuario)
    {
        treinoClientView = new TreinoClientView(usuario);
        exercicioClientView = new ExercicioClientView(usuario);
        alunoClientView = new AlunoClientView(usuario);

        bool sair = false;
        while(sair == false)
        {
            Console.Clear();
            Header(usuario);
            AnsiConsole.Write(new Rule("[dodgerblue1]Dashboard Admin[/]"));
            AnsiConsole.WriteLine("\n\n");

            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                    .Title("[dodgerblue1]Menu do Administrador[/]")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Sair"),
                        (1, "Página de Usuários"),
                        (2, "Página de Alunos"),
                        (3, "Página de Treinos"),
                        (4, "Página de Exercícios.")
                    })
                    .UseConverter(a => a.Name)
                    .HighlightStyle(Color.Blue)
                    );
            switch(opcao.Index)
            {
                case 0: 
                    sair = true;
                break;
                case 1:
                    PaginaUsuarios(usuario);
                break;
                case 2:
                    alunoClientView.PaginaAlunos(usuario);
                break;
                case 3:
                    treinoClientView.PaginaTreinos(usuario);
                break;
                case 4:
                    exercicioClientView.PaginaExercicios(usuario);
                break;
            }
        }
    } 

    public void PaginaUsuarios(Usuario usuario)
    {
        UsuarioRepository _usuarioRepository = new UsuarioRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            Console.Clear();
            Header(usuario);
            AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Usuários[/]"));
            AnsiConsole.MarkupLine(mensagem);
            //Obter todos os usuários
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                usuarios =_usuarioRepository.ObterTodos();

            } catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }

            //Montar tabela usuários
            var table = new Table();
            table.AddColumns("UsuarioId", "Nome", "Sobrenome", "Email", "Senha", "Tipo");
            
            foreach(var u in usuarios)
            {
                table.AddRow(u.UsuarioId.ToString(), u.Nome, u.Sobrenome, u.Email, u.Senha, u.UsuarioTipo.ToString());
            }
            AnsiConsole.Write(table);
            

            //Menu
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Voltar"),
                        (1, "Alterar Usuário"),
                        (2, "Criar Usuário"),
                        (3, "Remover Usuário"),
                    })
                    .UseConverter(a => a.Name)
                    .HighlightStyle(Color.Blue)
                    );
            switch(opcao.Index)
            {
                case 0: 
                    sair = true;
                break;
                case 1:
                    AlterarUsuario(_usuarioRepository);
                break;
                case 2:
                    mensagem = AdicionarUsuario(_usuarioRepository);
                break;
                case 3:
                    mensagem = RemoverUsuario(_usuarioRepository);
                break;
            }

        }
    }

    public void AlterarUsuario(UsuarioRepository usuarioRepository)
    {
        string mensagem = "";
        bool sair = false;

        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Usuários > Alterar Usuário[/]"));
        AnsiConsole.MarkupLine(mensagem);
        
        //Obter todos os usuários
        List<Usuario> usuarios = new List<Usuario>();
        try
        {
            usuarios = usuarioRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        usuarios.Insert(0, new Usuario());
        
        while(sair == false)
        {
            
            //Menu escolha de Usuario
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Usuario>()
                    .Title("Escolha o [dodgerblue1]Usuário[/] que deseja [green]alterar[/]")
                    .AddChoices(usuarios)
                    .UseConverter(a => a.UsuarioId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.UsuarioId == null) break;
            if(usuarioRepository.ExisteNaBaseDeDados("Usuarios", "UsuarioId", opcao.UsuarioId.ToString()) == null) throw new ArgumentException("Usuario inexistente!");
            
            //Menu escolha campo alterar
            var opcaoCampo = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                .Title("Escolha o [dodgerblue1]campo[/] do usuário que deseja  [green]alterar[/]:")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Voltar"),
                        (1, "Nome"),
                        (2, "Sobrenome"),
                        (3, "Email"),
                        (4, "Senha"),
                        (5, "Usuário Tipo"),
                    })
                    .UseConverter(a => a.Name)
                    .HighlightStyle(Color.Blue)
                    );
            if(opcaoCampo.Index == 0)
            {
                mensagem = "";
                break;
            } 

            //Cargo do usuário
            if(opcaoCampo.Index == 5)
            {   
                var opcaoUsuarioTipo = AnsiConsole.Prompt(
                new SelectionPrompt<UsuarioTipo>()
                    .Title("Escolha o [dodgerblue1]usuário tipo[/]:")
                    .AddChoices(Enum.GetValues(typeof(UsuarioTipo)).Cast<UsuarioTipo>().ToList())
                    // .UseConverter(a => a. == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
                    
                usuarioRepository.AtualizarCampo(filtro: e => e.UsuarioId == opcao.UsuarioId, "UsuarioTipo", opcaoUsuarioTipo);
            } 
            else
            {
                var valor = AnsiConsole.Ask<string>($"Atualize o campo de [dodgerblue1]{opcaoCampo.Name}[/] para: ");
                usuarioRepository.AtualizarCampo(filtro: e => e.UsuarioId == opcao.UsuarioId, opcaoCampo.Name, valor);
            }
            mensagem = "[green] Usuário alterado com sucesso![/]";
        }
    }

    public string AdicionarUsuario(UsuarioRepository usuarioRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Usuários > Adicionar Usuário[/]"));
        
        AnsiConsole.MarkupLine($"Digite os dados do novo [green]Usuário[/]");
        var nome = AnsiConsole.Ask<string>("Digite o [dodgerblue1] Nome[/]: ");
        var sobrenome = AnsiConsole.Ask<string>("Digite o [dodgerblue1] Sobrenome[/]: ");
        var email = AnsiConsole.Ask<string>("Digite o [dodgerblue1] E-mail[/]: ");
        var senha = AnsiConsole.Ask<string>("Digite a [dodgerblue1] Senha[/]: ");
        var cpf = AnsiConsole.Ask<string>("Digite o [dodgerblue1] CPF[/]: ");

        var usuarioTipo = AnsiConsole.Prompt(
            new SelectionPrompt<UsuarioTipo>()
                .Title("Escolha o [dodgerblue1]usuário tipo[/]:")
                .AddChoices(Enum.GetValues(typeof(UsuarioTipo)).Cast<UsuarioTipo>().ToList())
                .HighlightStyle(Color.Blue)
                );
        try
        {   
            usuarioRepository.Inserir(new Usuario(null, nome, sobrenome, email, senha, cpf, usuarioTipo));
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }

        return $"[green]Usuário criado com sucesso![/]"; 
    }

    public string RemoverUsuario(UsuarioRepository usuarioRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Usuários > Remover Usuário[/]"));

        //Obter todos os usuários
        List<Usuario> usuarios = new List<Usuario>();
        try
        {
            usuarios = usuarioRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        usuarios.Insert(0, new Usuario());

        //Menu escolha de Usuario
        var opcao = AnsiConsole.Prompt(
            new SelectionPrompt<Usuario>()
                .Title("Escolha o [dodgerblue1]Usuário[/] que deseja [red]remover[/]")
                .AddChoices(usuarios)
                .UseConverter(a => a.UsuarioId == null ? "Voltar" : a.ToString())
                .HighlightStyle(Color.Blue)
                );
            
        //Confirmação usuário excluído
        if (AnsiConsole.Confirm($"Deseja mesmo excluir o usuário: {opcao.Nome} {opcao.Sobrenome}?"))
        {
             usuarioRepository.Excluir(opcao.UsuarioId);
        }

        return $"[green]Usuário removido com sucesso![/]"; 

    }
}



