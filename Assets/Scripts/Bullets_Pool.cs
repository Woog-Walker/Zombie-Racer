using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_Pool : MonoBehaviour
{
    [SerializeField] GameObject bullet_prefab;

    [SerializeField] List<GameObject> bullets_list = new List<GameObject>();

    public GameObject Get_Bullet_From_Pull()
    {
        for (int i = 0; i < bullets_list.Count; i++)
        {
            if (!bullets_list[i].activeInHierarchy)
            {
                return bullets_list[i];
            }
        }

        // if no active bullet - create new and add to pool
        GameObject new_bullet = Instantiate(bullet_prefab, transform);
        new_bullet.SetActive(false);
        bullets_list.Add(new_bullet);

        return new_bullet;
    }


}