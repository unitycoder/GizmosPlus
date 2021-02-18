using System;
using UnityEngine;

namespace Zchfvy.Plus {
    /// <summary>
    /// Class for drawing Gizmos outside of OnDrawGizmos
    /// </summary>
    public static class GizmosPlusAsync {
        private static GizmosPlusAsyncDrawer drawer;

        private static GizmosPlusAsyncDrawer GetOrCreateDrawer() {
            if (drawer == null) {
                var go = new GameObject("_GizmosPlusAsyncDrawer");
                drawer = go.AddComponent<GizmosPlusAsyncDrawer>();
            }

            return drawer;
        }

        /// <summary>
        /// Allows drawing of Gizmos outside of OnDrawGizmos flow
        /// </summary>
        /// <param name="drawFunc">
        /// A lambda expression containing relevant drawing code
        /// </param>
        /// <example>
        /// In some Main Thread function other than OnDrawGizmos
        /// <code>
        /// GizmosPlusAsync.DrawAsync(() => {
        ///     Gizmos.DrawLine(...);
        ///     GizmosPlus.DrawCross(...);
        /// });
        /// </code>
        /// </example>
        public static void DrawAsync(Action drawFunc) {
            GetOrCreateDrawer().Enqueue(drawFunc);
        }

        /// <summary>
        /// Performs the provided draw logic when the specified game object is selected in the
        /// scene view.
        /// </summary>
        ///
        /// <param name="gameObject">
        /// The object that must be selected in order for the gizmos to be drawn.
        /// </param>
        /// <param name="drawFunc">
        /// A lambda expression that performs the gizmo drawing logic.
        /// </param>
        ///
        /// <example>
        /// In some main thread function other than <c>OnDrawGizmosSelected()</c>:
        /// <code>
        /// GizmosPlusAsync.DrawSelectedAsync(targetObject, () => {
        ///     Gizmos.DrawLine(...);
        ///     GizmosPlus.DrawCross(...);
        /// });
        /// </code>
        /// </example>
        public static void DrawSelectedAsync(this GameObject gameObject, Action drawFunc) {
            GetOrCreateDrawer().EnqueueSelected(gameObject, drawFunc);
        }

        /// <summary>
        /// Performs the provided draw logic when the specified component is selected in the
        /// scene view.
        /// </summary>
        ///
        /// <param name="component">
        /// A component on the object that must be selected in order for the gizmos to be drawn.
        /// </param>
        /// <param name="drawFunc">
        /// A lambda expression that performs the gizmo drawing logic.
        /// </param>
        ///
        /// <example>
        /// In some main thread function other than <c>OnDrawGizmosSelected()</c>:
        /// <code>
        /// GizmosPlusAsync.DrawSelectedAsync(this, () => {
        ///     Gizmos.DrawLine(...);
        ///     GizmosPlus.DrawCross(...);
        /// });
        /// </code>
        /// </example>
        public static void DrawSelectedAsync<T>(this T component, Action drawFunc) where T : Component {
            DrawSelectedAsync(component.gameObject, drawFunc);
        }
    }
}
