using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LangResolver : MonoBehaviour
{
    private const char Separator = '=';
    private readonly Dictionary<string, string> _lang = new Dictionary<string, string>();

    private SystemLanguage _language;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ReadProperties();
    }

    private void ReadProperties()
    {
        _language = Application.systemLanguage;
        var file = Resources.Load<TextAsset>(_language.ToString());
        if (file == null)
        {
            file = Resources.Load<TextAsset>(SystemLanguage.English.ToString());
            _language = SystemLanguage.English;
        }

        foreach (var line in file.text.Split('\n'))
        {
            var prop = line.Split(Separator);
            _lang[prop[0]] = prop[1];
        }
    }

    public void ResolveTexts()
    {
        var allTexts = Resources.FindObjectsOfTypeAll<LangText>();
        foreach (var langText in allTexts)
        {
            var text = langText.GetComponent<Text>();
            text.text = Regex.Unescape(_lang[langText.Identifier]);
        }
    }
}