using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Destroyer_Timer : MonoBehaviour
{
    [SerializeField] float time_to_destroy;

    private void Start()
    {
        Destroy(transform.gameObject, time_to_destroy);
    }
}