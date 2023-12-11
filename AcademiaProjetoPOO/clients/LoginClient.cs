using EFCore;
using Spectre.Console;

public class LoginClient
{

    public LoginClient()
    {
        
    }
    public Usuario Login()
    {
        UsuarioRepository _usuarioRepository = new UsuarioRepository();

        var font = FigletFont.Load("./Assets/fonts/slant.flf");
        AnsiConsole.Write( new FigletText(font, "ByteAcad").Centered().Color(Color.White));
        while(true)
        {
            AnsiConsole.Write( new Rule("[white]FAÇA O LOGIN[/]"));
            string cpf = AnsiConsole.Ask<string>("Digite seu [green]CPF[/]?");
            string senha = AnsiConsole.Prompt(
                new TextPrompt<string>("Digite sua [green]Senha[/]?").Secret());
            try
            {
                
                var usuario = _usuarioRepository.logarUsuario(cpf, senha);

                if(usuario == null)
                {
                    AnsiConsole.MarkupLine("[red italic]Login inválido![/]");
                }
                else{
                    return usuario;
                }

            } catch(Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);

            }
        }
       
    }
    public Usuario LoginMocado()
    {
        UsuarioRepository _usuarioRepository = new UsuarioRepository();
        
            
        var usuario = _usuarioRepository.logarUsuario("52850272841", "1234");

        return usuario;

        
       
    }

}