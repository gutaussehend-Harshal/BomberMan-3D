using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JetSynthesis.BomberMan3D
{
    public class EventService : MonoSingletonGeneric<EventService>
    {
        public static event Action onScoreIncreased;

        public void InvokeOnScoreIncreased()
        {
            onScoreIncreased?.Invoke();
        }
    }
}