using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFilter<T>
{
    public bool Filter(T data);
}
