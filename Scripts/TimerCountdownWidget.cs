using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace KulibinSpace.TimerSystem {


    // Показывает оставшееся время TimerCountdown через TextMeshPro.
    //
    // Особенности:
    // - поддержка Unity Localization
    // - без async/await (WebGL-safe)
    // - обновление через корутину
    // - кэширование локализованного шаблона
    // - поддержка RAW и HH:MM:SS режима
    //
    // Пример строки локализации:
    //
    // timer_countdown
    // EN: Time left: {0}
    // RU: Осталось: {0}
    /*
    шаблон: (Unity Localization Smart Strings: в Smart String: {0:1} означает не “формат числа”, а: взять аргумент 0 и применить formatter/options 1)
    До вспышки {0:0.0} сек.
    или:
    До вспышки {2:00}:{3:00}
    или:
    T-{1:00}:{2:00}:{3:00}
    */

    public class TimerCountdownWidget : MonoBehaviour {

        [Header("References")]
        public TextMeshProUGUI textMesh;
        bool HasLocalization => !localizedLabel.IsEmpty;
        public LocalizedString localizedLabel;
        [Header("Format")]
        [TextArea]
        public string format = "До вспышки {0:0.0} сек.";
        [Header("Settings")]
        public float refreshRate = 10f;
        string cachedFormat;
        float refreshInterval;
        float nextRefreshTime;

        void Awake () {
            if (!textMesh) textMesh = GetComponent<TextMeshProUGUI>();
            refreshRate = Mathf.Max(0.01f, refreshRate);
            refreshInterval = 1f / refreshRate;
            cachedFormat = format;
        }

        void OnEnable () {
            if (!HasLocalization) return;
            localizedLabel.StringChanged += OnLocalizedStringChanged;
            localizedLabel.RefreshString();
        }

        void OnDisable () {
            if (!HasLocalization) return;
            localizedLabel.StringChanged -= OnLocalizedStringChanged;
        }
        void OnLocalizedStringChanged (string value) {
            if (!string.IsNullOrEmpty(value)) cachedFormat = value;
            UpdateText();
        }

        void Update () {
            if (Time.time < nextRefreshTime) return;
            nextRefreshTime = Time.time + refreshInterval;
            UpdateText();
        }

        void UpdateText () {
            if (!textMesh) return;
            float remainder = Mathf.Max(0f, TimerCountdown.remainder);
            int totalSeconds = Mathf.CeilToInt(remainder);
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds / 60) % 60;
            int seconds = totalSeconds % 60;
            textMesh.text = string.Format(cachedFormat, remainder, hours, minutes, seconds);
        }

    }

}
