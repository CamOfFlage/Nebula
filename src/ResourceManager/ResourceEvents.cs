namespace WorldlessLibs.ResourceManager;

public class ResourceEvents
{
    public event EventHandler AllTemplatesLoaded;

    public static ResourceEvents Instance;
    
    public virtual void OnAllTemplatesLoaded()
    {
        AllTemplatesLoaded?.Invoke(this, EventArgs.Empty);
    }
}