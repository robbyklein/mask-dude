using UnityEngine;

public class CloudDrifter : MonoBehaviour {
    [SerializeField] private float speed = 1.0f;
    private Camera mainCamera;
    private float spriteWidth;

    private void Start() {
        mainCamera = Camera.main;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (IsOffScreenToLeft()) Reposition();
    }

    private bool IsOffScreenToLeft() {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + new Vector3(spriteWidth / 2, 0, 0));
        return screenPosition.x < 0;
    }

    private void Reposition() {
        Vector3 screenRight = new Vector3(Screen.width, Screen.height / 2f, mainCamera.nearClipPlane);
        Vector3 worldRight = mainCamera.ScreenToWorldPoint(screenRight);
        transform.position = new Vector3(worldRight.x + spriteWidth / 2, transform.position.y, transform.position.z);
    }
}