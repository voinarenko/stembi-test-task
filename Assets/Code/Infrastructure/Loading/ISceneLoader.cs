using Cysharp.Threading.Tasks;
using System;

namespace Code.Infrastructure.Loading
{
  public interface ISceneLoader
  {
    UniTaskVoid Load(string name, Action onLoaded = null);
  }
}