using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLoading : MonoBehaviour{
    private PuzzleLoadingUi lui;
    private void Awake(){ lui = GetComponent<PuzzleLoadingUi>();}
    [SerializeField] GameObject hexPrefab;
    [SerializeField] Graph graph;
    public void LoadSelectedPuzzle(){
        string path = lui.GetCurrentSelectedPath();
        if (path == ""){
            lui.ThrowErrorMessage(3, "Failed to Load path cannot be empty");
            return; 
        }
        if (!File.Exists(path)){
            lui.ThrowErrorMessage(3, "Failed to Load path File doesn't exist");

            return;
        }
        string json = File.ReadAllText(path);

        HexSaveWrapper hexSavedWrapper = JsonUtility.FromJson<HexSaveWrapper>(json);
        Debug.Log("Load Succesfully");
        InstantiateGraph(hexSavedWrapper);
    }
    private void InstantiateGraph(HexSaveWrapper hsw) {
        List<Hex> hexes = new List<Hex>();
        foreach(SavedHexData shd in hsw.hexes){
            GameObject hex = Instantiate(hexPrefab);
            hex.transform.position = shd.position;
            hex.GetComponent<Hex>().SetPlacements(shd.placements);
            hexes.Add(hex.GetComponent<Hex>());
        }
        GeneratedGraph gg = new GeneratedGraph(hexes.ToArray());
        graph.Instantialize(gg);
        Debug.Log("Graph Instantiated!");
    }
}