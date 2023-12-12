using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Car : MonoBehaviour
{
    [SerializeField] Image health_bar;
    [SerializeField] GameObject health_bar_object;

    public void Update_UI_Health_Bar(float health_amount)
    {
        health_bar.fillAmount = health_amount;
    }

    public void Hide_UI_Health_Bar()
    {
        health_bar_object.SetActive(false);
    }
}