using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(PixelPerfectCamera))]
public class PlayerCamera : MonoBehaviour
{
    private Vector3 _newPos;
    private Transform _player;
    private Transform _focus;
    private GameObject _posFocus;
    private SpriteRenderer _fade;

    private float _upperYBound;
    private float _lowerYBound;
    private float _upperXBound;
    private float _lowerXBound;

    private float _xFromCenter;
    private float _yFromCenter;

    private bool _isSmoothing;

    public Collider2D CurrentArea;

    public float smoothTime = 0.5f;
    public float fadeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _posFocus = new GameObject("CameraPositionFocus");
        _isSmoothing = false;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _focus = _player;
        _fade = gameObject.transform.Find("Fade").GetComponent<SpriteRenderer>();
        foreach (GameObject bounds in GameObject.FindGameObjectsWithTag("CameraBounds"))
        {
            if (bounds.GetComponent<Collider2D>().bounds.Contains(_player.position))
            {
                CurrentArea = bounds.GetComponent<Collider2D>();
            }
        }
        PixelPerfectCamera ppc = gameObject.GetComponent<PixelPerfectCamera>();
        _xFromCenter = ppc.refResolutionX / (2 * ppc.assetsPPU);
        _yFromCenter = ppc.refResolutionY / (2 * ppc.assetsPPU);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _upperXBound = CurrentArea.bounds.max.x;
        _upperYBound = CurrentArea.bounds.max.y;
        _lowerXBound = CurrentArea.bounds.min.x;
        _lowerYBound = CurrentArea.bounds.min.y;

        _newPos = new Vector3(
            _focus.position.x,
            _focus.position.y,
            _focus.position.z - 10);

        _newPos = KeepCameraInArea(_newPos);

        if (!_isSmoothing)
        {
            transform.position = _newPos;
        }
    }

    public Vector3 KeepCameraInArea(Vector3 pos)
    {
        if (pos.x - _xFromCenter < _lowerXBound)
        {
            pos.x = _lowerXBound + _xFromCenter;
        }

        if (pos.x + _xFromCenter > _upperXBound)
        {
            pos.x = _upperXBound - _xFromCenter;
        }

        if (pos.y - _yFromCenter < _lowerYBound)
        {
            pos.y = _lowerYBound + _yFromCenter;
        }

        if (pos.y + _yFromCenter > _upperYBound)
        {
            pos.y = _upperYBound - _yFromCenter;
        }

        return pos;
    }

    public void FocusCameraOnTarget(Transform target)
    {
        _focus = target;
        _newPos = new Vector3(
            transform.position.x,
            transform.position.y,
            _focus.position.z - 10);
        _isSmoothing = true;

        StartCoroutine(SmoothTransition());
    }

    public void UnlockCamera()
    {
        _focus = _player;
        _isSmoothing = true;
        StartCoroutine(SmoothTransition());
    }

    private IEnumerator SmoothTransition()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < smoothTime)
        {
            Vector3 endPos = KeepCameraInArea(_focus.position + Vector3.back);
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / smoothTime);
            yield return null;
        }
        _isSmoothing = false;
    }

    public void FocusCameraOnPosition(Vector3 position)
    {
        _posFocus.transform.position = position;
        FocusCameraOnTarget(_posFocus.transform);
    }

    public void FocusCameraOnDisplacement(Vector3 displacement)
    {
        FocusCameraOnPosition(_focus.position + displacement);
    }

    public IEnumerator Fade(float fadeAmount = 1f)
    {
        Vector3 startPos = transform.position;
        float ellapsedTime = 0f;
        Color startColor = _fade.color;
        Color color = startColor;
        while (ellapsedTime < fadeTime)
        {
            ellapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startColor.a, fadeAmount, ellapsedTime / smoothTime);
            _fade.color = color;
            yield return null;
        }
        _isSmoothing = false;
    }
}
