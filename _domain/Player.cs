using System;
using Godot;
public partial class Player:CharacterBody2D
{
bool is_alive;
	bool shooted;

	[Export]
	int speed = 400;
	[Export]
	int MAX_SPEED=125;
	[Export]
	int ACCELERATION = 1400;
	[Export]
	int FRICTION = 1000;
	// Gun gun;       //NOTE: Quitar el comentario luego de que se transcriba Gun a C#
	Vector2 screen_size;
	Timer timer;
	Gun gun;
	Area2D hitbox;
	CollisionShape2D collisionExt;
	CollisionShape2D collisionInt;
	
	// Node currentScene;
	    public override void _Ready()
    {
		// Node currentScene = GetTree().CurrentScene;
        screen_size = GetViewportRect().Size;
		Hide();
		hitbox = GetNode<Area2D>("hitbox");
		collisionExt = GetNode<CollisionShape2D>("CollisionExt");
		collisionInt = hitbox.GetNode<CollisionShape2D>("CollisionInt");
		gun = GetChild<Gun>(3);
		timer = new Timer();
		timer.WaitTime = 2.0f;
		// timer.ToSignal(GetTree(),SceneTreeTimer.SignalName.Timeout);
		timer.Timeout += _onShootedTimeout;
		timer.OneShot = true;
		AddChild(timer);
		
    }

    public override void _PhysicsProcess(double delta)
    {
		var velocity = Velocity;
		Vector2 direction = Input.GetVector("move_left","move_right","move_up","move_down");
		AnimatedSprite2D playerSprite = GetChild<AnimatedSprite2D>(0);
		if (!direction.IsZeroApprox())
		{
			velocity.X = Mathf.MoveToward(velocity.X,MAX_SPEED* direction.X, ACCELERATION * (float)delta);
			velocity.Y = Mathf.MoveToward(velocity.Y,MAX_SPEED* direction.Y, ACCELERATION * (float)delta);
			
			playerSprite.Play("caminar");
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X,MAX_SPEED* direction.X, FRICTION * (float)delta);
			velocity.Y = Mathf.MoveToward(velocity.Y,MAX_SPEED* direction.Y, FRICTION * (float)delta);
			playerSprite.Stop();
		}
		this.Velocity = velocity;
		MoveAndSlide();
		}

	public void _onShootedTimeout()
	{
		shooted= false;
	}
    public override void _Process(double delta)
    {
        float mousePosition;
		mousePosition = GetGlobalMousePosition().X;
		AnimatedSprite2D playerSprite = GetChild<AnimatedSprite2D>(0);
		if (mousePosition< GlobalPosition.X)
		{
			playerSprite.FlipH = true;
		}
		else
		{
			playerSprite.FlipH = false;
		}
		if (is_alive && Input.IsActionJustPressed("shoot"))
		{
			if (!shooted)
			{
				gun.Shoot();
				shooted = true;
				timer.Start();
			}
		}
    }
	public void start(Vector2 pos)
	{
		Node currentScene = GetTree().CurrentScene;
		Position = pos;
		Show();
		is_alive = true;
		collisionExt.Disabled=false;
		collisionInt.Disabled=false;
	}
	public void _OnHitboxBodyEntered(Rid bodyRid, Node2D body, long bodyShapeIndex, long localShapeIndex)
	{

		if (body is MobEnemy)
		{
			Hide();
			collisionInt.SetDeferred("disabled", true);
			collisionExt.SetDeferred("disabled", true);
			EventBus.Instance.EmitSignal(EventBus.SignalName.PlayerDied);
			is_alive = false;
		}
	}
}
# region Variables


// #NOTE: Cambiada la raiz del jugador de Node2D a CharacterBody2D
// extends CharacterBody2D
// var is_alive : bool = false
// var shooted :bool = false
// #Ajustar y cambiar a constante los ultimos 3 export
// #@export var speed = 400

// @export var MAX_SPEED: int = 125
// @export var ACCELERATION: int = 1400
// @export var FRICTION: int = 1000
// var screen_size
// var timer
#endregion

#region _ready
// NOTE2: Called when the node enters the scene tree for the first time.
// func _ready() -> void:
// 	screen_size = get_viewport_rect().size
// 	hide()
// 	timer = Timer.new()
// 	timer.wait_time = 2.0
// 	timer.timeout.connect(_on_shooted_timeout)
// 	timer.one_shot=true
// 	add_child(timer)
#endregion

#region _physics_process
// func _physics_process(delta: float) -> void:
// 	var direction = Input.get_vector("move_left", "move_right","move_up","move_down")
	
// 	if direction:
// 		velocity.x = move_toward(velocity.x, MAX_SPEED * direction.x, ACCELERATION * delta)
// 		velocity.y = move_toward(velocity.y, MAX_SPEED* direction.y, ACCELERATION * delta)
// 		$AnimatedSprite2D.play("caminar")
// 	else:
// 		velocity.x = move_toward(velocity.x, MAX_SPEED * direction.x, FRICTION * delta)
// 		velocity.y = move_toward(velocity.y, MAX_SPEED * direction.y, FRICTION * delta)
// 		$AnimatedSprite2D.stop()
// 	move_and_slide()

#endregion

#region _process 
// # Called every frame. 'delta' is the elapsed time since the previous frame.
// func _process(_delta: float) -> void:
// 	if get_global_mouse_position().x < global_position.x:
// 		$AnimatedSprite2D.flip_h = true
// 	else:
// 		$AnimatedSprite2D.flip_h = false
	
// 	# #NOTE : Disparar
// 		#TODO: Agregar un tempori zador a los disparos para evitar explotar la mecanica 
// 	if is_alive && Input.is_action_just_pressed("shoot"):
// 		if !shooted:
// 			$Gun.shoot()
// 			shooted = true # <- Esto bloquea nuevos disparos
// 			timer.start()

#endregion

#region start
// func start(pos):
// 	position = pos
// 	show()
// 	is_alive = true
// 	$hitbox/CollisionShape2D.disabled = false
// 	$CollisionShape2d.disabled = false
#endregion

// func _on_hitbox_body_entered(body) -> void:
// 	if (body is mobEnemy):
// 		hide()
// 		$hitbox/CollisionShape2D.set_deferred("disabled",true)
// 		$CollisionShape2d.set_deferred("disabled", true)
// 		GlobalEventBus.playerDied.emit()
// 		is_alive = false

// func _on_shooted_timeout():
// 	shooted = false
