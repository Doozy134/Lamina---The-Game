using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject marker;
    [SerializeField] private GameObject[] items;

    private float pos_x;
    private float pos_y;

    void Awake()
    {
        //getting x and y coordinates from marker
        pos_x = marker.transform.position.x;
        pos_y = marker.transform.position.y;

        generateItems();
    }

    private void generateItems()
    {
        //generating random number between 0 , 1 , 2
        int randomNum = Random.Range(0, items.Length);

        //selecting gameobject from array
        GameObject chosenItem = items[randomNum];

        //instantiating new item 
        GameObject newItem = Instantiate(chosenItem) as GameObject;

        //setting position of new item
        newItem.transform.position = new Vector3(pos_x, pos_y, 0);
    }
}
