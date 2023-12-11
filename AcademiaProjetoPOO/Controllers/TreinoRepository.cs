using EFCore;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using EFCore;

public class TreinoRepository : Repository<Treino> 
{
    public Treino ObterPorId(int id)
    {
        var treino = context.Set<Treino>()
            .Where(a => a.TreinoId == id)
            .Include(t => t.TreinosExercicios)
            .ThenInclude(t => t.Exercicio)
            .FirstOrDefault();
        return treino;
    }
    public new List<Treino> ObterTodos()
    {
        var treinos = context.Set<Treino>().Include(t => t.TreinosExercicios).ThenInclude(te => te.Exercicio).ToList();

        // Garantir que as coleções relacionadas estejam carregadas
        foreach (var treino in treinos)
        {
            context.Entry(treino).Collection(t => t.TreinosExercicios).Load();
            foreach (var treinoExercicio in treino.TreinosExercicios)
            {
                context.Entry(treinoExercicio).Reference(te => te.Exercicio).Load();
            }
        }

        return treinos;
    }
    public List<Treino> ObterTodosTreinosComAlunoId(int alunoId)
    {
         return context.Set<Treino>().Where(e => e.AlunosTreinos.Any(te => te.AlunoId == alunoId)).ToList();
    }
     public List<Treino> ObterTodosTreinosSemAlunoId(int alunoId)
    {
         return context.Set<Treino>().Where(e => e.AlunosTreinos.All(te => te.AlunoId != alunoId)).ToList();
    }
    
}