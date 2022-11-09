using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWheel : MonoBehaviour
{
    [SerializeField]
    private IngCategory circles;
    
    [SerializeField]
    private IngCategory herbs;

    [SerializeField] private GameObject optionWheel;

    [SerializeField] private List<GameObject> options;

    [SerializeField] private Image circleDisplay;
    [SerializeField] private Image herbDisplay;

    private List<int> optionValues = new List<int>();

    private int optionCategory;
    // Start is called before the first frame update
    void Start()
    {
        //OpenWheel(1);
    }

    public void OpenWheel(int category)
    {
        optionCategory = category;
        optionValues.Clear();
        optionWheel.SetActive(true);
        int i = 0;
        foreach (GameObject op in options)
        {
            switch(optionCategory)
            {
                case 1: 
                    optionValues.Add(circles.ingredients[i].id);
                    op.GetComponent<Image>().sprite = circles.ingredients[i].picture;

                    break;
                case 2:
                    optionValues.Add(herbs.ingredients[i].id);
                    op.GetComponent<Image>().sprite = herbs.ingredients[i].picture;

                    break;
            }
            i++;
        }
    }

    public void ChooseOption(int value)
    {
        switch (optionCategory)
        {
            case 1:
                circleDisplay.sprite = circles.GetIngredient(optionValues[value]).picture;
                circleDisplay.enabled = true;
                break;
            case 2:
                herbDisplay.sprite = herbs.GetIngredient(optionValues[value]).picture;
                herbDisplay.enabled = true;
                break;
        }
        
        
        optionWheel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
