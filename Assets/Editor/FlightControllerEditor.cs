using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlightController))]
public class FlightControllerEditor : Editor
{
    //FlightController fc;
    //Transform t;
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();
    //    fc = (FlightController)target;
    //    t = fc.transform;
    //}

    //private void OnSceneGUI()
    //{
    //    Debug.DrawRay(t.position, t.forward * 10f, Color.blue);
    //    Debug.DrawRay(t.position, t.forward * -10f, Color.red);
    //}

}
