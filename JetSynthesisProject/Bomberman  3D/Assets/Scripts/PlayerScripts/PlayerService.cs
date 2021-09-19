using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles player service which inherites from MonoSingletonGeneric class
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class PlayerService : MonoSingletonGeneric<PlayerService>
    {
        private PlayerController playerController;
        private PlayerModel playerModel;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // This methode return playerController
        public PlayerController GetPlayerController()
        {
            return playerController;
        }

        void Start()
        {
            CreatePlayer();
        }

        // This method used for create player
        public void CreatePlayer()
        {
            playerModel = new PlayerModel(playerScriptableObject);
            playerController = new PlayerController(playerScriptableObject.playerView, playerModel);
        }
    }
}