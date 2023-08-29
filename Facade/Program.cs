


using System.Net.Http.Headers;

VsCodeFacade vsCodeFacade = new VsCodeFacade(new TextEditor(), new Compiller(), new CLR());
Programmer programmer = new Programmer();
programmer.CreateApp(vsCodeFacade);



public class TextEditor
{
    public string Text { get; set; }
    public void AddText(string text)
    {
        Console.WriteLine("Text is added");
        Text += text;
    }

}
public class Compiller
{
    public string Code { get; set; }
    public void Compile(string code)
    {
        Code = code;
        Console.WriteLine("Compiling");
    }
}
public class CLR
{
    public void Run(string code)
    {
        Console.WriteLine("Running");
        Console.Write(code);
    }
}
public class Programmer
{
    public void CreateApp(VsCodeFacade vsCode)
    {
        vsCode.AddText("print('Hello World')");
        vsCode.RunApp();
    }
}
public class VsCodeFacade
{
    private readonly TextEditor textEditor;
    private readonly Compiller compiller;
    private readonly CLR clr;
    public VsCodeFacade(TextEditor textEditor, Compiller compiller, CLR clr)
    {
        this.textEditor = textEditor;
        this.compiller = compiller;
        this.clr = clr;
    }

    public void AddText(string text)
    {
        textEditor.AddText(text);
    }
    public void RunApp()
    {
        compiller.Compile(textEditor.Text);
        clr.Run(compiller.Code);
    }
}