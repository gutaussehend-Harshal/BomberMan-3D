using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    private PlayerController playerController;
    private PlayerModel playerModel;
    [SerializeField] private PlayerScriptableObject playerScriptableObject;

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

    void Start()
    {
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        playerModel = new PlayerModel(playerScriptableObject);
        playerController = new PlayerController(playerScriptableObject.playerView, playerModel);
    }
}
