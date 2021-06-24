using UnityEngine.SceneManagement;

public class SceneLoader : GlobalSingleton<SceneLoader>
{
    private void Start()
    {
        LoadLevelAsync(1);
    }

    public void LoadLevelAsync(int levelNumer)
    {
        SceneManager.LoadSceneAsync($"Level {levelNumer}");
        CameraHolder.Instance.Listener.enabled = false;
    }

    public void LoadMainMenuAsync()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        CameraHolder.Instance.Listener.enabled = true;
    }
}
