using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using TMPro;

public class VoiceCommands : MonoBehaviour
{
    // Source: https://github.com/lightbuzz/speech-recognition-unity

    protected KeywordRecognizer m_recognizer;

    public string[] Keywords = new string[] { "up", "down", "left", "right" };
    public ConfidenceLevel Confidence = ConfidenceLevel.Medium;

    public TMP_Text Text;

    public GameObject Target;

    private void Start()
    {
        m_recognizer = new KeywordRecognizer(Keywords, Confidence);
        m_recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        m_recognizer.Start();
    }
    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log(args.text);

        if (Text)
        {
            Text.text = args.text;
        }

        // TODO move target based on keyword
    }
    private void OnApplicationQuit()
    {
        if (m_recognizer != null && m_recognizer.IsRunning)
        {
            m_recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            m_recognizer.Stop();
        }
    }
}
