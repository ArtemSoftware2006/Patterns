

using System.Security.Cryptography.X509Certificates;

TicketHandler ticketHandler = new TicketHandler();
TicketMonitor ticketMonitor1 = new TicketMonitor("First Tiket Monitor");
TicketMonitor ticketMonitor2= new TicketMonitor("Second Tiket Monitor");

ticketHandler.AddTicket(new Ticket() {Id = 1, StartDate = new DateTime(2022, 12, 21), StopDate = new DateTime(2022, 12, 22), StartPlace = "Moscow", StopPlace = "Vena" });
ticketHandler.AddTicket(new Ticket() { Id = 2, StartDate = new DateTime(2022, 12, 12), StopDate = new DateTime(2022, 12, 13), StartPlace = "Vena", StopPlace = "Moscow" });


ticketMonitor1.Subscribe(ticketHandler);
ticketMonitor2.Subscribe(ticketHandler);

ticketHandler.AddTicket(new Ticket() { Id = 3, StartDate = new DateTime(2022, 12, 21), StopDate = new DateTime(2022, 12, 22), StartPlace = "Moscow", StopPlace = "Vena" });
ticketHandler.StopTikets();
public struct Ticket
{
    public int Id;
    public DateTime StartDate;
    public DateTime StopDate;
    public string StartPlace;
    public string StopPlace;
}

public class TicketHandler : IObservable<Ticket>
{
    private List<IObserver<Ticket>> _observers = new List<IObserver<Ticket>>();
    private List<Ticket> _tickets = new List<Ticket>();
    public IDisposable Subscribe(IObserver<Ticket> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            foreach (var item in _tickets)
            {
                observer.OnNext(item);
            }
        }
        return new Unsubscriber(_observers, observer);
    }
    public void AddTicket(Ticket ticket)
    {
        _tickets.Add(ticket);
        foreach (var item in _observers)
        {
            item.OnNext(ticket);
        }
    }
    public void StopTikets()
    {
        foreach (var item in _observers)
        {
            item.OnCompleted();
        }
    }
}

public class TicketMonitor : IObserver<Ticket>
{
    private string name;
    private IDisposable? _cancellation;    

    public TicketMonitor(string name) => this.name = name;

    public virtual void Subscribe(TicketHandler provider) =>
        _cancellation = provider.Subscribe(this);

    public virtual void Unsubscribe()
    {
        _cancellation?.Dispose();
    }
    public void OnCompleted()
    {
        Console.WriteLine($"{name} stoped");
        Console.WriteLine("Mission completed");
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    void IObserver<Ticket>.OnNext(Ticket value)
    {
        Console.WriteLine($"{name} have new ticket");
        Console.WriteLine(value.Id);
        Console.WriteLine(value.StartDate);
        Console.WriteLine(value.StopDate);
        Console.WriteLine(value.StartPlace);
        Console.WriteLine(value.StopPlace);
    }
}

public class Unsubscriber : IDisposable
{
    private readonly List<IObserver<Ticket>> _observers;
    private readonly IObserver<Ticket> _observer;

    internal Unsubscriber(
        List<IObserver<Ticket>> observers,
        IObserver<Ticket> observer) => (_observers, _observer) = (observers, observer);

    public void Dispose() => _observers.Remove(_observer);
}
