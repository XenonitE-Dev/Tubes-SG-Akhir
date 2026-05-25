using UnityEngine;
using UnityEngine.Rendering;

public class ForcePlayerSorting : MonoBehaviour
{
    public string sortingLayerName = "Player";
    public int orderInLayer = 9999;

    void Start()
    {
        SortingGroup sg = GetComponent<SortingGroup>();
        if (sg == null) sg = gameObject.AddComponent<SortingGroup>();

        sg.sortingLayerName = sortingLayerName;
        sg.sortingOrder = orderInLayer;

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in sprites)
        {
            sr.sortingLayerName = sortingLayerName;
            sr.sortingOrder = orderInLayer;
        }
    }
}