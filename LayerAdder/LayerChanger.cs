using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LayerChanger : MonoBehaviour
{
    public Layers LayerType;
    public bool ChangeAllChildren;

    public void ChangeLayer(Dictionary<string, int> layerDic)
    {
        if (!ChangeAllChildren)
        {
            Debug.Log(gameObject.name + " changing layer: " + LayerType.ToString());
            gameObject.layer = layerDic[LayerType.ToString()];
        }
        else
        {
            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach(Transform t in arr)
            {
                Debug.Log(t.gameObject.name + " changing layer: " + LayerType.ToString());
                t.gameObject.layer = layerDic[LayerType.ToString()];
            }
        }
    }
}
