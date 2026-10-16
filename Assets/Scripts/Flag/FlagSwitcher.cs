using UnityEngine;

public class FlagSwitcher : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    [SerializeField] private Flag _flag;

    private Ray _ray;

    private bool _isPressed;

    private RaycastHit _hit;

    private void OnEnable()
    {
        _inputReader.EnableFlagButtonPressed += EnableFlag;
        _inputReader.DisableFlagButtonPressed += SetIsPressed;
    }

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void OnDisable()
    {
        _inputReader.EnableFlagButtonPressed -= EnableFlag;
        _inputReader.DisableFlagButtonPressed -= SetIsPressed;
    }

    private void SetIsPressed()
    {
        if (Physics.Raycast(_ray, out _hit) && _hit.collider.TryGetComponent(out RobotWarehouse robotWarehouse))
        {
            _isPressed = true;
        }
    }

    private void EnableFlag()
    {
        if (Physics.Raycast(_ray, out _hit) && _isPressed)
        {
            _flag.transform.position = _hit.point;

            _flag.gameObject.SetActive(true);

            _isPressed = false;
        }
    }
}
