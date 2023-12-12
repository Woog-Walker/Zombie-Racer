using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Turret_Controller : MonoBehaviour
{
    [SerializeField] float shoot_range;
    [SerializeField] float shoot_cd;
    [Space]
    [SerializeField] Transform turret;
    [SerializeField] Transform shoot_point;
    [SerializeField] Transform pointer_place;
    [Space]
    [SerializeField] ParticleSystem shoot_vfx;
    [Space]
    [SerializeField] LayerMask layer_mask;
    [Space]
    [SerializeField] float rotation_speed;
    [SerializeField] float touch_rotation_speed;

    [Inject] Bullets_Pool bullet_pool;

    bool can_shoot = true;
    private const string enemy_tag = "Enemy";

    private void FixedUpdate()
    {
        Ray_Checker();

#if UNITY_ANDROID
        Rotate_Gun_Touch();
#endif

#if UNITY_EDITOR
        Rotate_Gun_Editor();
#endif
    }

    private void Rotate_Gun_Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotation_amount = touch.deltaPosition.x;

                Quaternion target_rotation = Quaternion.Euler(0f, rotation_amount, 0f);

                turret.rotation = Quaternion.Slerp(turret.rotation, turret.rotation * target_rotation, touch_rotation_speed * Time.fixedDeltaTime);
            }
        }
    }

    private void Rotate_Gun_Editor()
    {
        float rotation_input = Input.GetAxis("Horizontal");

        float rotation_amount = rotation_input * rotation_speed * Time.deltaTime;

        turret.Rotate(Vector3.up, rotation_amount);
    }

    private void Ray_Checker()
    {
        if (!can_shoot) return;

        Vector3 ray_direction = pointer_place.position - shoot_point.position;
        Ray ray = new Ray(shoot_point.position, ray_direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shoot_range, ~layer_mask))
            if (hit.transform.CompareTag(enemy_tag))
                Perform_Shoot();
    }

    private void Perform_Shoot()
    {
        GameObject _bullet = bullet_pool.Get_Bullet_From_Pull();

        _bullet.transform.SetParent(null);
        _bullet.transform.position = shoot_point.position;
        _bullet.transform.rotation = Quaternion.Euler(0, turret.transform.localEulerAngles.y, 0);

        _bullet.SetActive(true);

        shoot_vfx.Play();

        StartCoroutine(CD_Delay());
    }

    IEnumerator CD_Delay()
    {
        can_shoot = false;
        yield return new WaitForSeconds(shoot_cd);
        can_shoot = true;
    }
}