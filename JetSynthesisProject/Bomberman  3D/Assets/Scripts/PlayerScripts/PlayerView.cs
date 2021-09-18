using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private BombController bomb;
    private BombController currentBomb;
    public bool canPlaceBomb = true;
    public Transform playerTransform;
    public Transform spawnPoint;
    private Rigidbody playerRigidbody;
    private PlayerController playerController;
    private bool Damagable = false;
    private float notDamagableTime = 1f;
    private float currentTime = 0f;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (currentTime > notDamagableTime && !Damagable)
        {
            currentTime = 0;
            Damagable = true;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        
        playerController.Movement();

        TimeNeedToPlaceNextBomb();
    }

    private void TimeNeedToPlaceNextBomb()
    {
        if (currentBomb && currentBomb.bombBlastTimer <= 0.2)
        {
            canPlaceBomb = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canPlaceBomb)
        {
            DropBomb();
        }
    }

    public void SetController(PlayerController _controller)
    {
        playerController = _controller;
    }

    public Rigidbody GetRigidBody()
    {
        return playerRigidbody;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DestroyMe>() != null && Damagable)
        {
            playerController.PlayerDied();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null && Damagable)
        {
            playerController.PlayerDied();
        }
    }

    private void DropBomb()
    {
        Debug.Log("Bomb Placed");
        canPlaceBomb = false;
        Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(transform.position.x), bomb.transform.position.y, Mathf.RoundToInt(transform.position.z));
        currentBomb = GameObject.Instantiate(bomb, spawnPosition, Quaternion.identity);
    }
}
