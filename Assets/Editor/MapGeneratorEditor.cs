using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        MapGenerator mapGen = (MapGenerator)target;

        //if any value change
        if(DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
                mapGen.generateMap();
        }

        if(GUILayout.Button("Generate"))
        {
            mapGen.generateMap();
        }
    }
}
