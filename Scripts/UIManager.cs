using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Sprite[] livesSprite;
    [SerializeField]
    private Image livesImg;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text restartText;
    [SerializeField]
    private GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score : " + 0;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager == null){
            Debug.Log("The Game Manager is NULL");
        }
    }

    public void updateScore(int playerScore){
        scoreText.text = "Score : " + playerScore;
    }

    public void updateLives(int currentLives){
        livesImg.sprite = livesSprite[currentLives];
        if(currentLives == 0){
            gameoverSequence();
        }
    }

    void gameoverSequence(){
        gameManager.gameOver();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine(){
        while(true){
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}
