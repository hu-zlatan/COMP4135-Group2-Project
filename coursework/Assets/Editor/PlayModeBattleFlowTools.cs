using UnityEditor;
using UnityEngine;

namespace TacticalCards.Editor
{
    public static class PlayModeBattleFlowTools
    {
        [MenuItem("Tools/Tactical Cards/Testing/Start Active Battle Flow")]
        public static void StartActiveBattleFlow()
        {
            if (!EditorApplication.isPlaying)
            {
                Debug.LogWarning("[Testing] Enter Play Mode before starting the battle flow.");
                return;
            }

            var flowController = Object.FindFirstObjectByType<GameFlowController>();
            if (flowController == null)
            {
                Debug.LogWarning("[Testing] No GameFlowController found in the active scene.");
                return;
            }

            flowController.StartGame();
            Debug.Log("[Testing] Triggered GameFlowController.StartGame() from editor menu.");
        }

        [MenuItem("Tools/Tactical Cards/Testing/Return To Title Flow")]
        public static void ReturnToTitleFlow()
        {
            if (!EditorApplication.isPlaying)
            {
                Debug.LogWarning("[Testing] Enter Play Mode before returning to the title flow.");
                return;
            }

            var flowController = Object.FindFirstObjectByType<GameFlowController>();
            if (flowController == null)
            {
                Debug.LogWarning("[Testing] No GameFlowController found in the active scene.");
                return;
            }

            flowController.ReturnToMenu();
            Debug.Log("[Testing] Triggered GameFlowController.ReturnToMenu() from editor menu.");
        }
    }
}
