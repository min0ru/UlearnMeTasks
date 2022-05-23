using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// Найти угол отскока в радианах для данных угла направления шара и наклона стены.
        /// </summary>
        /// <param name="directionRadians">Угол направления движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns></returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            return 2 * wallInclinationRadians - directionRadians;
        }
    }
}