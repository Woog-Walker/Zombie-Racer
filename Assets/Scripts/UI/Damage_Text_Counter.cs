using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Damage_Text_Counter : MonoBehaviour
{
    [SerializeField] TMP_Text text_damage;
    [SerializeField] Color completeColor;

    float anim_duration = 1;
    float offset_y = 450;

    public void Show_Text_Damage(float tmpDamage)
    {
        text_damage.text = tmpDamage.ToString();

        // appends or regular ..

        text_damage.DOColor(completeColor, anim_duration);
        text_damage.rectTransform.DOScale(Vector3.one, anim_duration);

        text_damage.rectTransform
            .DOLocalMove(new Vector3(transform.localPosition.x, transform.localPosition.y + offset_y, transform.localPosition.z), anim_duration)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => { Destroy(transform.gameObject); });
    }
}