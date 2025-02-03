using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public float destinationX, destinationY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.layer == 6)
        {
            Vector3 newPos;
            newPos.x = this.transform.parent.transform.GetChild(1).transform.position.x-2;
            newPos.y = this.transform.parent.transform.GetChild(1).transform.position.y-2;
            newPos.z = player.transform.position.z;
            Debug.Log(newPos.x + ", " + newPos.y + ", " + newPos.z);
            player.transform.position = newPos;
        }
    }
}
