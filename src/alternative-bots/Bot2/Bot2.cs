using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// Fighter
//---------------------------------------------------------------------------------------------------------
// Bot dengan fokus utamanya adalah bullet dan ram damage.
// Bot ini menentukan kekuatan menembak berdasarkan jarak dan sudut dari bot musuh
//---------------------------------------------------------------------------------------------------------

public class Bot2 : Bot
{
    static void Main(string[] args)
    {
        new Bot2().Start();
    }

    Bot2() : base(BotInfo.FromFile("Bot2.json")) { }

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
        while (IsRunning){
        // jalan lurus dan melingkar
            Forward(450);
            SetTurnLeft(10_000);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {        
        var bearing = BearingTo(e.X, e.Y);
        var distance = DistanceTo(e.X, e.Y);

        // tembak sesuai jarak dan sudut terhadap musuh
        if (distance > -100 && distance < 100 && Math.Abs(bearing) <= 10){
            TurnLeft(bearing);
            Fire(Math.Min(4, Energy-0.1));
        }
        else if (Math.Abs(distance) <= 500){
            TurnLeft(bearing);
            Fire(Math.Min(2.5, Energy-0.1));
            Forward(150);
        }
        else{
            TurnLeft(bearing);
            Fire(Math.Min(2, Energy-0.1));
        }
    }

    public override void OnHitWall(HitWallEvent e){
        // kalau menabrak tembok, mundur lalu putar arah kiri
        Forward(-30);
        TurnLeft(10);
        Run();
    }

    public override void OnHitByBullet(HitByBulletEvent e){
        // kalau terkena peluru, menghindar dengan mundur lalu belok kanan lalu maju kembali
        Forward(-5);
        TurnRight(10);
        Run();
    }
    
    public override void OnHitBot(HitBotEvent e){
        // jika menabrak/ditabrak dan energi mencukupi, tembak dengan kekuatan tinggi
        var bearing = BearingTo(e.X, e.Y);
        if (Math.Abs(bearing) <= 10){
            TurnLeft(bearing);
            Fire(Math.Min(5, Energy-0.1));
            Run();
        }
        if (e.IsRammed) //kalau ditabrak bot lain, menghindar
        {
            TurnLeft(10);
        }
    }
}