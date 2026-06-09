using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
[Serializable]
public class HexSaveWrapper{
    public List<SavedHexData> hexes;
}
public class PuzzleSaving : MonoBehaviour{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TMP_InputField filename;
    [Space]
    [SerializeField] private Graph graph;
    public void SaveJson() {
        if (!ValidateInputField())
            return;
        List<SavedHexData> savedHexes = new List<SavedHexData>();
        // Gather all hex data
        foreach (Hex hex in graph.GetHexes())
            savedHexes.Add(hex.GetHexSavableData());
        // Wrap list for JsonUtility
        HexSaveWrapper wrapper = new HexSaveWrapper{
            hexes = savedHexes
        };
        // Build file path
        string path = Path.Combine(
            Application.persistentDataPath,
            filename.text + ".json"
        );
        // Serialize
        string json = JsonUtility.ToJson(wrapper, true);
        // Write file
        File.WriteAllText(path, json);
        Debug.Log("Save Successful!\nPath: " + path);
        Debug.Log(json);
    }
    private bool ValidateInputField(){
        if (string.IsNullOrWhiteSpace(filename.text)){
            StartCoroutine(
                ThrowError("Empty file name couldn't save puzzle", 3)
            );

            return false;
        }
        return true;
    }
    private IEnumerator ThrowError(string message, float time) {
        errorText.text = "ERROR: " + message;
        yield return new WaitForSeconds(time);
        errorText.text = "";
    }
}