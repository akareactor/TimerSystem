﻿using System.Collections;
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
		public static float remainder { get { return instance._remainder; }} // запрашивается из клиента
		float _remainder; // оставшееся время
		public static event TimerAction OnTimerEndAction; // подписка на завершение таймера
		public UnityEvent onTimerEndAction; // событие по завершении таймера

		public void Awake () {
			instance = this;
			instance._remainder = instance.duration;
		}

		public void ResetTimer () {
			startTime = Time.time;
			timerHasStarted = true;
			instance._remainder = instance.duration;
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
				if (instance._remainder > 0f) 
					instance._remainder = instance.duration - (Time.time - startTime);
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
	
		void Start() {
			if (startOnStart) ResetTimer();
		}

	}

}