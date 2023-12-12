using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Start_Race_Manager : MonoBehaviour
{
    [SerializeField] Item_Destroyer_Timer item_Destroyer_Timer; // enable first tile auto destruction

    [Inject] Camera_Manager camera_manager;
    [Inject] Canvas_Game_Manager canvas_overlay;
    [Inject] Player_Controller car_controller;

    public void Start_Race()
    {
        StartCoroutine(Start_Delay());
    }

    IEnumerator Start_Delay()
    {
        Debug.Log("START DELAY");
        canvas_overlay.Hide_Start_Button();
        yield return new WaitForSeconds(0.5f);

        canvas_overlay.Prepair_Start_UI();
        camera_manager.Move_Camera_To_Start();
        yield return new WaitForSeconds(1);

        item_Destroyer_Timer.enabled = true;
        car_controller.Car_Increase_Speed();
    }
}