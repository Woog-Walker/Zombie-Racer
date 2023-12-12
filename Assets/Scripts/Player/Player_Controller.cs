using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float speed;
    public float current_speed = 0;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * current_speed * Time.fixedDeltaTime);
    }

    public void Car_Increase_Speed()
    {
        Debug.Log("CAR SPEED");


        DOTween.To(() => current_speed, x => current_speed = x, speed, 1);
    }

    public void Car_Stop()
    {
        speed = 0;
    }
}