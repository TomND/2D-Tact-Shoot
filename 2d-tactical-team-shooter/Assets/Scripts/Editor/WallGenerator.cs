﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class WallGenerator : EditorWindow {

    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    public GameObject wall;
    public GameObject segment_prefab;
    int units_x = 1;
    int units_y = 1;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Wall Generator")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(WallGenerator));
    }

    void OnGUI()
    {
        /*
        Created the GUI for wallgenerator. applies a few values to some variables.
        Code also deletes all children of the gameobject before creating new ones to prevent over lapping wall objects.

        @rtype: None
        */
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        //myString = EditorGUILayout.TextField("Text Field", myString);
        units_x = EditorGUILayout.IntSlider("Slider", units_x, 1, 200);
        units_y = EditorGUILayout.IntSlider("Slider", units_y, 1, 200);
        wall = (GameObject)EditorGUI.ObjectField(new Rect(3, 110, position.width - 6, 20), "Wall", wall, typeof(GameObject));
        segment_prefab = (GameObject)EditorGUI.ObjectField(new Rect(3, 90, position.width - 12, 20), "Segment Prefab", segment_prefab, typeof(GameObject));
        if (GUILayout.Button("Apply"))
        {
            DeleteChildren();
            Apply();
        }
    }




    void Apply()
    {
        /*
        Processes what happens when you apply the given values. Generates a wall based off of a prefab and the given x and y lengths.
        @rtype: None
        */
        wall.AddComponent<SpriteRenderer>();
        wall.transform.localScale = segment_prefab.transform.localScale;
        wall.GetComponent<SpriteRenderer>().sprite = segment_prefab.GetComponent<SpriteRenderer>().sprite;
        wall.GetComponent<SpriteRenderer>().color = segment_prefab.GetComponent<SpriteRenderer>().color;
        for (int x = 0; x < units_x; x++)
        {
            for (int y = 0; y < units_y; y++)
            {
                Vector3 new_position = new Vector3(wall.transform.position.x + wall.transform.localScale.x * x, wall.transform.position.y + wall.transform.localScale.y * y, 1);
                GameObject newwall = (GameObject)Instantiate(segment_prefab, new_position, Quaternion.identity);
                newwall.transform.parent = wall.transform;
            }
        }
    }


    void DeleteChildren()
    {
        /*
        Deletes all the children of the wall, or sub walls.

        */
        Transform[] children = wall.GetComponentsInChildren<Transform>();
        
        for(int x = 0; x < children.Length; x++)
        {
            if (children[x].gameObject != wall)
            {
                DestroyImmediate(children[x].gameObject);
            }
        }
    }

}
