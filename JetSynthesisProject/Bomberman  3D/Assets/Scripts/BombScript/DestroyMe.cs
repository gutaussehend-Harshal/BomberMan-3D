using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class DestroyMe : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f;

    void Start()
    {
        Debug.Log("Destroy Successful");
        Destroy(this.gameObject, delay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UnbreakableBlock"))
        {
            Destroy(this.gameObject);
        }
        // if (other.gameObject.GetComponent<DestroyMe>() == null)
        // {
        //     Destroy(other.gameObject, 1f);
        // }
        // else if (other.gameObject.GetComponent<BombController>() != null)
        // if (other.gameObject.GetComponent<BombController>() != null)
        // {
        //     other.gameObject.GetComponent<BombSpawner>().Explosion();
        // }
    }
}
