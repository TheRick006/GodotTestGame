using System;
using System.Globalization;
using Godot;

public partial class Main:Node
{

    //Nota     -/   => Signnifica que ya esta hecho lo que dice el comentario
    PackedScene mobScene = GD.Load<PackedScene>("res://scenes/mob.tscn");
    int time;
    int score;
    // Node currentScene;
    Player player;
    Marker2D starterPosition;
    Timer MobTimer;
    Timer StartTimer;
    Timer ScoreTimer;
    Hud HUD;
    ColorRect HUDColorRect;
    AudioStreamPlayer Music;
    AudioStreamPlayer DeathSound;
    public override void _Ready()
    {
        // currentScene = GetTree().CurrentScene;
        player = GetNode<Player>("Player");
        starterPosition = GetChild<Marker2D>(5);
        MobTimer = GetChild<Timer>(2);
        ScoreTimer = GetChild<Timer>(3);
        StartTimer = GetChild<Timer>(4);
        HUD = GetNode<Hud>("HUD");
        HUDColorRect = HUD.GetChild<ColorRect>(0);
        Music = GetChild<AudioStreamPlayer>(8);
        DeathSound = GetChild<AudioStreamPlayer>(9);

        EventBus.Instance.PlayerDied += GameOver;
        EventBus.Instance.MobDied += _OnUpdateScore;
        EventBus.Instance.StartGame += NewGame;
    }
public void GameOver()
    {
        ScoreTimer.Stop();
        MobTimer.Stop();
        HUD.ShowGameOver();
        HUD.UnsetTransparency();
        Music.Stop();
        DeathSound.Play();
        HUDColorRect.Visible = true;
        GetTree().CallGroup("mobs", Node.MethodName.QueueFree);
    }
    public void NewGame()
    {
        Init();
    }
    public void _OnMobTimerTimeout()  // Conectar desde godot -/
    {
        RigidBody2D mob = (RigidBody2D) mobScene.Instantiate();
        Path2D MobPath = GetChild<Path2D>(6);
        PathFollow2D MobSpawnLocation = MobPath.GetChild<PathFollow2D>(0);
        RandomNumberGenerator randNumber = new RandomNumberGenerator();
        MobSpawnLocation.ProgressRatio = randNumber.Randf();
        mob.Position=MobSpawnLocation.Position;
        float direction = MobSpawnLocation.Rotation+ (float.Pi / 2);
        direction += randNumber.RandfRange(-float.Pi/4, float.Pi/4);
        mob.Rotation = direction;
        Vector2 velocity = new Vector2(randNumber.RandfRange(150.0f,250.0f),0.0f);
        mob.LinearVelocity = velocity.Rotated(direction);
        AddChild(mob);
    }
public void Init()
    {
        time=0;
        score=0;
        player.start(starterPosition.Position);
        StartTimer.Start();
        HUDColorRect.Visible = false;
        HUD.UpdateTime(time);
        HUD.UpdateScore(time);
        HUD.SetTransparency();
        HUD.ShowMessage("Get Ready!");
        Music.Play();
        
    }
public void _OnScoreTimerTimeout() //Conectar desde godot -/
    {
        time += 1;
        HUD.UpdateTime(time);
    }
public void _OnUpdateScore() 
    {//Este ya esta conectado
        score +=1;
        HUD.UpdateScore(score);
    }

public void _OnStartTimerTimeout() // Conectar desde godot -/
    {
        MobTimer.Start();
        ScoreTimer.Start();
    }
}
#region //Completado
// extends Node
// #TODO: Añadir el soporte para controles al "Juego"

// @export var mob_scene: PackedScene
// var time
// var score


// func _ready():
// 	GlobalEventBus.playerDied.connect(game_over)
// 	GlobalEventBus.mobDied.connect(_on_updateScore)
// 	pass

// func game_over() -> void:
// 	$ScoreTimer.stop()
// 	$MobTimer.stop()
// 	$HUD.show_game_over()
// 	$HUD.unset_transparency()
// 	$Music.stop()
// 	$DeathSound.play()
// 	$HUD/ColorRect.visible = true
	
// func new_game():
// 	init()


// func _on_mob_timer_timeout() -> void:
// 	var mob = mob_scene.instantiate()
// 	var mob_spawn_location = $MobPath/MobSpawnLocation
// 	mob_spawn_location.progress_ratio = randf()
// 	mob.position = mob_spawn_location.position
// 	var direction = mob_spawn_location.rotation + PI/2	
// 	direction += randf_range(-PI / 4, PI /4)
// 	mob.rotation = direction
// 	var velocity = Vector2(randf_range(150.0,250.0),0.0)
// 	mob.linear_velocity = velocity.rotated(direction)
// 	add_child(mob)


// func init()->void:
// 	time=0
// 	score=0
	
// 	$Player.start($StartPosition.position)
// 	$StartTimer.start()
// 	$HUD/ColorRect.visible = false
// 	$HUD.update_time(time)
// 	$HUD.update_score(score)
// 	$HUD.set_transparency()
// 	$HUD.show_message("Get Ready!")
// 	get_tree().call_group('mobs', 'queue_free')
	
// 	$Music.play()

// func _on_score_timer_timeout() -> void:
// 	time += 1
// 	$HUD.update_time(time)

// func _on_updateScore()-> void:
// 	score+=1
// 	$HUD.update_score(score)

// func _on_start_timer_timeout() -> void:
// 	$MobTimer.start()
// 	$ScoreTimer.start()
#endregion
