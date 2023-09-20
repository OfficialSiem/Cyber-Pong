using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int Index { get; private set; }

    private string confirmButton;

    private void Start()
    {
        Index = 1;
        confirmButton = "";
    }
}
