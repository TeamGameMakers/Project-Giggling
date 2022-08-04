using Save;
using UnityEngine;

namespace Puzzle
{
    public class PinLockPuzzle : PuzzleBase
    {
        public Transform pin;
        public Transform cap;
        public Transform bottom;
        public Transform top;

        protected PinLockPuzzleController ctrl;
        protected PinLockPuzzleModel model;

        protected float fBottom;
        protected float fTop;

        protected virtual void Awake()
        {
            ctrl = GetComponent<PinLockPuzzleController>();
            model = GetComponent<PinLockPuzzleModel>();
        }

        protected virtual void Start()
        {
            // TODO: 应该在触发器处直接禁止触发
            if (SaveManager.GetBool(ctrl.saveKey))
            {
                Destroy(gameObject);
            }

            fBottom = bottom.position.y;
            fTop = top.position.y;
            model.maxHeight = fTop - fBottom;

            Vector3 pos = cap.position;
            cap.position = new Vector3(pos.x, fBottom, pos.z);
            pos = pin.position;
            pin.position = new Vector3(pos.x, fBottom, pos.z);
        }

        protected virtual void Update()
        {
            Vector3 pos = cap.position;
            cap.position = new Vector3(pos.x, fBottom + model.Height, pos.z);
            pos = pin.position;
            pin.position = new Vector3(pos.x, fBottom + model.PinH, pos.z);
        }
    }
}
