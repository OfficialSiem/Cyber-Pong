using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MenuTracker : MonoBehaviour
{
    //We're gonna find all the buttons we need
    public GameObject mainMenuFirstButton, selectGameModeFirstButton, 
                        instructionsDisplayFirstButton, submitHighScoreFirstButton;

    public void SwitchToMainMenuFirstButton()
    {
        if (mainMenuFirstButton != null)
        {
            mainMenuFirstButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        }

    }

    public void SwitchToSelectGameModeFirstButton()
    {
        if(selectGameModeFirstButton != null)
        {
            selectGameModeFirstButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(selectGameModeFirstButton);
        }

    }

    public void SwitchToInstructionsDisplayFirstButton()
    {
        if(instructionsDisplayFirstButton != null)
        {
            instructionsDisplayFirstButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(instructionsDisplayFirstButton);
        }

    }

    public void SwitchToSubmitHighScoreFirstButton()
    {
        if(submitHighScoreFirstButton != null)
        {
            submitHighScoreFirstButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(submitHighScoreFirstButton);
        }

    }

}
