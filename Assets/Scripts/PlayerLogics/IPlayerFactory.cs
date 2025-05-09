using Configurations;
using UnityEngine;

namespace PlayerLogics
{
    public interface IPlayerFactory
    {
        Player Create(Vector3 position, Quaternion rotation);
    }
}