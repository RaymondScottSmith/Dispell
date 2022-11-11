using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float timeDelay =1f;
    [SerializeField] private GameObject enemySpell;

    [SerializeField] private Image spellCircle;
    [SerializeField] private Image spellHerb;
    [SerializeField] private Image spellGem;
    [SerializeField] private Image spellBook;

    [SerializeField] private Animator playerAnimator;

    private float speed = 1f / 20f;

    private float temp = 0f;

    private float timer = 0f;

    private bool spawnedSpell = false;

    private int beaterCircle, beaterHerb, beaterGem, beaterBook;

    [SerializeField] private AudioSource tickSource;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip winSound;

    [SerializeField] private Animator armAnimator;

    [SerializeField] private Animator rightArmAnimator;

    public int numIngredients = 4;

    [SerializeField] private Animator winLoseAnimator;

    [SerializeField] private AudioClip loseMusic, winMusic;

    void Awake()
    {
        numIngredients = PlayerPrefs.GetInt("Difficulty");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();
        FindObjectOfType<TesterScript>().maxIngredients = numIngredients;
        StartCoroutine(SpawnSpell());
        
        switch (numIngredients)
        {
            case 1:
                FindObjectOfType<TesterScript>().maxPages = 2;
                break;
            case 2:
                FindObjectOfType<TesterScript>().maxPages = 3;
                break;
            case 3:
                FindObjectOfType<TesterScript>().maxPages = 5;
                break;
            case 4:
                FindObjectOfType<TesterScript>().maxPages = 6;
                break;
        }
        
    }

    private void AssignSpellIng()
    {
        List<IngCategory> cats = FindObjectOfType<TesterScript>().testCategory;

        Ingredient circle = cats[0].ingredients[Random.Range(0, cats[0].ingredients.Count)];
        Ingredient herb = cats[1].ingredients[Random.Range(0, cats[1].ingredients.Count)];
        Ingredient gem = cats[2].ingredients[Random.Range(0, cats[2].ingredients.Count)];
        Ingredient book = cats[3].ingredients[Random.Range(0, cats[3].ingredients.Count)];
        
        spellCircle.sprite = circle.picture;
        spellHerb.sprite = herb.picture;
        spellGem.sprite = gem.picture;
        spellBook.sprite = book.picture;

        beaterCircle = circle.beaterId;
        beaterHerb = herb.beaterId;
        beaterGem = gem.beaterId;
        beaterBook = book.beaterId;

        spellGem.enabled = false;
        spellHerb.enabled = false;
        spellBook.enabled = false;
        
        if (numIngredients > 1)
        {
            spellHerb.enabled = true;
        }

        if (numIngredients > 2)
            spellGem.enabled = true;
        if (numIngredients > 3)
            spellBook.enabled = true;
    }

    private IEnumerator SpawnSpell()
    {
        AssignSpellIng();
        yield return new WaitForSeconds(timeDelay);
        
        enemySpell.SetActive(true);
        FindObjectOfType<TesterScript>().timerRunning = true;
        spawnedSpell = true;
        StartCoroutine(TickSound());
    }

    private IEnumerator TickSound()
    {
        tickSource.Play();
        yield return new WaitForSeconds(2f);
        if (FindObjectOfType<TesterScript>().timerRunning)
            StartCoroutine(TickSound());
    }

    public void CheckOutcome()
    {
        if (CheckIngredients())
        {
            _audioSource.PlayOneShot(winSound);
            playerAnimator.SetTrigger("Win");
            StartCoroutine(WinGame());
        }
        else
        {
            armAnimator.SetTrigger("Die");
            rightArmAnimator.SetTrigger("Die");
            _audioSource.PlayOneShot(deathSound);
            playerAnimator.SetTrigger("Die");
            StartCoroutine(LoseGame());
        }
    }

    private IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(1f);
        _audioSource.PlayOneShot(loseMusic);
        winLoseAnimator.SetTrigger("Lose");
    }
    
    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1f);
        _audioSource.PlayOneShot(winMusic);
        winLoseAnimator.SetTrigger("Win");
    }

    public bool CheckIngredients()
    {
        OptionWheel oWheel = FindObjectOfType<OptionWheel>();
        switch (numIngredients)
        {
            case 1:
            {
                if (beaterCircle == oWheel.circleId)
                    return true;
                break;
            }
            case 2:
            {
                if (beaterCircle == oWheel.circleId && beaterHerb == oWheel.herbId)
                    return true;
                break;
            }
            case 3:
                if (beaterCircle == oWheel.circleId && beaterHerb == oWheel.herbId &&
                    beaterGem == oWheel.gemId)
                    return true;
                break;
            case 4:
                if (beaterCircle == oWheel.circleId && beaterHerb == oWheel.herbId &&
                    beaterGem == oWheel.gemId && beaterBook == oWheel.bookId)
                    return true;
                break;
        }

        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }
}
