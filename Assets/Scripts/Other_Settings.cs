using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Zenject;

public class Other_Settings : MonoBehaviour
{
    [SerializeField] int target_fps;

    // slow motion vars
    float time_current = 1;
    float time_target = 0;
    float duration = 1;

    bool HD_settings = true;

    [Space]
    [Header("VIDEO SETTINGS")]
    [SerializeField] GameObject post_processing;
    [SerializeField] GameObject light_settings_low;
    [SerializeField] GameObject light_settings_high;

    [Space]
    [Inject] Canvas_Game_Manager canvas_overlay;

    private void Start()
    {
        Time.timeScale = 1;
        DOTween.KillAll();

        Set_FPS();
    }

    void Set_FPS()
    {
        Application.targetFrameRate = target_fps;
    }

    public void Restart_Scene()
    {
        SceneManager.LoadScene(0);
    }

    public void Start_Slow_Motion()
    {
        DOTween.To(() => time_current, x => time_current = x, time_target, duration).OnUpdate(() =>
        {
            Time.timeScale = time_current;
        });
    }

    public void Quality_Settings()
    {
        if (HD_settings)
        {
            HD_settings = false;

            QualitySettings.SetQualityLevel(0, true);
            post_processing.SetActive(false);

            light_settings_low.SetActive(true);
            light_settings_high.SetActive(false);

            canvas_overlay.Change_Button_Settings(0);

            return;
        }

        if (!HD_settings)
        {
            HD_settings = true;

            QualitySettings.SetQualityLevel(1, true);
            post_processing.SetActive(true);

            light_settings_high.SetActive(true);
            light_settings_low.SetActive(false);

            canvas_overlay.Change_Button_Settings(1);

            return;
        }
    }
}