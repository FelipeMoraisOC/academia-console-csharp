using EFCore;
using Spectre.Console;

public class AtendenteClient : Client
{
    TreinoClientView treinoClientView;
    ExercicioClientView exercicioClientView;
    AlunoClientView alunoClientView;

    public AtendenteClient(Usuario usuario) : base(usuario)
    {
        treinoClientView = new TreinoClientView(usuario);
        exercicioClientView = new ExercicioClientView(usuario);
        alunoClientView = new AlunoClientView(usuario);
        AlunoRepository _alunoRepository = new AlunoRepository(); 

        bool sair = false;
        while(sair == false)
        {
            Console.Clear();
            Header(usuario);
            AnsiConsole.Write(new Rule("[dodgerblue1]Dashboard Atendente[/]"));
            AnsiConsole.WriteLine("\n\n");

            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                    .Title("[dodgerblue1]Menu do Atendente[/]")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Sair"),
                        (1, "Página de Alunos"),
                        (2, "Verificar Treinos Alunos"),
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
                    alunoClientView.PaginaAlunosAtendente(usuario);
                break;
                case 2:
                    alunoClientView.PlanejamentoTreinoAluno(_alunoRepository);
                break;
            }
        }
    }
}