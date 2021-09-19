using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles explosion prefab
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class DestroyMe : MonoBehaviour
    {
        [SerializeField] private float delay = 0.5f;

        // In this start method explosion will destroy after some delay
        void Start()
        {
            Debug.Log("Destroy Successful");
            Destroy(gameObject, delay);
            SoundManager.Instance.Play(Sounds.Explosion);
        }

        // This method used for destroying explosion prefab when there is wall and unbreakable block
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("UnbreakableBlock"))
            {
                Destroy(gameObject);
            }
        }
    }
}