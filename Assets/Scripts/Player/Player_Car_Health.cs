using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Player_Car_Health : MonoBehaviour
{
    [SerializeField] float health;
    float max_health;
    [Space]
    [SerializeField] GameObject current_car;
    [SerializeField] GameObject broken_car;
    [SerializeField] Rigidbody[] wheels_rb;
    [Space]
    [SerializeField] ParticleSystem explosion_VFX;
    [Space]
    [SerializeField] Canvas_Car canvas_car;

    [Inject] Player_Controller player_controller;
    [Inject] Turret_Controller turret_controller;
    [Inject] Canvas_Game_Manager canvas_overlay;
    [Inject] Enemy_Push_Away_Explo enemy_pusher;
    [Inject] Camera_Manager camera_manager;

    private void Start()
    {
        max_health = health;
    }

    public void Deduct_Health(float dmgAmount)
    {
        health -= dmgAmount;

        canvas_car.Update_UI_Health_Bar(health / max_health);
        Hit_Effects();

        if (health <= 0) Nuke_Car();
    }

    private void Hit_Effects()
    {
        camera_manager.Shake_Camera();
        Handheld.Vibrate();
    }

    private void Nuke_Car()
    {
        player_controller.Car_Stop();

        current_car.SetActive(false);
        broken_car.SetActive(true);

        enemy_pusher.Push_Enemies_By_Explosion();
        enemy_pusher.gameObject.SetActive(false);

        explosion_VFX.transform.SetParent(null);

        broken_car.GetComponent<Rigidbody>().AddForce(Vector3.up * 35, ForceMode.Impulse);

        broken_car.transform.DORotate(new Vector3(broken_car.transform.eulerAngles.x + 280, broken_car.transform.eulerAngles.y, broken_car.transform.eulerAngles.z), 1.5f);

        for (int i = 0; i < wheels_rb.Length; i++)
        {
            wheels_rb[i].isKinematic = false;
            wheels_rb[i].AddForce((Vector3.up + Vector3.back) * 25, ForceMode.Impulse);
        }

        canvas_car.Hide_UI_Health_Bar();

        StartCoroutine(Show_UI_Delay());

        turret_controller.enabled = false;
        player_controller.enabled = false;
        camera_manager.enabled = false;
    }

    IEnumerator Show_UI_Delay()
    {
        yield return new WaitForSeconds(1.5f);
        canvas_overlay.Show_GameOver_Panel();
    }
}