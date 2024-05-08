using UnityEditor;
using UnityEngine;

namespace Facade
{
    public interface IUndoCanvasProvider
    {
        UndoCanvas UndoCanvas { get; }
    }
}
