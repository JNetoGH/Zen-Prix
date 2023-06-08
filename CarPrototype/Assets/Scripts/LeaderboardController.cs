using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    
    
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
        // send the scroll bar to the top
        _scrollbar.value = 1;
    }
    
    public void SubmitScore()
    {
        string label = _memberID.text + $" | ZP={ZenBarController.ZenPoints}";
        LootLockerSDKManager.SubmitScore(label, LapCounter.FinalTime, "zen-prix-lb", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Leaderboard Submit Success");
            }
            else
            {
                Debug.Log("Leaderboard Submit Failed");
            }
        });
        _submitButton.gameObject.SetActive(false);
        LoadScores();
    }
}
