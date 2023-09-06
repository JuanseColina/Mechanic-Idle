using UnityEngine;

[CreateAssetMenu(menuName = "Car", fileName = "CarName")]
public class Car : ScriptableObject
{
    [SerializeField] private float speed;

    public float Speed => speed;
}
