using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TesterScript : MonoBehaviour
{

    public List<IngCategory> testCategory;

    public int pageNumber;

    public int latestCat;

    public int placeInCategory;

    [SerializeField] private List<Image> images;

    [SerializeField] private Animator bookAnimator;

    private float timeRemaining = 21f;

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private List<GameObject> rightPics;

    public bool timerRunning = false;

    public int maxPages = 5;

    public int maxIngredients;
    // Start is called before the first frame update
    void Awake()
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
            //Debug.Log(ing.id + " is beaten by " + ing.beaterId);
        }
    }

    private void HideRight()
    {
        foreach (GameObject obj in rightPics)
        {
            obj.SetActive(false);
        }
    }

    private void RevealRight()
    {
        foreach (GameObject obj in rightPics)
        {
            obj.SetActive(true);
        }
    }

    public void PreviousPage()
    {

        if (pageNumber == 0)
        {
            return;
        }
        pageNumber--;
        bookAnimator.SetTrigger("TurnPrev");
    }

    public void PlayPageFlip()
    {
        GetComponent<AudioSource>().Play();
    }

    public void TurnNextPage()
    {
        for (int i = 0; i < images.Count; i++)
        {
            if (latestCat > maxIngredients-1)
            {
                latestCat++;
                HideRight();
                placeInCategory -= 3;
                if (placeInCategory < 0)
                {
                    placeInCategory = 3;
                    latestCat--;
                }
                    
                return;
            }
            else
            {
                RevealRight();
            }
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

    public void TurnPrevPage()
    {
        RevealRight();
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
            Debug.Log(latestCat);
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
    }

    public void NextPage()
    {
        if (pageNumber == maxPages-1)
        {
            return;
        }
        pageNumber++;
        bookAnimator.SetTrigger("TurnNext");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}", seconds);
            if (timeRemaining <= 1)
            {
                timerText.text = string.Format("{0:00}", 0);
                timerRunning = false;
                FindObjectOfType<GameManager>().CheckOutcome();
            }
        }
        
    }
}
