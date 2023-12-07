using ConsoleTables;
using EFCore;

namespace Services;

public class ExercicioService
{
    private ExercicioRepository _exercicioRepository = new ExercicioRepository();

    public Exercicio CriarExercicio()
    {
        string nome, descricao;
        int series, repeticoes;

        Console.WriteLine("--CADASTRO DE UM NOVO EXERCÍCIO--");

        Console.ForegroundColor = ConsoleColor.DarkYellow; 
        Console.WriteLine("**Observação: Este exercício pode ser utilizado por vários treinos, basta vincular um treino a esse exercício.");
        Console.ResetColor();

        while(true)
        {
            try{
                Console.WriteLine("Digite o nome do exercício: ");
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
                Console.WriteLine("Digite a descrição do exercício: ");
                descricao = Console.ReadLine();

                if(string.IsNullOrEmpty(descricao)) throw new ArgumentException("Descricao não pode estar vazio! Digite novamente.");

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
                Console.WriteLine("Digite a quantidade de séries: ");
                series = Int32.Parse(Console.ReadLine());

                if(series <= 0) throw new ArgumentException("Quantidade de séries não pode ser 0 ou menor que 0! Digite novamente.");

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
                Console.WriteLine("Digite a quantidade de repetições: ");
                repeticoes = Int32.Parse(Console.ReadLine());

                if(repeticoes <= 0) throw new ArgumentException("Quantidade de repetições não pode ser 0 ou menor que 0! Digite novamente.");

                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        try
        {
            Exercicio exercicio = new Exercicio(null, nome, descricao, series, repeticoes);
            _exercicioRepository.Inserir(exercicio);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exercicio criado com sucesso!");
            Console.WriteLine("Associe exercícios para esse treino!");
            Console.ResetColor();
            
            return exercicio;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
             throw;
        }
        
    }

    public string ExibirListaExercicios()
    {
        List<Exercicio> treinos = _exercicioRepository.ObterTodos();
        var table = new ConsoleTable("Exercicio Id", "Nome", "Descrição", "Séries", "Repetições");
    
        foreach(var u in treinos)
        {
           table.AddRow(u.ExercicioId, u.Nome, u.Descricao, u.Series, u.Repeticoes);
        }

        return table.ToStringAlternative();

    }

    public void AlterarExercicioPorId()
    {
        string exercicioId = "";
        string campo = "";

        Console.WriteLine("PÁGINA EXERCÍCIO > ALTERAR EXERCÍCIO");
        try
        {
            
            Console.WriteLine("Digite o Id do Exercício que deseja alterar: ");
            exercicioId = Console.ReadLine();

            if(_exercicioRepository.ExisteNaBaseDeDados("Exercicios", "ExercicioId", exercicioId) == null) throw new ArgumentException("Exercicio Id inexistente!");

    
            while(true)
            {
                try
                {
                    int opcao = 0;
                    Checkbox cbUsuarios = new Checkbox(
                        $"PÁGINA EXERCÍCIO > ALTERAR DADO EXERCÍCIO\n\nQual campo deseja alterar?\n", 
                        "Voltar",
                        "Nome",
                        "Descrição",
                        "Séries",
                        "Repetições"
                    );
                    foreach (var checkboxReturn in cbUsuarios.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }
                        
                    if(opcao == 0) break;
                    else if(opcao == 1) campo = "Nome";
                    else if(opcao == 2) campo = "Descricao";
                    else if(opcao == 3) campo = "Series";
                    else if(opcao == 4) campo = "Repeticoes";

                    Console.WriteLine("Digite o novo valor: ");
                    string valor = Console.ReadLine();
                    
                    _exercicioRepository.AtualizarCampo(filtro: e => e.ExercicioId == Int32.Parse(exercicioId), campo, opcao == 3 || opcao == 4 ? Int32.Parse(valor) : valor);

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

    public void RemoverExercicioPorId()
    {
        int alunoId;

         Console.WriteLine("PÁGINA EXERCÍCIO > REMOVER EXERCÍCIO");
        try
        {
            
            Console.WriteLine("Digite o Id do Exercício que deseja remover: ");
            alunoId = Int32.Parse(Console.ReadLine());

            if(_exercicioRepository.ExisteNaBaseDeDados("Exercicios", "ExercicioId", alunoId.ToString()) == null) throw new ArgumentException("Exercicio Id inexistente!");

    
            while(true)
            {
                try
                {
                    int opcao = 0;   
                    Checkbox cbExercicios = new Checkbox(
                        $"PÁGINA EXERCÍCIO > REMOVER EXERCÍCIO\n\nTem certeza que deseja exculir?\n", 
                        "Não",
                        "Sim"
                    );
                    foreach (var checkboxReturn in cbExercicios.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }
                    if(opcao == 1) _exercicioRepository.Excluir(alunoId);
                    else break;
                    
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine("Exercício removido com sucesso!");
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