using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeletePlayerPref : MonoBehaviour
{
    [MenuItem("Window/Delete Player Prefs All")]

    static void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
