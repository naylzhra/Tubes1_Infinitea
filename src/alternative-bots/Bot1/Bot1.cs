using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Bot1 : Bot
{   
    /* A bot that drives forward and backward, and fires a bullet */
    int turnDirection = 1;
    static void Main(string[] args)
    {
        new Bot1().Start();
    }

    Bot1() : base(BotInfo.FromFile("Bot1.json")) { }

    public override void Run()
    {
        /* Customize bot colors, read the documentation for more information */
        BodyColor = Color.LightPink;
        TurretColor = Color.LightPink;
        RadarColor = Color.Pink;
        BulletColor = Color.HotPink;
        ScanColor = Color.MistyRose;
        TracksColor = Color.Gray;
        GunColor = Color.FromArgb(0x00, 0x00, 0x00);

        while (IsRunning)
        {
            Forward(200);
            TurnRight(45);
            // TurnGunRight(45);
            // TurnGunLeft(45);
        }
    }

    // We saw another bot -> fire!
    public override void OnScannedBot(ScannedBotEvent e)
    {
        Console.WriteLine("I see a bot at " + e.X + ", " + e.Y);
        var distance = DistanceTo(e.X,e.Y);
        
        if (distance > 100){
            SetFire(1);
            Forward(100);
        } else if (distance > 50){
            Forward(50);
            if (Energy > 50){
                SetFire(3);
            } else {
                SetFire(2);
            }
        } else {
            if (Energy > 50){
                SetFire(4);
            } else {
                SetFire(3);
            }
        }
        // Rescan();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        Console.WriteLine("Ouch! I hit a bot at " + e.X + ", " + e.Y);
        var distance = DistanceTo(e.X,e.Y);
        
        if (Energy < 30){
            Back(50);
            TurnRight(45);
        } else {
            TurnToFaceTarget(e.X,e.Y);
            Fire(4);
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Console.WriteLine("Ouch! I hit a wall, must turn back!");
        Back(50);
        TurnRight(90);
    }

    // We were hit by a bullet -> turn perpendicular to the bullet
    public override void OnHitByBullet(HitByBulletEvent evt)
    {
        TurnRight(90);
        Forward(100);
        // // Calculate the bearing to the direction of the bullet
        // var bearing = CalcBearing(evt.Bullet.Direction);

        // // Turn 90 degrees to the bullet direction based on the bearing
        // TurnLeft(90 - bearing);
    }
    /* Read the documentation for more events and methods */
    private void TurnToFaceTarget(double x, double y)
    {
        var bearing = BearingTo(x, y);
        if (bearing >= 0)
            turnDirection = 1;
        else
            turnDirection = -1;

        TurnLeft(bearing);
    }
}