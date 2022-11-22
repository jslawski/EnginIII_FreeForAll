using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseLoader : MonoBehaviour
{
    private GameObject[] vasePrefabs;
    private Transform parentTransform;

    private GameObject currentVase;

    private void Awake()
    {
        this.vasePrefabs = Resources.LoadAll<GameObject>("Vases");
        this.parentTransform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.LoadVase(0);
    }

    public void LoadVase(int vaseIndex)
    {
        Destroy(this.currentVase);
        this.currentVase = Instantiate(this.vasePrefabs[vaseIndex], this.parentTransform);
    }   
}
