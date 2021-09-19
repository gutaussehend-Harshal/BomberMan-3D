using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores player data using scriptable object
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player/NewPlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        [Header("Player Settings")]
        public int health;
        public float movementSpeed;
        public PlayerView playerView;
    }
}