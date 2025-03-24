# **Tugas Besar 1 IF2211 Strategi Algoritma**
Game Robocode Tank Royale
By Kelompok 8 Infinitea
<br>

## Contributors
<div align="center">

| **NIM**  | **Nama** |
| ------------- |:-------------:|
| 13523078   | Anella Utari Gunadi |
| 13523079   | Nayla Zahira |
| 13523080   | Diyah Susan Nugrahani |

</div>

## Apa itu Robocode Tank Royale?
Robocode Tank Royale adalah game pemrograman di mana pemain berperan sebagai programmer yang menciptakan AI bot dalam bentuk tank.
Pertarungan berlangsung selama 10 ronde dengan memperhitungkan skor dari berbagai aspek.
Bot yang dibuat dalam program ini menggunakan strategi greedy yang berfokus pada pengambilan keputusan optimal dalam setiap langkah
untuk memaksimalkan peluang bertahan hidup dan mengalahkan lawan.

## Apa Saja Strategi Greedy yang Digunakan?
### Strategi Greedy Bot Utama
Strategi greedy yang digunakan pada bot utama adalah perpaduan antara "greedy by adaptability" dan "fire by considering distance, angle and energy"
yang mempertimbangkan kekuatan tembakan berdasarkan current position, jarak, sudut, serta energi.

### Strategi Greedy Alternatif Bot 1
Strategi greedy yang digunakan pada alternatif bot 1 adalah "fire by distance and energy"
yang mempertimbangkan kekuatan tembakan berdasarkan jarak dan energi yang dimiliki bot saat itu.

### Strategi Greedy Alternatif Bot 2
Strategi greedy yang digunakan pada alternatif bot 2 adalah "fire by distance and angle"
yang mempertimbangkan kekuatan tembakan berdasarkan jarak dan sudut bot dengan bot musuh.

### Strategi Greedy Alternatif Bot 3
Strategi greedy yang digunakan pada alternatif bot 3 adalah "greedy by adaptability"
yang mempertimbangkan pola gerakan yang membentuk angka 8 agar tidak terprediksi oleh lawan.

## Bagaimana Cara Menjalankan Game?
1. Clone repository https://github.com/naylzhra/Tubes1_Infinitea.git
2. Pastikan sudah terpasang JRE atau JDK di perangkat Anda. Jika belum terpasang, bisa diunduh melalui tautan berikut https://www.oracle.com/java/technologies/downloads/
Pastikan juga .NET 8.0 telah terpasang di perangkat Anda. Jika belum terpasang, bisa diunduh melalui tautan berikut https://dotnet.microsoft.com/en-us/download/dotnet/8.0
3. Unduh game engine Robocode Tank Royale di tautan berikut https://github.com/Ariel-HS/tubes1-if2211-starter-pack/releases/download/v1.0/robocode-tankroyale-gui-0.30.0.jar
4. Jalankan file jar dengan menggunakan command
``` sh
java -jar robocode-tankroyale-gui-0.30.0.jar
```
5. Lakukan setup konfigurasi booter dengan klik tombol "Config" lalu klik tombol "Boot Root Directories"
![Deskripsi Gambar](https://github.com/anellautari/image-storage/blob/main/Screenshot%20(2250).png)
6. Masukkan directory yang berisi folder-folder bot dari program ini lalu klik "OK"
![Deskripsi Gambar](https://github.com/anellautari/image-storage/blob/main/Screenshot%20(2251).png)
7. Jalankan battle dengan klik tombol "Battle" dan klik "Start Battle"
![Deskripsi Gambar](https://github.com/anellautari/image-storage/blob/main/Screenshot%20(2252).png)
8. Pilih bot mana saja yang ingin di-boot lalu klik "Boot"
![Deskripsi Gambar](https://github.com/anellautari/image-storage/blob/main/Screenshot%20(2253).png)
9. Lalu pilih kembali bot mana saja yang ingin di-battle dan klik "Add"
![Deskripsi Gambar](https://github.com/anellautari/image-storage/blob/main/Screenshot%20(2254).png)
10. Klik "Start Battle" untuk memulai permainan
![Deskripsi Gambar](https://github.com/anellautari/image-storage/blob/main/Screenshot%20(2255).png)



