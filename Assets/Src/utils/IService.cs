using System;

namespace Src.utils
{
    public interface IService:IDisposable
    {
        void Update();
    }
}