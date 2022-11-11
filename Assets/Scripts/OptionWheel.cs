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

    private List<Ingredient> myCircles, myHerbs, myGems, myBooks;
    //private List<int> circleIDs, herbIDs, gemIDs, bookIDs; 

    public int circleId, herbId, gemId, bookId;
    // Start is called before the first frame update


    void Awake()
    {
        myCircles = circles.ingredients;
        Shuffle(myCircles);
        myHerbs = herbs.ingredients;
        Shuffle(myHerbs);
        myGems = gems.ingredients;
        Shuffle(myGems);
        myBooks = books.ingredients;
        Shuffle(myBooks);
    }
    void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();
        circleId = -1;
        herbId = -1;
        gemId = -1;
        bookId = -1;
        SetIngredientOptions();
    }

    private void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
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
                    optionValues.Add(myCircles[i].id);
                    op.GetComponent<Image>().sprite = myCircles[i].picture;
                    break;
                case 2:
                    optionValues.Add(myHerbs[i].id);
                    op.GetComponent<Image>().sprite = myHerbs[i].picture;
                    break;
                case 3:
                    optionValues.Add(myGems[i].id);
                    op.GetComponent<Image>().sprite = myGems[i].picture;
                    break;
                
                case 4:
                    optionValues.Add(myBooks[i].id);
                    op.GetComponent<Image>().sprite = myBooks[i].picture;
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
                circleDisplay.sprite = myCircles[value].picture;
                circleDisplay.enabled = true;
                circleId = myCircles[value].id;
                break;
            case 2:
                _audioSource.PlayOneShot(selectHerb);
                herbDisplay.sprite = myHerbs[value].picture;
                herbDisplay.enabled = true;
                herbId = myHerbs[value].id;
                break;
            
            case 3:
                _audioSource.PlayOneShot(selectGem);
                gemDisplay.sprite = myGems[value].picture;
                gemDisplay.enabled = true;
                gemId = myGems[value].id;
                break;
            
            case 4:
                _audioSource.PlayOneShot(selectBook);
                bookDisplay.sprite = myBooks[value].picture;
                bookDisplay.enabled = true;
                bookId = myBooks[value].id;
                break;
        }
        
        
        optionWheel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
