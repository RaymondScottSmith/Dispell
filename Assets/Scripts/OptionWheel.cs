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
    
    [SerializeField]
    private IngCategory gems;
    
    [SerializeField]
    private IngCategory books;

    [SerializeField] private GameObject optionWheel;

    [SerializeField] private List<GameObject> options;

    [SerializeField] private Image circleDisplay;
    [SerializeField] private Image herbDisplay;
    [SerializeField] private Image gemDisplay;
    [SerializeField] private Image bookDisplay;

    [SerializeField] private GameObject herbBag;
    [SerializeField] private GameObject gemBag;
    [SerializeField] private GameObject bookPile;

    private List<int> optionValues = new List<int>();

    private int optionCategory;

    [SerializeField] private AudioClip openWheel, selectCircle, selectHerb, selectGem, selectBook;

    private AudioSource _audioSource;

    public int circleId, herbId, gemId, bookId;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        circleId = -1;
        herbId = -1;
        gemId = -1;
        bookId = -1;
        SetIngredientOptions();
    }

    public void SetIngredientOptions()
    {
        int numIngredients = FindObjectOfType<GameManager>().numIngredients;
        
        herbBag.SetActive(false);
        gemBag.SetActive(false);
        bookPile.SetActive(false);
        
        if (numIngredients > 1)
            herbBag.SetActive(true);
        if (numIngredients > 2)
            gemBag.SetActive(true);
        if (numIngredients > 3)
            bookPile.SetActive(true);

    }

    public void OpenWheel(int category)
    {
        _audioSource.PlayOneShot(openWheel);
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
                case 3:
                    optionValues.Add(gems.ingredients[i].id);
                    op.GetComponent<Image>().sprite = gems.ingredients[i].picture;
                    break;
                
                case 4:
                    optionValues.Add(books.ingredients[i].id);
                    op.GetComponent<Image>().sprite = books.ingredients[i].picture;
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
                _audioSource.PlayOneShot(selectCircle);
                circleDisplay.sprite = circles.GetIngredient(optionValues[value]).picture;
                circleDisplay.enabled = true;
                circleId = circles.GetIngredient(optionValues[value]).id;
                break;
            case 2:
                _audioSource.PlayOneShot(selectHerb);
                herbDisplay.sprite = herbs.GetIngredient(optionValues[value]).picture;
                herbDisplay.enabled = true;
                herbId = herbs.GetIngredient(optionValues[value]).id;
                break;
            
            case 3:
                _audioSource.PlayOneShot(selectGem);
                gemDisplay.sprite = gems.GetIngredient(optionValues[value]).picture;
                gemDisplay.enabled = true;
                gemId = gems.GetIngredient(optionValues[value]).id;
                break;
            
            case 4:
                _audioSource.PlayOneShot(selectBook);
                bookDisplay.sprite = books.GetIngredient(optionValues[value]).picture;
                bookDisplay.enabled = true;
                bookId = books.GetIngredient(optionValues[value]).id;
                break;
        }
        
        
        optionWheel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
