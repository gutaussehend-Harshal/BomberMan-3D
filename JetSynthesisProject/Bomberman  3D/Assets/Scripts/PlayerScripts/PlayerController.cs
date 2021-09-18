using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerView playerView;
    private PlayerModel playerModel;

    public PlayerController(PlayerView _view, PlayerModel _model)
    {
        playerView = GameObject.Instantiate<PlayerView>(_view);
        playerModel = _model;
        playerView.SetController(this);
    }
    
    public PlayerView GetPlayerView()
    {
        return playerView;
    }

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

    public void PlayerDied(bool showGameOverScreen = true)
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
        }
    }
}
