using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class PlatformerManager : SingletonPersistent<PlatformerManager>
{
    [SerializeField]
    public bool isThisAPhone = true;
    public override void Awake()
    {
        base.Awake();
        /*if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            //This is a touch screen device
        }
        else
        {
            //We are on a computer
        }*/
    }
}
