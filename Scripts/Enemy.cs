using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Player player;

    private Animator anim;

    private AudioSource audioSource;

    [SerializeField]
    private GameObject laserPrefab;
    private float fireRate = 3.0f;
    private float canFire = -1;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2.0f, 6.5f);
        player = GameObject.Find("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        if(player == null){
            Debug.Log("The player is null");
        }

        anim = GetComponent<Animator>();
        if(anim == null){
            Debug.Log("The animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        move();
        fire();
    }

    void move(){
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -10.0f){
            Destroy(this.gameObject);
        }
    }

    void fire(){
        if(Time.time > canFire){
            fireRate = Random.Range(3f, 7f);
            canFire = Time.time + fireRate;
            GameObject enemyLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            //Debug.Break();
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for(int i = 0; i < lasers.Length; i++){
                lasers[i].assignEnemy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(player != null){
                player.damage(); // calling the damage method inside Player
            }
            audioSource.Play();
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.5f);
        }

        if(other.tag == "Laser"){
            if(player != null){
                player.addScore(Random.Range(5,11));
            }
            if(other.GetComponent<Laser>().isEnemy() == false){
                audioSource.Play();
                Destroy(other.gameObject);
                anim.SetTrigger("OnEnemyDeath");
                speed = 0;
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.5f);
            }
        }
    }
}
