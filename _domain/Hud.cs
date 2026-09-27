using Godot;
using System;
using System.Data;

public partial class Hud:CanvasLayer
{
    Node currentScene;
    Button StartButton;
    StyleBox Styles;
    StyleBoxFlat NewStyles;
    Timer MessageTimer;
    Label Message;
    PanelContainer PanelContainerTime;
    PanelContainer PanelContainerScore;
    Panel PanelTime;
    Panel PanelScore;
    Label LabelTime;
    Label LabelScore;
    
    public override void _Ready()
    {
        Message = GetNode<Label>("Message");
        StartButton = GetNode<Button>("StartButton");
        MessageTimer = GetNode<Timer>("MessageTimer");
        
        PanelContainerTime = GetChild<PanelContainer>(4);
        PanelTime = PanelContainerTime.GetChild<Panel>(0);
        LabelTime = PanelTime.GetChild<Label>(0);

        PanelContainerScore = GetChild<PanelContainer>(5);
        PanelScore = PanelContainerScore.GetChild<Panel>(0);
        LabelScore = PanelScore.GetChild<Label>(0);

        Styles = PanelContainerTime.GetThemeStylebox("panel");
        NewStyles = Styles.Duplicate() as StyleBoxFlat; 

        //Conectando la señal del MessageTimer
        // MessageTimer.Timeout += _OnMessageTimerTimeout;

    }
    public void ShowMessage(String text)
    {
        Message.Text = text;
        Message.Show();
        MessageTimer.Start();
    }    
    public async void ShowGameOver()
    {
        ShowMessage("Game Over");
        MessageTimer.Start();
        await ToSignal(MessageTimer, Timer.SignalName.Timeout);
        Message.Text= "Dodge The Creeps!";
        Message.Show();
        await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
        StartButton.Show();
    }
    public void UpdateTime(int time)
    {
        LabelTime.Text = time.ToString();
    }
    public void UpdateScore(int score)
    {
        LabelScore.Text = score.ToString();
    }

    public void SetTransparency()
    {
        NewStyles.BgColor = new Color(0,0,0, 0.1f);
        PanelContainerScore.AddThemeStyleboxOverride("panel", NewStyles);
        PanelContainerTime.AddThemeStyleboxOverride("panel", NewStyles);

    }
    public void UnsetTransparency()
    {
        NewStyles.BgColor = new Color(0,0,0, 0.6f);
        PanelContainerScore.AddThemeStyleboxOverride("panel", NewStyles);
        PanelContainerTime.AddThemeStyleboxOverride("panel", NewStyles);

    }
    public void _OnStartButtonPressed() // Conectar la señal desde godot
    {
        EventBus.Instance.EmitSignal(EventBus.SignalName.StartGame);
        StartButton.Hide();
    }
    public void _OnMessageTimerTimeout()
    {
        Message.Hide();
    }

}
#region Completado
// extends CanvasLayer
// signal start_game
// var styles
// var new_style

// func _ready() -> void:
// 	styles = $PanelTime.get_theme_stylebox("panel")
// 	new_style = styles.duplicate() as StyleBoxFlat

// func show_message(text):
// 	$Message.text = text
// 	$Message.show()
// 	$MessageTimer.start()



//-------
// 
// func _on_start_button_pressed() -> void:
// 	$StartButton.hide()
// 	start_game.emit()
//-------


// func show_game_over():
// 	show_message("Game Over")
// 	# Esperar hasta que MessageTimer haya terminado
// 	await $MessageTimer.timeout
	
// 	$Message.text = "Dodge the Creeps!"
// 	$Message.show()
// 	# Crear un timer de una sola vez de un segundo y esperar a que termine
// 	await get_tree().create_timer(1.0).timeout
// 	$StartButton.show()

// func update_time(time):
// 	$PanelTime/Panel/ScoreLabel.text = str(time)
	
// func update_score(score):
// 	$PanelScore/Panel/ScoreLabel.text = str(score)

// func set_transparency():
// 	new_style.bg_color.a = 0.1
// 	#new_style.bg_color.r = 0
// 	#new_style.bg_color.g = 0
// 	#new_style.bg_color.b = 0
// 	$PanelScore.add_theme_stylebox_override("panel", new_style)
// 	$PanelTime.add_theme_stylebox_override("panel", new_style)
	
// func unset_transparency():
// 	new_style.bg_color.a = 0.6
	
// 	$PanelScore.add_theme_stylebox_override("panel", new_style)
// 	$PanelTime.add_theme_stylebox_override("panel", new_style)
	


// func _on_message_timer_timeout() -> void:
// 	$Message.hide()
#endregion	