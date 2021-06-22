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
    }

    public void LoadMainMenuAsync()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
