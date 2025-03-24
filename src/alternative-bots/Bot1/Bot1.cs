using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// Blackpink
// ----------------------------------------------------------------------------------------------
//                Bot dengan fokus utama ada pada scanned bot dan onhitbot dengan
//             mempertimbangkan jarak dan energi dalam strategi Greedy yang digunakan
// -----------------------------------------------------------------------------------------------

public class Bot1 : Bot
{   
    int turnDirection = 1;
    static void Main(string[] args)
    {
        new Bot1().Start();
    }

    Bot1() : base(BotInfo.FromFile("Bot1.json")) { }

    public override void Run()
    {
        BodyColor = Color.LightPink;
        TurretColor = Color.LightPink;
        RadarColor = Color.Pink;
        BulletColor = Color.HotPink;
        ScanColor = Color.MistyRose;
        TracksColor = Color.Gray;
        GunColor = Color.FromArgb(0x00, 0x00, 0x00);

        while (IsRunning)
        {
            Forward(200);           // pola jalannya adalah dengan maju dan mengganti arah jalan
            TurnRight(45);          // ke arah kanan sebesar 45 derajat setiap maju 200 piksel
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        var distance = DistanceTo(e.X,e.Y);
        
        if (distance > 100){            // jika jauh dari musuh, maka menembak dengan kekuatan kecil dan maju mendekati msuuh
            Fire(1);
            Forward(100);
        } else if (distance > 50){      // jika jaraknya lumayan dekat dengan musuh,
            Forward(50);                // maka maju mendekati musuh sambil menembak sesuai dengan energi saat ini
            if (Energy > 50){           // jika energi > 50 maka menembak dengan kekuatan 3, jika energi < 50 maka menembak dengan kekuatan 2
                Fire(3);
            } else {
                Fire(2);
            }
        } else {
            if (Energy > 50){           // jika jaraknya dekat dengan musuh, maka menembak sesuai dengan energi saat ini
                Fire(4);                // jika energi > 50 maka menembak dengan kekuatan 4
            } else {                    // jika energi sedikit (<=50) maka menembak dengan kekuatan 3
                Fire(3);
            }
        }
    }

    public override void OnHitBot(HitBotEvent e)
    {
        var distance = DistanceTo(e.X,e.Y);
        
        if (Energy < 30){                   // jika menabrak bot musuh dan energi saat ini < 30
            Back(50);                       // bot akan menghindar dengan mundur, belok ke arah kanan sebesar 90 derajat lalu maju
            TurnRight(90);
            Forward(100);
        } else {
            TurnToFaceTarget(e.X,e.Y);      // jika energinya masih banyak yaitu >= 30, maka bot akan menghadap ke bot musuh
            Fire(4);                        // dan menembak dengan kekuatan besar yaitu 4
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Back(50);                           // jika menabrak tembok, bot akan mundur dan putar balik
        TurnRight(180);
    }

    public override void OnHitByBullet(HitByBulletEvent evt)
    {
        TurnRight(90);                      // jika terkena bullet maka bot akan menghindar
        Forward(100);                       // dengan belok ke arah kanan sebanyak 90 derajat lalu maju
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