using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : GlobalSingleton<MusicPlayer>
{
    [SerializeField] private AudioCollection _music = null;

    public bool Playing { get; private set; } = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoadedEventHandler;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoadedEventHandler;
    }

    protected virtual void SceneLoadedEventHandler(Scene arg0, LoadSceneMode arg1)
    {
        if (!Playing)
        {
            Playing = true;
            StartCoroutine(PlayMusicForever());
        }
    }

    private IEnumerator PlayMusicForever()
    {
        while (true)
        {
            var audioData = _music.PlayRandomClipUnrepeated(Vector3.zero);

            yield return new WaitForSecondsRealtime(audioData.Clip.length);
        }
    }
}
