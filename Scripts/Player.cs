using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalInput;
    public float vertrcalInput;
    [SerializeField]
    public float speed = 10.0f;

    public GameObject laserPrefab;
    public GameObject tripleShotPrefab;

    [SerializeField]
    private float upperBound = 9.8f;
    private float lowerBound = -9.6f;
    private float leftBound = -20f;
    private float rightBound = 20f;

    [SerializeField]
    private float fireRate = 1.1f;
    private float canFire = -1f;

    [SerializeField]
    private int lives = 3;

    private SpawnManager spawnManager;

    [SerializeField]
    private bool isTripleShotActive = false;
    [SerializeField]
    private bool isSpeedActive = false;
    [SerializeField]
    private bool isShieldActive = false;
    [SerializeField]
    private GameObject shieldVisualizer;
    [SerializeField]
    private GameObject leftEngineVisualizer;
    [SerializeField]
    private GameObject rightEngineVisualizer;

    private UIManager uiManager;
    [SerializeField]
    private int score;

    [SerializeField]
    private AudioClip laserAudio;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioSource = GetComponent<AudioSource>();

        if(spawnManager == null){
            Debug.Log("The Spawn Manager is NULL");
        }
        
        if(uiManager == null){
            Debug.Log("The UI Manager is NULL");
        }

        if(audioSource == null){
            Debug.Log("The Audio Source is NULL");
        } else {
            audioSource.clip = laserAudio;
        }

        leftEngineVisualizer.SetActive(false);
        rightEngineVisualizer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        boundCheck();
        fire();
    }

    void move(){
        horizontalInput = Input.GetAxis("Horizontal");
        vertrcalInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
        // transform.Translate(Vector3.up * Time.deltaTime * vertrcalInput * speed);

        Vector3 direction = new Vector3(horizontalInput, vertrcalInput, 0) * speed * Time.deltaTime ;
        transform.Translate(direction);
    }

    void boundCheck(){
        // vertical bound check 
        if(transform.position.y > upperBound){
            transform.position = new Vector3(transform.position.x, upperBound, transform.position.z);
        }else if(transform.position.y < lowerBound){
            transform.position = new Vector3(transform.position.x, lowerBound, transform.position.z);
        }

        // horizontal bound check 
        if(transform.position.x > rightBound){
            transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        }else if(transform.position.x < leftBound){
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        }
    }

    void fire(){
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > canFire){ 
            if(isTripleShotActive == true){
                Vector3 mypos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameObject newLaser = Instantiate(tripleShotPrefab, mypos, Quaternion.identity);
            } else {
                canFire = Time.time + fireRate;
                GameObject newLaser = Instantiate(laserPrefab, transform.position + new Vector3(0, 1.5f, 0) , Quaternion.identity);
            }
            audioSource.Play();
        }
    }

    public void damage(){
        if(isShieldActive == false){
            lives--;
            if(lives == 2){
                rightEngineVisualizer.SetActive(true);
            }else if(lives == 1){
                leftEngineVisualizer.SetActive(true);
            }
            uiManager.updateLives(lives);
            if(lives < 1){
                spawnManager.playerDead();
                Destroy(this.gameObject);
            }
        }else{
            isShieldActive = false;
            shieldVisualizer.SetActive(false);
        }
        
    }

    public void tripleShotActive(){
        isTripleShotActive = true;
        StartCoroutine(tripleShotPowerDownRoutine());
    }

    IEnumerator tripleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    public void speedActive(){
        isSpeedActive = true;
        speed = 12.5f;
        StartCoroutine(speedDownRoutine());
    }

    IEnumerator speedDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        speed = 10.0f;
        isSpeedActive = false;
    }

    public void shieldActive(){
        isShieldActive = true;
        shieldVisualizer.SetActive(true);
    }

    public void addScore(int points){
        score += points;
        uiManager.updateScore(score);
    }

}
