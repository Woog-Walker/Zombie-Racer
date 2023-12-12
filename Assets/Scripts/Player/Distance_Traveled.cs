using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Distance_Traveled : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform start_position;
    [Space]
    [SerializeField] float distance_current;
    [SerializeField] float distance_needed;

    [Inject] Canvas_Game_Manager canvas_game_manager;
    [Inject] Tiles_Manager tiles_manager;

    private void Update()
    {
        distance_current = Vector3.Distance(start_position.position, player.position);

        canvas_game_manager.Repaint_Distnace_Text(distance_current);
        canvas_game_manager.Repaint_Distance_Bar(distance_current / distance_needed);

        if (distance_current > distance_needed)        
            Enable_Win_Tile();        
    }

    public void Enable_Win_Tile()
    {
        tiles_manager.Enable_Spawn_Win_Tile();
    }
}