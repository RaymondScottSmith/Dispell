using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Ing_Category", menuName = "Scriptable Object / New Category")]
public class IngCategory : ScriptableObject
{
    public List<Ingredient> ingredients;

    public void RandomizeList()
    {
        for (int i = 0; i < ingredients.Count; i++) {
            var temp = ingredients[i];
            int randomIndex = Random.Range(i, ingredients.Count);
            ingredients[i] = ingredients[randomIndex];
            ingredients[randomIndex] = temp;
        }

        //Assign beater values in groups of 3
        if (ingredients.Count % 3 == 0)
        {
            int j = 0;
            while (j < ingredients.Count)
            {
                ingredients[j].beaterId = ingredients[j + 1].id;
                ingredients[j+1].beaterId = ingredients[j + 2].id;
                ingredients[j+2].beaterId = ingredients[j].id;
                j += 3;
            }

            
        }
        


    }
}

