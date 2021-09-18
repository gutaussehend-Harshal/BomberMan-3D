using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class DestroyBreakableBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DestroyMe>() != null)
        {
            Destroy(gameObject);
        }
    }
}
