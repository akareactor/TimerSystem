using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace KulibinSpace.TimerSystem {

	// 18:44 06.03.2021 коннектится к TimerClockBody
	// Показывает оставшееся время, использует TextMeshPro
	// Запускается извне, через GameEventListener
	public class TimerClock : MonoBehaviour {

		public TextMeshProUGUI m_textMeshPro;
		//private TMP_FontAsset m_FontAsset;
		public string label = "<#0050FF>Time left: </color>{0:2}";
		public float refreshRate = 1; // частота обновления дисплея таймера, Гц
		float time;

		void OnEnable() {
			time = Time.time;
		}

		void Update () {
			if (Time.time - time > 1.0f / refreshRate) {
				time = Time.time;
				m_textMeshPro.SetText(label, TimerClockBody.remainder % 1000);
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
