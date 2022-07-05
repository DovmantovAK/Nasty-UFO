﻿using Generation.Base;
using SceneContext.NastyUFOGame.Base;

namespace SceneContext.NastyUFOGame
{
	public class UFOGameSystem : StateMachine
	{
		private ILevelGenerator _UFOLevelGenerator;
		
		public void OnStart() => StartCoroutine(State.Startup());

		public void OnPause() => StartCoroutine(State.Pause());
		
		public void OnStop() => StartCoroutine(State.Stop());
		
		public void OnJump() => StartCoroutine(State.Jump());
	}
}