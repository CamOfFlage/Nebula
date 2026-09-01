# Nebula (A Worldless modding library)

### Features:
- Management for accessing the EVA system
- Monitoring loading combat templates
- Access to individual enemies
- Tools for projectile management

### Getting Started
- Download BepInEx
- Launch the game once
- Go to the BepInEx folders, the required game libraries are in core and interop
- Add a dependency to ``` Nebula.dll ``` (Found in the "Releases" section of the github page)
- Add ``` [BepInDependency("com.CamOfFlage.Nebula")] ``` to your plugin's base class
- That's it!

### Examples
Note: These examples just serve as a basic reference, and will be removed once the example project has been set up
``` C#
public class SeraphPatches : GlobalCombatTemplatePatch  
{
  public override void Patch() { }  
  public override string templateId { get; } = "Seraph";  
  public override string patchId { get; } = "SeraphPatch";  
  public override void Patch(CombatTemplate combatTemplate)  
	 {  
		 EvaListener listener = combatTemplate.enemy._fighter.model.gameObject.GetComponent<EvaListener>();
		 EvaTrackFetcher fetcher = new EvaTrackFetcher();  
  
	  EvaTrack attack01Track = fetcher.FetchTrack(listener, "seraph", "seraph_attack01", 1);  
	  ProjectileClip projectileClip = attack01Track.clips[0].TryCast<ProjectileClip>();  
	  AddressableKey addressableKey = projectileClip.addressableKey;  
	  ProjectileFetcher projectileFetcher = new ProjectileFetcher();  
	  GameObject projectileObject = projectileFetcher.fetchProjectile(addressableKey);  
	  EvaTrack projectileTrack = fetcher.FetchTrack(projectileObject.GetComponent<EvaListener>(), "seraph_attack01_laser", "adaptation_dark_laser_weak", 0);  
	  Hit projectileHit = projectileTrack.clips[0].TryCast<HitClip>().hit;  
	  projectileHit.damage = 6;  
	  projectileHit.stagger = 2;

	  EvaTrack attack02Track = fetcher.FetchTrack(listener, "seraph", "seraph_attack02", 1);  
	  Il2CppSystem.Collections.Generic.List<EvaClip> hitClips = attack02Track.clips;  
	  foreach (EvaClip clip in hitClips)  
	  {
		 HitClip hitClip = clip.TryCast<HitClip>();  
		 Hit hit = hitClip.hit;  
		 hit.damage = 6;  
		 hit.stagger = 2;  
	  }
	

	//Blocks
	BlocksHandler blocksHandler = combatTemplate.enemy.blocksHandler;
    BlockSet blockSet01 = new BlockSet();
    blockSet01.blockDefs = new Il2CppReferenceArray<BlockDef>(2);
    blockSet01.blockDefs[0] = new BlockDef(BlockType.PhysicalAny, 10);
    blockSet01.blockDefs[1] = new BlockDef(BlockType.Fire, 10);
    BlockSet blockSet02 = new BlockSet();
    blockSet02.blockDefs = new Il2CppReferenceArray<BlockDef>(2);
    blockSet02.blockDefs[0] = new BlockDef(BlockType.PhysicalAny, 10);
    blockSet02.blockDefs[1] = new BlockDef(BlockType.Wind, 10);
    blocksHandler._blockSets[0] = blockSet01;
    blocksHandler._blockSets[1] = blockSet02;
    }  
}
```
```C#
[BepInPlugin("com.CamOfFlage.NebulaTest", "NebulaTest", "1.0.0")]  
[BepInDependency("com.CamOfFlage.Nebula")]  
public class Plugin : BasePlugin  
{  
  public override void Load()  
 {  
	 SeraphPatches seraphPatches = new SeraphPatches();
	 seraphPatches.Register();    
  }  
}
```

## Bug Reporting
All official bug reports should be handled through the GitHub issues system. Try to make sure that the issue is with Nebula itself, and not from either your own code or the game.

Any suggestions should either be sent as a "Enhancement" through issues. Alternatively, you could send suggestions through the Discord modding channel. 

Any personal usage issues or confusion are not bugs, and should not be reported as such. Instead, ask for help in the Discord and feel free to update the documentation later!
