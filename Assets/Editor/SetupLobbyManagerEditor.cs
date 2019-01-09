using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MunchkinLobbyManager))]
public class SetupLobbyManagerEditor : Editor
{
    private string doorPrefabsPath = @"Prefabs/Cards/Door Deck";
    private string treasurePrefabsPath = @"Prefabs/Cards/Treasure Deck";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MunchkinLobbyManager manager = (MunchkinLobbyManager)target;
        if(GUILayout.Button("Setup Spawnables"))
        {
            int numDoor = 81;
            int numTreasure = 76;
            int totalSpawnable = numDoor + numTreasure;

            // Clear array
            manager.spawnPrefabs.Clear();

            // First add door
            for(int i = 0; i < numDoor; i++)
            {
                GameObject obj = (GameObject)Resources.Load(doorPrefabsPath + "/" + i);
                manager.spawnPrefabs.Add(obj);
            }

            // Add treasure
            for(int i = 0; i < numTreasure; i++)
            {
                GameObject obj = (GameObject)Resources.Load(treasurePrefabsPath + "/" + i);
                manager.spawnPrefabs.Add(obj);
            }
        }
    }
}
