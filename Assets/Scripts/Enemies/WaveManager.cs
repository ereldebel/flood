using System;
using UnityEngine;

namespace Enemies
{
	public class WaveManager : MonoBehaviour
	{
		
		#region Public Properties

		public int NumberOfLivingBatches
		{
			get => _numberOfLivingBatches;
			set
			{
				_numberOfLivingBatches = value;
				if (_numberOfLivingBatches == 0)
					GameManager.ClearedWave(_waveNumber);
			}
		}

		#endregion
		
		#region Serialized Private Fields


		[SerializeField] private float deploymentInterval;

		#endregion

		#region Private Fields


		private float _currInterval;
		private EnemyGenerator _enemyGenerator;
		private int _waveNumber = 0;
		private int _numberOfBatchesToDeploy, _numberOfLivingBatches;
		private float _nextEnemyDeployment;

		#endregion

		#region Function Events

		private void Awake()
		{
			_currInterval = 1.1f * deploymentInterval;
			_enemyGenerator = GetComponent<EnemyGenerator>();
		}

		private void Update()
		{
			if (_numberOfBatchesToDeploy > 0 && Time.time >= _nextEnemyDeployment)
			{
				--_numberOfBatchesToDeploy;
				_enemyGenerator.GenerateEnemy();
				_nextEnemyDeployment = Time.time + _currInterval;
			}
		}

		#endregion

		#region Public Methods

		public void StartNextWave()
		{
			_numberOfBatchesToDeploy = _numberOfLivingBatches = NumberOfBatchesInWave(++_waveNumber);
			_currInterval = Math.Max(deploymentInterval * 0.5f, _currInterval * 0.9f);
		}

		#endregion

		#region Private Methods

		private static int NumberOfBatchesInWave(int waveNumber) => (int) (10 * Mathf.Log(waveNumber * waveNumber + 2));

		#endregion
	}
}