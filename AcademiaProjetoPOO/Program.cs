using EFCore;
using Enums;
using Spectre.Console;

using (var dbContext = new AppDbContext())
{

    TreinoRepository _treinoRepository = new TreinoRepository(); 

    LoginClient _loginClient = new LoginClient();

    Usuario usuario = new Usuario();
    while(true)
    {
        usuario = _loginClient.Login();
        switch(usuario.UsuarioTipo)
        {
            case UsuarioTipo.Admin:
                new AdminClient(usuario);
            break;
            case UsuarioTipo.Professor:
                new ProfessorClient(usuario);
            break;
                case UsuarioTipo.Atendente:
                new AtendenteClient(usuario);
            break;
        }
    }
    
} 

