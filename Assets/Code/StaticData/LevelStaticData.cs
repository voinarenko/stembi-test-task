using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public int TotalFigurines
    {
      get => _totalFigurines;
      set => _totalFigurines = Mathf.Max(3, value - value % 3);
    }

    [SerializeField, Min(3)] private int _totalFigurines = 3;
    [SerializeField] private List<GameObject> _shapes = new();
    [SerializeField] private float _shapeScale = 0.3f;
    [SerializeField] private List<Sprite> _icons = new();
    [SerializeField] private float _iconScale = 0.1f;
    [SerializeField] private List<Color> _colors = new();

#if UNITY_EDITOR
    [CustomEditor(typeof(LevelStaticData))]
    private class LevelStaticDataEditor : Editor
    {
      private string _inputText;
      private bool _isEditing;
      private SerializedProperty _shapesProp;
      private SerializedProperty _shapeScaleProp;
      private SerializedProperty _iconsProp;
      private SerializedProperty _iconScaleProp;
      private SerializedProperty _colorsProp;

      private void OnEnable()
      {
        _shapesProp = serializedObject.FindProperty("_shapes");
        _shapeScaleProp = serializedObject.FindProperty("_shapeScale");
        _iconsProp = serializedObject.FindProperty("_icons");
        _iconScaleProp = serializedObject.FindProperty("_iconScale");
        _colorsProp = serializedObject.FindProperty("_colors");
      }

      public override void OnInspectorGUI()
      {
        var data = (LevelStaticData)target;
        serializedObject.Update();

        if (!_isEditing)
          _inputText = data._totalFigurines.ToString();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.LabelField("Total Figurines (Must be multiple of 3)");
        var newText = EditorGUILayout.TextField(_inputText);

        if (EditorGUI.EndChangeCheck())
        {
          _isEditing = true;
          _inputText = newText;

          if (int.TryParse(newText, out var newValue))
          {
            Undo.RecordObject(data, "Change Total Figurines");
            data._totalFigurines = newValue;
          }
        }

        if (Event.current.type == EventType.KeyDown &&
            Event.current.keyCode == KeyCode.Return &&
            GUIUtility.keyboardControl == GUIUtility.GetControlID(FocusType.Keyboard))
        {
          _isEditing = false;
          ApplyRounding(data);
          GUI.FocusControl(null);
          Event.current.Use();
        }

        if (_isEditing && GUIUtility.keyboardControl != GUIUtility.GetControlID(FocusType.Keyboard))
        {
          _isEditing = false;
          ApplyRounding(data);
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_shapesProp, true);
        EditorGUILayout.PropertyField(_shapeScaleProp);
        EditorGUILayout.PropertyField(_iconsProp, true);
        EditorGUILayout.PropertyField(_iconScaleProp);
        EditorGUILayout.PropertyField(_colorsProp, true);

        serializedObject.ApplyModifiedProperties();
      }

      private void ApplyRounding(LevelStaticData data)
      {
        if (!int.TryParse(_inputText, out var parsedValue))
          return;

        Undo.RecordObject(data, "Round Total Figurines");
        data._totalFigurines = Mathf.Max(3, parsedValue - parsedValue % 3);
        EditorUtility.SetDirty(data);
      }
    }
#endif
  }
}