using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class FixShaderBuildError : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        string projectPath = Path.GetDirectoryName(UnityEngine.Application.dataPath);
        string packageCachePath = Path.Combine(projectPath, "Library", "PackageCache");

        if (Directory.Exists(packageCachePath))
        {
            // Find where the file path should be
            string[] directories = Directory.GetDirectories(packageCachePath, "com.unity.render-pipelines.core*", SearchOption.AllDirectories);

            foreach (string dir in directories)
            {
                string targetDir = Path.Combine(dir, "Editor", "Lighting", "ProbeVolume", "RenderingLayerMask");
                string targetFile = Path.Combine(targetDir, "TraceRenderingLayerMask.urtshader");

                if (Directory.Exists(targetDir))
                {
                    // Create a dummy empty file if it was deleted
                    if (!File.Exists(targetFile))
                    {
                        File.WriteAllText(targetFile, "// Dummy shader file to bypass Unity 6 build bug");
                        UnityEngine.Debug.LogWarning($"[Build Bypass] Created dummy shader file at: {targetFile}");
                    }
                }
            }
        }
    }
}