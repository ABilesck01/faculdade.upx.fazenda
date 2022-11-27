using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Animal")]
public class AnimalSO : ScriptableObject
{
    public string name;
    public AnimalController Visual;
    public int Price;
    [Tooltip("Time given in seconds")]public float timeToFeed;
    public int feedCost = 50;

}
