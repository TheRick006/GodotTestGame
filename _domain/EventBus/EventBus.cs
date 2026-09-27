using System;
using System.Collections.Generic;
using Godot;

public partial class EventBus:Node
{
    public static EventBus Instance {get;private set;}
    [Signal]
    public delegate void MobDiedEventHandler();
    [Signal]
    public delegate void PlayerDiedEventHandler();
    [Signal]
    public delegate void StartGameEventHandler();
    
    public override void _Ready()
    {
if (Instance != null)
        {
            QueueFree();
            return;
        }
        Instance = this;
    }
}