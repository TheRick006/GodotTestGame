using System;
using Godot;

// namespace Bullet;
public partial class Bullet:Area2D
{
	const int SPEED = 300;

	public override void _Process(double delta)
    {
		Position += Transform.X  * SPEED * (float)delta;
		
    }

	public void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		QueueFree();
	}
	public void mob_touched()
	{
		QueueFree();
	}
	public void _on_body_shape_entered(Rid bodyrid, Node2D body, int _body_shape_index, int _local_shape_index)
	{
		GD.Print("Bala: Algo ha entrado en mí");
		if (body is MobEnemy enemigo)
		{
			enemigo.death();
			mob_touched();
		}
		;
	}
}


// extends Area2D
// class_name Bullet

// const SPEED: int = 300

// func _process(delta: float) -> void:
// 	position += transform.x * SPEED * delta

// func _on_visible_on_screen_notifier_2d_screen_exited() -> void:
// 	queue_free()
		
// func mob_touched():
// 	queue_free()

// func _on_body_shape_entered(_body_rid: RID, body: Object, _body_shape_index: int, _local_shape_index: int) -> void:
// 	#NOTE: Ejemplo de como es que se usa el print_rich()
// 	#print_rich("[color=white][color=yellow]Test[/color] body_shape_entered\n [/color]")
// 	if body is mobEnemy:
// 		#print_rich("[color=gray] BALA dice: [/color][color=green]He colisionado con un enemigo en la [color=yellow]hitbox[/color] dentro de Area dentro del nodo Padre[/color]")
// 		mob_touched()
