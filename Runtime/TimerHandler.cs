﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Beef.Timer
{
	public static class TimerHandler
	{
		private static List<Timer> timers = new List<Timer>();

		private static int lastTickFrame = -1;

		static TimerHandler()
		{
			Tick();
		}

		private static async void Tick()
		{
			while (true)
			{
				//We havent moved to a new frame, so freeze this thread
				while (lastTickFrame == Time.frameCount)
				{
					await Task.Delay(1);
				}

				for (int i = timers.Count - 1; i >= 0; i--)
				{
					timers[i].TickTimer();
					if (timers[i].Completed && timers[i].DeleteOnCompletion)
					{
						timers.RemoveAt(i);
					}
				}
				lastTickFrame = Time.frameCount;
			}
		}

		public static void AddTimer(Timer timer)
		{
			timers.Add(timer);
		}
	}
}