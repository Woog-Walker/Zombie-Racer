using UnityEngine;
 
public class Enemy_Controller_Patrool : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] float coins_per_death;
    [Space]
    [SerializeField] float chase_speed;
    [SerializeField] float patrol_speed;
    [SerializeField] float distance_to_chase;
    [Space]
    [SerializeField] Transform player;
    [SerializeField] Transform enemy_gfx;
    [Space]
    [SerializeField] ParticleSystem hit_vfx;
    [SerializeField] CapsuleCollider capsule_collider;

    Enemy_Animator enemy_animator;
    Canvas_Enemy_Controller canvas_controller;

    [SerializeField] Transform[] travel_points;
    float distance_to_point;
    int current_point = 1;
    bool is_patrooling = true;

    float turn_speed = 7.5f;
    float distance_to_player;
    float max_health;
    bool is_dead = false;
    const string player_tag = "Player_Car";

    Vector3 start_transofrm_position;
    Vector3 start_transofrm_rotation;

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

        player = GameObject.FindGameObjectWithTag(player_tag).transform;
    }

    private void FixedUpdate()
    {
        distance_to_player = Vector3.Distance(transform.position, player.position); // distance to player

        if (is_patrooling)
            Patrol();

        if (!is_patrooling)
        {
            Move_To_Target();
            Rotate_To_Player();
        }

        if (distance_to_player < distance_to_chase)
            is_patrooling = false;
    }

    void Patrol()
    {
        distance_to_point = Vector3.Distance(transform.position, travel_points[current_point].position);

        Move_To_Target();
        Rotate_To_Point();

        if (distance_to_point < 0.25f)
        {
            current_point++;
            if (current_point >= travel_points.Length) current_point = 0;
        }
    }

    void Move_To_Target()
    {
        if (is_patrooling)
        {
            transform.Translate(Vector3.forward * patrol_speed * Time.deltaTime);
            enemy_animator.Play_Anim_Walk();
        }

        if (!is_patrooling)
        {
            transform.Translate(Vector3.forward * chase_speed * Time.deltaTime);
            enemy_animator.Play_Anim_Run();
        }
    }

    void Rotate_To_Point()
    {
        Vector3 direction_to_point = travel_points[current_point].position - transform.position;

        Quaternion rotation_to_point = Quaternion.LookRotation(direction_to_point);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction_to_point), Time.deltaTime * turn_speed);
    }

    void Rotate_To_Player()
    {
        Vector3 direction_to_player = player.position - transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction_to_player), Time.deltaTime * turn_speed);
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

        enemy_animator.Enable_RagDoll_Death();

        canvas_controller.Disable_HealthBar();
        canvas_controller.Update_Death_UI(coins_per_death);

        capsule_collider.enabled = false;


        this.enabled = false;
    }

    public void Death_From_Hit()
    {
        is_dead = true;

        hit_vfx.Play();

        // calculate  rb push power on hit
        Vector3 push_direction = transform.position - player.position;

        if (enemy_gfx.gameObject.activeInHierarchy)
            enemy_animator.Enable_RagDoll_Death_Hit(push_direction);

        canvas_controller.Disable_HealthBar();
        canvas_controller.Update_Death_UI(coins_per_death);

        capsule_collider.enabled = false;

        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player_tag))
        {
            Death_From_Hit();
            FindObjectOfType<Player_Car_Health>().Deduct_Health(damage);
        }
    }

    public void Refresh_NPC()
    {
        transform.tag = "Enemy";

        is_dead = false;
        health = max_health;
        is_patrooling = true;

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