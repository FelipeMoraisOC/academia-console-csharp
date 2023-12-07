// using ConsoleTables;
// using EFCore;

// namespace Services;

// public class PlanoService
// {
//     private PlanoRepository _alunoRepository = new PlanoRepository();

//     public Plano AdicionarPlano()
//     {
//         string nome, descricao;
//         decimal preco;
//         Duracao duracao;
//         bool ehMensal;

//         Console.WriteLine("--CADASTRO DE UM NOVO PLANO--");
//         while(true)
//         {
//             try{
//                 Console.WriteLine("Digite o nome do plano: ");
//                 nome = Console.ReadLine();
//                 if(string.IsNullOrEmpty(nome)) throw new ArgumentException("Nome não pode estar vazio! Digite novamente.");

//                 break;
//             } 
//             catch(Exception ex)
//             {
//                 Console.WriteLine("Erro: " + ex.Message);
//             }
//         }

//         while(true)
//         {
//             try{
//                 Console.WriteLine("Digite a Descricao do Plano: ");
//                 descricao = Console.ReadLine();

//                 if(string.IsNullOrEmpty(descricao)) throw new ArgumentException("Descrição não pode estar vazio! Digite novamente.");

//                 break;
//             } 
//             catch(Exception ex)
//             {
//                 Console.WriteLine("Erro: " + ex.Message);
//             }
            
            
//         }
//         while(true)
//         {
//             try{
//                 Console.WriteLine("Digite o preço: ");
//                 preco = decimal.Parse(Console.ReadLine());

//                 if(preco <= 0) throw new ArgumentException("Preço não pode ser 0 nem negativo.");
                
//                 break;
//             } 
//             catch(Exception ex)
//             {
//                 Console.WriteLine("Erro: " + ex.Message);
//             }
            
            
//         }
//         while(true)
//         {
//             try{
//                 Console.WriteLine("Escolha a recorrência do pagamento desse Plano: ");
//                 Console.WriteLine("**Observação: O plano mensal será cobrado todo mês, já o anual uma vez no ano.");
//                 Console.WriteLine("1 - Mensal\n2 -Semestral\n3 - Anual\nDigite o numero de sua escolha:");
//                 int ut = Int32.Parse(Console.ReadLine());

//                 if(ut != 1 && ut != 2 && ut != 3) throw new ArgumentException("Escolha uma das opções! Digite novamente.");

//                 switch(ut)
//                 {
//                     case 1: 
//                         usuarioTipo = UsuarioTipo.Admin;
//                     break;
//                     case 2:
//                         usuarioTipo = UsuarioTipo.Professor;
//                     break;
//                     case 3:
//                         usuarioTipo = UsuarioTipo.Atendente;
//                     break;
//                 }
//                 break;
//             } 
//             catch(Exception ex)
//             {
//                 Console.WriteLine("Erro: " + ex.Message);
//             }
//         }
//         try
//         {
//             Plano aluno = new Plano(null, nome, sobrenome, email, cpf);
//             _alunoRepository.Inserir(aluno);
//             Console.ForegroundColor = ConsoleColor.Green;
//             Console.WriteLine("Plano adicionado com sucesso!");
//             Console.WriteLine("Faça a matrícula do aluno para ele acessar a academia!");
//             Console.ResetColor();
            
//             return aluno;
//         }
//         catch(Exception ex)
//         {
//             Console.WriteLine("Erro: " + ex.Message);
//              throw;
//         }
        
//     }

//     public void ExibirListaPlanos()
//     {
//         List<Plano> alunos = _alunoRepository.ObterTodos();
//         var table = new ConsoleTable("PlanoId", "Nome", "Sobrenome", "Email", "CPF");
    
//         foreach(var u in alunos)
//         {
//            table.AddRow(u.PlanoId, u.Nome, u.Sobrenome, u.Email, u.CPF);
//         }

//         table.Write(Format.Alternative);

//     }

//     public void AlterarPlanoPorId()
//     {
//         string alunoId = "";
//         string campo = "";

//         Console.WriteLine("--------ALTERAR DADOS PLANO--------");
//         try
//         {
            
//             Console.WriteLine("Digite o Id do Plano que deseja alterar: ");
//             alunoId = Console.ReadLine();

//             if(_alunoRepository.ExisteNaBaseDeDados("Planos", "PlanoId", alunoId) == null) throw new ArgumentException("Plano Id inexistente!");

    
//             while(true)
//             {
//                 try
//                 {
//                     int opcao;
                        
//                     Console.WriteLine("Qual campo deseja alterar?");
//                     Console.WriteLine("1 - Nome\n2 - Sobrenome\n3 - E-mail\n4 - CPF\n0 - Voltar");
//                     opcao = Int32.Parse(Console.ReadLine());
//                     if(opcao == 0) break;
//                     else if(opcao == 1) campo = "Nome";
//                     else if(opcao == 2) campo = "Sobrenome";
//                     else if(opcao == 3) campo = "Email";
//                     else if(opcao == 4) campo = "CPF";
//                     else throw new ArgumentException("Opção Inválida! Digite Novamente.");

//                     Console.WriteLine("Digite o novo valor: ");
//                     string valor = Console.ReadLine();
                    
//                     _alunoRepository.AtualizarCampo(filtro: e => e.PlanoId == Int32.Parse(alunoId), campo, valor);
                    
//                     Console.ForegroundColor = ConsoleColor.Green; 
//                     Console.WriteLine("Dado atualizado com sucesso!");
//                     Console.ResetColor();
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine("Erro: " + ex.Message);
//                     Console.ResetColor();
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.WriteLine("Erro: " + ex.Message);
//             Console.ResetColor();
//         }
//     }

//     public void RemoverPlanoPorId()
//     {
//         int alunoId;

//         Console.WriteLine("--------REMOVER PLANO--------");
//         try
//         {
            
//             Console.WriteLine("Digite o Id do Plano que deseja remover: ");
//             alunoId = Int32.Parse(Console.ReadLine());

//             if(_alunoRepository.ExisteNaBaseDeDados("Planos", "PlanoId", alunoId.ToString()) == null) throw new ArgumentException("Plano Id inexistente!");

    
//             while(true)
//             {
//                 try
//                 {
//                     int opcao;
                        
//                     Console.WriteLine("Tem certeza que deseja exculir?\n1 - Sim\n2 - Não");
                    
//                     opcao = Int32.Parse(Console.ReadLine());
//                     if(opcao == 1) _alunoRepository.Excluir(alunoId);
//                     else break;
                    
//                     Console.ForegroundColor = ConsoleColor.Green; 
//                     Console.WriteLine("Plano removido com sucesso!");
//                     Console.ResetColor();
//                     break;
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.ForegroundColor = ConsoleColor.Red;
//                     Console.WriteLine("Erro: " + ex.Message);
//                     Console.ResetColor();
//                 }
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.ForegroundColor = ConsoleColor.Red;
//             Console.WriteLine("Erro: " + ex.Message);
//             Console.ResetColor();
//         }
//     }
// }