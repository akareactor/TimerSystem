using UnityEngine;

// таймер, часы, просто отсчитывает секунды, с учётом паузы

namespace KulibinSpace.TimerSystem {

	public class TimerClock : MonoBehaviour {

		float time;
		public bool startOnStart = true;
		public bool startOnEnable = false;
		public float value { get { return time; }} // запрашивается из клиента
		bool count;

		public void Reset () {
			time = 0;
			count = true;
		}

		public void Pause () {
			count = false;
		}

		public void Continue () {
			count = true;
		}
	
		public void PauseOrContinue () {
			count = !count;
		}

		void OnEnable () {
			if (startOnEnable) Reset();
		}

		void Start() {
			if (startOnStart) Reset();
		}

		void Update () {
			if (count) time += Time.deltaTime;
		}

	}

}