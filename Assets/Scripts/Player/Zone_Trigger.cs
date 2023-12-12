using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Zone_Trigger : MonoBehaviour
{
    [Inject] Tiles_Manager tiles_manager;
    [Inject] Canvas_Game_Manager canvas_overall;

    const string tile_spawn_tag = "Tile_Spawn_Tag";
    const string tile_win_tag = "Win_Zone";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tile_spawn_tag))
        {
            tiles_manager.Prepare_Next_Tile();
            other.GetComponentInParent<Tile_Settings>().Move_To_Pull();
        }

        if (other.CompareTag(tile_win_tag))
            canvas_overall.Show_Win_Panel();
    }
}