using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Самый простой таймер
namespace KulibinSpace.TimerSystem {

	public class TimerEvent : MonoBehaviour {

		public float duration; // время для отсчёта по таймеру
		float startTime;
		public bool startOnStart = true;
		public bool startOnEnable = false;
		private bool timerHasStarted = false; // таймер стартовал, перезапускать только по ResetTimer
		public float remainder { get { return _remainder; }} // запрашивается из клиента
		float _remainder; // оставшееся время
		public UnityEvent onTimerEndAction; // событие по завершении таймера

		public void ResetTimer () {
			startTime = Time.time;
			timerHasStarted = true;
			_remainder = duration;
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
