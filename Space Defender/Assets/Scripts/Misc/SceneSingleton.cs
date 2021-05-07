using UnityEngine;

public abstract class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; } = null;

    protected virtual void Awake()
    {
        SetupInstance();
    }

    private void SetupInstance()
    {
        if (Instance == null) Instance = (T)(MonoBehaviour)this;
        else if (Instance != null) Destroy(gameObject);
    }
}
