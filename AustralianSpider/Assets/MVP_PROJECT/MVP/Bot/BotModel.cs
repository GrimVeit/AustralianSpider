using System;

public class BotModel
{
   public event Action<string> OnSuccess;
    public event Action<string> OnFailure;

    public void Initialize()
    {
        OnSuccess?.Invoke("Test build 93.6.2");
    }

    public void Dispose()
    {

    }
}
