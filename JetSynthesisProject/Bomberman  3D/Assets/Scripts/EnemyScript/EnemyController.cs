using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// </summary>
public class EnemyController : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] private LayerMask Walls;
    private Movement currentMovement = Movement.Right;
    private Rigidbody enemyRigidBody;
    private float currentTime = 0;
    private float changeDirectionTimer = 2f;
    private static int enemiesDied = 0;
    private PlayerView playerView;
    private EnemyController enemyController;
    [SerializeField] private int score = 10;
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody>();
        enemiesDied = 0;
    }

    void Update()
    {
        enemyMovement();
    }

    private void enemyMovement()
    {
        Physics.IgnoreLayerCollision(9, 9);

        if (currentTime < Time.time)
        {
            currentTime = changeDirectionTimer + Time.time;
            currentMovement = (Movement)Random.Range(0, 4);
        }

        switch (currentMovement)
        {
            case Movement.Right:
                MoveInRightDirection();
                break;
            case Movement.Left:
                MoveInLeftDirection();
                break;
            case Movement.Forward:
                MoveInForwardtDirection();
                break;
            case Movement.Back:
                MoveInBackDirection();
                break;
        }
    }

    private void MoveInRightDirection()
    {
        var hitinfo = Physics.Raycast(transform.position, Vector3.right, 0.5f, Walls);
        if (hitinfo)
        {
            while (currentMovement == Movement.Right)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }
        }
        else
        {
            enemyRigidBody.velocity = new Vector3(movementSpeed, 0f, 0f);
        }
    }

    private void MoveInLeftDirection()
    {
        var hitinfo = Physics.Raycast(transform.position, Vector3.left, 0.5f, Walls);
        if (hitinfo)
        {
            while (currentMovement == Movement.Left)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }
        }
        else
        {
            enemyRigidBody.velocity = new Vector3(-movementSpeed, 0f, 0f);
        }
    }

    private void MoveInForwardtDirection()
    {
        var hitinfo = Physics.Raycast(transform.position, Vector3.forward, 0.5f, Walls);
        if (hitinfo)
        {
            while (currentMovement == Movement.Forward)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }

        }
        else
        {
            enemyRigidBody.velocity = new Vector3(0f, 0f, movementSpeed);
        }
    }

    private void MoveInBackDirection()
    {
        var hitinfo = Physics.Raycast(transform.position, Vector3.back, 0.5f, Walls);
        if (hitinfo)
        {
            while (currentMovement == Movement.Back)
            {
                currentMovement = (Movement)Random.Range(0, 4);
            }
        }
        else
        {
            enemyRigidBody.velocity = new Vector3(0f, 0f, -movementSpeed);
        }
    }

    private void OnDestroy()
    {
        enemiesDied++;
        UIManager.Instance.UpdateScore(score);

        if (enemiesDied == 5)
        {
            UIManager.Instance.ShowWinScreen();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DestroyMe>() != null)
        {
            Debug.Log("Enemy Destroy");
            Destroy(gameObject);
        }
    }
}

public enum Movement
{
    Right,
    Left,
    Forward,
    Back,
}