using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles_Manager : MonoBehaviour
{
    [Header("TILE SETTINGS")]
    [SerializeField] Vector3 pos_offset;
    [SerializeField] float offset_z;

    [SerializeField] List<GameObject> tile_list = new List<GameObject>();

    [SerializeField] GameObject win_tile;
    bool spawn_win_tile = false;

    GameObject _tile;

    public void Prepare_Next_Tile()
    {
        if (spawn_win_tile)
        {
            Instantiate(win_tile, new Vector3(0, 0, pos_offset.z), Quaternion.identity);
            return;
        }

        foreach (var tmp in tile_list)
        {
            if (tmp.gameObject.activeInHierarchy == false)
            {
                _tile = tmp;
                tmp.SetActive(true);
                break;
            }
        }

        // make it first element
        tile_list.Remove(_tile);
        tile_list.Insert(0, _tile);

        _tile.transform.position = new Vector3(0, 0, pos_offset.z);

        pos_offset.z += offset_z;
    }

    public void Move_Tile_To_Pull(GameObject tmp_tile)
    {
        tile_list.Remove(tmp_tile);
        tile_list.Add(tmp_tile);

        tmp_tile.GetComponent<Tile_Settings>().Refresh_Tile();
        tmp_tile.SetActive(false);
    }

    public void Enable_Spawn_Win_Tile()
    {
        spawn_win_tile = true;
    }
}