using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tile_Settings : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies_list = new List<GameObject>();

    [Inject] Tiles_Manager tiles_manager;

    public void Refresh_Tile()
    {
        foreach (var tpm_enemy in enemies_list)
        {
            if (tpm_enemy.GetComponent<Enemy_Controller_Patrool>() != null)
            {
                tpm_enemy.GetComponent<Enemy_Controller_Patrool>().enabled = true;
                tpm_enemy.GetComponent<Enemy_Controller_Patrool>().Refresh_NPC();
            }

            if (tpm_enemy.GetComponent<Enemy_Controller>() != null)
            {
                tpm_enemy.GetComponent<Enemy_Controller>().enabled = true;
                tpm_enemy.GetComponent<Enemy_Controller>().Refresh_NPC();
            }
        }
    }

    public void Move_To_Pull()
    {
        tiles_manager.Move_Tile_To_Pull(transform.gameObject);
    }
}