using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Push_Away_Explo : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies_list = new List<GameObject>();
    [SerializeField] SphereCollider sphere_collider;

    const string enemy_tag = "Enemy";

    public  void Push_Enemies_By_Explosion()
    {
        sphere_collider.enabled = false;

        foreach (var tmpEnemy in enemies_list)
        {
            if (tmpEnemy == null) continue;

            if (tmpEnemy.GetComponent<Enemy_Controller>() != null)
                tmpEnemy.GetComponent<Enemy_Controller>().Death_From_Hit();

            if (tmpEnemy.GetComponent<Enemy_Controller_Patrool>() != null)
                tmpEnemy.GetComponent<Enemy_Controller_Patrool>().Death_From_Hit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemy_tag))
            enemies_list.Add(other.gameObject);
    }

    private void OnTiggerExit(Collider other)
    {
        if (other.CompareTag(enemy_tag))
            enemies_list.Remove(other.gameObject);
    }
}