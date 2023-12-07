using EFCore;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using EFCore;

public class TreinoRepository : Repository<Treino> 
{
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
}