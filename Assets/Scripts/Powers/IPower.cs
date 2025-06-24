using UnityEngine;

namespace DefaultNamespace.Powers
{
    public interface IPower
    {
        string Name { get; }

        string Description { get; }

        void Use(TrickManager trickManager);
    }
}