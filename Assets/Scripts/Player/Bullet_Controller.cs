using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] ParticleSystem blood_VFX;

    [SerializeField] Transform bullet_pool;

    float start_speed;
    private const string enemy_tag = "Enemy";

    private void Start()
    {
        start_speed = speed;
    }

    private void OnEnable()
    {
        // get vfx back to bullet
/*        blood_VFX.transform.SetParent(transform);
        blood_VFX.transform.localPosition = Vector3.zero;*/

        // if bullet flies for a long time - back it to pull
        StartCoroutine(Wait_And_Back_To_Pool());
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed);
    }

    IEnumerator Wait_And_Back_To_Pool()
    {
        yield return new WaitForSeconds(3);
        Refresh_Before_Pool();
    }

    public void Refresh_Before_Pool()
    {
        transform.gameObject.SetActive(false);

        blood_VFX.Stop();

        speed = start_speed;
        transform.localPosition = bullet_pool.transform.localPosition;

        transform.SetParent(bullet_pool);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemy_tag))
        {
            speed = 0;

            blood_VFX.Play();

            // select target to deduct health
            // pref enemy types via enums or abstract class but to save time -> next method

            if (other.GetComponent<Enemy_Controller>() != null)
                other.GetComponent<Enemy_Controller>().Deduct_Health(damage);

            if (other.GetComponent<Enemy_Controller_Patrool>() != null)
                other.GetComponent<Enemy_Controller_Patrool>().Deduct_Health(damage);

            StartCoroutine(Before_Pool_Delay());
        }
    }

    IEnumerator Before_Pool_Delay()
    {
        yield return new WaitForSeconds(1);
        Refresh_Before_Pool();
    }
}