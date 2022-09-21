using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalController : MonoBehaviour
{
    public bool isFull = false;
   
    public int pickableIndex = 0;

    public List<Transform> socketList = new List<Transform>();
    public List<PlatformMovement> plataformas = new List<PlatformMovement>();
    public List<Transform> platformsPositions = new List<Transform>();
    public List<GameObject> gems = new List<GameObject>();


    void Start()
    {
        


    }
    private void OnCollisionEnter(Collision collision)
    {
      
        
    }


    public void ReceiveGem(GameObject newObject)
    {
        if (newObject.GetComponent<PickableObject>())
        {
            if (pickableIndex < socketList.Count)
            {
                newObject.GetComponent<PickableObject>().SetPicked(socketList[pickableIndex]); // Release gem on the table

                gems.Add(newObject); //Add Gem to the list of gems
                
                plataformas[pickableIndex].SetNewPosition(platformsPositions[pickableIndex].position); //
                plataformas[pickableIndex].moving = true;


                pickableIndex++;

                if (pickableIndex == 2)
                {
                    isFull = true;
                }
            }
            else
            {
                Debug.Log("Esa mesa esta llena");
            }
            
        }
       
    }
     
}

