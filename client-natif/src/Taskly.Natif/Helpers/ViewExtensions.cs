using System.Runtime.CompilerServices;

namespace Taskly.Natif.Helpers
{
    public static class ViewExtensions
    {
        public static Point GetAbsolutePosition(this VisualElement view)
        {
            double x = view.X;
            double y = view.Y;
            Element parent = view.Parent;

            while (parent is VisualElement visualParent)
            {
                x += visualParent.X;
                y += visualParent.Y;
                parent = visualParent.Parent;
            }

            return new Point(x, y);
        }

        public static Rect GetBoundsRelativeToParent(this VisualElement view, VisualElement parent)
        {
            var position = new Point(view.X, view.Y);
            var current = view.Parent as VisualElement;

            while (current != null && current != parent)
            {
                position = new Point(position.X + current.X, position.Y + current.Y);
                current = current.Parent as VisualElement;
            }

            return new Rect(position, new Size(view.Width, view.Height));
        }
    }
}
