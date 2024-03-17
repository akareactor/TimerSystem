using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace KulibinSpace.TimerSystem {

	// Показывает оставшееся время TimerCountdown, использует TextMeshPro
	// Запускается извне

	public class TimerClockWidget : MonoBehaviour {

		public TimerClock timerClock;
		public TextMeshProUGUI m_textMeshPro;
		//private TMP_FontAsset m_FontAsset;
		public string label = "<#0050FF>Time left: </color>{0:D2}:{1:D2}:{2:D2}";
		public float refreshRate = 1; // частота обновления дисплея таймера, Гц
		public bool raw = false; // выводить секунды одним числом, без разбивки на HH:MM:SS 
		float time;

		void OnEnable() {
			time = Time.time;
		}

		void SetFormattedText () {
			float s = timerClock.value % 1000;
			if (raw) {
				m_textMeshPro.SetText(label, s);
			} else {
				TimeSpan sec = TimeSpan.FromSeconds(timerClock.value % 1000);
				m_textMeshPro.SetText(string.Format(label, sec.Hours, sec.Minutes, sec.Seconds));
			}
			//m_textMeshPro.SetText(label, TimeSpan.FromSeconds(s).ToString(@"mm\:ss"));
			//m_textMeshPro.SetText(TimeSpan.FromSeconds(s).ToString(@"mm\:ss"));
			//string.Format("{0:D2}:{1:D2}:{2:D2}", sec.Hours, sec.Minutes, sec.Seconds
			//m_textMeshPro.SetText(label, sec.Minutes, sec.Seconds); // label не форматирует нулями, как надо
			//m_textMeshPro.SetText(string.Format("{0:D2}:{1:D2}:{2:D2}", sec.Hours, sec.Minutes, sec.Seconds));
		}

		void Update () {
			if (Time.time - time > 1.0f / refreshRate) {
				time = Time.time;
				SetFormattedText();
			}
		}

		void Start() {
			// Add new TextMesh Pro Component
			if (!m_textMeshPro) m_textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
			//m_textMeshPro.autoSizeTextContainer = true;
			// Load the Font Asset to be used.
			//m_FontAsset = Resources.Load("Fonts & Materials/LiberationSans SDF", typeof(TMP_FontAsset)) as TMP_FontAsset;
			//m_textMeshPro.font = m_FontAsset;
			// Assign Material to TextMesh Pro Component
			//m_textMeshPro.fontSharedMaterial = Resources.Load("Fonts & Materials/LiberationSans SDF - Bevel", typeof(Material)) as Material;
			//m_textMeshPro.fontSharedMaterial.EnableKeyword("BEVEL_ON");
			// Set various font settings.
			//m_textMeshPro.fontSize = 20;
			//m_textMeshPro.alignment = TextAlignmentOptions.Center;
			//m_textMeshPro.anchorDampening = true; // Has been deprecated but under consideration for re-implementation.
			//m_textMeshPro.enableAutoSizing = true;
			//m_textMeshPro.characterSpacing = 0.2f;
			//m_textMeshPro.wordSpacing = 0.1f;
			//m_textMeshPro.enableCulling = true;
			//m_textMeshPro.enableWordWrapping = false; 
			//textMeshPro.fontColor = new Color32(255, 255, 255, 255);
		}

	}
}
