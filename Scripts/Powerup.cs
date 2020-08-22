using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -9.5f){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(clip, transform.position);
            if(player != null){
                switch(powerupID){
                    case 0:
                        player.tripleShotActive();
                        break;
                    case 1:
                        player.speedActive();
                        break;
                    case 2:
                        player.shieldActive();
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
