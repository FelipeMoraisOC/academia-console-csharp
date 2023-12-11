using EFCore;
using Interfaces;
using Microsoft.EntityFrameworkCore;

public class AlunoTreinoRepository : Repository<AlunoTreino> 
{
    public AlunoTreino ObterPorAlunoIdTreinoId(int aId, int tId)
    {
        var res = context.Set<AlunoTreino>().Where(u => u.AlunoId == aId && u.TreinoId == tId).Include(a => a.Aluno).Include(t => t.Treino).FirstOrDefault();
        if(res == null) throw new ArgumentException("Aluno Treino inexistente!");

        return res;
    }

    public void Excluir(AlunoTreino alunoTreino)
    {
        try
        {
            var alunoTreinoToDelete = context.Set<AlunoTreino>()
                        .FirstOrDefault(u => u.AlunoId == alunoTreino.AlunoId && u.TreinoId == alunoTreino.TreinoId);
            if (alunoTreinoToDelete != null)
            {
                context.Set<AlunoTreino>().Remove(alunoTreinoToDelete);
                context.SaveChanges();
                
                // Recarregar a entidade Aluno
                context.Entry(alunoTreino.Aluno).Collection(t => t.AlunosTreinos).Load();

                // Recarregar a entidade Treino
                context.Entry(alunoTreino.Treino).Collection(e => e.AlunosTreinos).Load();
            }
        } 
        catch(Exception ex)
        {
            throw new ArgumentException(ex.ToString());
        } 
        
    }  
}