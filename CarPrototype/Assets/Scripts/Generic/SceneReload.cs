using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReload : MonoBehaviour
{

    [SerializeField] private int _sceneIndex = 0;
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
