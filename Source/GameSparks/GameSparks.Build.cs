using UnrealBuildTool;
using System.IO;
using System;

public class GameSparks : ModuleRules
{
    #if WITH_FORWARDED_MODULE_RULES_CTOR
    public GameSparks(ReadOnlyTargetRules Target) : base(Target) // > 4.15
    #else
    public GameSparks(TargetInfo Target) // < 4.15
    #endif
    {
        AddDefinition("GS_BUILDING_MODULE=1");
        AddDefinition("GAMESPARKS_PACKAGE=1");

        PrivateDependencyModuleNames.AddRange(
            new string[] { 
                "Core", 
                "CoreUObject", 
                "Engine", 
                "InputCore",
                "OnlineSubsystemUtils",
                "OnlineSubsystem", // required for OnlineDelegateMacros.h
                "Json",
            }
        );

        PublicIncludePaths.Add(Path.Combine(GameSparksPath, "include"));

        PrivateIncludePaths.AddRange(new string[]{
            Path.Combine(GameSparksPath, "src", "GameSparks"),
            Path.Combine(GameSparksPath, "src", "cjson"),
            Path.Combine(GameSparksPath, "src", "easywsclient"),
            Path.Combine(GameSparksPath, "src", "google"),
            Path.Combine(GameSparksPath, "src", "mbedtls"),
            Path.Combine(GameSparksPath, "src")
        });

        PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		
		if (Target.Platform == UnrealTargetPlatform.Android)
        {
            string PluginPath = Path.GetFullPath(ModulePath);

            //#if UE_4_19_OR_LATER
            //            RuntimeDependencies.Add(Path.Combine(PluginPath, "GameSparks_APL.xml"));
            //#else
            AdditionalPropertiesForReceipt.Add("AndroidPlugin", Path.Combine(PluginPath, "GameSparks_APL.xml"));
            //#endif
        }

        PublicIncludePathModuleNames.Add("GameSparks");
    }

    private void AddDefinition(string def)
    {
        #if UE_4_19_OR_LATER
            PublicDefinitions.Add(def);
        #else
            Definitions.Add(def);
        #endif
    }

    private string ModulePath
    {
        get { return ModuleDirectory; }
    }

    private string GameSparksPath
    {
        get { return Path.GetFullPath(Path.Combine(ModulePath, "..", "GameSparksBaseSDK")); }
    }
}
