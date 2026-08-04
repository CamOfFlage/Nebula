namespace Nebula.Patching;

public abstract class PatchHandler
{
    public abstract List<NebulaPatch> patches { get; set; }
}