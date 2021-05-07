using System.Collections;
using UnityEngine;

public class BackgroundScroller : SceneSingleton<BackgroundScroller>
{
    public const float ScrollSpeedMax = 0.01f;

    public const float ScrollDurationMin = 4f;
    public const float ScrollDurationMax = 64f;

    public const float ScrollLerpDurationMin = 2f;
    public const float ScrollLerpDurationMax = 16f;

    [SerializeField] private float _horizontalScrollSpeed = 0f;
    [SerializeField] private float _horizontalScrollSpeedRandom = 0f;
    [SerializeField] private float _horizontalScrollDuration = ScrollDurationMin;
    [SerializeField] private float _horizontalScrollDurationRandom = 0f;

    [SerializeField] private float _verticalScrollSpeed = 0f;
    [SerializeField] private float _verticalScrollSpeedRandom = 0f;
    [SerializeField] private float _verticalScrollDuration = ScrollDurationMin;
    [SerializeField] private float _verticalScrollDurationRandom = 0f;

    [SerializeField] private float _scrollLerpDuration = ScrollLerpDurationMin;
    [SerializeField] private float _ScrollLerpDurationRandom = 0f;

    private Renderer _renderer = null;

    private Coroutine _hScrollCoroutine = null;
    private Coroutine _vScrollCoroutine = null;

    private float _oldHorizontalScrollSpeed = 0f;
    private float _oldVerticalScrollSpeed = 0f;

    private bool _firstHorizontalScroll = true;
    private bool _firstVerticalScroll = true;

    public float HorizontalScrollSpeed => AuxMath.Randomize(_horizontalScrollSpeed, _horizontalScrollSpeedRandom);
    public float HorizontalScrollDuration => AuxMath.Randomize(_horizontalScrollDuration, _horizontalScrollDurationRandom);
    public float VerticalScrollSpeed => AuxMath.Randomize(_verticalScrollSpeed, _verticalScrollSpeedRandom);
    public float VerticalScrollDuration => AuxMath.Randomize(_verticalScrollDuration, _verticalScrollDurationRandom);

    protected override void Awake()
    {
        base.Awake();

        _renderer = FindRenderer();
    }

    private void Start()
    {
        SetMainTextureRandomOffset();
        StartCoroutine(ScrollEngine());
    }

    private Renderer FindRenderer()
    {
        Renderer renderer = null;

        if (gameObject.TryGetComponent(out Renderer r)) renderer = r;

        return renderer;
    }

    private void SetMainTextureRandomOffset()
    {
        float xOffset = Random.Range(0f, 1f);
        float yOffset = Random.Range(0f, 1f);

        _renderer.material.mainTextureOffset = new Vector2(xOffset, yOffset);
    }

    private IEnumerator ScrollEngine()
    {
        while (true)
        {
            if (_hScrollCoroutine == null) _hScrollCoroutine = StartCoroutine(HorizontalScroller());
            if (_vScrollCoroutine == null) _vScrollCoroutine = StartCoroutine(VerticalScroller());

            yield return null;
        }
    }

    private IEnumerator HorizontalScroller()
    {
        float duration = HorizontalScrollDuration;
        float newScrollSpeed = HorizontalScrollSpeed * AuxMath.RandomSign;
        float timer = 0f;

        float currentScrollSpeed;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            if (_firstHorizontalScroll) currentScrollSpeed = newScrollSpeed;
            else currentScrollSpeed = Mathf.Lerp(_oldHorizontalScrollSpeed, newScrollSpeed, timer / duration);

            _renderer.material.mainTextureOffset += new Vector2(currentScrollSpeed * Time.deltaTime, 0f);

            yield return null;
        }

        if (_firstHorizontalScroll) _firstHorizontalScroll = false;

        _oldHorizontalScrollSpeed = newScrollSpeed;
        _hScrollCoroutine = null;
    }

    private IEnumerator VerticalScroller()
    {
        float duration = VerticalScrollDuration;
        float newScrollSpeed = VerticalScrollSpeed; 
        float timer = 0f;

        float currentScrollSpeed;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            if (_firstVerticalScroll) currentScrollSpeed = newScrollSpeed;
            else currentScrollSpeed = Mathf.Lerp(_oldVerticalScrollSpeed, newScrollSpeed, timer / duration);

            _renderer.material.mainTextureOffset += new Vector2(0f, currentScrollSpeed * Time.deltaTime);

            yield return null;
        }

        if (_firstVerticalScroll) _firstVerticalScroll = false;

        _oldVerticalScrollSpeed = newScrollSpeed;
        _vScrollCoroutine = null;
    }
}
