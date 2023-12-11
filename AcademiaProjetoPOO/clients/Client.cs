using EFCore;
using Spectre.Console;

public class Client
{
    public Usuario Usuario {get;}
    public Client(Usuario usuario)
    {   
        Usuario = usuario;    
    }

    public void Header(Usuario usuario)
    {
        var grid = new Grid();

        // Add columns 
        grid.AddColumn().Expand();
        grid.AddColumn();
        grid.AddColumn();
        //Adding Rows
        grid.AddRow(new Text[]{
            new Text($"{usuario.Nome}", new Style(Color.Red, Color.Black)),
            new Text($"{usuario.UsuarioTipo}", new Style(Color.Green, Color.Black)).Centered(),
            new Text($"{usuario.Email}", new Style(Color.Blue, Color.Black)).Centered()
        });

        var table = new Table();

        // Add some columns
        table.AddColumn($"{usuario.Nome}").Expand();
        table.AddColumn($"{usuario.UsuarioTipo}");
        table.AddColumn($"{usuario.Email}");

        table.Expand();

        var rule = new Rule($"[dodgerblue1]Nome: [/]{usuario.Nome}  [dodgerblue1]Cargo: [/]{usuario.UsuarioTipo}  [dodgerblue1]E-mail: [/]{usuario.Email}");
        AnsiConsole.WriteLine("\n\n");
        AnsiConsole.Write(new Rule("ByteAcad feito por Felipe Campos 🚀"));
        AnsiConsole.Write(rule);
    }
}
