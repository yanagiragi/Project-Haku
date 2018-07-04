using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(EffectSettings))]
[CanEditMultipleObjects]
public class EffectSettingsEditor : Editor
{
  public SerializedProperty
    EffectTypeProp,
    ColliderRadiusProp,
    EffectRadiusProp,
    UseMoveVectorProp,
    TargetProp,
    MoveVectorProp,
    MoveSpeedProp,
    MoveDistanceProp,
    IsHomingMoveProp,
    IsVisibleProp,
    InstanceBehaviourProp,
    DeactivateTimeDelayProp,
    DestroyTimeDelayProp,
    LayerMaskProp;

  void OnEnable()
  {
    EffectTypeProp = serializedObject.FindProperty("EffectType");
    ColliderRadiusProp = serializedObject.FindProperty("ColliderRadius");
    EffectRadiusProp = serializedObject.FindProperty("EffectRadius");
    UseMoveVectorProp = serializedObject.FindProperty("UseMoveVector");
    TargetProp = serializedObject.FindProperty("Target");
    MoveVectorProp = serializedObject.FindProperty("MoveVector");
    MoveSpeedProp = serializedObject.FindProperty("MoveSpeed");
    MoveDistanceProp = serializedObject.FindProperty("MoveDistance");
    IsHomingMoveProp = serializedObject.FindProperty("IsHomingMove");
    IsVisibleProp = serializedObject.FindProperty("IsVisible");
    InstanceBehaviourProp = serializedObject.FindProperty("InstanceBehaviour");
    DeactivateTimeDelayProp = serializedObject.FindProperty("DeactivateTimeDelay");
    DestroyTimeDelayProp = serializedObject.FindProperty("DestroyTimeDelay");
    LayerMaskProp = serializedObject.FindProperty("LayerMask");
  }

  public override void OnInspectorGUI()
  {
    serializedObject.Update();

    EditorGUILayout.PropertyField(EffectTypeProp);
    
    var effectType = (EffectSettings.EffectTypeEnum)EffectTypeProp.enumValueIndex;
    var useMoveVector = UseMoveVectorProp.boolValue;
    var deactivateAfterCollision = (EffectSettings.DeactivationEnum)InstanceBehaviourProp.enumValueIndex;

    switch (effectType) {
      case EffectSettings.EffectTypeEnum.Projectile: {
        EditorGUILayout.PropertyField(ColliderRadiusProp, new GUIContent("ColliderRadius"));

        EditorGUILayout.PropertyField(UseMoveVectorProp, new GUIContent("UseMoveVector"));
        if (useMoveVector) EditorGUILayout.PropertyField(MoveVectorProp, new GUIContent("MoveVector"));
        else EditorGUILayout.PropertyField(TargetProp, new GUIContent("Target"));

        EditorGUILayout.PropertyField(IsHomingMoveProp, new GUIContent("IsHomingMove"));
        EditorGUILayout.PropertyField(MoveDistanceProp, new GUIContent("MoveDistance"));
        EditorGUILayout.PropertyField(MoveSpeedProp, new GUIContent("MoveSpeed"));
        break;
      }
      case EffectSettings.EffectTypeEnum.AOE:
        {
          EditorGUILayout.PropertyField(EffectRadiusProp, new GUIContent("EffectRadius"));
          EditorGUILayout.PropertyField(IsVisibleProp, new GUIContent("IsVisible"));
        break;
      }
      case EffectSettings.EffectTypeEnum.Other:
        {
          EditorGUILayout.PropertyField(IsVisibleProp, new GUIContent("IsVisible"));
        break;
      }
    }
    EditorGUILayout.PropertyField(InstanceBehaviourProp, new GUIContent("InstanceBehaviour"));
    if (deactivateAfterCollision == EffectSettings.DeactivationEnum.Deactivate)
    {
      EditorGUILayout.PropertyField(DeactivateTimeDelayProp, new GUIContent("DeactivateTimeDelay"));
    }
    else if (deactivateAfterCollision == EffectSettings.DeactivationEnum.DestroyAfterCollision
             || deactivateAfterCollision == EffectSettings.DeactivationEnum.DestroyAfterTime)
    {
      EditorGUILayout.PropertyField(DestroyTimeDelayProp, new GUIContent("DestroyTimeDelay"));
    }
    EditorGUILayout.PropertyField(LayerMaskProp, new GUIContent("LayerMask"));
    //var isProjectile = EffectTypeIsProjectile_Prop.boolValue;
    //if (isProjectile) {
    
    //}
   // else {
   // }

    serializedObject.ApplyModifiedProperties();
  }
}
