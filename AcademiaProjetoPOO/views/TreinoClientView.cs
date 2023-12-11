using ConsoleTables;
using EFCore;
using Spectre.Console;

public class TreinoClientView : Client
{
    private ExercicioRepository _exercicioRepository = new ExercicioRepository();
    private TreinoExercicioRepository _treinoExercicioRepository = new TreinoExercicioRepository();

    public TreinoClientView(Usuario usuario) : base(usuario)
    {
        
    }
    public void PaginaTreinoProfessor(Usuario usuario)
    {
        TreinoRepository _treinoRepository = new TreinoRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            try{
                Console.Clear();
                Header(usuario);
                AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Treinos[/]"));
                AnsiConsole.MarkupLine(mensagem);
                //Obter todos os treinos
                List<Treino> treinos = new List<Treino>();
                try
                {
                    treinos =_treinoRepository.ObterTodos();

                } catch(Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }

                //Montar tabela treinos
                var table = new Table();
                table.AddColumns("Id", "Nome", "Descrição", "Criado Por", "Quantidade de Exercícios");
                
                foreach(var u in treinos)
                {
                    table.AddRow(u.TreinoId.ToString(), u.Nome, u.Descricao, u.CriadoPor, u.TreinosExercicios.Count.ToString());
                }
                AnsiConsole.Write(table);

                //Menu
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int Index, string Name)>()
                        .AddChoices( new List<(int Index, string Name)>
                        {
                            (0, "Voltar"),
                            (1, "Alterar Treino"),
                            (2, "Criar Treino"),
                            (3, "Vincular Exercício"),
                            (4, "Desvincular Exercício"),
                            (5, "Detalhes Treino"),
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
                        AlterarTreino(_treinoRepository);
                    break;
                    case 2:
                        mensagem = CriarTreino(usuario, _treinoRepository);
                    break;
                    case 3:
                        mensagem = VincularTreinoExercicios(_treinoRepository);
                    break;
                    case 4:
                        mensagem = DesvincularTreinoExercicios(_treinoRepository);
                    break;
                    case 5:
                        DetalheTreino(_treinoRepository);
                    break;
                }
            } 
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }     
        }
    }
    public void PaginaTreinos(Usuario usuario)
    {
        TreinoRepository _treinoRepository = new TreinoRepository();
        string mensagem = "";
        bool sair = false;
        while(sair == false)
        {
            try{
                Console.Clear();
                Header(usuario);
                AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {usuario.UsuarioTipo} > Treinos[/]"));
                AnsiConsole.MarkupLine(mensagem);
                //Obter todos os treinos
                List<Treino> treinos = new List<Treino>();
                try
                {
                    treinos =_treinoRepository.ObterTodos();

                } catch(Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                }

                //Montar tabela treinos
                var table = new Table();
                table.AddColumns("Id", "Nome", "Descrição", "Criado Por", "Quantidade de Exercícios");
                
                foreach(var u in treinos)
                {
                    table.AddRow(u.TreinoId.ToString(), u.Nome, u.Descricao, u.CriadoPor, u.TreinosExercicios.Count.ToString());
                }
                AnsiConsole.Write(table);
            
            //Menu
                var opcao = AnsiConsole.Prompt(
                    new SelectionPrompt<(int Index, string Name)>()
                        .AddChoices( new List<(int Index, string Name)>
                        {
                            (0, "Voltar"),
                            (1, "Alterar Treino"),
                            (2, "Criar Treino"),
                            (3, "Vincular Exercício"),
                            (4, "Desvincular Exercício"),
                            (5, "Excluir Treino"),
                            (6, "Detalhes Treino"),
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
                        AlterarTreino(_treinoRepository);
                    break;
                    case 2:
                        mensagem = CriarTreino(usuario, _treinoRepository);
                    break;
                    case 3:
                        mensagem = VincularTreinoExercicios(_treinoRepository);
                    break;
                    case 4:
                        mensagem = DesvincularTreinoExercicios(_treinoRepository);
                    break;
                    case 5:
                        mensagem = RemoverTreino(_treinoRepository);
                    break;
                    case 6:
                        DetalheTreino(_treinoRepository);
                    break;
                }
            } 
            catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }     
        }
    }
    public string CriarTreino(Usuario usuario, TreinoRepository _treinoRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Treinos > Criar Treino[/]"));

        AnsiConsole.MarkupLine($"Digite os dados do novo [green]Treino[/]");
        AnsiConsole.MarkupLine($"[yellow4]**Observação: Este treino pode ser utilizado por vários alunos, basta vincular um aluno a esse treino.[/]");

        var nome = AnsiConsole.Ask<string>("Digite o [dodgerblue1] Nome[/]: ");
        var descricao = AnsiConsole.Ask<string>("Digite a [dodgerblue1] Descrição[/]: ");

        try
        {
            _treinoRepository.Inserir( new Treino(null, nome, descricao, usuario.Nome));
        }
        catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }

        return "[green]Treino criado com sucesso[/]";
    }
    public void AlterarTreino(TreinoRepository _treinoRepository)
    {

        bool sair = false;
        string mensagem = "";

        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Treinos > Alterar Treino[/]"));
        AnsiConsole.MarkupLine(mensagem);
        

        //Obter todos os treinos
        List<Treino> treinos = new List<Treino>();
        try
        {
            treinos = _treinoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        treinos.Insert(0, new Treino()); 
            
        while(true)
        {
           //Menu escolha de Treino
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Treino>()
                    .Title("Escolha o [dodgerblue1]Treino[/] que deseja [green]alterar[/]")
                    .AddChoices(treinos)
                    .UseConverter(a => a.TreinoId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.TreinoId == null) break;
            if(_treinoRepository.ExisteNaBaseDeDados("Treinos", "TreinoId", opcao.TreinoId.ToString()) == null) throw new ArgumentException("Treino inexistente!");
             
             //Menu escolha campo alterar
            var opcaoCampo = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                .Title("Escolha o [dodgerblue1]campo[/] do treino que deseja  [green]alterar[/]:")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Voltar"),
                        (1, "Nome"),
                        (2, "Descricao"),
                    })
                    .UseConverter(a => a.Name)
                    .HighlightStyle(Color.Blue)
                    );

            if(opcaoCampo.Index == 0) break;
            try
            {
                var valor = AnsiConsole.Ask<string>($"Atualize o campo de [dodgerblue1]{opcaoCampo.Name}[/] para: ");
                _treinoRepository.AtualizarCampo(filtro: e => e.TreinoId == opcao.TreinoId, opcaoCampo.Name, valor);

            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
            mensagem = "[green]Treino alterado com sucesso![/]";
        }
    }
    public string RemoverTreino(TreinoRepository _treinoRepository)
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Treinos > Remover Treino[/]"));

        //Obter todos os treinos
        List<Treino> Treinos = new List<Treino>();
        try
        {
            Treinos = _treinoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        Treinos.Insert(0, new Treino());

        //Menu escolha de Treino
        var opcao = AnsiConsole.Prompt(
            new SelectionPrompt<Treino>()
                .Title("Escolha o [dodgerblue1]Treino[/] que deseja [red]remover[/]")
                .AddChoices(Treinos)
                .UseConverter(a => a.TreinoId == null ? "Voltar" : a.ToString())
                .HighlightStyle(Color.Blue)
                );
            
        //Excluir treino
        if (AnsiConsole.Confirm($"Deseja mesmo excluir o treino: {opcao.Nome}?"))
        {
            try
            {
                _treinoRepository.Excluir(opcao.TreinoId);

            } catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
        }

        return $"[green]Treino removido com sucesso![/]"; 
    }
    public string VincularTreinoExercicios(TreinoRepository _treinoRepository)
    {
        
        string mensagem = "";
        try
        {
            var vinculos = EscolherVinculosTreinoExercicio("VINCULAR", _treinoRepository);

            if(vinculos.Item1 == 0 && vinculos.Item2 == 0) return "";
            
            TreinoExercicio treinoExercicio = new TreinoExercicio(vinculos.Item1, vinculos.Item2);
            _treinoExercicioRepository.Inserir(treinoExercicio); 
            mensagem = vinculos.Item3;

        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.ToString());

        }
        return mensagem;
    }
    public string DesvincularTreinoExercicios(TreinoRepository _treinoRepository)
    {
        string mensagem = "";
        try
        {
            var vinculos = EscolherVinculosTreinoExercicio("DESVINCULAR", _treinoRepository, true);

            if(vinculos.Item1 == 0 && vinculos.Item2 == 0) return "";
            TreinoExercicio treinoExercicio = _treinoExercicioRepository.ObterPorTreinoIdExercicioId(vinculos.Item1, vinculos.Item2);
            
            _treinoExercicioRepository.Excluir(treinoExercicio); 

            mensagem = vinculos.Item3;
        } 
        catch (Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        }

        return mensagem;
    }
    public void DetalheTreino(TreinoRepository _treinoRepository) 
    {
        Console.Clear();
        Header(Usuario);
        AnsiConsole.Write(new Rule($"[dodgerblue1]Dashboard {Usuario.UsuarioTipo} > Treinos > Detalhes do Treino[/]"));

        //Obter todos os treinos
        List<Treino> treinos = new List<Treino>();
        try
        {
            treinos = _treinoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        treinos.Insert(0, new Treino()); 

        while(true)
        {

            //Menu do treino

            //Menu escolha de Treino
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Treino>()
                    .Title("Escolha o [dodgerblue1]Treino[/] que deseja [green]ver detalhes[/]")
                    .AddChoices(treinos)
                    .UseConverter(a => a.TreinoId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.TreinoId == null) break;

            //Busca no banco
            Treino treino = _treinoRepository.ObterPorId(opcao.TreinoId);
            if(treino == null) throw new ArgumentException("Treino não encontrado");

            //Montar tabela treinos
            var table = new Table();
            table.AddColumns("Treino Id", "Nome", "Descrição", "Criado Por", "Quantidade de Exercícios");
            table.AddRow(treino.TreinoId.ToString(), treino.Nome, treino.Descricao, treino.CriadoPor, treino.TreinosExercicios.Count.ToString());
            AnsiConsole.Write(table);
        
            //Montar tabela Exercicios
            var tableExe = new Table();
            tableExe.AddColumns("Nome", "Descrição", "Séries", "Repetições");
            
            foreach(var t in treino.TreinosExercicios)
            {
                tableExe.AddRow(t.Exercicio.Nome, t.Exercicio.Descricao, t.Exercicio.Series.ToString(), t.Exercicio.Repeticoes.ToString());
            }
            AnsiConsole.Write(tableExe);

             //Menu voltar
            var opcaoCampo = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                .Title("Aperte [dodgerblue1]ESPAÇO[/] para voltar.")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Voltar"),
                    })
                    .UseConverter(a => a.Name)
                    .HighlightStyle(Color.Blue)
                    );
            if(opcaoCampo.Index == 0) break;
        }
    }
    private (int, int, string) EscolherVinculosTreinoExercicio(string texto, TreinoRepository _treinoRepository, bool desvincular = false)
    {
        string treinoEscolhido = "";
        int treinoEscolhidoId = 0;
        int exercicioEscolhidoId = 0;
        string mensagem = "";

        //Obter todos os treinos
        List<Treino> treinos = new List<Treino>();
        try
        {
            treinos = _treinoRepository.ObterTodos();

        } catch(Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
        treinos.Insert(0, new Treino());
    

        while(true)
        {
            //Menu escolha de Exercicio
            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<Treino>()
                    .Title($"Qual [dodgerblue1]Treino[/] que deseja [dodgerblue1]{texto.ToLower()}[/] exercícios:")
                    .AddChoices(treinos)
                    .UseConverter(a => a.TreinoId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            if(opcao.TreinoId == null) break;
            
            treinoEscolhido = opcao.Nome;
            treinoEscolhidoId = (int)opcao.TreinoId;

            //Obter todos os exercicios 
            List<Exercicio> exercicios = new List<Exercicio>();
            try
            {
                //Obtem exercicio para vincular ou desvincular
                exercicios = desvincular == true ?  _exercicioRepository.ObterTodosComTreinoId(treinoEscolhidoId) : _exercicioRepository.ObterTodosSemTreinoId(treinoEscolhidoId);

            } catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
            }
            exercicios.Insert(0, new Exercicio());

            //Menu escolha de Exercicios para o Treino
            var opcaoExercicio = AnsiConsole.Prompt(
                new SelectionPrompt<Exercicio>()
                    .Title($"Qual [dodgerblue1]Exercício[/] que deseja [dodgerblue1]{texto.ToLower()}[/] ao [dodgerblue1]Treino: {treinoEscolhido}[/]:")
                    .AddChoices(exercicios)
                    .UseConverter(a => a.ExercicioId == null ? "Voltar" : a.ToString())
                    .HighlightStyle(Color.Blue)
                    );
            
            if(opcaoExercicio.ExercicioId == null) continue;

            exercicioEscolhidoId = (int)opcaoExercicio.ExercicioId;
            mensagem = $"[green]Ação de {texto.ToLower()} exercício ao Treino feita com sucesso![/]";
            break;
        }
        return (treinoEscolhidoId, exercicioEscolhidoId, mensagem);
    }

}