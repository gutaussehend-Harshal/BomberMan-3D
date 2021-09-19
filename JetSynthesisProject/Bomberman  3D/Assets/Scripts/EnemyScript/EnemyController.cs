using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class handles all enemy logic
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private LayerMask Walls;
        [SerializeField] private int score = 10;
        private Movement currentMovement = Movement.Right;
        private Rigidbody enemyRigidBody;
        private float currentTime = 0;
        private float changeDirectionTimer = 2f;
        private static int enemiesDied;
        private PlayerView playerView;
        private EnemyController enemyController;

        void Start()
        {
            enemyRigidBody = gameObject.GetComponent<Rigidbody>();
            enemiesDied = 0;
        }

        void Update()
        {
            enemyMovement();
        }

        // This method used for enemy movement
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

        // This method used for enemy movement in right direction
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

        // This method used for enemy movement in left direction
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

        // This method used for enemy movement in forward direction
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

        // This method used for enemy movement in backward direction
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

        // This method used for after killing enemy it will update a score by 10 and it will show game win pannel after all enemies died
        private void OnDestroy()
        {
            enemiesDied++;
            UIManager.Instance.UpdateScore(score);

            if (enemiesDied == 5)
            {
                UIManager.Instance.ShowWinScreen();
            }
        }

        // This method used for if enemy touches to an explosion prefab, the enemy will die
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<DestroyMe>() != null)
            {
                Debug.Log("Enemy Destroy");
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// This are enums for enemy direction
    /// </summary>
    public enum Movement
    {
        Right,
        Left,
        Forward,
        Back,
    }
}