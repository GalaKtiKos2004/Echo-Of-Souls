using UnityEngine;
using UnityEngine.SceneManagement;

namespace Echo.Composition
{
    /// Временная панель для проверки жизненного цикла областей.
    /// В спринте 10 её заменит дебаг-консоль.
    public sealed class ScopeDebugPanel : MonoBehaviour
    {
        private const string GameplayScene = "GameplayScene";
        private const string RegionScene = "RegionScene";
        private const string BootScene = "BootScene";

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 260, 200), GUI.skin.box);

            if (GUILayout.Button("Загрузить регион (аддитивно)"))
                SceneManager.LoadSceneAsync(RegionScene, LoadSceneMode.Additive);

            if (GUILayout.Button("Выгрузить регион"))
                SceneManager.UnloadSceneAsync(RegionScene);

            if (GUILayout.Button("Завершить прохождение"))
                SceneManager.LoadSceneAsync(BootScene, LoadSceneMode.Single);

            if (GUILayout.Button("Начать прохождение"))
                SceneManager.LoadSceneAsync(GameplayScene, LoadSceneMode.Single);

            GUILayout.EndArea();
        }
    }
}
