using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RefactorMe
{
    public class Drawer
    {
        private float _x, _y;
        private Graphics _canvas;
        private Pen _pen;

        public Drawer(Graphics canvas, Pen pen)
        {
            _canvas = canvas;
            _canvas.SmoothingMode = SmoothingMode.None;
            _canvas.Clear(Color.Black);
            _x = 0;
            _y = 0;
            _pen = pen;
        }

        public void SetPosition(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public void DrawLine(double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x = (float)(_x + length * Math.Cos(angle));
            var y = (float)(_y + length * Math.Sin(angle));
            _canvas.DrawLine(_pen, _x, _y, x, y);
            _x = x;
            _y = y;
        }

        public void MoveBy(double length, double angle)
        {
            _x = (float)(_x + length * Math.Cos(angle));
            _y = (float)(_y + length * Math.Sin(angle));
        }
    }

    public static class ImpossibleSquare
    {
        private static readonly Pen PenColor = Pens.Yellow;
        private const float EdgeLengthCoefficient = 0.375f;
        private const float EdgeWidthCoefficient = 0.04f;

        private static void DrawEdge(
            double edgeLength,
            double edgeWidth,
            double directionAngle,
            Drawer drawer)
        {
            drawer.DrawLine(edgeLength, directionAngle);
            drawer.DrawLine(edgeWidth * Math.Sqrt(2), directionAngle + Math.PI / 4);
            drawer.DrawLine(edgeLength, directionAngle + Math.PI);
            drawer.DrawLine(edgeLength - edgeWidth, directionAngle + Math.PI / 2);
            drawer.MoveBy(edgeWidth, directionAngle - Math.PI);
            drawer.MoveBy(edgeWidth * Math.Sqrt(2), directionAngle + 3 * Math.PI / 4);
        }

        public static void Draw(
            int canvasWidth,
            int canvasHeight,
            double rotationAngle,
            Graphics canvas)
        {
            var drawer = new Drawer(canvas, PenColor);

            var minCanvasSide = Math.Min(canvasWidth, canvasHeight);
            double edgeLength = minCanvasSide * EdgeLengthCoefficient;
            double edgeWidth = minCanvasSide * EdgeWidthCoefficient;

            var diagonalLength = Math.Sqrt(2) * (edgeLength + edgeWidth) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + canvasWidth / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + canvasHeight / 2f;
            drawer.SetPosition(x0, y0);

            DrawEdge(edgeLength, edgeWidth, rotationAngle + 0, drawer);
            DrawEdge(edgeLength, edgeWidth, rotationAngle - Math.PI / 2, drawer);
            DrawEdge(edgeLength, edgeWidth, rotationAngle + Math.PI, drawer);
            DrawEdge(edgeLength, edgeWidth, rotationAngle + Math.PI / 2, drawer);
        }
    }
}