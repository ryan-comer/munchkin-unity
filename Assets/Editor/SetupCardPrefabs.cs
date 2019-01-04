using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetupCardPrefabs
{
    private static string doorTexturesPath = @"Textures/Cards/Door Deck";
    private static string doorMaterialsPath = @"Materials/Cards/Door Deck";
    private static string doorPrefabsPath = @"Prefabs/Cards/Door Deck";

    [MenuItem("Tools/Setup Door Deck")]
    private static void SetupDoorDeck()
    {
        int numItems = 81;

        AssetDatabase.StartAssetEditing();

        for(int i = 0; i < numItems; i++)
        {
            // Update material
            var mat = Resources.Load<Material>(doorMaterialsPath + "/" + i);
            var tex = Resources.Load<Texture>(doorTexturesPath + "/" + i);
            var pref = Resources.Load<Card>(doorPrefabsPath + "/" + i);
            mat.mainTexture = tex;
            pref.frontTexture = tex;
            pref.transform.GetChild(0).GetComponent<Renderer>().material = mat;
        }

        AssetDatabase.StopAssetEditing();
        AssetDatabase.SaveAssets();

    }

    [MenuItem("Tools/Setup Treasure Deck")]
    private static void SetupTreasureDeck()
    {

    }
}
