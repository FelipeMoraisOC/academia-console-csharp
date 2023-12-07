using ConsoleTables;
using EFCore;

namespace Services;

public class TreinoService
{
    private ExercicioRepository _exercicioRepository = new ExercicioRepository();
    private TreinoExercicioRepository _treinoExercicioRepository = new TreinoExercicioRepository();
    public Treino CriarTreino(Usuario usuario)
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        string nome, descricao, criadoPor;

        Console.WriteLine("--CADASTRO DE UM NOVO TREINO--");

        Console.ForegroundColor = ConsoleColor.DarkYellow; 
        Console.WriteLine("**Observação: Este treino pode ser utilizado por vários alunos, basta vincular um aluno a esse treino.");
        Console.ResetColor();

        while(true)
        {
            try{
                Console.WriteLine("Digite o nome do treino: ");
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
                Console.WriteLine("Digite a descrição do treino: ");
                descricao = Console.ReadLine();

                if(string.IsNullOrEmpty(descricao)) throw new ArgumentException("Descricao não pode estar vazio! Digite novamente.");

                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            
            
        }
        try
        {
            Treino treino = new Treino(null, nome, descricao, usuario.Nome);
            _treinoRepository.Inserir(treino);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Treino criado com sucesso!");
            Console.WriteLine("Associe exercícios para esse treino!");
            Console.ResetColor();
            
            return treino;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
             throw;
        }
        
    }
    public string ExibirListaTreinos()
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        try
        {
            List<Treino> treinos = _treinoRepository.ObterTodos();
            var table = new ConsoleTable("TreinoId", "Nome", "Descrição", "Criado Por", "Quantidade de Exercícios");
        
            foreach(var u in treinos)
            {
            table.AddRow(u.TreinoId, u.Nome, u.Descricao, u.CriadoPor, u.TreinosExercicios.Count);
            }

            return table.ToStringAlternative();
        }catch(Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
        

    }
    public void AlterarTreinoPorId()
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        string alunoId = "";
        string campo = "";

