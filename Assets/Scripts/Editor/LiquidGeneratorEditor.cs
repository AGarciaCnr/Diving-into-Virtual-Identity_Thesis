using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(LiquidGenerator))]
public class LiquidGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LiquidGenerator liquidGen = (LiquidGenerator)target;

        if (DrawDefaultInspector())
        {
            if (liquidGen.autoUpdate)
            {
                liquidGen.Generate();
            }
        };

        if (GUILayout.Button("Generate"))
        {
            liquidGen.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            liquidGen.Clear();
        }
    }
}

//[CustomEditor(typeof(MapGenerator))]
//public class MapGeneratorEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        MapGenerator mapGen = (MapGenerator)target;

//        if (DrawDefaultInspector())
//        {
//            if (mapGen.autoUpdate)
//            {
//                mapGen.GenerateMap(false);
//            }
//        };

//        if (GUILayout.Button("Generate"))
//        {
//            mapGen.GenerateMap(false);
//        }
//    }
//}
