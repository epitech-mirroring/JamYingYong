using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpiritGaugeManagement : MonoBehaviour
{
    public GameObject whiteGauge;
    public GameObject blackGauge;
    private float _maxAgitation = 60f;
    private float _currentWhiteAgitation = 0f;
    private float _currentBlackAgitation = 0f;
    private float _elapsed = 0f;
    public bool isWhite = true;
    private Vector3 _angle = new Vector3(0f, 0f, 0f);
    private float _scale = 1f;
    private float _whiteRotationSpeed = 50f;
    private float _blackRotationSpeed = 50f;

    void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= 0.1f) {
            _elapsed = _elapsed % 0.1f;
            UpdateAgitation();
            UpdateGauges();
            UpdateRotationSpeed();
        }
        if (Input.GetKeyDown(KeyCode.F))
            ChangeState();
        AnimateGauges();
    }

    void UpdateRotationSpeed()
    {
        if (isWhite)
            _whiteRotationSpeed = 50f + (_currentWhiteAgitation * 3f);
        else
            _blackRotationSpeed = 50f + (_currentBlackAgitation * 3f);
    }

    void AnimateGauges()
    {
        _angle = whiteGauge.transform.eulerAngles;
        _angle.z += Time.deltaTime * _whiteRotationSpeed;
        whiteGauge.transform.eulerAngles = _angle;
        _angle = blackGauge.transform.eulerAngles;
        _angle.z += Time.deltaTime * _blackRotationSpeed;
        blackGauge.transform.eulerAngles = _angle;
    }

    void UpdateAgitation()
    {
        if (isWhite) {
            _currentWhiteAgitation += 0.1f;
            _currentBlackAgitation -= 0.1f;
            if (_currentWhiteAgitation >= _maxAgitation) {
                ReturnToMainMenu();
                return;
            }
        } else {
            _currentBlackAgitation += 0.1f;
            _currentWhiteAgitation -= 0.1f;
            if (_currentBlackAgitation >= _maxAgitation) {
                ReturnToMainMenu();
                return;
            }
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ChangeState()
    {
        if (!isWhite)
            _blackRotationSpeed = 50f;
        else
            _whiteRotationSpeed = 50f;
        isWhite = !isWhite;
    }

    void UpdateGauge(GameObject gauge, float agitation)
    {
        _scale = 1 + agitation / _maxAgitation;
        if (agitation >= (_maxAgitation - 10f) && agitation % 2 == 1)
            gauge.transform.localScale = new Vector3(_scale + 0.05f, _scale, _scale);
        else if (agitation >= (_maxAgitation - 10f) && agitation % 2 == 0)
            gauge.transform.localScale = new Vector3(_scale, _scale + 0.05f, _scale);
        else 
            gauge.transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    void UpdateGauges()
    {
        UpdateGauge(whiteGauge, _currentWhiteAgitation);
        UpdateGauge(blackGauge, _currentBlackAgitation);
    }
}
