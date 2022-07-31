using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace GridSystem
{
    public class Test: MonoBehaviour
    {
        private Grid<bool> _grid;
        private void Start()
        {
            _grid = new Grid<bool>(5, 5, 1f, new Vector3(-9, -5), true);
            _grid.SetValue(2, 3 , true);
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                _grid.SetValue(Utils.GetMouseWorldPosition(), true);
            }
        }
    }
}