using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Vector3 _offsetPosition;
    public Material gridMaterial;
    public float movementSpeedOffset = 0.2f;

    public GameObject target;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex_");

    void Start()
    {
        gridMaterial.SetVector(MainTex, Vector4.zero);
        _offsetPosition = transform.position;
    }

    void Update()
    {
        var transformRef = transform;
        var targetPosRef = target.transform.position;

        Vector3 dir = target.transform.forward;
        var direction = new Vector4(dir.x, dir.z);
        direction = -direction * (Time.deltaTime * movementSpeedOffset) + gridMaterial.GetVector(MainTex);
        gridMaterial.SetVector(MainTex, direction);
        transformRef.position = targetPosRef + _offsetPosition;
    }
}