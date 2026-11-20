namespace Nebula;

public static class NebulaGameEvents
{
    public static event Action? BootStarted;
    public static void InvokeBootStarted() => BootStarted?.Invoke();

    public static event Action? BaseGameBootFinished;
    public static void InvokeBaseGameBootFinished() => BaseGameBootFinished?.Invoke();
    public static event Action? NebulaLoadFinished;
    public static void InvokeNebulaLoadFinished() => NebulaLoadFinished?.Invoke();
}