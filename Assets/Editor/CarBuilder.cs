using UnityEngine;
using UnityEditor;

public class CarBuilder : EditorWindow
{
    [MenuItem("Tools/Buat Mobil Pickup Baru")]
    public static void BuatMobilPickupBaru()
    {
        // 1. Buat Objek Utama Mobil Baru di samping mobil lama
        GameObject car = new GameObject("Pickup_Car");
        car.transform.position = new Vector3(4f, 1.5f, 0f);

        // Rigidbody
        Rigidbody rb = car.AddComponent<Rigidbody>();
        rb.mass = 1500f;
        rb.linearDamping = 1.0f;

        // Pasang Skrip Car.cs
        Car carScript = car.AddComponent<Car>();
        carScript.rigid = rb;
        carScript.driveSpeed = 800f;
        carScript.steerSpeed = 40f;

        // 2. Folder Parent Roda
        GameObject collidersParent = new GameObject("Wheel_Colliders");
        collidersParent.transform.parent = car.transform;
        collidersParent.transform.localPosition = Vector3.zero;

        GameObject wheelsParent = new GameObject("Wheels");
        wheelsParent.transform.parent = car.transform;
        wheelsParent.transform.localPosition = Vector3.zero;

        Vector3[] wheelPositions = new Vector3[]
        {
            new Vector3(-1.1f, 0.2f, 1.3f),  // FL
            new Vector3(1.1f, 0.2f, 1.3f),   // FR
            new Vector3(-1.1f, 0.2f, -1.3f), // RL
            new Vector3(1.1f, 0.2f, -1.3f)   // RR
        };

        string[] wheelNames = new string[] { "FL", "FR", "RL", "RR" };
        WheelCollider[] cols = new WheelCollider[4];

        for (int i = 0; i < 4; i++)
        {
            // A. Wheel Collider
            GameObject colObj = new GameObject(wheelNames[i] + "_Collider");
            colObj.transform.parent = collidersParent.transform;
            colObj.transform.localPosition = wheelPositions[i];
            cols[i] = colObj.AddComponent<WheelCollider>();

            // B. Parent Visual Roda
            GameObject wheelMeshParent = new GameObject(wheelNames[i] + "_Wheel");
            wheelMeshParent.transform.parent = wheelsParent.transform;
            wheelMeshParent.transform.localPosition = wheelPositions[i];

            // C. Visual Ban (Cylinder)
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.name = "VisualMesh";
            cylinder.transform.parent = wheelMeshParent.transform;
            cylinder.transform.localPosition = Vector3.zero;
            cylinder.transform.localRotation = Quaternion.Euler(0, 0, 90f);
            cylinder.transform.localScale = new Vector3(0.75f, 0.22f, 0.75f);
            DestroyImmediate(cylinder.GetComponent<Collider>());
        }

        // Hubungkan wheel collider ke Car
        carScript.wheel1 = cols[0];
        carScript.wheel2 = cols[1];
        carScript.wheel3 = cols[2];
        carScript.wheel4 = cols[3];

        // 3. Desain Bodi Pikap Offroad
        GameObject carBody = new GameObject("Car Body");
        carBody.transform.parent = car.transform;
        carBody.transform.localPosition = Vector3.zero;

        Shader urpShader = Shader.Find("Universal Render Pipeline/Lit");
        if (urpShader == null) urpShader = Shader.Find("Standard");

        Material redMat = new Material(urpShader) { color = new Color(0.85f, 0.18f, 0.18f) };
        Material darkMat = new Material(urpShader) { color = new Color(0.2f, 0.22f, 0.24f) };
        Material glassMat = new Material(urpShader) { color = new Color(0.82f, 0.88f, 0.92f) };

        // Sasis & Bumper
        GameObject chassis = CreatePart(carBody.transform, "Chassis_Base", new Vector3(0, 0.45f, -0.1f), new Vector3(1.9f, 0.4f, 4.0f), redMat);
        if (chassis.GetComponent<BoxCollider>() == null) chassis.AddComponent<BoxCollider>();

        CreatePart(carBody.transform, "Rear_Bumper", new Vector3(0, 0.35f, -2.15f), new Vector3(2.0f, 0.25f, 0.3f), darkMat, true);
        CreatePart(carBody.transform, "Step_L", new Vector3(-1.05f, 0.3f, 0.1f), new Vector3(0.18f, 0.12f, 1.8f), darkMat, true);
        CreatePart(carBody.transform, "Step_R", new Vector3(1.05f, 0.3f, 0.1f), new Vector3(0.18f, 0.12f, 1.8f), darkMat, true);

        // Kap Depan & Kabin
        CreatePart(carBody.transform, "Hood", new Vector3(0, 0.7f, 1.15f), new Vector3(1.6f, 0.35f, 1.6f), redMat, true);
        CreatePart(carBody.transform, "Cabin", new Vector3(0, 1.15f, 0.2f), new Vector3(1.65f, 0.75f, 1.25f), redMat, true);

        // Kaca Kabin
        GameObject windshield = CreatePart(carBody.transform, "Windshield", new Vector3(0, 1.18f, 0.84f), new Vector3(1.4f, 0.5f, 0.05f), glassMat, true);
        windshield.transform.localRotation = Quaternion.Euler(20f, 0, 0);

        CreatePart(carBody.transform, "Rear_Window", new Vector3(0, 1.25f, -0.44f), new Vector3(1.35f, 0.42f, 0.05f), glassMat, true);
        CreatePart(carBody.transform, "Side_Window_L", new Vector3(-0.84f, 1.2f, 0.2f), new Vector3(0.05f, 0.42f, 0.8f), glassMat, true);
        CreatePart(carBody.transform, "Side_Window_R", new Vector3(0.84f, 1.2f, 0.2f), new Vector3(0.05f, 0.42f, 0.8f), glassMat, true);

        // Spion
        CreatePart(carBody.transform, "Mirror_L", new Vector3(-0.95f, 1.12f, 0.65f), new Vector3(0.1f, 0.18f, 0.12f), darkMat, true);
        CreatePart(carBody.transform, "Mirror_R", new Vector3(0.95f, 1.12f, 0.65f), new Vector3(0.1f, 0.18f, 0.12f), darkMat, true);

        // Bak Terbuka
        CreatePart(carBody.transform, "Bed_Floor", new Vector3(0, 0.62f, -1.25f), new Vector3(1.6f, 0.05f, 1.6f), darkMat, true);
        CreatePart(carBody.transform, "Bed_Wall_L", new Vector3(-0.85f, 0.85f, -1.25f), new Vector3(0.15f, 0.45f, 1.65f), redMat, true);
        CreatePart(carBody.transform, "Bed_Wall_R", new Vector3(0.85f, 0.85f, -1.25f), new Vector3(0.15f, 0.45f, 1.65f), redMat, true);
        CreatePart(carBody.transform, "Tailgate", new Vector3(0, 0.85f, -2.05f), new Vector3(1.65f, 0.45f, 0.15f), redMat, true);

        // Spakbor Roda (Fender)
        CreatePart(carBody.transform, "Fender_FL", new Vector3(-1.05f, 0.62f, 1.3f), new Vector3(0.35f, 0.22f, 0.95f), redMat, true);
        CreatePart(carBody.transform, "Fender_FR", new Vector3(1.05f, 0.62f, 1.3f), new Vector3(0.35f, 0.22f, 0.95f), redMat, true);
        CreatePart(carBody.transform, "Fender_RL", new Vector3(-1.05f, 0.62f, -1.3f), new Vector3(0.35f, 0.22f, 0.95f), redMat, true);
        CreatePart(carBody.transform, "Fender_RR", new Vector3(1.05f, 0.62f, -1.3f), new Vector3(0.35f, 0.22f, 0.95f), redMat, true);

        Selection.activeGameObject = car;
        Debug.Log("Mobil Pickup baru berhasil dibuat!");
    }

    private static GameObject CreatePart(Transform parent, string name, Vector3 pos, Vector3 scale, Material mat, bool removeCollider = false)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.parent = parent;
        obj.transform.localPosition = pos;
        obj.transform.localScale = scale;
        obj.GetComponent<MeshRenderer>().material = mat;
        if (removeCollider) DestroyImmediate(obj.GetComponent<Collider>());
        return obj;
    }
}