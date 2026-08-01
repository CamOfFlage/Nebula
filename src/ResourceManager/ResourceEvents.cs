namespace Nebula.ResourceManager;

public class ResourceEvents
{
    public event EventHandler AllTemplatesLoaded;

    public static ResourceEvents Instance;
    
    public void OnAllTemplatesLoaded()
    {
        AllTemplatesLoaded?.Invoke(this, EventArgs.Empty);
        Plugin.logger.LogMessage("Templates loaded invoked!");
    }
}