using Interfaces;
using EFCore;
using Microsoft.EntityFrameworkCore;

public class AlunoRepository : Repository<Aluno> 
{
    public new List<Aluno> ObterTodos()
    {
        var alunos = context.Set<Aluno>().Include(t => t.AlunosTreinos).ThenInclude(te => te.Treino).ToList();

        // Garantir que as coleções relacionadas estejam carregadas
        foreach (var aluno in alunos)
        {
            context.Entry(aluno).Collection(t => t.AlunosTreinos).Load();
            foreach (var alunoTreino in aluno.AlunosTreinos)
            {
                context.Entry(alunoTreino).Reference(te => te.Treino).Load();
            }
        }

        return alunos;
    }

    public Aluno ObterPorId(int id)
    {
        var aluno = context.Set<Aluno>()
            .Where(a => a.AlunoId == id)
            .Include(t => t.AlunosTreinos)
            .ThenInclude(t => t.Treino)
            .ThenInclude(te => te.TreinosExercicios)
            .ThenInclude(e => e.Exercicio)
            .FirstOrDefault();
        return aluno;
    }
}