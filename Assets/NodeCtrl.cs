using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCtrl : MonoBehaviour

{
    public GameObject NodePrefab;
    public static NodeCtrl Instance;



    // Start is called before the first frame update
    void Start () {
        if (Instance != null) {
            Debug.LogError ("There should never be two world controllers");
        }

        float nodeX = GameCtrl.Instance.topRight.x / 3;
        float nodeY = GameCtrl.Instance.topRight.y / 3 ;

        Instance = this;
        GameObject node0 = Instantiate (NodePrefab, new Vector3 (nodeX, nodeY, 0), NodePrefab.transform.rotation);
        node0.transform.SetParent (transform, true);
        GameObject node1 = Instantiate (NodePrefab, new Vector3 (nodeX, -nodeY, 0), NodePrefab.transform.rotation);
        node1.transform.SetParent (transform, true);
        GameObject node2 = Instantiate (NodePrefab, new Vector3 (-nodeX, nodeY, 0), NodePrefab.transform.rotation);
        node2.transform.SetParent (transform, true);
        GameObject node3 = Instantiate (NodePrefab, new Vector3 (-nodeX, -nodeY, 0), NodePrefab.transform.rotation);
        node3.transform.SetParent (transform, true);
    }

    // Update is called once per frame
    void Update () {

    }

    public GameObject PickRandomNode () {
        if(transform.childCount == 0){
            return null;
        }
        return transform.GetChild(Random.Range(0, transform.childCount)).gameObject;
    }

}