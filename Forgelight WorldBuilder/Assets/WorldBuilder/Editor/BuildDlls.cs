#if UNITY_STANDALONE
namespace WorldBuilder.Editor
{
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;

    public class BuildDlls : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;
        public void OnPostprocessBuild(BuildReport report)
        {
            BuildTarget target = report.summary.platform;
            string pathToBuiltProject = report.summary.outputPath;

            switch (target)
            {
                case BuildTarget.StandaloneWindows64:
                    FileUtil.ReplaceFile("lzham-forgelightx64.dll", System.IO.Path.GetDirectoryName(pathToBuiltProject) + "/lzham-forgelightx64.dll");
                    break;
            }
        }
    }
}
#endif