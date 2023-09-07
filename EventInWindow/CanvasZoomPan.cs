using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RevitDev.EventInWindow
{
    public static class CanvasZoomPan
    {
        private static readonly MatrixTransform _transform = new MatrixTransform();
        private static Point _initialMousePosition;
        private static float Zoomfactor = 1.1f;
        public static void ActiveZoomPan(this Canvas canvas)
        {
            canvas.MouseDown += PanAndZoomCanvas_MouseDown;
            canvas.MouseMove += PanAndZoomCanvas_MouseMove;
            canvas.MouseWheel += PanAndZoomCanvas_MouseWheel;
        }
        private static void PanAndZoomCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle)
            {
                _initialMousePosition = _transform.Inverse.Transform(e.GetPosition(sender as Canvas));
            }
        }
        private static void PanAndZoomCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Point mousePosition = _transform.Inverse.Transform(e.GetPosition(sender as Canvas));
                Vector delta = Point.Subtract(mousePosition, _initialMousePosition);
                var translate = new TranslateTransform(delta.X, delta.Y);
                _transform.Matrix = translate.Value * _transform.Matrix;

                foreach (UIElement child in (sender as Canvas).Children)
                {
                    child.RenderTransform = _transform;
                }
            }
        }
        private static void PanAndZoomCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            float scaleFactor = Zoomfactor;
            if (e.Delta < 0)
            {
                scaleFactor = 1f / scaleFactor;
            }

            Point mousePostion = e.GetPosition(sender as Canvas);

            Matrix scaleMatrix = _transform.Matrix;
            scaleMatrix.ScaleAt(scaleFactor, scaleFactor, mousePostion.X, mousePostion.Y);
            _transform.Matrix = scaleMatrix;

            foreach (UIElement child in (sender as Canvas).Children)
            {
                double x = Canvas.GetLeft(child);
                double y = Canvas.GetTop(child);

                double sx = x * scaleFactor;
                double sy = y * scaleFactor;

                Canvas.SetLeft(child, sx);
                Canvas.SetTop(child, sy);

                child.RenderTransform = _transform;
            }
        }
    }
}
