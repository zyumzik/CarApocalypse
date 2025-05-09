using System;

namespace Core.Interfaces
{
    public interface IInput
    {
        event Action<float> OnInput;
    }
}