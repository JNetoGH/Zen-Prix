using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ZenBarController : MonoBehaviour
{
    
    // released by the lap controller
    public static bool Run;
    
    public const float TimeBonusPerZenPoint = 5f;
    public const float SliderDurationInSec = 15f;
    public const float BarDecrementPerHit = 5f;

    public static int ZenPoints { get; private set; } = 0;
    public static float BarTimer { get; private set; } = 0;
    
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private Slider _slider;

    
    private void UpdateZenTextUI() => _textUI.text = $"ZEN POINTS: {ZenPoints}";
    
    private void UpdatedZenBar() => _slider.value = BarTimer;
    
    // can't be smaller than 0, that's why the pedestrians should call this method
    public static void DecrementBonusTime()
    {
        BarTimer -= BarDecrementPerHit;
        if (BarTimer < 0)
            BarTimer = 0;
    }
    
    void Start()
    {
        Run = false;
        ZenPoints = 0;
        BarTimer = 0;
        _slider.maxValue = SliderDurationInSec;
    }
    
    void Update()
    {
        // used by the lap controller
        if (!Run) return;
        
        // é o current time menos oq já passou em blocos de X segundos
        BarTimer += Time.deltaTime;
        if (BarTimer >= SliderDurationInSec)
        {
            ZenPoints++;
            UpdateZenTextUI();
            BarTimer = 0;
        }
        UpdatedZenBar();
    }
    
}
