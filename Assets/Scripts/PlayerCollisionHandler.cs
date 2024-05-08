using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollisionHandler : MonoBehaviour
{
    CameraSwitcher myCameraSwitcher;
    // Start is called before the first frame update
    void Start()
    {
        myCameraSwitcher = GetComponent<CameraSwitcher>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("room"))
        {
            myCameraSwitcher.ActiavteLeftCam();
        }

        if (other.gameObject.CompareTag("room left"))
        {
            myCameraSwitcher.ActiavteRightCam();
        }

        if (other.gameObject.CompareTag("Reception area"))
        {
            Debug.Log("Player entered reception area");
            ReceptionManager.instance.StartTimer();
            ReceptionManager.instance.SetPlayerInReceptionArea(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("room"))
        {
            myCameraSwitcher.ActiavteMainCam();
        }

        if (other.gameObject.CompareTag("room left"))
        {
            myCameraSwitcher.ActiavteMainCam();
        }

        if (other.gameObject.CompareTag("Reception area"))
        {
            Debug.Log("Player got out of reception area");
            ReceptionManager.instance.StopTimer();
            ReceptionManager.instance.SetPlayerInReceptionArea(false);

        }
    }


}
