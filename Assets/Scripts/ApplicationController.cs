using Enums;

public class ApplicationController
{
    public bool IsStarted { get; private set; }
    
    private static ApplicationController _instance;
    public static ApplicationController Instance => _instance ??= new ApplicationController();

    private ApplicationController()
    {
        EventBus.Instance.Subscribe(EventBusAction.GameStart,StartGame);
        EventBus.Instance.Subscribe(EventBusAction.Win,StopGame);
        EventBus.Instance.Subscribe(EventBusAction.Lose,StopGame);
    }
    
    public void StartGame()
    { 
        IsStarted = true;
    }
    
    public void StopGame()
    {
        IsStarted = false;
    }
    
    ~ApplicationController()
    {
        EventBus.Instance.Unsubscribe(EventBusAction.GameStart,StartGame);
        EventBus.Instance.Unsubscribe(EventBusAction.Win,StopGame);
        EventBus.Instance.Unsubscribe(EventBusAction.Lose,StopGame);
    }
}
