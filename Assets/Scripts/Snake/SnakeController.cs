using UnityEngine;
using System.Collections;

public class SnakeController : MonoBehaviour
{
    public float speed;
    private int directionPressed = 0;
    private Vector3 yMove = new Vector3(0, 0.225f, 0);
    private Vector3 xMove = new Vector3(0.225f, 0, 0);
    private SpawnManager spawnManager;
    public GameObject head ,body;

    private float xBodyPos;
    private float yBodyPos;
    private int nChildObjects;
    public Vector3[] bodyPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        InvokeRepeating(nameof(MoveForwards), 1, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            directionPressed = 0;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            directionPressed = 1;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            directionPressed = 2;
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            directionPressed = 3;
        }
    }

    private void MoveForwards()
    {
        switch (directionPressed)
        {
            case 0:
                transform.Translate(yMove);
                break;
            case 1:
                transform.Translate(-xMove);
                break;
            case 2:
                transform.Translate(-yMove);
                break;
            case 3:
                transform.Translate(xMove);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnFood();

        switch (directionPressed)
        {
            case 0:
                Instantiate(body, (head.transform.GetChild(nChildObjects)).position-yMove, transform.rotation, head.transform);
                break;
            case 1:
                Instantiate(body, (head.transform.GetChild(nChildObjects)).position+xMove, transform.rotation, head.transform);
                break;
            case 2:
                Instantiate(body, (head.transform.GetChild(nChildObjects)).position+yMove, transform.rotation, head.transform);
                break;
            case 3:
                Instantiate(body, (head.transform.GetChild(nChildObjects)).position-xMove, transform.rotation, head.transform);
                break;
        }
    }
}
