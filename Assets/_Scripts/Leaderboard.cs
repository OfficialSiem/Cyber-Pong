using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _listOfNames;

    [SerializeField]
    private List<TextMeshProUGUI> _listOfScores;

    [SerializeField]
    private string subfolderName = "Leaderboard";

    [SerializeField]
    private string txtFileName = "allHighScores";

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
        locationOfTextFile = Application.streamingAssetsPath + $"/{subfolderName}/" + $"{txtFileName}" + ".csv";

        //Check if file name already exists, if it does skip writting a header-
        if (!File.Exists(locationOfTextFile))
        {

            //-otherwise begin writing a header to using a file
            File.WriteAllText(locationOfTextFile, "All Player Inputs Recieved:" + System.Environment.NewLine + System.Environment.NewLine);
        }

        //Create a New Line outside of the header to help seperate sessions
        File.AppendAllText(locationOfTextFile, " " + System.Environment.NewLine);

    }
}
