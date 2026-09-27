using Godot;
using System;
public partial class Gun:Node2D
{
	[Export] public PackedScene BulletScene {get; set;}
	Marker2D Muzzle;
    public override void _Ready()
	{
		Muzzle = GetChild<Marker2D>(1);
	}
    public override void _Process(double delta)
    {
        LookAt(GetGlobalMousePosition());
		
		Vector2 nuevaEscala = Scale;

		RotationDegrees = Mathf.Wrap(RotationDegrees,0,360);
		if (RotationDegrees>90 && RotationDegrees < 270)
		{
			nuevaEscala.Y = -1;
		}
		else
		{
			nuevaEscala.Y = 1;
		}
		
		Scale = nuevaEscala;
    }
	public void Shoot()
	{
		var bullet_inst =(Bullet)BulletScene.Instantiate();
		GetTree().Root.AddChild(bullet_inst);
		bullet_inst.GlobalPosition = Muzzle.GlobalPosition;
		bullet_inst.Rotation = Rotation;
	}
}


// extends Node2D

// const BULLET = preload("res://scenes/bullet.tscn")
// @onready var muzzle: Marker2D = $Marker2D

// func _process(_delta: float) -> void:
// 	look_at(get_global_mouse_position())
// 	rotation_degrees = wrap(rotation_degrees,0,360)
// 	if rotation_degrees > 90 and rotation_degrees < 270:
// 		scale.y = -	1
// 	else:
// 		scale.y = 1
	
// func shoot():
// 		var bullet_instance = BULLET.instantiate()
// 		get_tree().root.add_child(bullet_instance)
// 		bullet_instance.global_position = muzzle.global_position
// 		bullet_instance.rotation = rotation
