﻿using System.Threading.Tasks;
using Actors.NastyUFO;
using Generation.Generators.NastyUFO.States;
using Input;
using Miscellaneous.Generators.ObjectGenerator;
using Miscellaneous.StateMachines.Base;
using SceneBehavior.UFOGame.Difficulty;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneBehavior.UFOGame.States
{
	public class GameRunState : State
	{
		private UFO_DifficultyController _difficultyController;
		private readonly ObjectGenerator<MonoBehaviour> _generator;
		private readonly UFO _player;
			
		public GameRunState(
			UFO player, 
			ObjectGenerator<MonoBehaviour> generator,
			UFO_DifficultyController difficultyController)
		{
			_difficultyController = difficultyController;
			_generator = generator;
			_player = player;
			
			_player.Died += Die;
		}
		
		public override async Task OnEnter()
		{
			InputManager.CurrentInputManager.JumpAction.performed += PerformedActionSubscription;
			InputManager.CurrentInputManager.PauseAction.performed += PerformedActionSubscription;
			InputManager.CurrentInputManager.JumpAction.canceled += CanceledActionSubscription;
			
			await _generator.SwitchState(typeof(RunState));
		}

		public override async Task Update()
		{
			await _generator.CurrentState.Update();
		}
		
		public override Task OnExit()
		{
			InputManager.CurrentInputManager.JumpAction.performed -= PerformedActionSubscription;
			InputManager.CurrentInputManager.PauseAction.performed -= PerformedActionSubscription;
			InputManager.CurrentInputManager.JumpAction.canceled -= CanceledActionSubscription;
			
			return Task.CompletedTask;
		}

		private void PerformedActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.PauseAction)
				CurrentStateMachine.SwitchState(typeof(PauseState));

			if (context.action == InputManager.CurrentInputManager.JumpAction)
			{
				_player.Accelerating(true);
			}
			
				
		}

		private void CanceledActionSubscription(InputAction.CallbackContext context)
		{
			if (context.action == InputManager.CurrentInputManager.JumpAction)
				_player.Accelerating(false);
		}
		
		private void Die(UFO player)
		{
			_player.Died -= Die;
			CurrentStateMachine.SwitchState(typeof(GameOverState));
		}
	}
}