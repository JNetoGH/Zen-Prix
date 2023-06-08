using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ZenBarController : MonoBehaviour
{

    public const float SliderDurationInSec = 20;
    public const float DecrementPerHit = 5;
    
    public static int ZenPoints { get; private set; }
    public static float CurrentBonusTime { get; private set; }
    
    [SerializeField] private TextMeshProUGUI _textUI;
    [SerializeField] private Slider _slider;

    
    private void UpdateZenTextUI() => _textUI.text = $"ZEN POINTS: {ZenPoints}";
    
    private void UpdatedZenBar() => _slider.value = CurrentBonusTime;
    
    // can't be smaller than 0, that's why the pedestrians should call this method
    public static void DecrementBonusTime()
    {
        CurrentBonusTime -= DecrementPerHit;
        if (CurrentBonusTime < 0)
            CurrentBonusTime = 0;
    }
    
    void Start()
    {
        ZenPoints = 0;
        CurrentBonusTime = 0;
        _slider.maxValue = SliderDurationInSec;
    }
    
    void Update()
    {
        // é o current time menos oq já passou em blocos de 20 segundos
        CurrentBonusTime += Time.deltaTime;
        if (CurrentBonusTime >= SliderDurationInSec)
        {
            ZenPoints++;
            UpdateZenTextUI();
            CurrentBonusTime = 0;
        }
        UpdatedZenBar();
    }
    
}
