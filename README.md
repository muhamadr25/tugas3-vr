1. DESKRIPSI SINGKAT
   
    Proyek ini berfokus pada pembuatan sebuah lingkungan virtual berupa hutan alami
    (memanfaatkan fitur Terrain dan penyebaran objek pepohonan Low Poly secara massal)
    serta sebuah objek mobil pikap (pickup) yang dapat digerakkan. Dalam simulasi ini,
    mobil pikap telah dipasangi sistem fisika dan kendali (*collider* dan skrip) sehingga
    pemain dapat melajukannya, memundurkan, dan berbelok menyusuri rute di dalam
    hutan secara interaktif. Selain itu, sistem kamera utama dirancang menggunakan skrip
    *Smooth Camera Follow* agar selalu mengikuti pergerakan dan arah rotasi mobil dari
    sudut pandang belakang secara dinamis dan halus tanpa sentakan.

2. DAFTAR DAN HIRARKI OBJEK SCENE
   
    a. Main Camera: Objek kamera utama dengan skrip CameraFollow.cs yang
    bertugas mengikuti pergerakan mobil pikap dari sudut pandang belakang atas
    secara halus.
   
    b. Directional Light: Sumber pencahayaan utama yang menghasilkan bayangan
    lingkungan secara dinamis.
   
    c. Global Volume: Komponen pascapemrosesan grafis pada Universal Render
    Pipeline(URP).
   
    d. Terrain: Objek permukaan tanah berkontur bukit yang dilapisi tekstur rumput
    dan tanah, dilengkapi komponen Terrain Collider untuk menangani kontak fisik
    ban dan mencegah mobil tembus ke bawah.
   
    e. Trees (Vegetasi): Aset pepohonan model Low Poly Tree Pack yang disebarkan
    pada permukaan terrain untuk menghidupkan suasana lingkungan hutan dan
    memiliki Capsule Collider agar bisa ditabrak.
   
    f. Pickup_Car: Objek kendaraan utama yang dilengkapi Rigidbody, Collider, dan
    skrip kendali Car.cs untuk menerima input arah dari pemain.
4. HASIL PROJECT
   
   a. Kamera view
   
   <img width="939" height="586" alt="image" src="https://github.com/user-attachments/assets/b08103fe-a4cc-4df7-b243-e1aa93914e28" />

    b. Mode game
   
   <img width="939" height="586" alt="image" src="https://github.com/user-attachments/assets/024cdd31-edef-48f2-a277-ba85f0f2fd45" />

