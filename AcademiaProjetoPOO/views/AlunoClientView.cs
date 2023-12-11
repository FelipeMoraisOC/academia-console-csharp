using ConsoleTables;
using EFCore;
using Spectre.Console;


public class AlunoClientView : Client
{
    public AlunoClientView(Usuario usuario) : base(usuario)
    {
    }
    public void PaginaAlunosAtendente(Usuario usuario)
    {
        AlunoRepository _alunoRepository = new AlunoRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            try{
                Console.Clear();
                Header(usuario);
                AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Alunos[/]"));
                AnsiConsole.MarkupLine(mensagem);
                //Obter todos os alunos
                List<Aluno> alunos = new List<Aluno>();
                try
                {
                    alunos =_alunoRepository.ObterTodos();

                } catch(Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }

                //Montar tabela alunos
                var table = new Table();
                table.AddColumns("Id", "Nome", "Sobrenome", "E-mail", "CPF", "Idade", "Treinos");
                
                foreach(var u in alunos)
                {
                    table.AddRow(u.AlunoId.ToString(), u.Nome, u.Sobrenome, u.Email, u.CPF, u.Idade.ToString(), u.AlunosTreinos.Count.ToString());
                }
                AnsiConsole.Write(table);
            
                //Menu
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int Index, string Name)>()
                        .AddChoices( new List<(int Index, string Name)>
                        {
                            (0, "Voltar"),
                            (2, "Criar Aluno"),
                            (3, "Alterar Aluno"),
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
                        PlanejamentoTreinoAluno(_alunoRepository);
                    break;
                    case 2:
                        mensagem = CriarAluno(_alunoRepository);
                    break;
                    case 3:
                       AlterarAluno(_alunoRepository);
                    break;
                }
            } 
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }     
        }
    }
    public void PaginaAlunosProfessor(Usuario usuario)
    {
        AlunoRepository _alunoRepository = new AlunoRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            try{
                Console.Clear();
                Header(usuario);
                AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Alunos[/]"));
                AnsiConsole.MarkupLine(mensagem);
                //Obter todos os alunos
                List<Aluno> alunos = new List<Aluno>();
                try
                {
                    alunos =_alunoRepository.ObterTodos();

                } catch(Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }

                //Montar tabela alunos
                var table = new Table();
                table.AddColumns("Id", "Nome", "Sobrenome", "E-mail", "CPF", "Idade", "Treinos");
                
                foreach(var u in alunos)
                {
                    table.AddRow(u.AlunoId.ToString(), u.Nome, u.Sobrenome, u.Email, u.CPF, u.Idade.ToString(), u.AlunosTreinos.Count.ToString());
                }
                AnsiConsole.Write(table);
            
                //Menu
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int Index, string Name)>()
                        .AddChoices( new List<(int Index, string Name)>
                        {
                            (0, "Voltar"),
                            (1, "Planejamento do Treino do Aluno"),
                            (2, "Vincular Treino para Aluno"),
                            (3, "Desvincular Treino para Aluno"),
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
                        PlanejamentoTreinoAluno(_alunoRepository);
                    break;
                    case 2:
                        mensagem = VincularAlunoTreinos(_alunoRepository);
                    break;
                    case 3:
                        mensagem = DesvincularAlunoTreinos(_alunoRepository);
                    break;
                }
            } 
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }     
        }
    }
    public void PlanejamentoTreinoAluno(AlunoRepository _alunoRepository)
    {
        TreinoRepository _treinoRepository = new TreinoRepository();
        string mensagem = "";
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Alunos > Alterar Aluno[/]"));
        AnsiConsole.MarkupLine(mensagem);

        //Obter todos os alunos
        List<Aluno> alunos = new List<Aluno>();
        try
        {
            alunos = _alunoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        alunos.Insert(0, new Aluno()); 

        while(true)
        {
           //Menu escolha de Aluno
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Aluno>()
                    .Title("Escolha o [dodgerblue1]Aluno[/] que deseja visualizar o [green]Programa de Treinamento[/]:")
                    .AddChoices(alunos)
                    .UseConverter(a => a.AlunoId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.AlunoId == null) break;
            if(!opcao.AlunosTreinos.Any())
            {
                AnsiConsole.MarkupLine("[dodgerblue1]Aluno não contém nenhum treino vinculado![/]");
                break;
            }
            try
            {
                if(_alunoRepository.ExisteNaBaseDeDados("Alunos", "AlunoId", opcao.AlunoId.ToString()) == null) throw new ArgumentException("Aluno inexistente!");
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
            Aluno aluno = _alunoRepository.ObterPorId(opcao.AlunoId);
            Tree root = new Tree($"Programa de Treinamento do [dodgerblue1]{aluno.Nome} {aluno.Sobrenome}[/]");
             var atPorDiaSemana = aluno.AlunosTreinos
                .OrderByDescending(x => x.DiaSemana)
                .ToList();
            foreach (var alunoTreino in atPorDiaSemana)
            {
                
                try
                {
                    root.AddNode($"{alunoTreino.Treino.Nome} [dodgerblue1]Descrição: [/]{alunoTreino.Treino.Descricao}");
                    root.AddNode($"Dia da Semana: [dodgerblue1]{alunoTreino.DiaSemana}[/]");

                    var table = new Table()
                        .RoundedBorder()
                        .AddColumn("Exercício")
                        .AddColumn("Descrição")
                        .AddColumn("Séries")
                        .AddColumn("Repetições");
                    Treino treino = _treinoRepository.ObterPorId(alunoTreino.TreinoId);
                    
                    foreach (TreinoExercicio treinoExercicio in treino.TreinosExercicios)
                    {
                        table.AddRow($"{treinoExercicio.Exercicio.Nome}", $"{treinoExercicio.Exercicio.Descricao}", $"{treinoExercicio.Exercicio.Series}", $"{treinoExercicio.Exercicio.Repeticoes}");
                    }
                    root.AddNode(table);
                } catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }   
            }
            AnsiConsole.Write(root);
        }
    }
    public void PaginaAlunos(Usuario usuario)
    {
        AlunoRepository _alunoRepository = new AlunoRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            try{
                Console.Clear();
                Header(usuario);
                AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Alunos[/]"));
                AnsiConsole.MarkupLine(mensagem);
                //Obter todos os alunos
                List<Aluno> alunos = new List<Aluno>();
                try
                {
                    alunos =_alunoRepository.ObterTodos();

                } catch(Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }

                //Montar tabela alunos
                var table = new Table();
                table.AddColumns("Id", "Nome", "Sobrenome", "E-mail", "CPF", "Idade", "Treinos");
                
                foreach(var u in alunos)
                {
                    table.AddRow(u.AlunoId.ToString(), u.Nome, u.Sobrenome, u.Email, u.CPF, u.Idade.ToString(), u.AlunosTreinos.Count.ToString());
                }
                AnsiConsole.Write(table);
            
                //Menu
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int Index, string Name)>()
                        .AddChoices( new List<(int Index, string Name)>
                        {
                            (0, "Voltar"),
                            (1, "Alterar Aluno"),
                            (2, "Criar Aluno"),
                            (3, "Vincular Treino para Aluno"),
                            (4, "Desvincular Treino para Aluno"),
                            (5, "Excluir Aluno"),
                            
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
                        AlterarAluno(_alunoRepository);
                    break;
                    case 2:
                        mensagem = CriarAluno(_alunoRepository);
                    break;
                    case 3:
                        mensagem = VincularAlunoTreinos(_alunoRepository);
                    break;
                    case 4:
                        mensagem = DesvincularAlunoTreinos(_alunoRepository);
                    break;
                    case 5:
                        mensagem = RemoverAluno(_alunoRepository);
                    break;
                }
            } 
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }     
        }
    }
    public string CriarAluno(AlunoRepository _alunoRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Alunos > Criar Aluno[/]"));

        AnsiConsole.MarkupLine($"Digite os dados do novo [green]Aluno[/]");
        
        var nome = AnsiConsole.Ask<string>("Digite o [dodgerblue1] Nome[/]: ");
        var sobrenome = AnsiConsole.Ask<string>("Digite a [dodgerblue1] Sobrenome[/]: ");
        var email = AnsiConsole.Ask<string>("Digite o [dodgerblue1] E-mail[/]: ");
        var cpf = AnsiConsole.Ask<string>("Digite o [dodgerblue1] CPF[/]: ");
        var idade = AnsiConsole.Prompt(
            new TextPrompt<int>("Digite a [dodgerblue1] Idade[/]:")
                .ValidationErrorMessage("[red]Não é uma idade válida[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        <= 0 => ValidationResult.Error("[red]Deve pelo menos ter 1 ano de idade![/]"),
                        >= 123 => ValidationResult.Error("[red]Idade inválida![/]"),
                        _ => ValidationResult.Success(),
                    };
                }));

        try
        {
            _alunoRepository.Inserir( new Aluno(null, email, nome, sobrenome, cpf, idade));
        }
        catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }

        return "[green]Aluno criado com sucesso[/]";
        
    }
    public void AlterarAluno(AlunoRepository _alunoRepository)
    {
        string mensagem = "";

        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Alunos > Alterar Aluno[/]"));
        AnsiConsole.MarkupLine(mensagem);
        

        //Obter todos os alunos
        List<Aluno> alunos = new List<Aluno>();
        try
        {
            alunos = _alunoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        alunos.Insert(0, new Aluno()); 
            
        while(true)
        {
           //Menu escolha de Aluno
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Aluno>()
                    .Title("Escolha o [dodgerblue1]Aluno[/] que deseja [green]alterar[/]")
                    .AddChoices(alunos)
                    .UseConverter(a => a.AlunoId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.AlunoId == null) break;
            if(_alunoRepository.ExisteNaBaseDeDados("Alunos", "AlunoId", opcao.AlunoId.ToString()) == null) throw new ArgumentException("Aluno inexistente!");
             
             //Menu escolha campo alterar
            var opcaoCampo = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                .Title("Escolha o [dodgerblue1]campo[/] do aluno que deseja  [green]alterar[/]:")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Voltar"),
                        (1, "Nome"),
                        (2, "Sobrenome"),
                        (3, "Email"),
                        (4, "CPF"),
                        (5, "Idade"),
                    })
                    .UseConverter(a => a.Name)
                    .HighlightStyle(Color.Blue)
                    );

            if(opcaoCampo.Index == 0) break;
            try
            {
                if(opcaoCampo.Index == 5)
                {
                    int valor = AnsiConsole.Ask<int>($"Atualize o campo de [dodgerblue1]{opcaoCampo.Name}[/] para: ");
                    _alunoRepository.AtualizarCampo(filtro: e => e.AlunoId == opcao.AlunoId, opcaoCampo.Name, valor);

                }
                else
                {
                    string valor = AnsiConsole.Ask<string>($"Atualize o campo de [dodgerblue1]{opcaoCampo.Name}[/] para: ");
                    _alunoRepository.AtualizarCampo(filtro: e => e.AlunoId == opcao.AlunoId, opcaoCampo.Name, valor);
                    
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
            mensagem = "[green]Aluno alterado com sucesso![/]";
        }
    }
    public string RemoverAluno(AlunoRepository _alunoRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Alunos > Remover Aluno[/]"));

        //Obter todos os alunos
        List<Aluno> alunos = new List<Aluno>();
        try
        {
            alunos = _alunoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        alunos.Insert(0, new Aluno());

        //Menu escolha de Aluno
        var opcao = AnsiConsole.Prompt(
            new SelectionPrompt<Aluno>()
                .Title("Escolha o [dodgerblue1]Aluno[/] que deseja [red]remover[/]")
                .AddChoices(alunos)
                .UseConverter(a => a.AlunoId == null ? "Voltar" : a.ToString())
                .HighlightStyle(Color.Blue)
                );
        //Excluir aluno
        if (AnsiConsole.Confirm($"Deseja mesmo excluir o aluno: {opcao.Nome}?"))
        {
            try
            {
                _alunoRepository.Excluir(opcao.AlunoId);

            } catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        return $"[green]Aluno removido com sucesso![/]"; 
    }
    public string VincularAlunoTreinos(AlunoRepository _alunoRepository)
    {
        AlunoTreinoRepository _alunoTreinoRepository = new AlunoTreinoRepository();
        string mensagem = "";
        try
        {
            var vinculos = EscolherVinculosAlunosTreinos("VINCULAR", _alunoRepository);

            if(vinculos.Item1 == 0 || vinculos.Item2 == 0) return "";

            var opcaoDiaSemana = AnsiConsole.Prompt(
                new SelectionPrompt<DiaSemana>()
                    .Title("Escolha o [dodgerblue1]Dia da Semana[/] para esse aluno realizar o treino:")
                    .AddChoices(Enum.GetValues(typeof(DiaSemana)).Cast<DiaSemana>().ToList())
                    .HighlightStyle(Color.Blue)
                    );

            
            AlunoTreino alunoTreino = new AlunoTreino(null, vinculos.Item1, vinculos.Item2, opcaoDiaSemana.ToString());
            _alunoTreinoRepository.Inserir(alunoTreino); 
            mensagem = vinculos.Item3;

        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
        return mensagem;
    }
    public string DesvincularAlunoTreinos(AlunoRepository _alunoRepository)
    {
        AlunoTreinoRepository _alunoTreinoRepository = new AlunoTreinoRepository();
        string mensagem = "";
        try
        {
            var vinculos = EscolherVinculosAlunosTreinos("DESVINCULAR", _alunoRepository);
            if(vinculos.Item1 == 0 || vinculos.Item2 == 0) return $"[red]Ação imcompleta, tente novamente![/]";

            AlunoTreino alunoTreino = _alunoTreinoRepository.ObterPorAlunoIdTreinoId(vinculos.Item1, vinculos.Item2);
            _alunoTreinoRepository.Excluir(alunoTreino);
            mensagem = vinculos.Item3;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
        return mensagem;
    }
    public (int, int, string) EscolherVinculosAlunosTreinos(string texto, AlunoRepository _alunoRepository, bool desvincular = false)
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        string alunoEscolhido = "";
        int alunoEscolhidoId = 0;
        int treinoEscolhidoId = 0;
        string mensagem = "";

        //Obter todos os alunos
        List<Aluno> alunos = new List<Aluno>();
        try
        {
            alunos = _alunoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        alunos.Insert(0, new Aluno());
    

        while(true)
        {
            //Menu escolha de Aluno
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Aluno>()
                    .Title($"Qual [dodgerblue1]Aluno[/] que deseja [dodgerblue1]{texto.ToLower()}[/] treinos:")
                    .AddChoices(alunos)
                    .UseConverter(a => a.AlunoId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.AlunoId == null) break;
            
            alunoEscolhido = opcao.Nome;
            alunoEscolhidoId = (int)opcao.AlunoId;

            //Obter todos os treinos 
            List<Treino> treinos = new List<Treino>();
            try
            {
                //Obtem treino para vincular ou desvincular
                treinos = desvincular == true ?  _treinoRepository.ObterTodosTreinosComAlunoId(alunoEscolhidoId) : _treinoRepository.ObterTodosTreinosSemAlunoId(alunoEscolhidoId);

            } catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
            treinos.Insert(0, new Treino());

            //Menu escolha de Treinos para o Aluno
            var opcaoTreino = AnsiConsole.Prompt(
                new SelectionPrompt<Treino>()
                    .Title($"Qual [dodgerblue1]Treino[/] que deseja [dodgerblue1]{texto.ToLower()}[/] ao [dodgerblue1]Aluno: {alunoEscolhido}[/]:")
                    .AddChoices(treinos)
                    .UseConverter(a => a.TreinoId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            
            if(opcaoTreino.TreinoId == null) continue;

            treinoEscolhidoId = (int)opcaoTreino.TreinoId;
            mensagem = $"[green]Ação de {texto.ToLower()} Treino ao Aluno feita com sucesso![/]";
            break;
        }
        return (alunoEscolhidoId, treinoEscolhidoId, mensagem);
    }
}