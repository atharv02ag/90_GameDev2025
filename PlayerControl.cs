using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float spawnX = 44;
    public float spawnY = -37;
    private Vector2 movement;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawn;
        spawn.x = spawnX;
        spawn.y = spawnY;
        spawn.z = transform.position.z;
        rb = GetComponent<Rigidbody2D>();
        transform.position = spawn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
        move();
    }

    void HandleInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void move()
    {
        rb.position += movement * moveSpeed * Time.fixedDeltaTime;
    }
}
