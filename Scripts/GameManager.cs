using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver;

    public void gameOver(){
        isGameOver = true;
    }

    // Update is called once per frame
    private void Update(){
        if(Input.GetKeyDown("r") && isGameOver == true){
            SceneManager.LoadScene(1);
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

}
