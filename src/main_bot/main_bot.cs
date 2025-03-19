using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class main_bot : Bot
{
    int turnDirection = 1;   
    static void Main(string[] args)
    {
        new main_bot().Start();
    }

    main_bot() : base(BotInfo.FromFile("main_bot.json")) { }

    public override void Run()
    {
        BodyColor = Color.FromArgb(0x00, 0xC8, 0x00);   
        TurretColor = Color.FromArgb(0x00, 0x96, 0x32); 
        RadarColor = Color.FromArgb(0x00, 0x64, 0x64);  
        BulletColor = Color.FromArgb(0xFF, 0xFF, 0x64); 
        ScanColor = Color.FromArgb(0xFF, 0xC8, 0xC8);  

        SetTurnRight(360);

        while (IsRunning)
        {
            
            // first loop, pola 8 (clockwise)
            for (int i = 0; i < 8; i++)
            {
                SetTurnRight(45);
                Forward(70);
                // TurnGunRight(45);
            }
            
            // second loop, pola 8 (counter-clockwise)    
            for (int i = 0; i < 8; i++)
            {
                SetTurnLeft(45);
                Forward(70);
                // TurnGunLeft(45);
            }


        }
    }

    // We saw another bot -> fire!
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
        TurnRight(180);
        Forward(50);
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