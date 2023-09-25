using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-50)]
public class TrackingEveryInput : SingletonPersistent<TrackingEveryInput>
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

        //Create a New Line outside of the header to help seperate sessions
        File.AppendAllText(locationOfTextFile, " " + System.Environment.NewLine);

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

    //Omg there's only one way to do this, brute force, see https://gamedevdustin.medium.com/the-any-key-problem-with-the-new-input-system-for-simultaneous-input-detection-in-unity-2023-1-e436226d4818
    private void CheckWhatKeyboardButtonWasPressed()
    {
        //if a
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() + 
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tKey Pressed: Keyboard A" + 
                System.Environment.NewLine);
        }

        //if s
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tKey Pressed: Keyboard S" + 
                System.Environment.NewLine);
        }

        //if k
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tKey Pressed: Keyboard K" + 
                System.Environment.NewLine);
        }

        //if l
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" + 
                $"\tKey Pressed: Keyboard L" +
                System.Environment.NewLine);
        }

        //if q
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard Q" +
                System.Environment.NewLine);
        }

        //if w
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard W" +
                System.Environment.NewLine);
        }

        //if e
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard E" +
                System.Environment.NewLine);
        }

        //if r
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard R" +
                System.Environment.NewLine);
        }

        //if t
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard R" +
                System.Environment.NewLine);
        }

        //if q
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard Q" +
                System.Environment.NewLine);
        }

        //if w
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard W" +
                System.Environment.NewLine);
        }

        //if e
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard E" +
                System.Environment.NewLine);
        }

        //if r
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard R" +
                System.Environment.NewLine);
        }

        //if t
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard T" +
                System.Environment.NewLine);
        }

        //if y
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard Y" +
                System.Environment.NewLine);
        }

        //if u
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard U" +
                System.Environment.NewLine);
        }


        //if i
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard I" +
                System.Environment.NewLine);
        }

        //if o
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard O" +
                System.Environment.NewLine);
        }

        //if p
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard P" +
                System.Environment.NewLine);
        }

        //if d
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard D" +
                System.Environment.NewLine);
        }

        //if f
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard F" +
                System.Environment.NewLine);
        }

        //if g
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard G" +
                System.Environment.NewLine);
        }

        //if h
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard H" +
                System.Environment.NewLine);
        }

        //if j
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard J" +
                System.Environment.NewLine);
        }

        //if z
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard Z" +
                System.Environment.NewLine);
        }

        //if x
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard X" +
                System.Environment.NewLine);
        }

        //if c
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard C" +
                System.Environment.NewLine);
        }

        //if v
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard V" +
                System.Environment.NewLine);
        }

        //if b
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard B" +
                System.Environment.NewLine);
        }

        //if n
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard N" +
                System.Environment.NewLine);
        }

        //if m
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard M" +
                System.Environment.NewLine);
        }

        //if space
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard Space" +
                System.Environment.NewLine);
        }

        //if enter
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            File.AppendAllText(locationOfTextFile, GetTodayDate() +
                $"\tTime (seconds): {GetTimeSinceGameBootedUp()}" +
                $"\tKey Pressed: Keyboard Enter" +
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
