using UnityEngine;

public class FitCameraToGrid : MonoBehaviour
{
    [SerializeField]
    private float padding = 2.5f;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        EventAggregator.setupLevelEvent += SetCamera;
    }

    private void OnDisable()
    {
        EventAggregator.setupLevelEvent -= SetCamera;        
    }

    private void SetCamera(LevelData levelData)
    {
        if (mainCam == null) return;

        Debug.Log($"CameraFitter :: Col: {levelData.ColNum}, Rows: {levelData.RowNum}");
        mainCam.transform.localPosition = new Vector3((levelData.ColNum / 2f) - 0.5f, (levelData.RowNum / 2f) - 0.5f, -10);             
        mainCam.orthographicSize = (levelData.RowNum / 2f) + padding;        
    }
}
