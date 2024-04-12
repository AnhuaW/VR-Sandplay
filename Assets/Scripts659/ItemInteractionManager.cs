using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractionManager : MonoBehaviour
{
    [SerializeField]
    private bool isOnShelf = true;
    public DistanceGrabInteractable distanceGrab;
    public DistanceHandGrabInteractable distanceGrab2;
    public DistanceHandGrabInteractable distanceGrab3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isOnShelf)
        {
            toggle(true);
        }
        else
        {
            toggle(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Shelf")
        {
            isOnShelf = true;
        }
        else
        {
            isOnShelf = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Shelf")
        {
            isOnShelf = true;
        }
        else
        {
            isOnShelf = false;
        }
    }

    private void toggle(bool activeState)
    {
        distanceGrab.enabled = (activeState);
        distanceGrab2.enabled = (activeState);
        distanceGrab3.enabled = (activeState);
    }
}
