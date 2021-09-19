using UnityEngine;

/// <summary>
/// This class handles all player logic
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        private bool showGameOverScreen = true;

        // Consructor to set values
        public PlayerController(PlayerView _view, PlayerModel _model)
        {
            playerView = GameObject.Instantiate<PlayerView>(_view);
            playerModel = _model;
            playerView.SetController(this);
        }

        // This method used for player movement
        public void Movement()
        {
            float HorizontalMovement = Input.GetAxisRaw("Horizontal");
            float VerticalMovement = Input.GetAxisRaw("Vertical");

            if (HorizontalMovement > 0)
            {
                playerView.GetRigidBody().velocity = new Vector3(playerModel.movementSpeed, 0, 0);
            }
            else if (HorizontalMovement < 0)
            {
                playerView.GetRigidBody().velocity = new Vector3(-playerModel.movementSpeed, 0, 0);
            }
            else if (VerticalMovement > 0)
            {
                playerView.GetRigidBody().velocity = new Vector3(0f, 0, playerModel.movementSpeed);
            }
            else if (VerticalMovement < 0)
            {
                playerView.GetRigidBody().velocity = new Vector3(0f, 0, -playerModel.movementSpeed);
            }
            else
            {
                playerView.GetRigidBody().velocity = Vector3.zero;
            }
        }

        // This method used for destroying player object and showing game over panel
        public void PlayerDied()
        {
            if (playerView)
            {
                GameObject.Destroy(playerView.gameObject);
            }

            playerView = null;
            playerModel = null;

            if (showGameOverScreen)
            {
                UIManager.Instance.ShowGameOverScreen();
                SoundManager.Instance.Play(Sounds.LevelLose);
            }
        }
    }
}