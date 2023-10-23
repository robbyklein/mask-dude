using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats")]
public class CharacterStatsSO : ScriptableObject {
  [SerializeField] public float Health = 100;
  [SerializeField] public float PlayerTouchDamage = 10;
  [SerializeField] public float projectileDamage = 10;
}