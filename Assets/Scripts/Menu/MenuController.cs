using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private enum MenuState
    {
        OutOfInfo,
        Info,
    }
    
    [SerializeField] private GameObject _outOfInfoMenu;
    [SerializeField] private GameObject _infoMenu;
    [SerializeField] private MenuState _currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentState = MenuState.OutOfInfo;
    }

    // Update is called once per frame
    void Update()
    {

        if (_currentState == MenuState.OutOfInfo)
        {
            _infoMenu.SetActive(false);
            _outOfInfoMenu.SetActive(true);
        }
        else
        {
            _infoMenu.SetActive(true);
            _outOfInfoMenu.SetActive(false);
        }
        
    }

    public void SetToOutOfInfoMode()
    {
        _currentState = MenuState.OutOfInfo;
    }

    
    public void SetToInfoMode()
    {
        _currentState = MenuState.Info;
    }
    
}
