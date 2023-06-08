using System;
using TMPro;
using UnityEngine;

public class LapCounter : MonoBehaviour
{
    
    public const int TotLaps = 3;
    public int CurrentLap { get; private set; }
    [SerializeField] private GameObject _initialInvisibleWall; 
    [SerializeField] private TextMeshProUGUI _textUI;
    
    [SerializeField] private GameObject _player;
    [SerializeField] private Timer _timer;

    private void UpdateLapUI() =>  _textUI.text = $"LAP: {CurrentLap}/{TotLaps}";
    
    private void StopPlayer() => _player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    
    private void ResetTotLaps()
    {
        CurrentLap = 0;
        UpdateLapUI();
    }

    private void AddLap()
    {
        CurrentLap++;
        
        if (CurrentLap == 1)
        {
            _timer.ReleaseTimer();
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
        float endTime = _timer.InnerTimer;
        Debug.Log($"final time: {endTime}");
    }
    
    void Start()
    {
        ResetTotLaps();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.tag.Equals("Player"))
            return;
        AddLap();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player"))
            return;
        AddLap();
    }
    
    
}
