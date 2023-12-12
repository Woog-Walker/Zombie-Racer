using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;

public class Canvas_Game_Manager : MonoBehaviour
{
    [SerializeField] Transform game_over_panel;
    [SerializeField] Transform game_win_panel;
    [Space]
    [SerializeField] Image distance_bar_fill;
    [SerializeField] Image background_fill;
    [Space]
    [SerializeField] TMP_Text distance_text;
    [SerializeField] Button start_button;
    [SerializeField] GameObject side_goal_panel;
    [Space]
    [SerializeField] Image settings_image;
    [SerializeField] Color color_disable;
    [SerializeField] Color color_enable;


    [Inject] Other_Settings other_settings;

    float anim_duration = 0.5f;

    public void Show_GameOver_Panel()
    {
        game_over_panel.gameObject.SetActive(true);
        background_fill.enabled = true;

        game_over_panel.DOScale(Vector3.one, anim_duration);
        game_over_panel.DOLocalMove(Vector3.zero, anim_duration).OnComplete(() =>
        {
            other_settings.Start_Slow_Motion();
        });
    }

    public void Show_Win_Panel()
    {
        game_win_panel.gameObject.SetActive(true);
        background_fill.enabled = true;

        game_win_panel.DOScale(Vector3.one, anim_duration);
        game_win_panel.DOLocalMove(Vector3.zero, anim_duration).OnComplete(() =>
        {
            other_settings.Start_Slow_Motion();
        });
    }

    public void Repaint_Distance_Bar(float distance_amount)
    {
        distance_bar_fill.fillAmount = distance_amount;
    }

    public void Repaint_Distnace_Text(float tmp_amount)
    {
        int distance_amount = (int)tmp_amount;

        distance_text.text = $"{distance_amount}m";
    }

    public void Prepair_Start_UI()
    {
        side_goal_panel.SetActive(true);

        side_goal_panel.transform.DOLocalMove(Vector3.zero, anim_duration);
        side_goal_panel.transform.DOScale(Vector3.one, anim_duration);

        settings_image.transform.DOLocalMove(new Vector3(settings_image.transform.localPosition.x - 500, settings_image.transform.localPosition.y, settings_image.transform.localPosition.z), 0.5f)
            .OnComplete(() => 
            {
                settings_image.gameObject.SetActive(false);
            });
    }

    public void Hide_Start_Button()
    {
        start_button.transform.DOMove(new Vector3(0, -1234, 0), anim_duration);
    }


    // 0 - low quality | 1 - high quality
    public void Change_Button_Settings(int option)
    {
        if (option == 0)
            settings_image.color = color_disable;

        if (option == 1)
            settings_image.color = color_enable;
    }
}