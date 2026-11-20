namespace Nebula.GameSystems;

internal abstract class Loadable
{
    protected Loadable()
    {
        NebulaLoadManager.AddLoadable(this);
    }

    protected void FinishLoad()
    {
        NebulaLoadManager.FinishLoadable(this);
    }
}