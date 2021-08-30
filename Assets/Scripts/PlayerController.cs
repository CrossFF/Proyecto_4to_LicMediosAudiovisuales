using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public CharacterController characterController;
    public Animator animator;
    private IInteractable interactuable; // objeto interactuable
    public CinemachineVirtualCamera camPJ;// camara del personaje
    private CameraManager cameraManager;// manager de las camaras

    void Start()
    {
        cameraManager = GameObject.Find("Cameras Manager").GetComponent<CameraManager>();
        cameraManager.ChangePriority(camPJ);
    }

    void Update()
    {
        Interactuar();
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 fixedSpeed = new Vector3(x, 0f, z);
        fixedSpeed = fixedSpeed * speed * Time.deltaTime;
        // si estoy enfocando al personaje
        if (camPJ.Priority > 0)
        {
            // muevo el personaje
            characterController.Move(fixedSpeed);
            //caminata
            if (fixedSpeed != Vector3.zero)
            {
                animator.SetFloat("speed", 1);
                animator.transform.forward = fixedSpeed;
            }
            else
            {
                animator.SetFloat("speed", 0);
            }
        }
    }

    private void Interactuar()
    {
        // interactuar
        if (interactuable != null)
        {
            if (Input.GetButtonDown("Interactuar") && camPJ.Priority > 0)
            {
                interactuable.Interact();
            }
            // salir de la seccion actual
            if (Input.GetButtonDown("Salir") && camPJ.Priority == 0)
            {
                SalirInteractuable();
            }
        }
    }

    public void SalirInteractuable()
    {
        cameraManager.ChangePriority(camPJ);
        interactuable.Salir();
        interactuable = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        interactuable = other.GetComponent<IInteractable>();
        if (interactuable != null)
        {
            interactuable.Resaltar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactuable = other.GetComponent<IInteractable>();
        if (interactuable != null)
        {
            interactuable.Desmarcar();
            interactuable = null;
        }
    }
}
