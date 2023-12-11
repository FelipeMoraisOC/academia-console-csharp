using EFCore;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

public class TreinoExercicioRepository : Repository<TreinoExercicio> 
{
    public TreinoExercicio ObterPorTreinoIdExercicioId(int tId, int eId)
    {
         var res = context.Set<TreinoExercicio>().Where(u => u.TreinoId == tId && u.ExercicioId == eId).Include(t => t.Treino).Include(te => te.Exercicio).FirstOrDefault();
         if(res == null) throw new ArgumentException("TreinoExercicio inexistente!");

         return res;
    }
    public void Excluir(TreinoExercicio treinoExercicio)
    {
        try
        {
            var treinoExercicioToDelete = context.Set<TreinoExercicio>()
                        .FirstOrDefault(u => u.TreinoId == treinoExercicio.TreinoId && u.ExercicioId == treinoExercicio.ExercicioId);
            if (treinoExercicioToDelete != null)
            {
                context.Set<TreinoExercicio>().Remove(treinoExercicioToDelete);
                context.SaveChanges();
                
                // Recarregar a entidade Treino
                context.Entry(treinoExercicio.Treino).Collection(t => t.TreinosExercicios).Load();

                // Recarregar a entidade Exercicio
                context.Entry(treinoExercicio.Exercicio).Collection(e => e.TreinosExercicios).Load();
            }
        } 
        catch(Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        } 
        
    }   
}