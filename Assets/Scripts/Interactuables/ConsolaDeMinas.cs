using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConsolaDeMinas : MonoBehaviour, IInteractable
{
    [SerializeField]private Outline outline;
    [SerializeField]private CinemachineVirtualCamera vCam;
    public void Desmarcar()
    {
        outline.enabled = false;
    }

    public void Resaltar()
    {
        outline.enabled = true;
    }

    public void Interact()
    {
        //cambio proridad de camaras
        GameObject.Find("Cameras Manager").GetComponent<CameraManager>().ChangePriority(vCam);
        //activo el menu de crafteo
        GameObject.Find("Mine Manager").GetComponent<MineManager>().ShowMenu();
    }
    
    public void Salir()
    {
        GameObject.Find("Mine Manager").GetComponent<MineManager>().HideMenu();
    }
}