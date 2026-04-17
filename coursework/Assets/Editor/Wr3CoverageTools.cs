using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.SettingsManagement;

namespace TacticalCards.Editor
{
    [InitializeOnLoad]
    public static class Wr3CoverageBootstrap
    {
        private const string SessionKey = "TacticalCards.WR3CoverageBootstrapped";

        static Wr3CoverageBootstrap()
        {
            if (SessionState.GetBool(SessionKey, false))
            {
                return;
            }

            SessionState.SetBool(SessionKey, true);
            Wr3CoverageTools.ApplyCoverageSettings();
            CompilationPipeline.RequestScriptCompilation();
            UnityEngine.Debug.Log("[WR3] Coverage bootstrap applied. Requested one extra script compilation so Code Coverage can initialize with the new settings.");
        }
    }

    public static class Wr3CoverageTools
    {
        // WR3 keeps coverage output under versioned project-record paths for report traceability.
        private const string PackageName = "com.unity.testtools.codecoverage";
        private const string CoverageRoot = "F:/unity/Unity-Project-clean-init/doc/src/project-records/testing/coverage/2026-04-17";
        private const string IncludedAssemblies = "Assembly-CSharp";
        private const string ExcludedPaths = "**/Assets/Tests/**";

        [MenuItem("Tools/WR3/Configure Code Coverage")]
        public static void ConfigureCodeCoverage()
        {
            ApplyCoverageSettings();

            CompilationPipeline.RequestScriptCompilation();
            UnityEngine.Debug.Log("[WR3] Code coverage settings configured. Script compilation requested.");
        }

        [MenuItem("Tools/WR3/Disable Code Coverage")]
        public static void DisableCodeCoverage()
        {
            Settings settings = new Settings(PackageName);
            settings.Set("EnableCodeCoverage", false, SettingsScope.Project);
            settings.Save();

            CompilationPipeline.RequestScriptCompilation();
            UnityEngine.Debug.Log("[WR3] Code coverage disabled. Script compilation requested.");
        }

        public static void ApplyCoverageSettings()
        {
            Settings settings = new Settings(PackageName);

            settings.Set("EnableCodeCoverage", true, SettingsScope.Project);
            settings.Set("Path", CoverageRoot, SettingsScope.Project);
            settings.Set("HistoryPath", CoverageRoot, SettingsScope.Project);
            settings.Set("GenerateAdditionalMetrics", true, SettingsScope.Project);
            settings.Set("GenerateTestReferences", true, SettingsScope.Project);
            settings.Set("IncludeHistoryInReport", true, SettingsScope.Project);
            settings.Set("IncludeAssemblies", IncludedAssemblies, SettingsScope.Project);
            settings.Set("PathsToInclude", string.Empty, SettingsScope.Project);
            settings.Set("PathsToExclude", ExcludedPaths, SettingsScope.Project);
            settings.Set("VerbosityLevel", 1, SettingsScope.Project);
            settings.Set("GenerateHTMLReport", true, SettingsScope.Project);
            settings.Set("GenerateAdditionalReports", true, SettingsScope.Project);
            settings.Set("GenerateBadge", true, SettingsScope.Project);
            settings.Set("AutoGenerateReport", true, SettingsScope.Project);
            settings.Set("OpenReportWhenGenerated", false, SettingsScope.Project);
            settings.Save();
        }
    }
}
