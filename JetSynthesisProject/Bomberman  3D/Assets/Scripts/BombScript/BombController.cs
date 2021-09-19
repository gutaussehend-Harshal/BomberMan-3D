using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles bomb logic
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class BombController : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private float bombStrength = 3;
        public float bombBlastTimer;
        [SerializeField] private LayerMask levelMask;

        private void Update()
        {
            TimeForNextBombPlaced();
        }

        // This method used for placing the bomb after some interval of time
        private void TimeForNextBombPlaced()
        {
            bombBlastTimer -= Time.deltaTime;
            if (bombBlastTimer <= 0.1f)
            {
                Explode();
                Destroy(gameObject);
            }
        }

        // This method used for instantiating explosion Prefab at center and remaining four direction 
        public void Explode()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            StartCoroutine(CreateExplosions(Vector3.forward));
            StartCoroutine(CreateExplosions(Vector3.right));
            StartCoroutine(CreateExplosions(Vector3.back));
            StartCoroutine(CreateExplosions(Vector3.left));
        }

        // This method used for creating explosion and avoid explosion when there is wall or unbreakable block
        private IEnumerator CreateExplosions(Vector3 direction)
        {
            for (int i = 1; i < bombStrength; i++)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction, out hit, i, levelMask);

                if (!hit.collider)
                {
                    Instantiate(explosionPrefab, transform.position + (i * direction), Quaternion.identity);
                }
                else
                {
                    break;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        // This method used for when player leaves after bomb spawning so it will make trigger false of a bomb
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerView>() != null)
            {
                sphereCollider.isTrigger = false;
            }
        }
    }
}
