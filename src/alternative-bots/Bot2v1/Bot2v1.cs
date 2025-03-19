using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// need to fix the radar, make it ongoing all the time
// spin the body and gun at the same time
// fire with more damage
// move away to corner if energy is low

public class Bot2v1 : Bot
{
    static void Main(string[] args)
    {
        new Bot2v1().Start();
    }

    Bot2v1() : base(BotInfo.FromFile("Bot2v1.json")) { }

    public override void Run()
    {
        // Set colors
        BodyColor = Color.FromArgb(0x80, 0x00, 0x80);   // purple
        GunColor = Color.FromArgb(0x60, 0x00, 0x60);    // dark purple
        TurretColor = Color.FromArgb(0x60, 0x00, 0x60); // dark purple
        RadarColor = Color.FromArgb(0xFF, 0xFF, 0x00);  // yellow
        ScanColor = Color.FromArgb(0xFF, 0x00, 0x00);   // red
        BulletColor = Color.FromArgb(0xFF, 0x00, 0x00); // red

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