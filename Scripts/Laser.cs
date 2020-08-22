using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10.0f;

    [SerializeField]
    private bool isEnemyLaser = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyLaser == false){ // player laser
            moveUp();
        } else { // enemy laser
            moveDown();
        }
    }

    void moveUp(){
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if(transform.position.y >= 10.0f){
            if(transform.parent != null){
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void moveDown(){
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if(transform.position.y <= -10.0f){
            if(transform.parent != null){
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void assignEnemy(){
        isEnemyLaser = true;
    }

    public bool isEnemy(){
        return isEnemyLaser;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && isEnemyLaser == true){
            Player player = other.GetComponent<Player>();
            if(player != null){
                player.damage();
                Destroy(this.gameObject);
            }
        }
    }
}
