using UnityEngine;

public class S_PlayerTint : MonoBehaviour
{
    public MeshRenderer playerMesh;
    public Material tintMaterial; // Drag and drop the tint material of designers choice 
    public float tintMaterialDuration = 0.2f; // Duration to use tint material, customizable in the Inspector

    private Material originalMaterial;
    private bool isTintMaterialActive = false;
    private float tintMaterialTimer = 0f;

    private void Start() // Checks if the mesh is assigned to the inspector (had some issues where it'd disappear)
    {
        if (playerMesh == null)
        {
            playerMesh = GetComponent<MeshRenderer>();
        }

        originalMaterial = playerMesh.material; // Saves the original material
    }

    private void Update()
    {
        if (isTintMaterialActive)
        {
            tintMaterialTimer += Time.deltaTime;

            if (tintMaterialTimer >= tintMaterialDuration)
            {
                playerMesh.material = originalMaterial; // Reverts back to the old material after "tintMaterialDuration"
                isTintMaterialActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) // Collider detection
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Collision detected!");
            ChangeMaterialToTint();
        }
    }

    private void ChangeMaterialToTint() // Function to change material
    {
        if (tintMaterial != null && playerMesh != null)
        {
            playerMesh.material = tintMaterial;
            isTintMaterialActive = true;
            tintMaterialTimer = 0f;
        }
    }
}