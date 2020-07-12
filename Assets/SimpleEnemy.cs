using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour

{

    Vector3 spawnPoint;
    Vector3 targetNodePos;
    public bool hasKey = false;
    public bool stealing = false;
    public bool die = false;
    GameObject node;

    public float MoveSpeed = 3f;
    float KeyStealingThreshold = 3f;
    float ElapsedKeyTime = 0f;
    float NodeWaitTime = 1f;

    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start () {

        hasKey = false;
        stealing = false;
        node = null;

        spawnPoint = this.transform.position;
        node = NodeCtrl.Instance.PickRandomNode ();
        if (node != null) {
            targetNodePos = node.transform.position;
        }

    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate () {
        if (die == false) {

            if (node == null) {
                node = NodeCtrl.Instance.PickRandomNode ();
                targetNodePos = node.transform.position;
            }

            if (node != null) {

                if (hasKey &&
                    ((Mathf.Abs (transform.position.x) > GameCtrl.Instance.topRight.x + 4) || (Mathf.Abs (transform.position.y) > GameCtrl.Instance.topRight.y + 4) ||
                        transform.position == spawnPoint))

                {
                    GameCtrl.Instance.NodesCaptured++;
                    Destroy (node.gameObject);
                    Destroy (gameObject);
                }

                if ((node.transform.parent != null && node.transform.parent.name == "NodeCtrl") || stealing) {
                    if (transform.position == targetNodePos) {
                        Debug.Log ("ENEMY GABBING NODE");
                        GrabNode ();
                        stealing = true;
                        ElapsedKeyTime += Time.deltaTime;
                        if (ElapsedKeyTime > KeyStealingThreshold) {
                            hasKey = true;
                        }
                    }

                } else {
                    ElapsedKeyTime = 0f;

                    GameObject potentialNode = NodeCtrl.Instance.PickRandomNode ();
                    if (potentialNode != null) {
                        node = potentialNode;
                    }
                    targetNodePos = node.transform.position;
                }

                if (hasKey == false) {
                    transform.position = Vector3.MoveTowards (transform.position, targetNodePos, .04f);
                    FixRotation (targetNodePos);
                } else if (hasKey == true) {
                    transform.position = Vector3.MoveTowards (transform.position, spawnPoint, .02f);
                    FixRotation (spawnPoint);
                    NodeWaitTime -= Time.deltaTime;
                    if (NodeWaitTime < 0) {
                        node.transform.position = Vector3.MoveTowards (node.transform.position, transform.position, .02f);
                    }

                }
            }
        }
    }

    void PickNode () {
        node = NodeCtrl.Instance.PickRandomNode ();
        targetNodePos = node.transform.position;
    }

    void FixRotation (Vector3 targ) {
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2 (targ.y, targ.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
    }

    void GrabNode () {
        node.transform.parent = null;
    }

    public void Die () {
        die = true;

        if (node.transform.parent == null) {
            node.transform.SetParent (NodeCtrl.Instance.transform);
        }
        EnemyCtrl.Instance.numEnemiesLeftInWave--;

        transform.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);

        explosion.Play ();

        Destroy (gameObject, 3f);
    }

}