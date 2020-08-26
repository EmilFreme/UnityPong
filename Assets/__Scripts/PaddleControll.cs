using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControll : MonoBehaviour
{
    float velocity;
    int direction;

    [SerializeField]
    KeyCode upArrow;
    [SerializeField]
    KeyCode downArrow;

    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        velocity = 5;
        manager = manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gameState != GameManager.GameState.Playing) return;

        if (Input.GetKey(upArrow))
        {
            direction = 1;
        }
        else if (Input.GetKey(downArrow))
        {
            direction = -1;
        }

        if(Input.GetKeyUp(upArrow) || Input.GetKeyUp(downArrow))
        {
            direction = 0;
        }

        transform.position += new Vector3(0, direction, 0) * velocity * Time.deltaTime;
    }
}
