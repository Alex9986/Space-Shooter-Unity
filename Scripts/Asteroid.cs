using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float rotateSpeed = 19.0f;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start(){
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Laser"){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spawnManager.startSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}
