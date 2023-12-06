using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VehicleBehavoiur))]
public class VehicleSettingsEditor : Editor
{
    [Header("Vehicle Settings")]
    private Editor editorInstance;
    private void OnEnable()
    {
        // reset the editor instance
        editorInstance = null;
    }
    public override void OnInspectorGUI()
    {
        // the inspected target component
        VehicleBehavoiur vehicle = (VehicleBehavoiur)target;
        if (editorInstance == null)
            editorInstance = CreateEditor(vehicle.vehicleSettings);
        // show the variables from the MonoBehaviour
        base.OnInspectorGUI();
        // draw the ScriptableObjects inspector
        editorInstance.DrawDefaultInspector();
    }
    
}
