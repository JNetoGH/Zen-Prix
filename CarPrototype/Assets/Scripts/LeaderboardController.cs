using System;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{

    private bool scolledUpOnce = false;
    private const int NameMaxLength = 12;
    [SerializeField] private int _maxScores = 1000;
    [SerializeField] private TMP_InputField _memberID;
    [SerializeField] private Button _submitButton;
    [SerializeField] private GameObject _leaderboardContentContainer;
    [SerializeField] private GameObject _leaderboardCell;
    [SerializeField] private Scrollbar _scrollbar;
    
    private void Start()
    {
        LootLockerSDKManager.StartGuestSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Leaderboard Start Success");
                LoadScores();
            }
            else
            {
                Debug.Log("Leaderboard Start Failed");
            }
        });
    }
    
    public void ClearLeaderboard()
    {
        for (int i = 0; i < _leaderboardContentContainer.transform.childCount; i++)
        {
            Destroy(_leaderboardContentContainer.transform.GetChild(i).gameObject);
        }
    }
    
    public void LoadScores()
    {
        ClearLeaderboard();
        LootLockerSDKManager.GetScoreList("zen-prix-lb", _maxScores, (response) =>
        {
            if (response.success)
            {
                Debug.Log($"Leaderboard Loading Success tot members {response.items.Length}");
                
                LootLockerLeaderboardMember[] members = response.items;
                foreach (LootLockerLeaderboardMember m in members)
                {
                    GameObject newCell = Instantiate(_leaderboardCell, _leaderboardContentContainer.transform);
                    newCell.GetComponent<TextMeshProUGUI>().text =
                        $"{m.rank}) {m.member_id}: {m.score}s";
                }
            }
            else
            {
                Debug.Log("Leaderboard Loading Failed");
            }
        });
    }
    
    public void SubmitScore()
    {
        string playerName = _memberID.text.Equals(string.Empty) || _memberID.text is null ? "Unnamed" : _memberID.text;
        if (playerName.Length > NameMaxLength)
            playerName = playerName.Substring(0, NameMaxLength);
        string label = $"{playerName} ({ DateTime.UtcNow.ToString("d/M/yy-HH:m:s") })";
        LootLockerSDKManager.SubmitScore(label, LapCounter.FinalTime, "zen-prix-lb", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Leaderboard Submit Success");
                LoadScores();
            }
            else
            {
                Debug.Log("Leaderboard Submit Failed");
            }
        });
        _submitButton.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (scolledUpOnce) return;
        scolledUpOnce = true;
        // send the scroll bar to the top
        _scrollbar.value = 1;
    }
}
