using EFCore;
using Enums;
using EFCore;
using Services;

using (var dbContext = new AppDbContext())
{
    UsuarioService usuarioService = new UsuarioService();
    AlunoService alunoService = new AlunoService();
    TreinoService treinoService = new TreinoService();
    ExercicioService exercicioService = new ExercicioService();

    Usuario usuario = new Usuario();

    bool sair = false;
    bool logado = false;

    Console.ForegroundColor = ConsoleColor.White;
    while(sair == false)
    {
        if(logado)
        {
            switch(usuario.UsuarioTipo)
            {
                case UsuarioTipo.Admin:
                while(sair == false)
                {
                    int opcao = 0;
                    Checkbox cbAdmin = new Checkbox(
                        "DASHBOARD ADMINISTRADOR\n\nQuantidade de Alunos: \nQuantidade de Usuarios no sistema: \nFaturamento: \n--------------MENU--------------\nSelecione uma das opções abaixo",
                        "Sair",
                        "Página de Usuários.",
                        "Página de Alunos.", 
                        "Página de Planos.",
                        "Página de Treinos.",
                        "Página de Exercícios."
                    );
                    foreach (var checkboxReturn in cbAdmin.Select())
                    {
                        opcao = checkboxReturn.Index;
                    }
                    switch (opcao)
                    {
                        case 1:  // Pagina de Usuários
                            int opcao1 = 0;
                            while(true)
                            {
                                Checkbox cbUsuarios = new Checkbox(
                                    $"ADMIN > PÁGINA USUÁRIO\n\n{usuarioService.ExibirListaUsuarios()}\n--------------MENU--------------", 
                                    "Voltar", 
                                    "Alterar Usuário",
                                    "Adicionar Usuario",
                                    "Remover Usuario"
                                );
                                foreach (var checkboxReturn in cbUsuarios.Select())
                                {
                                    opcao1 = checkboxReturn.Index;
                                }

                                if(opcao1 == 0) break;
                                switch(opcao1)
                                {
                                    case 1:
                                        usuarioService.AlterarUsuarioPorId();
                                    break;
                                    case 2:
                                        usuarioService.AdicionarUsuario();
                                    break;
                                    case 3:
                                        usuarioService.RemoverUsuarioPorId();
                                    break;
                                }
                            }
                        break;
                        case 2: //Página de Alunos
                            int opcao2 = 0;
                            
                            while(true)
                            {

                                Checkbox cbAlunos = new Checkbox(
                                    $"ADMIN > PÁGINA ALUNOS\n\n{alunoService.ExibirListaAlunos()}\n--------------MENU--------------", 
                                    "Voltar", 
                                    "Alterar Aluno",
                                    "Adicionar Aluno",
                                    "Remover Aluno"
                                );
                                foreach (var checkboxReturn in cbAlunos.Select())
                                {
                                    opcao2 = checkboxReturn.Index;
                                }
                                if(opcao2 == 0) break;

                                switch(opcao2)
                                {
                                    case 1:
                                        alunoService.AlterarAlunoPorId();
                                    break;
                                    case 2:
                                        alunoService.AdicionarAluno();
                                    break;
                                    case 3:
                                        alunoService.RemoverAlunoPorId();
                                    break;
                                }
                            }
                        break;
                        case 3:
                        break;
                        case 4:
                            int opcao4 = 0;
                            while(true)
                            {
                                
                                Checkbox cbTreinos = new Checkbox(
                                    $"ADMIN > PÁGINA TREINO\n\n{treinoService.ExibirListaTreinos()}\n--------------MENU--------------", 
                                    "Voltar", 
                                    "Alterar Treino",
                                    "Adicionar Treino",
                                    "Remover Treino",
                                    "Vincular Exercícios aos Treinos",
                                    "Desvincular Exercícios dos Treinos",
                                    "Detalhes Treino"
                                );
                                foreach (var checkboxReturn in cbTreinos.Select())
                                {
                                    opcao4 = checkboxReturn.Index;
                                }
                                if(opcao4 == 0) break;

                                switch(opcao4)
                                {
                                    case 1:
                                        treinoService.AlterarTreinoPorId();
                                    break;
                                    case 2:
                                        treinoService.CriarTreino(usuario);
                                    break;
                                    case 3:
                                        treinoService.RemoverTreinoPorId();
                                    break;
                                    case 4:
                                        treinoService.VincularTreinoExercicios();
                                    break;
                                    case 5:
                                        treinoService.DesvincularTreinoExercicios();
                                    break;
                                    case 6:
                                        treinoService.DetalheTreino();
                                    break;
                                }
                            }
                        break;
                        case 5:
                            int opcao5 = 0;
                            while(true)
                            {
                                Checkbox cbTreinos = new Checkbox(
                                    $"ADMIN > PÁGINA EXERCÍCIO\n\n{exercicioService.ExibirListaExercicios()}\n--------------MENU--------------", 
                                    "Voltar", 
                                    "Alterar Exercicio",
                                    "Adicionar Exercicio",
                                    "Remover Treino"
                                );
                                foreach (var checkboxReturn in cbTreinos.Select())
                                {
                                    opcao5 = checkboxReturn.Index;
                                }
                                if(opcao5 == 0) break;

                                switch(opcao5)
                                {
                                    case 1:
                                        exercicioService.AlterarExercicioPorId();
                                    break;
                                    case 2:
                                        exercicioService.CriarExercicio();
                                    break;
                                    case 3:
                                        exercicioService.RemoverExercicioPorId();
                                    break;
                                }
                            }
                        break;
                        case 0:
                            sair = true;
                        break;
                    }
                }
                break;
                case UsuarioTipo.Professor:
                break;
                case UsuarioTipo.Atendente:
                break;
            }
        }
        else
        {
            try
            {
                usuario = usuarioService.Logar();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Usuário logado com sucesso!");
                Console.ResetColor();
                Console.WriteLine();
                
                logado = true;

            } catch (Exception ex)
            {
                logado = false;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: " + ex.Message);
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
} 


int OpcaoReadLine(int minValor, int maxValor)
{
    int opcao;
    while(true)
    {
        try
        {
            Console.WriteLine("Selecione uma opção: ");
            opcao = Int32.Parse(Console.ReadLine());
            if(minValor <= opcao && opcao <= maxValor)
            {
                break;
            }
            else 
            {
                throw new ArgumentException("Opção inválida digite novamente!");
            }
            
        }
        catch(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro: " + ex.Message);
            Console.ResetColor();
        }
    }
    return opcao;
}