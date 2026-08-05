namespace Nebula.Patching;

public abstract class PatchHandler
{
    public List<NebulaPatch> patches { get; set; } = new List<NebulaPatch>();
}