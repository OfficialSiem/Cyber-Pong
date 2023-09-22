using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TrackingEveryInput : MonoBehaviour
{
    [SerializeField]
    private string subfolderName = string.Empty;

    [SerializeField]
    private string txtFileName = string.Empty;

    [SerializeField]
    private string locationOfTextFile = string.Empty;

    [SerializeField]
    private PlayerInputActions _playerInputActions = null;


    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        //Create a folder called Streaming Asset Path and a sub folder called
        Directory.CreateDirectory(Application.streamingAssetsPath + $"/{subfolderName}/");
        CreateTextFile();

    }


    public void CreateTextFile()
    {
        //Create the name of the text file
        locationOfTextFile = Application.streamingAssetsPath + $"/{subfolderName}/" + $"{txtFileName}" + ".txt";

        //Check if file name already exists, if it does skip-
        if(!File.Exists(locationOfTextFile)){

            //-otherwise begin writing a header to usage file
            File.WriteAllText(locationOfTextFile, "All Player Inputs Recieved:" + System.Environment.NewLine + System.Environment.NewLine);
        }
    }

    public void LogInputs(InputAction.CallbackContext context)
    {
        //Check if a Player Input System Exsits
        if(_playerInputActions != null)
        {
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
                        if (Keyboard.current.aKey.wasPressedThisFrame)
                        {
                            File.AppendAllText(locationOfTextFile, System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy   hh:mm tt") +
                                $"\tTime (seconds): {Time.realtimeSinceStartup}\t" + $" Button Pressed:Keyboard A Key" + System.Environment.NewLine);
                        }

                        //if s
                        if (Keyboard.current.sKey.wasPressedThisFrame)
                        {
                            File.AppendAllText(locationOfTextFile, System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy   hh:mm tt") +
                                $"\tTime (seconds): {Time.realtimeSinceStartup}\t" + $" Button Pressed:Keyboard S Key" + System.Environment.NewLine);
                        }

                        //if k
                        if (Keyboard.current.kKey.wasPressedThisFrame)
                        {
                            File.AppendAllText(locationOfTextFile, System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy   hh:mm tt") +
                                $"\tTime (seconds): {Time.realtimeSinceStartup}\t" + $" Button Pressed:Keyboard K Key" + System.Environment.NewLine);
                        }

                        //if l
                        if (Keyboard.current.lKey.wasPressedThisFrame)
                        {
                            File.AppendAllText(locationOfTextFile, System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy   hh:mm tt") +
                                $"\tTime (seconds): {Time.realtimeSinceStartup}\t" + $" Button Pressed:Keyboard L Key" + System.Environment.NewLine);
                        }
                        #endregion
                    }
                    
                    if(Gamepad.current != null)
                    {

                    }

                    if(Touchscreen.current != null)
                    {
                        /*
                        _playerInputActions.
                        var touchZero = Touchscreen.current.touches[0]
                        var touchOne = Touchscreen.current.touches[1]
                        
                        if (touchFi) {
                            var touch = Touchscreen.current.touches[0].position;
                            var inWorldPosition = Camera.main.ScreenToWorldPoint(touch);
                            File.AppendAllText(locationOfTextFile, System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy   hh:mm tt") +
                                $"\tTime (seconds): {Time.realtimeSinceStartup}\t" + $" Touch Pressed At Pixel Coordinate:{touch}" + System.Environment.NewLine);
                        }*/


                    }


                }

                //if Touchpad
                //File.AppendAllText(txtDocumentName, $"{Time.realtimeSinceStartup}\t" + "WhereInSpace Pressed\t");
                //if UI Button Pressed
                //File.AppendAllText(txtDocumentName, $"{Time.realtimeSinceStartup}\t" + "UI-Button-Name Pressed\t");
            }
        }
        
        
    }


}
