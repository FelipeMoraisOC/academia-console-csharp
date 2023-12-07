using Interfaces;
using EFCore;

public class ExercicioRepository : Repository<Exercicio> 
{
    public List<Exercicio> ObterTodosComTreinoId(int treinoId)
    {
         return context.Set<Exercicio>().Where(e => e.TreinosExercicios.Any(te => te.TreinoId == treinoId)).ToList();
    }
    public List<Exercicio> ObterTodosSemTreinoId(int treinoId)
    {
         return context.Set<Exercicio>().Where(e => e.TreinosExercicios.All(te => te.TreinoId != treinoId)).ToList();
    }
}