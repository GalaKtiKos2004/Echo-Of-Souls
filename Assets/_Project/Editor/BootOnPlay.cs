using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Echo.Editor
{
    [InitializeOnLoad]
    public static class BootOnPlay
    {
        private const string BootScenePath  = "Assets/_Project/Content/Scenes/BootScene.unity";
        private const string ReturnSceneKey = "BootOnPlay.ReturnScene";
        private const string EnabledKey     = "BootOnPlay.Enabled";
        private const string MenuPath       = "Tools/Boot On Play";

        static BootOnPlay()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            EditorApplication.playModeStateChanged += OnPlayModeChanged;

            // AssetDatabase на момент статического конструктора
            // может быть ещё не готова — откладываем на следующий тик
            EditorApplication.delayCall += Apply;
        }

        private static void Apply()
        {
            if (!EditorPrefs.GetBool(EnabledKey, true))
            {
                EditorSceneManager.playModeStartScene = null;
                return;
            }

            var boot = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootScenePath);
            if (boot == null)
            {
                Debug.LogWarning($"[BootOnPlay] Не найдена сцена: {BootScenePath}");
                return;
            }

            EditorSceneManager.playModeStartScene = boot;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode) return;
            if (!EditorPrefs.GetBool(EnabledKey, true)) return;

            // Только чтение — это разрешено
            var current = SceneManager.GetActiveScene().path;
            SessionState.SetString(
                ReturnSceneKey,
                current == BootScenePath ? string.Empty : current);
        }

        [MenuItem(MenuPath)]
        private static void Toggle()
        {
            EditorPrefs.SetBool(EnabledKey, !EditorPrefs.GetBool(EnabledKey, true));
            Apply();
        }

        [MenuItem(MenuPath, isValidateFunction: true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked(MenuPath, EditorPrefs.GetBool(EnabledKey, true));
            return true;
        }
    }
}