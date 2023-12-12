using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Element_LookAt : MonoBehaviour
{
    [SerializeField] Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(_camera.transform);
        transform.Rotate(0, 180, 0);
    }
}