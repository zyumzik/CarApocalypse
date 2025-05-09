using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TimerService
{
    public class TimerService : ITimerService
    {
        public async UniTask Delay(float seconds, CancellationToken cancellationToken = default)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                Debug.Log($"Operation cancelled: {e}");
            }
        }

        public async UniTask Every(float interval, Func<UniTask> callback, CancellationToken cancellationToken = default)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: cancellationToken);
                    if (cancellationToken.IsCancellationRequested) break;
                    await callback.Invoke();
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Operation cancelled: {e}");
            }
        }
        
        public async UniTask Every(float interval, Action callback, CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    callback();
                    await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Operation EVERY cancelled: {e}");
            }
        }
    }
}