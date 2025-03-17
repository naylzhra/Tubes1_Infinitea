using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class Bot3_infinitea : Bot
{
    int turnDirection = 1;
    static void Main(string[] args)
    {
        new Bot3_infinitea().Start();
    }

    // Constructor, which loads the bot config file
    Bot3_infinitea() : base(BotInfo.FromFile("Bot3_infinitea.json")) { }

    // Called when a new round is started -> initialize and do some movement
    public override void Run()
    {

        BodyColor = Color.FromArgb(0xFF, 0x8C, 0x00);   // Dark Orange
        TurretColor = Color.FromArgb(0xFF, 0xA5, 0x00); // Orange
        RadarColor = Color.FromArgb(0xFF, 0xD7, 0x00);  // Gold
        BulletColor = Color.FromArgb(0xFF, 0x45, 0x00); // Orange-Red
        ScanColor = Color.FromArgb(0xFF, 0xFF, 0x00);   // Bright Yellow 
        TracksColor = Color.FromArgb(0x99, 0x33, 0x00); // Dark Brownish-Orange
        GunColor = Color.FromArgb(0xCC, 0x55, 0x00);    // Medium Orange


        // scan for enemies
        SetTurnRight(360);
      
        // Repeat while the bot is running
        while (IsRunning)
        {
            // // pola zigzag
            // TurnLeft(45);
            // Forward(100);
            // TurnRight(90);
            // Forward(100);
            // TurnLeft(90);
            // Forward(100);
            // TurnRight(90);
            // Forward(100);

            // TurnGunRight(360);

            // TurnRight(90);
            // Forward(100);
            // TurnRight(90);
            // Forward(100);
            // TurnLeft(90);
            // Forward(100);
            // TurnRight(90);
            // Forward(100);

            // TurnGunLeft(360);
            SetTurnLeft(10_000);
            // Limit our speed to 5
            MaxSpeed = 5;
            // Start moving (and turning)
            Forward(10_000);
            
            

        }
    }

    // We saw another bot -> fire!
    public override void OnScannedBot(ScannedBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);
        var distance = DistanceTo(e.X, e.Y);
        if (distance > 100 && Energy > 80){
            SetFire(2);
            Forward(50);
        }
        else{
            SetFire(3); 
        }
        

    }

    // We were hit by a bullet -> turn perpendicular to the bullet
    public override void OnHitByBullet(HitByBulletEvent evt)
    {
        
        Back(100);
        TurnLeft(120);
        Forward(100);
        TurnRight(60);
        // // Calculate the bearing to the direction of the bullet
        // var bearing = CalcBearing(evt.Bullet.Direction);

        // // Turn 90 degrees to the bullet direction based on the bearing
        // TurnLeft(90 - bearing);
    }

   
    public override void OnHitBot(HitBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);
        Fire(5);
        Run();

    }

    public override void OnHitWall(HitWallEvent evt)
    {
        TurnLeft(180);
        Forward(100);
        TurnRight(90);
        Forward(100);

        // Run();
    }

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

