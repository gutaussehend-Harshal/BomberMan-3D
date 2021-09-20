using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class inherites from monobehaviour used for all monobehaviour class method
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private BombController bomb;
        private BombController currentBomb;
        private Rigidbody playerRigidbody;
        private PlayerController playerController;
        private float notDamagableTime = 1f;
        private float currentTime = 0f;
        [HideInInspector]
        public bool canPlaceBomb = true;
        private bool damagable = false;
        [SerializeField] private Transform respawnPoint;
        private MeshRenderer meshRenderer;
        private CapsuleCollider capsuleCollider;
        private void Start()
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            playerRigidbody = gameObject.GetComponent<Rigidbody>();
            capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        }

        private void Update()
        {

            if (currentTime > notDamagableTime && !damagable)
            {
                currentTime = 0;
                damagable = true;
            }
            else
            {
                currentTime += Time.deltaTime;
            }

            playerController.Movement();

            TimeNeedToPlaceNextBomb();

            PauseTheGame();
        }

        // This method used for placing a bomb using space-bar after some time interval
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

        // This method used for set playercontroller
        public void SetController(PlayerController _controller)
        {
            playerController = _controller;
        }

        // This method return player rigid body
        public Rigidbody GetRigidBody()
        {
            return playerRigidbody;
        }

        // This method used for update player health and destroying player when player collides to explosion prefab
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<DestroyMe>() != null && damagable)
            {
                PlayerRespawn();
            }
        }

        // This method used for update player health and destroying player when player collides to enemy
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<EnemyController>() != null && damagable)
            {
                PlayerRespawn();
            }
        }

        // This method used for placed bomb at spawn position which is player position
        private void DropBomb()
        {
            Debug.Log("Bomb Placed");
            canPlaceBomb = false;
            Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(transform.position.x), bomb.transform.position.y, Mathf.RoundToInt(transform.position.z));
            currentBomb = GameObject.Instantiate(bomb, spawnPosition, Quaternion.identity);
        }

        // This method used for pause the game on the press of escape key
        private void PauseTheGame()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.Instance.OnClickPauseBtn();
            }
        }

        // This function used for respawning of enemy after killed by explosion or enemy
        private void PlayerRespawn()
        {
            SoundManager.Instance.Play(Sounds.playerDied);
            EventService.Instance.InvokeOnHealthUpdate();
            // UIManager.Instance.UpdateHealth(10);
            StartCoroutine(EnableDisableEffectToPlayer());
            if (UIManager.health <= 0)
            {
                playerController.PlayerDied();
            }

            transform.position = respawnPoint.position;
        }

        // This function used for providing enable-disable effect to player
        private IEnumerator EnableDisableEffectToPlayer()
        {
            meshRenderer.enabled = false;
            capsuleCollider.isTrigger = false;
            yield return new WaitForSeconds(0.5f);
            meshRenderer.enabled = true;
            yield return new WaitForSeconds(1f);
            capsuleCollider.isTrigger = true;
            meshRenderer.enabled = false;
            yield return new WaitForSeconds(1f);
            meshRenderer.enabled = true;
            yield return new WaitForSeconds(1f);
            meshRenderer.enabled = false;
            yield return new WaitForSeconds(1f);
            meshRenderer.enabled = true;
        }
    }
}