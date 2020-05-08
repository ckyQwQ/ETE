using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitPartical : MonoBehaviour
{
	public ParticleSystem mParticleSystem;
	int mMaxParticles = 0;

	public int maxParticles
	{
		get
		{
			return mMaxParticles;
		}
		set
		{
			mMaxParticles = value;
		}
	}

	void Awake()
	{
		mMaxParticles = mParticleSystem.main.maxParticles;
		mParticleSystem.Clear();
	}

	void Update()
	{
		if (mParticleSystem != null)
		{
			if (mParticleSystem.particleCount < mMaxParticles)
			{
				mParticleSystem.Emit(mMaxParticles - mParticleSystem.particleCount);
			}
		}
	}
}
