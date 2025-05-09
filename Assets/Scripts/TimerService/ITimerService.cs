using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace TimerService
{
    public interface ITimerService
    {
        UniTask Delay(float seconds, CancellationToken cancellationToken = default);
        UniTask Every(float interval, Func<UniTask> callback, CancellationToken cancellationToken = default);
        UniTask Every(float interval, Action callback, CancellationToken token);
    }
}