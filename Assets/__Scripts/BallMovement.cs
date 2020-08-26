using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    GameManager manager;
    Vector3 direction;
    float velocity;


    // Start is called before the first frame update
    void Start()
    {
        velocity = 10;
        direction = new Vector3(1, 1, 0);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gameState != GameManager.GameState.Playing) return;

        transform.position += direction * velocity * Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Dentro da tela");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BoundsColision();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float x = direction.x;
        Debug.Log(collision.name);
        if(collision.name == "Paddle1")
        {
            x = 1;
        }
        else if(collision.name =="Paddle2")
        {
            x = -1;
        }

        direction = new Vector3(x, direction.y, 0);
    }

    private void BoundsColision()
    {
        Debug.Log("Saindo da tela");

        float x = direction.x;
        float y = direction.y;

        if (transform.position.y > 3.5f)
        {
            y = -1;
        }
        else if (transform.position.y < -3.5f)
        {
            y = 1;
        }

        if (transform.position.x > 7.5f)
        {
            manager.PointMade(1);
            StartCoroutine("ResetBall", 1);
        }
        else if (transform.position.x < -7.5f)
        {
            manager.PointMade(2);
            StartCoroutine("ResetBall", 2);
        }

        direction = new Vector3(x, y, 0);
    }

    IEnumerator ResetBall(int player)
    {
        transform.position = Vector3.zero;
        velocity = 0;
        yield return new WaitForSeconds(2);

        float x = 0;
        float y = 0;

        if(player == 1)
        {
            x = Random.Range(0.1f, 1.0f);
        }
        else if(player == 2)
        {
            x = Random.Range(-1.0f, -0.1f);
        }

        y = Random.Range(-1.0f, 1.0f);

        direction = new Vector3(x, y, 0).normalized;
        velocity = 10;
    }

}
