using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, itemContainer, fpsCam, playerRB;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    Material mat;

    Color maincolor;


    // Start is called before the first frame update
    void Start()
    {
        if (!equipped){
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped){
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }

        mat = GetComponent<Renderer>().material;
        maincolor = mat.color;

    }

    // Update is called once per frame
    void Update()
    {
        ///Check if the player is in range of the item
        
        Vector3 distanceToPlaer = player.position - transform.position;

        if(!equipped && distanceToPlaer.magnitude <= pickUpRange && !slotFull){
            
            mat.color = Color.green;

            if(Input.GetKeyDown(KeyCode.E)){
                PickUp();
            }

        }else if(!equipped && distanceToPlaer.magnitude > pickUpRange){
            mat.color = Color.red;
        }else if(equipped){
            mat.color = maincolor;
        }


        

        // Debug.Log(currentMat);


        if(equipped){
            if(Input.GetKeyDown(KeyCode.R)){
                // Debug.Log("Dropped"); 
                Drop();
            }
        }
    }

    private void PickUp(){

        equipped = true;
        slotFull = true;

        //Make item a child of the player and move it to the item container
        transform.SetParent(itemContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider isTrigger

        rb.isKinematic = true;
        coll.isTrigger = true;

    }

    private void Drop(){

        equipped = false;
        slotFull = false;

        //Set item parent to null
        transform.SetParent(null);

        //Make Rigidbody kinematic and BoxCollider isTrigger

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = playerRB.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random), ForceMode.Impulse);

    }
}
