using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera leftCamera;
    [SerializeField] CinemachineVirtualCamera rightCamera;


    // Start is called before the first frame update
    void Start()
    {
        ActiavteMainCam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiavteMainCam()
    {
        mainCamera.gameObject.SetActive(true);
        leftCamera.gameObject.SetActive(false);
        rightCamera.gameObject.SetActive(false);
    }

    public void ActiavteLeftCam()
    {
        mainCamera.gameObject.SetActive(false);
        leftCamera.gameObject.SetActive(true);
        rightCamera.gameObject.SetActive(false);
    }

    public void ActiavteRightCam()
    {
        mainCamera.gameObject.SetActive(false);
        leftCamera.gameObject.SetActive(false);
        rightCamera.gameObject.SetActive(true);
    }

}
