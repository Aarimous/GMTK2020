using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour




{

        float ElapsedTime = 0f;

        void OnCollisionEnter2D(Collision2D collision){

        Debug.Log(collision.gameObject.tag);

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        if(collision.gameObject.tag == "Enemy"){
            collision.gameObject.GetComponentInParent<SimpleEnemy>().Die();
        }

        Destroy(gameObject);
        
    }

    void Update(){

        if(Mathf.Abs(transform.position.x) > GameCtrl.Instance.topRight.x || Mathf.Abs(transform.position.y) > GameCtrl.Instance.topRight.y){
            Destroy(gameObject);
        }
    }
}
