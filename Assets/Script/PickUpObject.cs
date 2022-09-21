using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform interactionZone;

    public Transform socket;

    //PedestalController



    // Update is called once per frame
    void Update()
    {
        if (PickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Debug.Log("Releasing");
                //PickedObject = ObjectToPickUp;
                PickableObject pickeable = PickedObject.GetComponent<PickableObject>();
                PickedObject = null;
                pickeable.SetFree();

            }
        }

        if (ObjectToPickUp != null)
        {
            if (ObjectToPickUp.GetComponent<PickableObject>().isPickable == false)            
                return;            

            if (PickedObject != null)            
                return;    

            if (Input.GetKeyDown(KeyCode.F))
            {
               // Debug.Log("Picking");
                PickedObject = ObjectToPickUp;
                PickableObject pickeable = PickedObject.GetComponent<PickableObject>();
                pickeable.SetPicked(socket);
                ObjectToPickUp = null;
            }
        }         
    }


  
}
