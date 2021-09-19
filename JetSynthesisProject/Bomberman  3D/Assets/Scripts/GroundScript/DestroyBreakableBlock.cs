using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles destroying of breakable blocks
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class DestroyBreakableBlock : MonoBehaviour
    {
        // This method used for destroying brekable blocks
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<DestroyMe>() != null)
            {
                Destroy(gameObject);
            }
        }
    }
}