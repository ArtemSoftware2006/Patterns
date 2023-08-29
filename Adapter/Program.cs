
MessageSender messageSender = new();
Adapter adapter = new();

Send(messageSender,"MessageSender is working");
Send(adapter, "Adapter is working");





static void Send(MessageSender sender, string message)
{
    sender.Send(message);
}

public class MessageSender
{
    public virtual void Send(string message)
    {
        Console.WriteLine(message);
    }
}

public class Adapter : MessageSender
{
    private EmailSender emailSender;

    public Adapter()
    {
        emailSender = new EmailSender();
    }

    public override void Send(string message)
    {
        emailSender.SendMail(message);
    }
}

public class EmailSender
{
    public void SendMail(string message)
    {
        Console.WriteLine("Sending email");
        Console.WriteLine(message);
    }
}