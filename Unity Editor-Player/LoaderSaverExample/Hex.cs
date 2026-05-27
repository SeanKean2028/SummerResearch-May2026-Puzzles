using System;
using TMPro;
using UnityEngine;
[Serializable]
public struct SavedHexData{
    public int placements;
    public Vector3 position;
    public SavedHexData(int _placements, Vector3 _position){
        this.placements = _placements;
        this.position = _position;
    }
}
public class Hex : MonoBehaviour{
    [field: SerializeField] public int placements { get; private set; }
    [SerializeField] private Vector3 position;
    [SerializeField] private TMP_InputField inputField;
    private void OnEnable() {
        position = transform.position;
    }
    public void SetPlacements(int _placements){
        this.placements = _placements;
        if(inputField != null)
            inputField.text = placements.ToString();
    }
    public void AddToPlacements(int _delta){
        this.placements += _delta;
    }
    public SavedHexData GetHexSavableData() {
        SavedHexData savedData = new SavedHexData(placements, position);
        return savedData;
    }
}
