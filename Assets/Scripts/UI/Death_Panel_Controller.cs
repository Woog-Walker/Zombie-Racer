using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Death_Panel_Controller : MonoBehaviour
{
    [SerializeField] TMP_Text text_damage;

    float anim_duration = 1;
    float offset_y = 450;

    public void Show_Panel_Death(float tmpCoins)
    {
        text_damage.text = tmpCoins.ToString();

        // appends or regular ..

        transform.DOScale(Vector3.one, anim_duration);

        transform
            .DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + offset_y, transform.localPosition.z), anim_duration)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => { Destroy(transform.gameObject); });
    }
}