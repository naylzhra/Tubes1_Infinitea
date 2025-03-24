using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// The8
//---------------------------------------------------------------------------------------------------------
// Bot dengan fokus utamanya adalah pola jalur yang dijalani, yaitu dengan mamakai pola menyerupai angka 8
// Bot ini juga memilih ukuran peluru yang dikeluarkan berdasarkan jarak
//---------------------------------------------------------------------------------------------------------

public class Bot3_infinitea : Bot
{
    int turnDirection = 1;
    static void Main(string[] args)
    {
        new Bot3_infinitea().Start();
    }

    // Constructor, which loads the bot config file
    Bot3_infinitea() : base(BotInfo.FromFile("Bot3_infinitea.json")) { }


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


    public override void OnScannedBot(ScannedBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);

        // kalau jauh, tembak kecil saja tapi sambil maju
        var distance = DistanceTo(e.X, e.Y);
        if (distance > 100 && Energy > 80){
            SetFire(2);
            Forward(50);
        }
        else{
            SetFire(3); 
        }
    }


    public override void OnHitByBullet(HitByBulletEvent evt)
    {
        // menghindar kalau kena peluru, mundur belok maju
        Back(100);
        TurnLeft(120);
        Forward(100);
        TurnRight(60);
    }

   
    public override void OnHitBot(HitBotEvent e)
    {
        // kalau menabrak bot, arahin ke wajahnya terus tembak yang besar
        TurnToFaceTarget(e.X, e.Y);
        Fire(5);
        Run();

    }

    public override void OnHitWall(HitWallEvent evt)
    {
        // kalau menabrak tembok, putar balik terus belok biar ngga nabrak lagi
        TurnLeft(180);
        Forward(100);
        TurnRight(90);
        Forward(100);
    }


    // untuk melacak target, biar pas langsung ke wajahnya
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

