using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject gridView = null;
    [SerializeField] private GameObject cellPrefab = null;

    private void Awake()
    {
        int[,] gridIn =
            {
            { 1, 0, 2, 3 },
            { 4, 5, 6, 7 },
            { 8, 9, 10, 11 },
            { 12, 13, 14, 15 } };
        gridView.GetComponent<IGridView>().Initialize(4, gridIn, cellPrefab);
    }
}
