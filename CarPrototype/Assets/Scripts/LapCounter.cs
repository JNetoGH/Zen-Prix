using TMPro;
using UnityEngine;

public class LapCounter : MonoBehaviour
{

    public static int FinalTime { get; private set; } = 0;
    
    public const int TotLaps = 3;
    public int CurrentLap { get; private set; }
    
    [SerializeField] private GameObject _initialInvisibleWall; 
    [SerializeField] private TextMeshProUGUI _textUI;
    
    [SerializeField] private GameObject _player;
    [SerializeField] private Timer _timer;
    
    [SerializeField] private GameObject _lastScreen;
    [SerializeField] private TextMeshProUGUI _lastScreenMainText;
    [SerializeField] private TextMeshProUGUI _lastScreenDescriptionText;

    private void UpdateLapUI() =>  _textUI.text = $"LAP: {CurrentLap}/{TotLaps}";
    
    private void StopPlayer() => _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    
    private void ResetTotLaps()
    {
        FinalTime = 0;
        CurrentLap = 0;
        UpdateLapUI();
    }

    private void AddLap()
    {
        CurrentLap++;
        
        if (CurrentLap == 1)
        {
            _timer.ReleaseTimer();
            ZenBarController.Run = true;
            if (_initialInvisibleWall is not null)
                Destroy(_initialInvisibleWall);
        }

        bool isLastLap = CurrentLap == TotLaps + 1;
        if (isLastLap)
            EndSequence();
        else 
            UpdateLapUI();
    }

    private void EndSequence()
    {
        Invoke(nameof(StopPlayer), 0.18f);
        _timer.StopTimer();
        ZenBarController.Run = false;
        float rawTime = _timer.InnerTimer;
        int totZenPoints = ZenBarController.ZenPoints;
        float timeBonusPerZenPoint = ZenBarController.TimeBonusPerZenPoint;
        FinalTime = (int)(rawTime - totZenPoints * timeBonusPerZenPoint);
        _lastScreenMainText.text = $"Final Time: {FinalTime:F0}s";
        _lastScreenDescriptionText.text = $"Raw Time = {rawTime:F0}s\n" +
                                          $"Zen Points = {totZenPoints}\n" +
                                          $"Discount = {totZenPoints * timeBonusPerZenPoint:F0}s ({totZenPoints} * {timeBonusPerZenPoint:F0}s)";
        _lastScreen.SetActive(true);
        Debug.Log($"final time: {rawTime}");
    }
    
    void Start()
    {
        ResetTotLaps();
        _lastScreen.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player"))
            return;
        AddLap();
    }
    
}