        Console.WriteLine("PÁGINA TREINO > ALTERAR TREINO");
        try
        {
            
            Console.WriteLine("Digite o Id do Treino que deseja alterar: ");
            alunoId = Console.ReadLine();

            if(_treinoRepository.ExisteNaBaseDeDados("Treinos", "TreinoId", alunoId) == null) throw new ArgumentException("Treino Id inexistente!");

    
            while(true)
            {
                try
                {
                    int opcao = 0;
                    Checkbox cbUsuarios = new Checkbox(
                        $"PÁGINA TREINO > ALTERAR DADO TREINO\n\nQual campo deseja alterar?\n", 
                        "Voltar",
                        "Nome",
                        "Descricao"
                    );
                    foreach (var checkboxReturn in cbUsuarios.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }
                        
                    if(opcao == 0) break;
                    else if(opcao == 1) campo = "Nome";
                    else if(opcao == 2) campo = "Descricao";

                    Console.WriteLine("Digite o novo valor: ");
                    string valor = Console.ReadLine();
                    
                    _treinoRepository.AtualizarCampo(filtro: e => e.TreinoId == Int32.Parse(alunoId), campo, valor);
                    
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
    public void RemoverTreinoPorId()
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        int alunoId;

         Console.WriteLine("PÁGINA TREINO > REMOVER TREINO");
        try
        {
            
            Console.WriteLine("Digite o Id do Treino que deseja remover: ");
            alunoId = Int32.Parse(Console.ReadLine());

            if(_treinoRepository.ExisteNaBaseDeDados("Treinos", "TreinoId", alunoId.ToString()) == null) throw new ArgumentException("Treino Id inexistente!");

    
            while(true)
            {
                try
                {
                    int opcao = 0;   
                    Checkbox cbTreinos = new Checkbox(
                        $"PÁGINA TREINO > REMOVER TREINO\n\nTem certeza que deseja exculir?\n", 
                        "Não",
                        "Sim"
                    );
                    foreach (var checkboxReturn in cbTreinos.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }
                    if(opcao == 1) _treinoRepository.Excluir(alunoId);
                    else break;
                    
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine("Treino removido com sucesso!");
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
    public void VincularTreinoExercicios()
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        try
        {
            var vinculos = EscolherVinculosTreinoExercicio("VINCULAR");

            if(vinculos.Item1 == 0 && vinculos.Item2 == 0) return;
            
            TreinoExercicio treinoExercicio = new TreinoExercicio(vinculos.Item1, vinculos.Item2);
            _treinoExercicioRepository.Inserir(treinoExercicio); 

        } catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro:" + ex.Message); 
            Checkbox cbError = new Checkbox(
                $"Erro: {ex.Message}\n", 
                "Ok"
            );
            cbError.Select();
            Console.ResetColor();
        }
    }
    public void DesvincularTreinoExercicios()
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        try
        {
            var vinculos = EscolherVinculosTreinoExercicio("DESVINCULAR", true);

            if(vinculos.Item1 == 0 && vinculos.Item2 == 0) return;
            TreinoExercicio treinoExercicio = _treinoExercicioRepository.ObterPorTreinoIdExercicioId(vinculos.Item1, vinculos.Item2);
            
            _treinoExercicioRepository.Excluir(treinoExercicio); 
            

        } catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro:" + ex.Message); 
            Checkbox cbError = new Checkbox(
                $"Erro: {ex.Message}\n", 
                "Ok"
            );
            cbError.Select();
            Console.ResetColor();
        }
    }
    public void DetalheTreino() //
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        try
        {
            while(true)
            {

                //Escolha do Treino

                List<string> treinosOptions = new List<string>{"--Voltar--"};
                List<int> treinosOptionId = new List<int>();
                treinosOptionId.Add(0);

                List<Treino> treinos = _treinoRepository.ObterTodos();
                foreach (var u in treinos)
                {
                    treinosOptions.Add(u.ToString());
                    treinosOptionId.Add((int)u.TreinoId);
                }

                int escolhaTreino = 0;
                Checkbox cbTreinos = new Checkbox(
                    $"PÁGINA TREINO >  DETALHE TREINO > ESCOLHA \n\nQual treino deseja escolher para ver os detalhes\n", 
                    treinosOptions.ToArray()
                );
                foreach (var checkboxReturn in cbTreinos.Select())
                {
                    escolhaTreino = checkboxReturn.Index;
                }
                if(escolhaTreino == 0) break;

                //Detalhe do Treino

                Treino treino = _treinoRepository.ObterPorId(treinosOptionId[escolhaTreino]);

                if(treino == null) throw new ArgumentException("Treino não encontrado");
                
                var tableTreino = new ConsoleTable("Treino Id", "Nome", "Descrição", "Criado Por", "Quantidade de Exercícios");
                tableTreino.AddRow(treino.TreinoId, treino.Nome, treino.Descricao, treino.CriadoPor, treino.TreinosExercicios.Count);

                var tableExercicios = new ConsoleTable("Nome", "Descrição", "Séries", "Repetições");

                foreach(var t in treino.TreinosExercicios)
                {
                    tableExercicios.AddRow(t.Exercicio.Nome, t.Exercicio.Descricao, t.Exercicio.Series, t.Exercicio.Repeticoes);
                }
                

                Checkbox cbTreino = new Checkbox(
                    $"PÁGINA > DETALHE TREINO\n\n{tableTreino.ToStringAlternative()}\n\n\nEXERCÍCIOS\n{tableExercicios.ToStringAlternative()}", 
                    "Voltar"
                );
                cbTreino.Select();
            }
        } 
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    private (int, int) EscolherVinculosTreinoExercicio(string texto, bool desvincular = false)
    {
        TreinoRepository _treinoRepository = new TreinoRepository();

        try
        {
            while(true)
            {
                string treinoEscolhido = "";
                int treinoEscolhidoId = 0;

                int exercicioEscolhidoId = 0;

                List<string> treinosOptions = new List<string>{"--Voltar--"};
                List<int> treinosOptionId = new List<int>();
                treinosOptionId.Add(0);

                List<string> exerciciosOptions = new List<string>{"--Voltar--"};
                List<int> exerciciosOptionsId = new List<int>();
                exerciciosOptionsId.Add(0);
                
                List<Treino> treinos = _treinoRepository.ObterTodos();
                foreach (var u in treinos)
                {
                    treinosOptions.Add(u.ToString());
                    treinosOptionId.Add((int)u.TreinoId);
                }

                int escolhaTreino = 0;
                Checkbox cbTreinos = new Checkbox(
                    $"PÁGINA TREINO > {texto} EXERCÍCIOS AO TREINO\n\nQual treino deseja escolher para {texto.ToLower()} exercícios?\n", 
                    treinosOptions.ToArray()
                );
                foreach (var checkboxReturn in cbTreinos.Select())
                {
                    escolhaTreino = checkboxReturn.Index;
                }
                if(escolhaTreino == 0) return (0, 0);

                treinoEscolhido = treinosOptions[escolhaTreino];
                treinoEscolhidoId = treinosOptionId[escolhaTreino];

                //Exercicios
                List<Exercicio> exercicios = new List<Exercicio>();
                exercicios = desvincular == true ?  _exercicioRepository.ObterTodosComTreinoId(treinoEscolhidoId) : _exercicioRepository.ObterTodosSemTreinoId(treinoEscolhidoId);
                foreach (var u in exercicios)
                {
                    exerciciosOptions.Add(u.ToString());
                    exerciciosOptionsId.Add((int)u.ExercicioId);
                }

                int escolhaExercicio = 0;
                Checkbox cbExercicios = new Checkbox(
                    $"PÁGINA TREINO > {texto} EXERCÍCIOS AO TREINO\n\n{texto} A ESSE TREINO ==> {treinoEscolhido}\n\nQual exercício deseja escolher para {texto.ToLower()} a esse treino?\n", 
                    exerciciosOptions.ToArray()
                );
                foreach (var checkboxReturn in cbExercicios.Select())
                {
                    escolhaExercicio = checkboxReturn.Index;
                }
                if(escolhaExercicio == 0) continue; 
                exercicioEscolhidoId = exerciciosOptionsId[escolhaExercicio];

                return (treinoEscolhidoId, exercicioEscolhidoId);
            }

        } catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }

}