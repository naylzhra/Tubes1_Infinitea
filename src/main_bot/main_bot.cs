using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// BOT INFINITEA
// Bot utama kelompok infinitea, menggabungkan beberapa pendekatan algoritma greedy dari bot alternatif
// Menggunakan gerakan dengan pola 8 terinspirasi dari nomor kelompok 8 infinitie
// Menggunakan strategi tembak berdasarkan jarak dan sudut dari bot musuh
// Berusaha menghindar apabila energi tidak mencukupi

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
        BodyColor = Color.Cyan;   
        TurretColor = Color.Cyan; 
        RadarColor = Color.FromArgb(0x00, 0x64, 0x64);  
        BulletColor = Color.FromArgb(0xFF, 0xFF, 0x64); 
        ScanColor = Color.Cyan; 
        TracksColor = Color.Gray;
        GunColor = Color.FromArgb(0x00, 0x00, 0x00); 

        SetTurnRight(360);

        // jalan dengan pola 8
        while (IsRunning)
        {
            
            // first loop, pola 8 (clockwise)
            for (int i = 0; i < 8; i++)
            {
                SetTurnRight(45);
                Forward(70);
            }
            
            // second loop, pola 8 (counter-clockwise)    
            for (int i = 0; i < 8; i++)
            {
                SetTurnLeft(45);
                Forward(70);
            }


        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {        
        var bearing = BearingTo(e.X, e.Y);
        var distance = DistanceTo(e.X, e.Y);

        // menembak based on jarak dan sudut
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

    // kalau energi mencukupi maka tembak
    public override void OnHitBot(HitBotEvent e)
    {
        var distance = DistanceTo(e.X,e.Y);
        
        if (Energy < 30){
            TurnRight(90);
            Forward(100);
        } else {
            TurnToFaceTarget(e.X,e.Y);
            Fire(5);
        }
    }

    // putar balik saat menabrak tembok
    public override void OnHitWall(HitWallEvent e)
    {
        TurnRight(180);
        Forward(50);
    }

    // kalau ketembak, menghindar dengan berbelok
    public override void OnHitByBullet(HitByBulletEvent evt)
    {
        TurnRight(90);
        Forward(100);
    
    }


    // menghadap ke musuh
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