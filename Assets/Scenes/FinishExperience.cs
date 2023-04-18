using UnityEngine;
using System.Collections;

public class FinishExperience : MonoBehaviour {
    private GameObject[] items ;
    private int itemCount;
    private void Start() {
        items = GameObject.FindGameObjectsWithTag("Item");
        itemCount=items.Length;
    }
 
    // appeler lorsque un objet entre en collision avec le plane
    void OnTriggerEnter(Collider other) {
        // si l'objet a le tag "item"
        if(other.CompareTag("Item")) {
            // décrémente le compteur d'objets
            itemCount--;
        }
 
        // si tous les objets ont été placés sur le plane
        if(itemCount == 0) {
            // affiche le message
            Debug.Log("You have finished the experience!");
        }
    }
    private void OnTriggerExit(Collider other) {
         // si l'objet a le tag "item"
        if(other.CompareTag("Item")) {
            // décrémente le compteur d'objets
            itemCount++;
        }
    }
}
