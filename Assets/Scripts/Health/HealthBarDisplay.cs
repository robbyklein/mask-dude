using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarDisplay : MonoBehaviour {
  [SerializeField] private UIDocument uiDoc;
  [SerializeField] private Transform enemyTransform;
  [SerializeField] private Camera mainCamera;
  [SerializeField] private SpriteRenderer sprite;
  private VisualElement rootEl;
  private VisualElement filledEl;

  #region Lifecycle
  private void Awake() {
    if (mainCamera == null) {
      mainCamera = Camera.main;
    }
  }

  private void Start() {
    SetPosition();
  }

  private void OnEnable() {
    rootEl = uiDoc.rootVisualElement;
    filledEl = rootEl.Q("health-bar__filled");
  }

  private void LateUpdate() {
    if (enemyTransform != null)
      SetPosition();
  }
  #endregion

  private void SetPosition() {
    // Get sprite size
    float offsetY = sprite.bounds.size.y / 2; // Changed from extents.y to size.y to get the full height

    // Create a position vector
    Vector2 newPosition = RuntimePanelUtils.CameraTransformWorldToPanel(
        rootEl.panel,
        enemyTransform.position + new Vector3(0, offsetY, 0), // Adjusted the y position here
        mainCamera
    );

    // Assuming the rootEl is the container of the health bar
    rootEl.transform.position = newPosition;
  }

  public void UpdateFilled(float percentage) {
    percentage = Mathf.Clamp(percentage, 0, 100);
    filledEl.style.width = new Length(percentage, LengthUnit.Percent);
  }
}