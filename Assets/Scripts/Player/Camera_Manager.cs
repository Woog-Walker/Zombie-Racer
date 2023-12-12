using UnityEngine;
using DG.Tweening;

public class Camera_Manager : MonoBehaviour
{
    [SerializeField] Vector3 camera_offset;
    [SerializeField] Vector3 camera_rotation;

    [Space]
    [Header("TRANSFORMS")]
    [SerializeField] Transform camera_transform;
    [SerializeField] Transform holder_transform;
    [SerializeField] Transform player_transform;

    float anim_duration = 1f;
    bool ready_to_race = false;

    private void FixedUpdate()
    {
        if (!ready_to_race) return;

        Camera_Position();
        Camera_Rotation();
    }

    void Camera_Rotation() { holder_transform.rotation = Quaternion.Euler(camera_rotation); }

    void Camera_Position() { holder_transform.position = player_transform.position + camera_offset; }


    public void Shake_Camera()
    {
        camera_transform.DOShakePosition(0.5f, 1);
    }

    public void Move_Camera_To_Start()
    {
        holder_transform.transform.DOLocalMove(player_transform.position + camera_offset, anim_duration);
        holder_transform.transform.DOLocalRotate(camera_rotation, anim_duration).OnComplete(() =>
        {
            ready_to_race = true;
        });
    }
}