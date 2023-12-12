using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Canvas_Enemy_Controller : MonoBehaviour
{
    [SerializeField] Image health_bar_fill;
    [SerializeField] GameObject health_bar;
    [Space]
    [SerializeField] GameObject damage_text_prefab;
    [SerializeField] Transform damage_text_pos;
    [Space]
    [SerializeField] GameObject death_panel_prefab;
    [SerializeField] Transform death_panel_pos;

    public void Update_Helath_Bar(float fill_amount)
    {
        health_bar_fill.fillAmount = fill_amount;
    }

    public void Update_Damage_Text(float damage_amount)
    {
        health_bar.SetActive(true);

        var damage_text = Instantiate(damage_text_prefab, damage_text_pos);

        damage_text.GetComponent<Damage_Text_Counter>().Show_Text_Damage(damage_amount);
    }

    public void Update_Death_UI(float coinsAmount)
    {
        var death_panel = Instantiate(death_panel_prefab, death_panel_pos);
        death_panel.GetComponent<Death_Panel_Controller>().Show_Panel_Death(coinsAmount);
    }

    public void Disable_HealthBar()
    {
        health_bar.gameObject.SetActive(false);
    }
}