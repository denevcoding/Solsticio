using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalInteracter : MonoBehaviour
{
    public PickUpObject picker;
    public PedestalController pedestal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pedestal != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (picker.PickedObject != null)
                {
                    //Debug.Log("Entrgue Gema");
                   
                    pedestal.ReceiveGem(picker.PickedObject);
                    picker.PickedObject = null;
                }
                else
                {
                   // Debug.Log("No me estas dando ni M");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.GetComponent<PedestalController>())
        {
            pedestal = other.gameObject.GetComponent<PedestalController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.GetComponent<PedestalController>())
        {
            pedestal = null;
        }

    }
}
