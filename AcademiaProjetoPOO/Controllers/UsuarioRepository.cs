using Interfaces;
using EFCore;



public class UsuarioRepository : Repository<Usuario> 
{
    public Usuario logarUsuario(string cpf, string senha)
    {
        try
        {
            Usuario a = context.Set<Usuario>().Where(u => u.CPF == cpf && u.Senha == senha).ToList().FirstOrDefault(); 
            if(a == null) throw new ArgumentException("Login inválido!");
            
            return a;
            
        } catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}