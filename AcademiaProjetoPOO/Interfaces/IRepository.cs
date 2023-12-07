using System.Linq.Expressions;
using EFCore;
using Microsoft.EntityFrameworkCore;

namespace Interfaces;


public interface IRepository<T>
{
    public void Inserir(T entity);
    public void Atualizar(int id, T newEntity);
    public void Excluir(int? id);
    public T ObterPorId(int? id);
    public List<T> ObterTodos();
    public T ExisteNaBaseDeDados(string tabela, string coluna, string dado);

}

public class Repository<T> : IRepository<T> where T : class
{
    public AppDbContext context = new AppDbContext();

    public void Inserir(T entity)
    {
        context.Set<T>().Add(entity);
        context.SaveChanges();
    }

    public void Atualizar(int id, T newEntity)
    {
        context.Entry(context.Set<T>().Find(id)).CurrentValues.SetValues(newEntity);
        context.SaveChanges();
    }

    public void Excluir(int? id)
    {
        context.Set<T>().Remove(context.Set<T>().Find(id));
        context.SaveChanges();
    }

    public T ObterPorId(int? id)
    {
        return context.Set<T>().Find(id);
    }

    public List<T> ObterTodos()
    {
        return context.Set<T>().ToList();
    }

   public T ExisteNaBaseDeDados(string tabela, string coluna, string dado)
    {
        var a = context.Set<T>()
            .FromSqlRaw($"SELECT * FROM {tabela} Where {coluna} =  '{dado}'")
            .ToList().FirstOrDefault();

        return a;
    }

    public void AtualizarCampo(Expression<Func<T, bool>> filtro, Expression<Func<T, object>> campo, object novoValor)
    {
        var entidade = context.Set<T>().SingleOrDefault(filtro);

        if (entidade != null)
        {
            context.Entry(entidade).Property(campo).CurrentValue = novoValor;
            context.SaveChanges();
        }
        else
        {
            // Trate o caso quando a entidade não é encontrada
            throw new InvalidOperationException("Entidade não encontrada");
        }
    }
    public void AtualizarCampo(Expression<Func<T, bool>> filtro, string nomeCampo, object novoValor)
    {
        var entidade = context.Set<T>().SingleOrDefault(filtro);

        if (entidade != null)
        {
            var propriedade = entidade.GetType().GetProperty(nomeCampo);

            if (propriedade != null)
            {
                propriedade.SetValue(entidade, novoValor);
                context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"A propriedade '{nomeCampo}' não foi encontrada na entidade '{typeof(T).Name}'.");
            }
        }
        else
        {
            throw new InvalidOperationException("Entidade não encontrada");
        }
    }
}