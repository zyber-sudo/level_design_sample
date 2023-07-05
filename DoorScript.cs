using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator doorAnimationController;
    [SerializeField] GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimationController = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the box.");
        doorAnimationController.SetBool("door_bool", true);
        doorAnimationController.SetBool("door_close", false);

    }


    private void OnTriggerExit(Collider other)
    {

        doorAnimationController.SetBool("door_close", true);
        doorAnimationController.SetBool("door_bool", false);

    }

}
