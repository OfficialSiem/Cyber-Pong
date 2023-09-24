using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class TrackingEveryInput : Singleton<TrackingEveryInput>
{
    [SerializeField]
    private string subfolderName = "EveryPressOrKeyPlayerMade";

    [SerializeField]
    private string txtFileName = "usage";

    [SerializeField]
    private string locationOfTextFile = string.Empty;




    private void Start()
    {
        //Create a folder called Streaming Asset Path and a sub folder called
        Directory.CreateDirectory(Application.streamingAssetsPath + $"/{subfolderName}/");
        CreateTextFile();

    }


    public void CreateTextFile()
    {
        //Create the name of the text file
        locationOfTextFile = Application.streamingAssetsPath + $"/{subfolderName}/" + $"{txtFileName}" + ".txt";

        //Check if file name already exists, if it does skip writting a header-
        if(!File.Exists(locationOfTextFile)){

            //-otherwise begin writing a header to using a file
            File.WriteAllText(locationOfTextFile, "All Player Inputs Recieved:" + System.Environment.NewLine + System.Environment.NewLine);
        }
    }

    public void LogKeyboardInputs(InputAction.CallbackContext context)
    {
        //Check if file exists
        if (File.Exists(locationOfTextFile))
        {
            //Going Grandular
            //For Keyboard
                
            //If a button was pressed
            if(context.performed)
            {
                //Check which keyboard button was pressed!
                if (Keyboard.current != null)
                {
                    #region Keyboard Presses
                    CheckWhatKeyboardButtonWasPressed();
                    #endregion
                }

            }

            //if Touchpad
            //File.AppendAllText(txtDocumentName, $"{Time.realtimeSinceStartup}\t" + "WhereInSpace Pressed\t");
            //if UI Button Pressed
            //File.AppendAllText(txtDocumentName, $"{Time.realtimeSinceStartup}\t" + "UI-Button-Name Pressed\t");
        }
        
        
        
    }

    private void CheckWhatKeyboardButtonWasPressed()
    {
        //if a
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() + 
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tButton Pressed:Keyboard A Key" + 
                System.Environment.NewLine);
        }

        //if s
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tButton Pressed:Keyboard S Key" + 
                System.Environment.NewLine);
        }

        //if k
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tButton Pressed:Keyboard K Key" + 
                System.Environment.NewLine);
        }

        //if l
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tButton Pressed:Keyboard L Key" +
                System.Environment.NewLine);
        }
    }

    public void LogScreenPressDown(Vector2 screenPosition)
    {
        Debug.Log("Finger was Pressed Down");
        File.AppendAllText(locationOfTextFile, GetTodayDate() +
            $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
            $"\tFinger Down Location: {screenPosition}"+
            System.Environment.NewLine);
    }

    public void LogScreenLyftUp(Vector2 screenPosition)
    {
        Debug.Log("Finger was Lyfted");
        File.AppendAllText(locationOfTextFile, GetTodayDate() +
            $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
            $"\tFinger Up   Location {screenPosition}" +
            System.Environment.NewLine);
    }

    public void LogScreenPressDown(Vector2 screenPosition, int playerNumber)
    {
        Debug.Log($"P{playerNumber} Finger was Pressed Down");
        File.AppendAllText(locationOfTextFile, GetTodayDate() +
            $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
            $"\tP{playerNumber} Finger Down Location: {screenPosition}" +
            System.Environment.NewLine);
    }

    public void LogScreenLyftUp(Vector2 screenPosition, int playerNumber)
    {
        Debug.Log($"P{playerNumber} was Lyfted");
        File.AppendAllText(locationOfTextFile, GetTodayDate() +
            $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
            $"\tP{playerNumber} Finger Up   Location {screenPosition}" +
            System.Environment.NewLine);
    }

    private string GetTimeSinceGameBootedUp()
    {
        return Time.realtimeSinceStartup.ToString("F5");
    }

    private string GetTodayDate()
    {
        return System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy   hh:mm tt");
    }
}
