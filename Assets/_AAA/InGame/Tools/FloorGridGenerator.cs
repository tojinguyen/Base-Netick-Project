using UnityEngine;

public class FloorGridGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows = 5;
    public int columns = 5;

    [Header("Floor Prefabs")]
    public GameObject[] floorPrefabs;

    [Header("Grid Parent")]
    public Transform gridParent;

    private void GenerateGrid()
    {
        if (floorPrefabs == null || floorPrefabs.Length == 0)
        {
            ConsoleLogger.LogError("Floor Prefabs array is empty. Please assign some prefabs.");
            return;
        }

        ClearGrid();

        var prefabSize = GetPrefabSize();

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < columns; col++)
            {
                var randomPrefab = floorPrefabs[Random.Range(0, floorPrefabs.Length)];

                var position = new Vector3(col * prefabSize.x, 0, row * prefabSize.z);

                var floor = Instantiate(randomPrefab, position, Quaternion.identity);

                if (gridParent != null)
                {
                    floor.transform.SetParent(gridParent);
                }
            }
        }
    }

    private Vector3 GetPrefabSize()
    {
        if (floorPrefabs.Length > 0 && floorPrefabs[0] != null)
        {
            var rendComp = floorPrefabs[0].GetComponent<Renderer>();
            if (rendComp != null)
            {
                return rendComp.bounds.size;
            }
        }
        return Vector3.one;
    }

    [ContextMenu("Generate Grid")]
    public void GenerateGridContextMenu()
    {
        GenerateGrid();
    }

    [ContextMenu("Clear Grid")]
    public void ClearGrid()
    {
        if (gridParent != null)
        {
            var childTrans = gridParent.GetComponentsInChildren<Transform>();
            foreach (var child in childTrans)
            {
                if (child == gridParent || child == gameObject.transform)
                    continue;
                DestroyImmediate(child.gameObject);
            }
        }
    }
}
