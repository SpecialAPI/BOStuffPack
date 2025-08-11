using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack
{
    [ExecuteInEditMode]
    public class SortingOrderSetter : MonoBehaviour
    {
        private Renderer _renderer;
        public int targetSOrder;

        public void Awake()
        {
            _renderer = GetComponent<Renderer>();

            if (_renderer)
                _renderer.sortingOrder = targetSOrder;
        }

        public void Update()
        {
            if (_renderer)
                _renderer.sortingOrder = targetSOrder;
        }
    }
}
