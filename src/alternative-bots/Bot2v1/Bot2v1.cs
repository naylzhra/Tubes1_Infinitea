using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Bot2v1 : Bot
{
    static void Main(string[] args)
    {
        new Bot2v1().Start();
    }

    Bot2v1() : base(BotInfo.FromFile("Bot2v1.json")) { }

    public override void Run()
    {
        BodyColor = Color.Plum;   
        GunColor = Color.FromArgb(0x60, 0x00, 0x60);    
        TurretColor = Color.Plum; 
        RadarColor = Color.Purple;  
        ScanColor = Color.Plum;   
        BulletColor = Color.Purple; 
        TracksColor = Color.Gray;
        GunColor = Color.FromArgb(0x00, 0x00, 0x00);

        // Move and scan continuously
        while (IsRunning)
        {
            SetTurnLeft(10_000);
            // Limit our speed to 5
            MaxSpeed = 5;
            // Start moving (and turning)
            Forward(10_000);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {        
        var bearing = BearingTo(e.X, e.Y);
        var distance = DistanceTo(e.X, e.Y);

        // Fire depending on angle and distance
        if (distance > -100 && distance < 100 && Math.Abs(bearing) <= 10){
            TurnLeft(bearing);
            Fire(Math.Min(3, Energy-0.1));
        }
        else if (Math.Abs(bearing) <= 10){
            TurnLeft(bearing);
            Fire(Math.Min(2, Energy-0.1));
        }
        else if (Math.Abs(distance) <= 500){
            TurnLeft(bearing);
            Fire(Math.Min(2, Energy-0.1));
            Forward(150);
        }
        else{
            TurnLeft(bearing);
            Fire(Math.Min(2, Energy-0.1));
        }
    }

    public override void OnHitWall(HitWallEvent e){
        Forward(-30);
        TurnLeft(10);
        Run();
    }

    public override void OnHitByBullet(HitByBulletEvent e){
        Forward(-5);
        TurnRight(10);
        Run();
    }
    
    public override void OnHitBot(HitBotEvent e)
    {
        var bearing = BearingTo(e.X, e.Y);
        if (Math.Abs(bearing) <= 10){
            TurnLeft(bearing);
            Fire(Math.Min(3, Energy-0.1));
            Run();
        }
        if (e.IsRammed)
        {
            TurnLeft(10);
        }
    }
}
