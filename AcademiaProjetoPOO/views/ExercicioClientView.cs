using ConsoleTables;
using EFCore;
using Spectre.Console;

public class ExercicioClientView : Client
{
    private ExercicioRepository _exercicioRepository = new ExercicioRepository();

    public ExercicioClientView(Usuario usuario) : base(usuario)
    {
    }
    public void PaginaExerciciosProfessor(Usuario usuario)
    {
        ExercicioRepository _exercicioRepository = new ExercicioRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            try{
                Console.Clear();
                Header(usuario);
                AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Exercicios[/]"));
                AnsiConsole.MarkupLine(mensagem);
                //Obter todos os exercicios
                List<Exercicio> exercicios = new List<Exercicio>();
                try
                {
                    exercicios =_exercicioRepository.ObterTodos();

                } catch(Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }

                //Montar tabela exercicios
                var table = new Table();
                table.AddColumns("Id", "Nome", "Descrição", "Séries", "Repetições");
                
                foreach(var u in exercicios)
                {
                    table.AddRow(u.ExercicioId.ToString(), u.Nome, u.Descricao, u.Series.ToString(), u.Repeticoes.ToString());
                }
                AnsiConsole.Write(table);
            
                //Menu
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int Index, string Name)>()
                        .AddChoices( new List<(int Index, string Name)>
                        {
                            (0, "Voltar"),
                            (1, "Alterar Exercício"),
                            (2, "Criar Exercício"),                            
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
                        AlterarExercicio(_exercicioRepository);
                    break;
                    case 2:
                        mensagem = CriarExercicio(_exercicioRepository);
                    break;
                }
            } 
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }     
        }
    }
    public void PaginaExercicios(Usuario usuario)
    {
        ExercicioRepository _exercicioRepository = new ExercicioRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            try{
                Console.Clear();
                Header(usuario);
                AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Exercicios[/]"));
                AnsiConsole.MarkupLine(mensagem);
                //Obter todos os exercicios
                List<Exercicio> exercicios = new List<Exercicio>();
                try
                {
                    exercicios =_exercicioRepository.ObterTodos();

                } catch(Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }

                //Montar tabela exercicios
                var table = new Table();
                table.AddColumns("Id", "Nome", "Descrição", "Séries", "Repetições");
                
                foreach(var u in exercicios)
                {
                    table.AddRow(u.ExercicioId.ToString(), u.Nome, u.Descricao, u.Series.ToString(), u.Repeticoes.ToString());
                }
                AnsiConsole.Write(table);
            
                //Menu
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int Index, string Name)>()
                        .AddChoices( new List<(int Index, string Name)>
                        {
                            (0, "Voltar"),
                            (1, "Alterar Exercício"),
                            (2, "Criar Exercício"),
                            (3, "Excluir Exercício"),
                            
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
                        AlterarExercicio(_exercicioRepository);
                    break;
                    case 2:
                        mensagem = CriarExercicio(_exercicioRepository);
                    break;
                    case 3:
                        mensagem = RemoverExercicio(_exercicioRepository);
                    break;
                }
            } 
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }     
        }
    }
    public string CriarExercicio(ExercicioRepository _exercicioRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Exercicios > Criar Exercício[/]"));

        AnsiConsole.MarkupLine($"Digite os dados do novo [green]Exercício[/]");
        
        var nome = AnsiConsole.Ask<string>("Digite o [dodgerblue1] Nome[/]: ");
        var descricao = AnsiConsole.Ask<string>("Digite a [dodgerblue1] Descrição[/]: ");
        var series = AnsiConsole.Ask<int>("Digite a quantidade de [dodgerblue1] Séries[/]: ");
        var repeticoes = AnsiConsole.Ask<int>("Digite a quantidade de [dodgerblue1] Repetições[/]: ");

        try
        {
            _exercicioRepository.Inserir( new Exercicio(null, nome, descricao, series, repeticoes));
        }
        catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }

        return "[green]Exercício criado com sucesso[/]";
        
    }
    public void AlterarExercicio(ExercicioRepository _exercicioRepository)
    {
        string mensagem = "";

        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Exercicios > Alterar Exercício[/]"));
        AnsiConsole.MarkupLine(mensagem);
        

        //Obter todos os exercicios
        List<Exercicio> exercicios = new List<Exercicio>();
        try
        {
            exercicios = _exercicioRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        exercicios.Insert(0, new Exercicio()); 
            
        while(true)
        {
           //Menu escolha de Exercicio
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Exercicio>()
                    .Title("Escolha o [dodgerblue1]Exercício[/] que deseja [green]alterar[/]")
                    .AddChoices(exercicios)
                    .UseConverter(a => a.ExercicioId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.ExercicioId == null) break;
            if(_exercicioRepository.ExisteNaBaseDeDados("Exercicios", "ExercicioId", opcao.ExercicioId.ToString()) == null) throw new ArgumentException("Exercício inexistente!");
             
             //Menu escolha campo alterar
            var opcaoCampo = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                .Title("Escolha o [dodgerblue1]campo[/] do exercício que deseja  [green]alterar[/]:")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Voltar"),
                        (1, "Nome"),
                        (2, "Descricao"),
                        (3, "Series"),
                        (4, "Repeticoes"),
                    })
                    .UseConverter(a => a.Name)
                    .HighlightStyle(Color.Blue)
                    );

            if(opcaoCampo.Index == 0) break;
            try
            {
                if(opcaoCampo.Index == 3 || opcaoCampo.Index == 4)
                {
                    int valor = AnsiConsole.Ask<int>($"Atualize o campo de [dodgerblue1]{opcaoCampo.Name}[/] para: ");
                    _exercicioRepository.AtualizarCampo(filtro: e => e.ExercicioId == opcao.ExercicioId, opcaoCampo.Name, valor);

                }
                else
                {
                    string valor = AnsiConsole.Ask<string>($"Atualize o campo de [dodgerblue1]{opcaoCampo.Name}[/] para: ");
                    _exercicioRepository.AtualizarCampo(filtro: e => e.ExercicioId == opcao.ExercicioId, opcaoCampo.Name, valor);
                    
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
            mensagem = "[green]Exercício alterado com sucesso![/]";
        }
    }
    public string RemoverExercicio(ExercicioRepository _exercicioRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Exercícios > Remover Exercício[/]"));

        //Obter todos os exercicios
        List<Exercicio> exercicios = new List<Exercicio>();
        try
        {
            exercicios = _exercicioRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        exercicios.Insert(0, new Exercicio());

        //Menu escolha de Exercicio
        var opcao = AnsiConsole.Prompt(
            new SelectionPrompt<Exercicio>()
                .Title("Escolha o [dodgerblue1]Exercício[/] que deseja [red]remover[/]")
                .AddChoices(exercicios)
                .UseConverter(a => a.ExercicioId == null ? "Voltar" : a.ToString())
                .HighlightStyle(Color.Blue)
                );
        //Excluir exercicio
        if (AnsiConsole.Confirm($"Deseja mesmo excluir o exercício: {opcao.Nome}?"))
        {
            try
            {
                _exercicioRepository.Excluir(opcao.ExercicioId);

            } catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        return $"[green]Exercício removido com sucesso![/]"; 
    }
}