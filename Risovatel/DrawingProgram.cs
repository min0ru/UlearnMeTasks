using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RefactorMe
{
    // TODO: Make class non-static with constructor
    public static class Drawer
    {
        private static float _x, _y;
        private static Graphics _canvas;

        public static void SetCanvas(Graphics newCanvas)
        {
            _canvas = newCanvas;
            _canvas.SmoothingMode = SmoothingMode.None;
            _canvas.Clear(Color.Black);
        }

        public static void SetPosition(float x, float y)
        {
            _x = x;
            _y = y;
        }

        public static void Draw(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x = (float)(_x + length * Math.Cos(angle));
            var y = (float)(_y + length * Math.Sin(angle));
            _canvas.DrawLine(pen, _x, _y, x, y);
            _x = x;
            _y = y;
        }

        public static void MoveBy(double length, double angle)
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
            Pen penColor,
            double edgeLength,
            double edgeWidth,
            double directionAngle,
            Graphics canvas)
        {
            // TODO: canvas is unused, refactor it to drawer object
            Drawer.Draw(penColor, edgeLength, directionAngle);
            Drawer.Draw(penColor, edgeWidth * Math.Sqrt(2), directionAngle + Math.PI / 4);
            Drawer.Draw(penColor, edgeLength, directionAngle + Math.PI);
            Drawer.Draw(penColor, edgeLength - edgeWidth, directionAngle + Math.PI / 2);
            Drawer.MoveBy(edgeWidth, directionAngle - Math.PI);
            Drawer.MoveBy(edgeWidth * Math.Sqrt(2), directionAngle + 3 * Math.PI / 4);
        }

        public static void Draw(
            int canvasWidth,
            int canvasHeight,
            double rotationAngle,
            Graphics canvas)
        {
            Drawer.SetCanvas(canvas);

            var minCanvasSide = Math.Min(canvasWidth, canvasHeight);
            double edgeLength = minCanvasSide * EdgeLengthCoefficient;
            double edgeWidth = minCanvasSide * EdgeWidthCoefficient;

            var diagonalLength = Math.Sqrt(2) * (edgeLength + edgeWidth) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + canvasWidth / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + canvasHeight / 2f;

            Drawer.SetPosition(x0, y0);

            DrawEdge(PenColor, edgeLength, edgeWidth, rotationAngle + 0, canvas);
            DrawEdge(PenColor, edgeLength, edgeWidth, rotationAngle - Math.PI / 2, canvas);
            DrawEdge(PenColor, edgeLength, edgeWidth, rotationAngle + Math.PI, canvas);
            DrawEdge(PenColor, edgeLength, edgeWidth, rotationAngle + Math.PI / 2, canvas);
        }
    }
}