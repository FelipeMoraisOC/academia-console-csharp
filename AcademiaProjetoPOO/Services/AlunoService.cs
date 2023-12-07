using ConsoleTables;
using EFCore;

namespace Services;

public class AlunoService
{
    private AlunoRepository _alunoRepository = new AlunoRepository();

    public Aluno AdicionarAluno()
    {
        string nome, sobrenome, email, cpf;

        Console.WriteLine("--CADASTRO DE UM NOVO ALUNO--");
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

                if(_alunoRepository.ExisteNaBaseDeDados("Alunos", "CPF", cpf) != null) 
                throw new ArgumentException("Aluno com esse CPF já existente na base de dados! Digite novamente.");

                break;
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        try
        {
            Aluno aluno = new Aluno(null, email, nome, sobrenome, cpf);
            _alunoRepository.Inserir(aluno);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Aluno adicionado com sucesso!");
            Console.WriteLine("Faça a matrícula do aluno para ele acessar a academia!");
            Console.ResetColor();
            
            return aluno;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
             throw;
        }
        
    }
    public string ExibirListaAlunos()
    {
        List<Aluno> alunos = _alunoRepository.ObterTodos();
        var table = new ConsoleTable("AlunoId", "Nome", "Sobrenome", "Email", "CPF");
    
        foreach(var u in alunos)
        {
           table.AddRow(u.AlunoId, u.Nome, u.Sobrenome, u.Email, u.CPF);
        }

        return table.ToStringAlternative();

    }
    public void AlterarAlunoPorId()
    {
        string alunoId = "";
        string campo = "";

        try
        {
            
            Console.WriteLine("Digite o Id do Aluno que deseja alterar: ");
            alunoId = Console.ReadLine();

            if(_alunoRepository.ExisteNaBaseDeDados("Alunos", "AlunoId", alunoId) == null) throw new ArgumentException("Aluno Id inexistente!");

    
            while(true)
            {
                try
                {
                    int opcao = 0;
                    Checkbox cbUsuarios = new Checkbox(
                        $"PÁGINA ALUNO > ALTERAR DADO ALUNO\n\nQual campo deseja alterar?\n", 
                        "Voltar", 
                        "Nome",
                        "Sobrenome",
                        "E-mail",
                        "CPF"
                    );
                    foreach (var checkboxReturn in cbUsuarios.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }

                    if(opcao == 0) break;
                    else if(opcao == 1) campo = "Nome";
                    else if(opcao == 2) campo = "Sobrenome";
                    else if(opcao == 3) campo = "Email";
                    else if(opcao == 4) campo = "CPF";

                    Console.WriteLine("Digite o novo valor: ");
                    string valor = Console.ReadLine();
                    
                    _alunoRepository.AtualizarCampo(filtro: e => e.AlunoId == Int32.Parse(alunoId), campo, valor);
                    
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
    public void RemoverAlunoPorId()
    {
        int alunoId;

        Console.WriteLine("PÁGINA ALUNO > REMOVER ALUNO");
        try
        {
            
            Console.WriteLine("Digite o Id do Aluno que deseja remover: ");
            alunoId = Int32.Parse(Console.ReadLine());

            if(_alunoRepository.ExisteNaBaseDeDados("Alunos", "AlunoId", alunoId.ToString()) == null) throw new ArgumentException("Aluno Id inexistente!");

    
            while(true)
            {
                try
                {
                    int opcao = 0;
                        
                    Console.WriteLine("Tem certeza que deseja exculir?\n");
                    Checkbox cbUsuarios = new Checkbox(
                        $"PÁGINA ALUNO > REMOVER ALUNO\n\nTem certeza que deseja exculir?\n", 
                        "Não",
                        "Sim"
                    );
                    foreach (var checkboxReturn in cbUsuarios.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }

                    if(opcao == 1) _alunoRepository.Excluir(alunoId);
                    else break;

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