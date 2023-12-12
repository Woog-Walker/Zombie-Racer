using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animator : MonoBehaviour
{
    [SerializeField] float hit_force;
    [SerializeField] Rigidbody part_to_push;
    [Space]
    [SerializeField] Rigidbody[] ragdoll_rbs;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Play_Anim_Walk()
    {
        animator.SetFloat("posX", 1, 0.1f, Time.deltaTime);
        animator.SetFloat("posY", 0, 0.1f, Time.deltaTime);
    }

    public void Play_Anim_Run()
    {
        animator.SetFloat("posX", 0, 0.1f, Time.deltaTime);
        animator.SetFloat("posY", 1, 0.1f, Time.deltaTime);
    }

    public void Play_Anim_Idle()
    {
        animator.SetFloat("posX", 0, 0.1f, Time.deltaTime);
        animator.SetFloat("posY", 0, 0.1f, Time.deltaTime);
    }

    public void Enable_RagDoll_Death()
    {
        StartCoroutine(Ragdoll_Delay_Death());
    }

    IEnumerator Ragdoll_Delay_Death()
    {
        animator.enabled = false;

        yield return new WaitForEndOfFrame();

        foreach (var tmp in ragdoll_rbs)
            tmp.isKinematic = false;
    }

    public void Enable_RagDoll_Death_Hit(Vector3 push_direction)
    {
        StartCoroutine(Ragdoll_Delay_Hit(push_direction));
    }

    IEnumerator Ragdoll_Delay_Hit(Vector3 push_direction)
    {
        animator.enabled = false;

        yield return new WaitForEndOfFrame();

        foreach (var tmp in ragdoll_rbs)
            tmp.isKinematic = false;

        yield return new WaitForEndOfFrame();

        part_to_push.AddForce(Vector3.up * hit_force * 3, ForceMode.Impulse);
        part_to_push.AddForce(push_direction * hit_force, ForceMode.Impulse);
    }

    public void Enable_Aimator()
    {
        animator.enabled = true;
    }
}