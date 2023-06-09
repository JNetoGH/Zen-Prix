using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    
    public float InnerTimer { get; private set; } = 0;
    private bool _relased = false;
    private TextMeshProUGUI _textUI;
    
    public void ResetTimer() => InnerTimer = 0;
    public void ReleaseTimer() => _relased = true;
    public void StopTimer() => _relased = false;



    private void Start()
    {
        ResetTimer();
        _textUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_relased) InnerTimer += Time.deltaTime;
        _textUI.text = $"TIMER: {InnerTimer:F2}s";
    }
}
