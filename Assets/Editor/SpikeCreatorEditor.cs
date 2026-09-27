using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SpikeCreator))]
public class SpikeCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Generate Spikes")) 
        {
            // finding spikecreator component
            SpikeCreator spikeCreator = target as SpikeCreator;

            // deleting old spikes
            List <GameObject> allChildren = new List<GameObject>();
            foreach (Transform child in spikeCreator.transform) 
            {
                allChildren.Add(child.gameObject);
            }
            for (int i = 0; i < allChildren.Count; i++) 
            {
                DestroyImmediate(allChildren[i]);
            }

            // generate visual spikes
            float startXCoord = (-(spikeCreator.spikeCount - 1) / 2f) * spikeCreator.spikeSize.x;
            for (int i = 0; i < spikeCreator.spikeCount; i++) 
            {
                GameObject spike = PrefabUtility.InstantiatePrefab(spikeCreator.spikePrefab) as GameObject;
                spike.transform.parent = spikeCreator.transform;
                spike.transform.localRotation = Quaternion.identity;
                spike.transform.localScale = Vector3.one;
                float xCoord = startXCoord + i * spikeCreator.spikeSize.x;
                spike.transform.localPosition = new Vector3(xCoord, 0, 0);
            } 
            /*
            // generate hitbox
            float hitboxWidth = spikeCreator.spikeCount * spikeCreator.spikeSize.x;
            float hitboxHeight = spikeCreator.spikeSize.y;
            float hitboxDepth = spikeCreator.spikeSize.z;
            // making a box collider if the spike doesn't have one
            BoxCollider boxCollider = spikeCreator.gameObject.GetComponent<BoxCollider>();
            boxCollider.size = new Vector3(hitboxWidth, hitboxHeight, hitboxDepth);
            boxCollider.center = Vector3.zero; */
        }
    }
}
