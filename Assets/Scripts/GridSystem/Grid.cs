using System;
using UnityEngine;
using Utilities;

namespace GridSystem
{
    public class Grid<TGridObject>
    {
        public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged; 

        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly Vector3 _originPosition;
        private readonly TGridObject[,] _gridArray;
        
        #if UNITY_EDITOR
        
        private readonly TextMesh[,] _debugTextArray;
        
        #endif

        /// <param name="width">网格宽度</param>
        /// <param name="height">网格高度</param>
        /// <param name="cellSize">网格大小</param>
        /// <param name="originPosition">原点</param>
        /// <param name="showDebugView">是否显示Debug视图</param>
        public Grid(int width, int height, float cellSize, Vector3 originPosition, bool showDebugView = false)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;
            
            _gridArray = new TGridObject[width, height];
                
#if UNITY_EDITOR

            if (!showDebugView) return;
            
            _debugTextArray = new TextMesh[width, height];
            GameObject go = new GameObject("Grid Info");
            
            for (int x = 0; x < width; x ++)
            {
                for (int y = 0; y < height; y++)
                {
                    _debugTextArray[x, y] = Utils.CreateWorldText(go.transform, _gridArray[x, y]?.ToString(), 
                        GetWorldPosition(x, y) + Vector3.one * cellSize * 0.5f,
                        TextAnchor.MiddleCenter, TextAlignment.Left, 30, 0.12f, Color.white);
                    
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }
            }
            
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, height), GetWorldPosition(width, 0), Color.white, 100f);

            OnGridValueChanged += (sender, args) =>
            {
                _debugTextArray[args.x, args.y].text = _gridArray[args.x, args.y].ToString();
            };

#endif
        }

        private Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * _cellSize + _originPosition;

        private void GetXY(Vector3 position, out int x, out int y)
        {
            x = Mathf.FloorToInt((position - _originPosition).x / _cellSize);
            y = Mathf.FloorToInt((position - _originPosition).y / _cellSize);
        }

        public void SetValue(int x, int y, TGridObject value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
                
                if (OnGridValueChanged != null)
                {
                    OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
                }
            }
        }

        public void SetValue(Vector3 position, TGridObject value)
        {
            GetXY(position, out var x, out var y);
            
            SetValue(x, y, value);
        }

        public TGridObject GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
                return _gridArray[x, y];
            else
            {
                Debug.LogError("Boundary Exceed!");
                return default(TGridObject);
            }
        }

        public TGridObject GetValue(Vector3 position)
        {
            GetXY(position, out var x, out var y);
            return GetValue(x, y);
        }
        
        public sealed class OnGridValueChangedEventArgs: EventArgs
        {
            public int x;
            public int y;
        }

    }
}