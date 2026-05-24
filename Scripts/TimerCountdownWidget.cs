using System;
using System.Collections;
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

    public class TimerCountdownWidget : MonoBehaviour {

        [Header("References")]
        public TextMeshProUGUI textMesh;

        [Header("Localization")]
        public LocalizedString localizedLabel;

        [Header("Settings")]
        public float refreshRate = 1f;
        public bool raw = false;
        public bool showHours = true;

        string cachedFormat = "{0}";
        float refreshInterval;
        float nextRefreshTime;

        void Awake () {
            if (!textMesh) textMesh = GetComponent<TextMeshProUGUI>();
            if (refreshRate <= 0f) refreshRate = 1f;
            refreshRate = Mathf.Max(0.01f, refreshRate);
            refreshInterval = 1f / refreshRate;
        }

        void Update () {
            if (Time.time < nextRefreshTime) return;
            nextRefreshTime = Time.time + refreshInterval;
            UpdateText();
        }

        void OnEnable () {
            localizedLabel.StringChanged += OnLocalizedStringChanged;
            localizedLabel.RefreshString();
        }

        void OnDisable () {
            localizedLabel.StringChanged -= OnLocalizedStringChanged;
        }

        void OnLocalizedStringChanged (string value) {
            if (!string.IsNullOrEmpty(value)) cachedFormat = value;
            UpdateText();
        }

        void UpdateText () {
            if (!textMesh) return;
            float remainder = Mathf.Max(0f, TimerCountdown.remainder);
            string timeText;
            if (raw) {
                timeText = Mathf.CeilToInt(remainder).ToString();
            } else {
                TimeSpan ts = TimeSpan.FromSeconds(remainder);
                if (showHours || ts.Hours > 0) {
                    timeText = string.Format(
                        "{0:D2}:{1:D2}:{2:D2}",
                        ts.Hours,
                        ts.Minutes,
                        ts.Seconds
                    );
                } else {
                    timeText = string.Format(
                        "{0:D2}:{1:D2}",
                        ts.Minutes,
                        ts.Seconds
                    );
                }
            }
            textMesh.text = string.Format(cachedFormat, timeText);
        }

    }
}
