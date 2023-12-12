using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Zen_Initialization : MonoInstaller
{
    [SerializeField] private Canvas_Game_Manager canvas_overall;
    [SerializeField] private Distance_Traveled distance_traveled;
    [SerializeField] private Start_Race_Manager start_race_manager;
    [SerializeField] private Other_Settings other_settings;
    [SerializeField] private Tiles_Manager tiles_manager;
    [SerializeField] private Player_Controller player_controller;
    [SerializeField] private Turret_Controller turret_controller;
    [SerializeField] private Player_Car_Health player_car_health;
    [SerializeField] private Camera_Manager camera_manager;
    [SerializeField] private Zone_Trigger zone_trigger;
    [SerializeField] private Enemy_Push_Away_Explo enemy_pusher_explo;
    [SerializeField] private Bullets_Pool bullet_pool;

    public override void InstallBindings()
    {
        Container.Bind<Canvas_Game_Manager>().FromInstance(canvas_overall);
        Container.Bind<Distance_Traveled>().FromInstance(distance_traveled);
        Container.Bind<Start_Race_Manager>().FromInstance(start_race_manager);
        Container.Bind<Other_Settings>().FromInstance(other_settings);
        Container.Bind<Tiles_Manager>().FromInstance(tiles_manager);
        Container.Bind<Player_Controller>().FromInstance(player_controller);
        Container.Bind<Turret_Controller>().FromInstance(turret_controller);
        Container.Bind<Player_Car_Health>().FromInstance(player_car_health);
        Container.Bind<Camera_Manager>().FromInstance(camera_manager);
        Container.Bind<Zone_Trigger>().FromInstance(zone_trigger);
        Container.Bind<Enemy_Push_Away_Explo>().FromInstance(enemy_pusher_explo);
        Container.Bind<Bullets_Pool>().FromInstance(bullet_pool);
    }
}