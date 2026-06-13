using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Глобальный счётчик обратного времени

namespace KulibinSpace.TimerSystem {

	public class TimerCountdown : MonoBehaviour {

		public delegate void TimerAction();

		static TimerCountdown instance;
		public float duration; // время для отсчёта по таймеру
		float startTime;
		public bool startOnStart = true;
		public bool startOnEnable = false;
		private bool timerHasStarted = false; // таймер стартовал, перезапускать только по ResetTimer
		public static float remainder { get { return instance ? instance._remainder : 0f; }} // запрашивается из клиента
		float _remainder; // оставшееся время
		public static event TimerAction OnTimerEndAction; // подписка на завершение таймера
		public UnityEvent onTimerEndAction; // событие по завершении таймера

		public void Awake () {
			if (instance && instance != this) {
				Debug.LogError("Multiple TimerCountdown instances");
				Destroy(gameObject);
				return;
			}

			instance = this;
			_remainder = duration;
		}

		void OnDestroy () {
			if (instance == this) instance = null;
		}

		public void ResetTimer () {
			startTime = Time.time;
			timerHasStarted = true;
			_remainder = duration;
		}

		public void ResetTimer (float newDuration) {
			duration = newDuration;
			ResetTimer();
		}

		public void StopTimer () {
			timerHasStarted = false;
		}

		void Update () {
			if (timerHasStarted) {
				if (_remainder > 0f)
					_remainder = duration - (Time.time - startTime);
				else {
					timerHasStarted = false;
					if (OnTimerEndAction != null) OnTimerEndAction();
					onTimerEndAction.Invoke();
				}
			}
		}

		void OnEnable () {
			if (startOnEnable) ResetTimer();
		}

		void Start () {
			if (startOnStart) ResetTimer();
		}

	}

}
