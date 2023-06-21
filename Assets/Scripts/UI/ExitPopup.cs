using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPopup : MonoBehaviour
{
    public void OnNoButton()
    {
        GameMenu.isPaused = false; //we created a static variable isPaused which is why we can change the value
        gameObject.SetActive(false);
    }
    public void OnYesButton()
    {
        //we want to leave the game
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
