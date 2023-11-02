using System.Collections;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class S_CardMovementController : MonoBehaviour
{
    [SerializeField] private float selectionScaleSpeed = 0.1f;
    public Vector3 rotationAngle = new Vector3(10f, 10f, 10f);

    public float zOffset = 1f;
    public bool isSelected = false;
    private Vector3 initialLocalPosition;
    private Quaternion targetRotation;

    public float floatFrequency = 1f;
    public float floatAmplitude = 0.1f;
    public float maxLerpSpeed = 1f;

    public float rotationChangeInterval = 1f;
    private float frequencyOffset;

    public AnimationCurve lerpSpeedCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
    private float maxAngle;
    private float minAngle = 0.01f;
    private Vector3 camDir;
    private Quaternion initalRotation;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Renderer rend;


    private void Start()
    {
        Vector3 worldDir = transform.position - Camera.main.transform.position;
        camDir = transform.InverseTransformDirection(worldDir.normalized);

        initalRotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        //set initial rotation's z to half
        initalRotation = Quaternion.Euler(0, initalRotation.eulerAngles.y / 2f, initalRotation.eulerAngles.z * 0.1f);
        if (initalRotation.eulerAngles.y > 100f)
        {
            initalRotation.eulerAngles = new Vector3(initalRotation.eulerAngles.x, initalRotation.eulerAngles.y - 180f, initalRotation.eulerAngles.z);
        }
        transform.rotation *= initalRotation;

        frequencyOffset = Random.Range(-floatFrequency, 10f);
        initialLocalPosition = transform.localPosition;

        GenerateRandomRotation();
    }

    public void SetSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }


    private void Update()
    {
        float zFloat = Mathf.Sin((Time.unscaledTime + frequencyOffset) * floatFrequency) * floatAmplitude;

        if (isSelected)
        {
            rend.materials = new Material[] { rend.materials[0] ,highlightMaterial };
            Vector3 offsetPosition = initialLocalPosition + (camDir * zOffset);
            transform.localPosition = Vector3.Lerp(transform.localPosition, offsetPosition, selectionScaleSpeed);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, initalRotation, selectionScaleSpeed);
        }
        else
        {
            rend.materials = new Material[] { rend.materials[0] };
            Vector3 floatPosition = new Vector3(initialLocalPosition.x, initialLocalPosition.y, initialLocalPosition.z + zFloat);
            transform.localPosition = Vector3.Lerp(transform.localPosition, floatPosition, selectionScaleSpeed);

            //if the rotation is close enough to the target rotation, generate a new target rotation
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.01f)
            {
                GenerateRandomRotation();
            }

            float angleDistance = Mathf.InverseLerp(minAngle, maxAngle, Mathf.Abs(Quaternion.Angle(transform.localRotation, targetRotation)));
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.unscaledDeltaTime * lerpSpeedCurve.Evaluate(angleDistance) * maxLerpSpeed);
        }

    }

    private void GenerateRandomRotation()
    {

        Vector3 targetEulerRotation = new Vector3(
            Random.Range(-rotationAngle.x, rotationAngle.x),
            Random.Range(-rotationAngle.y, rotationAngle.y),
            Random.Range(-rotationAngle.z, rotationAngle.z)
        );



        targetRotation = Quaternion.Euler(targetEulerRotation) * initalRotation;
        maxAngle = Mathf.Abs(Quaternion.Angle(transform.localRotation, targetRotation));

    }
}