using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeaponStats))]
public class WeaponEditor : Editor
{
    WeaponStats weaponStats;

    private void OnEnable()
    {
        weaponStats = target as WeaponStats;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (weaponStats.Sprite == null) return;

        Texture2D texture = AssetPreview.GetAssetPreview(weaponStats.Sprite);
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width((80*texture.width)/texture.height));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
