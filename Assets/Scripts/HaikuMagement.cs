using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaikuMagement : MonoBehaviour
{
    public int level = 1;
    public string[] haikuLines;
    public Text haikuText;
    private bool _isWriting = false;
    private bool _isHaikuWritten = false;
    private float _elapsed = 0f;
    private int _currentChar = 0;
    private int _currentLine = 0;
    private float _currentOpacity = 1f;

    void Start()
    {
        WriteHaiku();
    }

    void Update()
    {
        _elapsed += Time.deltaTime;
        if (_isWriting && !_isHaikuWritten && _elapsed >= 0.1f) {
            _elapsed = _elapsed % 0.1f;
            haikuText.text += haikuLines[_currentLine][_currentChar];
            _currentChar++;
            if (_currentChar >= haikuLines[_currentLine].Length) {
                haikuText.text += "\n";
                _currentChar = 0;
                _currentLine++;
                if (_currentLine >= haikuLines.Length) {
                    _isHaikuWritten = true;
                    _isWriting = false;
                }
            }
        }
        if (_isHaikuWritten && _elapsed >= 0.13f) {
            _elapsed = _elapsed % 0.13f;
            _currentOpacity -= 0.1f;
            haikuText.color = new Color(1f, 1f, 1f, _currentOpacity);
        }
        if (_currentOpacity <= 0f) {
            haikuText.text = "";
            _currentOpacity = 1f;
            _isHaikuWritten = false;
        }
    }

    public void WriteHaiku()
    {
        _isWriting = true;
        haikuText.text = "";
        _elapsed = 0f;
        _currentChar = 0;
        _currentLine = 0;
    }
}
