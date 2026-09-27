using System;
using System.Threading.Tasks;
using Godot;


public partial class MobEnemy:RigidBody2D
{
    public override void _Ready()
    {
		AnimatedSprite2D animatedSprite2D = GetChild<AnimatedSprite2D>(0);
        string[] mob_types;
		mob_types= animatedSprite2D.SpriteFrames.GetAnimationNames();
        var longitud= mob_types.Length;
		GD.Print("Longitud: " , longitud);
		Random rnd = new Random();
		int numero = rnd.Next(0, longitud); 
		animatedSprite2D.Animation = mob_types[numero];
		animatedSprite2D.Play();
    }

	public void death()
	{
		QueueFree();
		EventBus.Instance.EmitSignal(EventBus.SignalName.MobDied);
	}
	
	private void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		QueueFree();
	}
}


// extends RigidBody2D
// class_name mobEnemy

// func _ready():
// 	var mob_types = Array($AnimatedSprite2D.sprite_frames.get_animation_names())
// 	$AnimatedSprite2D.animation = mob_types.pick_random()
// 	$AnimatedSprite2D.play()

// func _on_visible_on_screen_notifier_2d_screen_exited():
// 	queue_free()

// func _on_hitbox_mob_area_entered(area):
// 	if area is Bullet:
// 		await get_tree().create_timer(0.05).timeout
// 		# NOTE: Timer agregado debido a que las comprobaciones de colision eran demasiado rapidas
// 		#		por esto en ocasiones los enemigos desaparecian antes de que la bala pueda colisionar
// 		#		con las hitbox internas y por lo tanto los atravesaba
// 		death()

// func death():
// 	queue_free()
// 	GlobalEventBus.mobDied.emit()
