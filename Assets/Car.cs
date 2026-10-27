using UnityEngine;

public class Car : MonoBehaviour
{
    // Komponen referensi
    public Rigidbody rigid;
    public WheelCollider wheel1; // Roda Depan Kiri (FL)
    public WheelCollider wheel2; // Roda Depan Kanan (FR)
    public WheelCollider wheel3; // Roda Belakang Kiri (RL)
    public WheelCollider wheel4; // Roda Belakang Kanan (RR)

    // Pengaturan kecepatan & sudut putar
    public float driveSpeed = 600f;
    public float steerSpeed = 40f;

    // Nilai penampung input
    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        // Membaca input tombol keyboard (A/D untuk setir, W/S untuk gas/mundur)
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        // Hitung tenaga putaran mesin
        float motor = verticalInput * driveSpeed;

        // Berikan tenaga ke keempat roda (All-Wheel Drive)
        wheel1.motorTorque = motor;
        wheel2.motorTorque = motor;
        wheel3.motorTorque = motor;
        wheel4.motorTorque = motor;

        // Belokkan roda depan mengikuti setir
        wheel1.steerAngle = steerSpeed * horizontalInput;
        wheel2.steerAngle = steerSpeed * horizontalInput;
    }
}