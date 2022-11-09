using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TesterScript : MonoBehaviour
{

    public List<IngCategory> testCategory;

    public int pageNumber;

    public int latestCat;

    public int placeInCategory;

    [SerializeField] private List<Image> images;
    // Start is called before the first frame update
    void Start()
    {
        pageNumber = 0;
        latestCat = 0;
        placeInCategory = 0;
        foreach (IngCategory ingCat in testCategory)
        {
            ingCat.RandomizeList();
        }
        for (int i = 0; i < images.Count; i++)
        {
            images[i].sprite = testCategory[0].ingredients[i].picture;
            placeInCategory++;
        }

        foreach (Ingredient ing in testCategory[0].ingredients)
        {
            Debug.Log(ing.id + " is beaten by " + ing.beaterId);
        }
    }

    public void PreviousPage()
    {

        if (pageNumber == 0)
        {
            return;
        }
        pageNumber--;

        if (placeInCategory == 3)
        {
            placeInCategory = 0;
            latestCat--;
        }
        else if (placeInCategory == 0)
        {
            latestCat -= 2;
            placeInCategory = 6;
        }
        else if (placeInCategory == 6)
        {
            latestCat--;
            placeInCategory = 3;
        }
        
        for (int i = 0; i < images.Count; i++)
        {
            if (latestCat >= testCategory.Count || placeInCategory >= testCategory[latestCat].ingredients.Count)
            {
                return;
            }
            images[i].sprite = testCategory[latestCat].ingredients[placeInCategory].picture;
            if (placeInCategory < testCategory[latestCat].ingredients.Count - 1)
            {
                placeInCategory++;
            }
            else
            {
                placeInCategory = 0;
                latestCat++;
            }
            
        }
        /*
        for (int i = 0; i < images.Count; i++)
        {
            if (latestCat == 0 || placeInCategory >= testCategory[latestCat].ingredients.Count)
            {
                return;
            }
            images[i].sprite = testCategory[latestCat].ingredients[placeInCategory].picture;
            if (placeInCategory < testCategory[latestCat].ingredients.Count - 1)
            {
                placeInCategory++;
            }
            else
            {
                placeInCategory = 0;
                latestCat--;
            }
            
        }
        */


    }

    public void NextPage()
    {
        pageNumber++;
        for (int i = 0; i < images.Count; i++)
        {
            if (latestCat >= testCategory.Count || placeInCategory >= testCategory[latestCat].ingredients.Count)
            {
                pageNumber--;
                return;
            }
            images[i].sprite = testCategory[latestCat].ingredients[placeInCategory].picture;
            if (placeInCategory < testCategory[latestCat].ingredients.Count - 1)
            {
                placeInCategory++;
            }
            else
            {
                placeInCategory = 0;
                latestCat++;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
