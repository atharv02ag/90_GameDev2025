using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}
