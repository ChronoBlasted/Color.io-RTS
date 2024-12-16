using BaseTemplate.Behaviours;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoSingleton<CameraController>
{
    [Header("Movement Camera")]
    [SerializeField] float _cameraSpeed = 10;
    [SerializeField] GameObject _cameraTarget;
    [SerializeField] Vector2 _maxCameraXOffset, _maxCameraZOffset;
    Vector2 movementModifier;

    [Header("Zoom Camera")]
    [SerializeField] float zoomSpeed = 1;
    [SerializeField] Vector2 _maxCameraYOffset;

    float mouseHorizontalValue;
    float mouseVerticalValue;
    float mouseScrollValue;

    [Header("Rotate Camera")]
    [SerializeField] float _rotateSpeed = .5f;
    int currentAngle = 0;
    Tween rotateTween;

    bool canMove;

    public void HandleMoveCamera()
    {
        _cameraTarget.transform.position += (_cameraTarget.transform.forward * (mouseVerticalValue / _cameraSpeed))
                                            + (_cameraTarget.transform.right * (mouseHorizontalValue / _cameraSpeed));

        if (_cameraTarget.transform.position.x > _maxCameraXOffset.y)
        {
            _cameraTarget.transform.position = new Vector3(_maxCameraXOffset.y, _cameraTarget.transform.position.y, _cameraTarget.transform.position.z);
        }
        if (_cameraTarget.transform.position.x < _maxCameraXOffset.x)
        {
            _cameraTarget.transform.position = new Vector3(_maxCameraXOffset.x, _cameraTarget.transform.position.y, _cameraTarget.transform.position.z);
        }

        if (_cameraTarget.transform.position.z > _maxCameraZOffset.y)
        {
            _cameraTarget.transform.position = new Vector3(_cameraTarget.transform.position.x, _cameraTarget.transform.position.y, _maxCameraZOffset.y);
        }
        if (_cameraTarget.transform.position.z < _maxCameraZOffset.x)
        {
            _cameraTarget.transform.position = new Vector3(_cameraTarget.transform.position.x, _cameraTarget.transform.position.y, _maxCameraZOffset.x);
        }
    }

    public void HandleZoomCamera()
    {
        Vector3 pos = _cameraTarget.transform.position;
        pos.y += mouseScrollValue * zoomSpeed;

        if (_cameraTarget.transform.position != pos)
        {
            _cameraTarget.transform.position = pos;

            if (_cameraTarget.transform.position.y > _maxCameraYOffset.y)
            {
                _cameraTarget.transform.position = new Vector3(_cameraTarget.transform.position.x, _maxCameraYOffset.y, _cameraTarget.transform.position.z);
            }
            if (_cameraTarget.transform.position.y < _maxCameraYOffset.x)
            {
                _cameraTarget.transform.position = new Vector3(_cameraTarget.transform.position.x, _maxCameraYOffset.x, _cameraTarget.transform.position.z);
            }
        }
    }

    public void RotateCamera(int amountRotate)
    {
        if (rotateTween != null)
        {
            rotateTween.Kill();
        }

        currentAngle += amountRotate;

        if (currentAngle == 450) currentAngle = 90;
        if (currentAngle == -90) currentAngle = 270;

        rotateTween = _cameraTarget.transform.DORotate(new Vector3(0, currentAngle, 0), _rotateSpeed).SetEase(Ease.OutSine);
    }

    void FixedUpdate()
    {
        if (canMove == false) return;

        mouseHorizontalValue = Input.GetAxis("Mouse X") * -1;
        mouseVerticalValue = Input.GetAxis("Mouse Y") * -1;

        mouseScrollValue = Input.mouseScrollDelta.y * -1;

        if (Mouse.current.rightButton.isPressed) HandleMoveCamera();

        HandleZoomCamera();
    }

    void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Game:
                canMove = true;
                break;
            default:
                canMove = false;
                break;
        }
    }

    public void Init()
    {
        GameManager.Instance.OnGameStateChanged += HandleStateChange;
    }

    private void Update()
    {
        if (canMove == false) return;

        if (Input.GetKeyDown(KeyCode.E)) RotateCamera(-90);
        if (Input.GetKeyDown(KeyCode.Q)) RotateCamera(90);
    }
}
