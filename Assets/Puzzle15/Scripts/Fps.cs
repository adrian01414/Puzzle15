using TMPro;
using UnityEngine;

public class Fps : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;
    public float updateInterval = 0.5f;

    private float accum = 0f;
    private int frames = 0;
    private float timeleft;

    void Awake()
    {
        timeleft = updateInterval;

        if (fpsText == null && GetComponent<TMP_Text>())
        {
            fpsText = GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        timeleft -= Time.unscaledDeltaTime;
        accum += Time.unscaledDeltaTime;
        frames++;

        if (timeleft <= 0.0f)
        {
            float fps = frames / accum;

            if (fpsText != null)
            {
                fpsText.text = $"FPS: {fps:F2}\nFrame time: {accum / frames * 1000:F2} ms";
            }

            frames = 0;
            accum = 0f;
            timeleft = updateInterval;
        }
    }
}
