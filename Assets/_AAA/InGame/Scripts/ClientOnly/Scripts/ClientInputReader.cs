using UnityEngine;

public class ClientInputReader : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _groundLayer;
    
    private void Update()
    {
        UserInputDataHolder.UserInput.Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        UpdateLookDirection();
    }
    
    private void UpdateLookDirection()
    {
        if (!_mainCamera)
        {
            ConsoleLogger.LogError("Main Camera is not assigned in ClientInputReader.");
            return;
        }

        var mousePosition = Input.mousePosition;
        var ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            return;
        var groundPoint = hit.point;
        var playerPosition = transform.position;

        var direction = new Vector2(groundPoint.x - playerPosition.x, groundPoint.z - playerPosition.z);
        UserInputDataHolder.UserInput.LookDirection = direction.normalized;
    }
}