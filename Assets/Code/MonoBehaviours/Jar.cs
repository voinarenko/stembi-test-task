using UnityEngine;

namespace Code.MonoBehaviours
{
  public class Jar : MonoBehaviour
  {
    [SerializeField] private Transform _container;

    public Transform GetContainer() =>
      _container;
  }
}