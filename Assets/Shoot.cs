using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPreFab;
    public float bulletForce = 12f;

    public float ShootTime = 1f;
    public float ElapsedTime = 0f;

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown ("Fire1")) {
            //ShootBullet ();
        }
        ElapsedTime += Time.deltaTime;
        if( ElapsedTime >= ShootTime){
            ShootBullet();
            ElapsedTime = 0;
        }

    }

    void FixedUpdate(){

    }

    void ShootBullet () {

        GameObject bullet = Instantiate (bulletPreFab, firePoint.position, firePoint.rotation);

        List<GameObject> ignoreObjects = new List<GameObject>( GameObject.FindGameObjectsWithTag ("Player"));
        ignoreObjects.AddRange( new List<GameObject>(GameObject.FindGameObjectsWithTag ("Pusher")));
        ignoreObjects.AddRange( new List<GameObject>(GameObject.FindGameObjectsWithTag ("Clockwise")));
        ignoreObjects.AddRange( new List<GameObject>(GameObject.FindGameObjectsWithTag ("CounterClock")));
        ignoreObjects.AddRange( new List<GameObject>(GameObject.FindGameObjectsWithTag ("Node")));


        foreach (GameObject obj in ignoreObjects) {
            Physics2D.IgnoreCollision (obj.GetComponent<Collider2D> (), bullet.transform.GetComponent<Collider2D> ());
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D> ();

        rb.AddForce (firePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}