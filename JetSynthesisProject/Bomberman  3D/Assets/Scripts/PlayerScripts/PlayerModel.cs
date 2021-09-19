using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles player model
/// </summary>

namespace JetSynthesis.BomberMan3D
{
public class PlayerModel
{
    public float movementSpeed { get; private set; }
    public int health { get; private set; }

    // Constructor used to set values using scriptable object
    public PlayerModel(PlayerScriptableObject playerSO)
    {
        movementSpeed = playerSO.movementSpeed;
        health = playerSO.health;
    }
}
}