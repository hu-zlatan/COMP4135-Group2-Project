using UnityEditor;
using UnityEditor.Compilation;

namespace TacticalCards.Editor
{
    [InitializeOnLoad]
    public static class TestDebugOptimizationBootstrap
    {
        private const string AutoDebugOptimizationKey = "TacticalCards.AutoDebugOptimization";

        static TestDebugOptimizationBootstrap()
        {
            EditorApplication.delayCall += EnsureConfigured;
        }

        [MenuItem("Tools/Tactical Cards/Testing/Enable Auto Debug Optimization")]
        public static void EnableAutoDebugOptimization()
        {
            EditorPrefs.SetBool(AutoDebugOptimizationKey, true);
            EnsureConfigured();
        }

        [MenuItem("Tools/Tactical Cards/Testing/Disable Auto Debug Optimization")]
        public static void DisableAutoDebugOptimization()
        {
            EditorPrefs.SetBool(AutoDebugOptimizationKey, false);
            UnityEngine.Debug.Log("[Testing] Auto debug optimization disabled for this editor profile.");
        }

        [MenuItem("Tools/Tactical Cards/Testing/Force Debug Optimization Now")]
        public static void ForceDebugOptimizationNow()
        {
            SetDebugOptimization();
        }

        private static void EnsureConfigured()
        {
            if (!EditorPrefs.GetBool(AutoDebugOptimizationKey, true))
            {
                return;
            }

            SetDebugOptimization();
        }

        private static void SetDebugOptimization()
        {
            if (CompilationPipeline.codeOptimization == CodeOptimization.Debug)
            {
                return;
            }

            CompilationPipeline.codeOptimization = CodeOptimization.Debug;
            UnityEngine.Debug.Log("[Testing] Switched Unity code optimization to Debug automatically for test and coverage runs.");
        }
    }
}
