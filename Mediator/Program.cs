
Robot robot1 = new Robot();
Assistant assistant1 = new Assistant();
MediatorLaboratory mediatorLaboratory = new MediatorLaboratory(robot1,assistant1);
Scientist scientist1 = new Scientist(mediatorLaboratory);
 scientist1.StartExperiment();
 scientist1.StartExperiment();
 scientist1.StartExperiment();



public interface IMediator
{
    Task InteractionAsync();
}

public class MediatorLaboratory : IMediator
{
    private Robot robot;
    private Assistant assistant;

    public MediatorLaboratory(Robot robot, Assistant assistant)
    {
        this.robot = robot;
        this.assistant = assistant;
    }

    public async Task InteractionAsync()
    {
        Thread.Sleep(1000);
        int speedMode = assistant.chooseSpeedMode();
        robot.Go(speedMode);
        Console.WriteLine("Робот проехал " + robot.DistanceTraveled());
    }
}

public class Robot
{
    private int _speedMode;

    public void Go(int speedMode)
    {
        _speedMode = speedMode;

        if (_speedMode == 0)
            Console.WriteLine("Я еду медленно");
        if (_speedMode == 1)
            Console.WriteLine("Я еду быстро");
    }
    public int DistanceTraveled()
    {
        if (_speedMode == 0)
            return (int)new Random().NextInt64(10,20);
        if (_speedMode == 1)
            return (int)new Random().NextInt64(10);
        else
            return 0;
    }
}

public class Scientist
{
    private readonly IMediator _mediator;
    public Scientist(IMediator mediator)
    {
        this._mediator = mediator;
    }
    public void StartExperiment()
    {
        _mediator.InteractionAsync();
    }
}

public class Assistant
{
    public int chooseSpeedMode()
    {
        int speedMode = (int)new Random().NextInt64(2);
        Console.WriteLine("Выбрана скорость " + speedMode);
        return speedMode;
    }
}