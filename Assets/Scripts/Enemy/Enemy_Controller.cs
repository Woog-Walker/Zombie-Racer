using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] float coins_per_death;
    [Space]
    [SerializeField] float move_speed;
    [SerializeField] float distance_to_chase;
    [Space]
    [SerializeField] Transform player;
    [SerializeField] Transform enemy_gfx;
    [Space]
    [SerializeField] ParticleSystem hit_vfx;
    [SerializeField] CapsuleCollider capsule_collider;

    Enemy_Animator enemy_animator;
    Canvas_Enemy_Controller canvas_controller;

    Vector3 start_transofrm_position;
    Vector3 start_transofrm_rotation;

    float distance_to_player;
    float max_health;
    bool is_dead = false;
    const string player_tag = "Player_Car";

    private void Awake()
    {
        enemy_animator = GetComponentInChildren<Enemy_Animator>();
        canvas_controller = GetComponentInChildren<Canvas_Enemy_Controller>();
    }

    private void Start()
    {
        max_health = health;
        start_transofrm_position = transform.localPosition;
        start_transofrm_rotation = transform.localEulerAngles;

        enemy_animator.Play_Anim_Idle();

        player = GameObject.FindGameObjectWithTag(player_tag).transform;
    }

    private void FixedUpdate()
    {
        distance_to_player = Vector3.Distance(transform.position, player.position); // distance to player

        if (distance_to_player < distance_to_chase)
        {
            Rotate_To_Player();
            Chase_Player();
        }
    }

    void Chase_Player()
    {
        transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
        enemy_animator.Play_Anim_Run();
    }

    void Rotate_To_Player()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);

        transform.rotation = rotationToPlayer;
    }

    public void Deduct_Health(float damage_amount)
    {
        if (is_dead) return;

        health -= damage_amount;

        canvas_controller.Update_Helath_Bar(health / max_health);
        canvas_controller.Update_Damage_Text(damage_amount);

        if (health <= 0) Death();
    }

    private void Death()
    {
        is_dead = true;
        transform.tag = "Test_Tag";

        capsule_collider.enabled = false;

        enemy_animator.Enable_RagDoll_Death();

        canvas_controller.Disable_HealthBar();
        canvas_controller.Update_Death_UI(coins_per_death);

        this.enabled = false;
    }

    public void Death_From_Hit()
    {
        is_dead = true;
        transform.tag = "Test_Tag";

        hit_vfx.Play();

        capsule_collider.enabled = false;


        Vector3 push_direction = transform.position - player.position;

        if (enemy_gfx.gameObject.activeInHierarchy)
            enemy_animator.Enable_RagDoll_Death_Hit(push_direction);

        canvas_controller.Disable_HealthBar();
        canvas_controller.Update_Death_UI(coins_per_death);

        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player_tag))
        {
            FindObjectOfType<Player_Car_Health>().Deduct_Health(damage);
            Death_From_Hit();
        }
    }   

    public void Refresh_NPC()
    {
        transform.tag = "Enemy";

        is_dead = false;
        health = max_health;

        transform.localPosition = start_transofrm_position;
        transform.localEulerAngles = start_transofrm_rotation;

        enemy_gfx.transform.localPosition = Vector3.zero;
        enemy_gfx.transform.localRotation = Quaternion.identity;

        capsule_collider.enabled = true;

        enemy_animator.Enable_Aimator();
        enemy_animator.Play_Anim_Idle();
        canvas_controller.Update_Helath_Bar(1);
    }
}