using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Echo.Composition
{
    public sealed class BootEntryPoint : IStartable
    {
        public void Start()
        {
            Debug.Log("[Boot] Точка входа вызвана");

#if UNITY_EDITOR
            var path = UnityEditor.SessionState.GetString("BootOnPlay.ReturnScene", "");
            if (!string.IsNullOrEmpty(path))
            {
                SceneManager.LoadScene(path, LoadSceneMode.Single);
                return;
            }
#endif
            SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
        }
    }
}
