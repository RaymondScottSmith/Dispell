using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterScript : MonoBehaviour
{

    public IngCategory testCategory;
    // Start is called before the first frame update
    void Start()
    {
        testCategory.RandomizeList();
        foreach(Ingredient ing in testCategory.ingredients)
        {
            Debug.Log(ing.id + " is beaten by " + ing.beaterId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
