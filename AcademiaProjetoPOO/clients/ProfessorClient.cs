using EFCore;
using Spectre.Console;

public class ProfessorClient : Client
{
    TreinoClientView treinoClientView;
    ExercicioClientView exercicioClientView;
    AlunoClientView alunoClientView;
    public ProfessorClient(Usuario usuario) : base(usuario)
    {
        treinoClientView = new TreinoClientView(usuario);
        exercicioClientView = new ExercicioClientView(usuario);
        alunoClientView = new AlunoClientView(usuario);

        bool sair = false;
        while(sair == false)
        {
            Console.Clear();
            Header(usuario);
            AnsiConsole.Write(new Rule("[dodgerblue1]Dashboard Professor[/]"));
            AnsiConsole.WriteLine("\n\n");

            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<(int Index, string Name)>()
                    .Title("[dodgerblue1]Menu do Professor[/]")
                    .AddChoices( new List<(int Index, string Name)>
                    {
                        (0, "Sair"),
                        (1, "Planejamento de Treinos"),
                        (2, "Planejamento de Exercícios"),
                        (3, "Treinamento Alunos"),
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
                    treinoClientView.PaginaTreinoProfessor(usuario);
                break;
                case 2:
                    exercicioClientView.PaginaExerciciosProfessor(usuario);
                break;
                case 3:
                    alunoClientView.PaginaAlunosProfessor(usuario);
                break;
            }
        }
    }


}
