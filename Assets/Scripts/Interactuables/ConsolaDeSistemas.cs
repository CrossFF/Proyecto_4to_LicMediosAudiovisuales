using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsolaDeSistemas : MonoBehaviour, IInteractable
{
    public Outline outline;
    public MechaManager manager;

    public void Desmarcar()
    {
        // desmarco el reborde
        outline.enabled = false;
    }

    public void Interact()
    {
        manager.ShowSystemMenu();
        //sonido de activacion
        GameObject.Find("Manager de Sonido").GetComponent<SonidoManager>().PlayUISound(EventoSonoroUI.AbrirConsola);
    }

    public void Resaltar()
    {
        // marco el reborde
        outline.enabled = true;
    }

    public void Salir()
    {
        // cierro la consola
        manager.HideMenu();
        //sonido de desactivacion
        GameObject.Find("Manager de Sonido").GetComponent<SonidoManager>().PlayUISound(EventoSonoroUI.CerrarConsola);
    }
}
