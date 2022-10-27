using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Kolmogor
{
    internal static class KolmogorMath
    {
        public const double TimeDelta = 1e-3;
        public const int MaxStatesCount = 10;

        public static Matrix<double> GetUltimatePropabilities(Matrix<double> matrix)
        {
            var coefsMatrix = BuildCoefsMatrix(matrix);
            var augmMatrix = BuildAugmentationMatrix(matrix.RowCount);

            return coefsMatrix.Solve(augmMatrix);
        }

        public static IEnumerable<double> GetStabilizationTimes(Matrix<double> matrix, Vector<double> ultimatePropabilities)
        {
            int n = matrix.RowCount;
            double currTime = 0;
            Vector<double> currPropabilities = Vector<double>.Build.Dense(ultimatePropabilities.Count);
            Vector<double> stabilizationTimes = Vector<double>.Build.Dense(n);

            double totalLambdaSum = matrix.RowSums().Sum() * MaxStatesCount;
            double[] Eps = ultimatePropabilities.Select(p => p / totalLambdaSum).ToArray();

            while (!stabilizationTimes.All(p => Math.Abs(p) >= 1e-3))
            {
                var currDps = Dps(matrix, currPropabilities).ToArray();
                for (int i = 0; i < n; i++)
                {
                    if (Math.Abs(stabilizationTimes[i]) < 1e-3 && currDps[i] <= Eps[i] && Math.Abs(currPropabilities[i] - ultimatePropabilities[i]) <= Eps[i])
                        stabilizationTimes[i] = currTime;

                    currPropabilities[i] += currDps[i];
                }

                currTime += TimeDelta;
            }

            return stabilizationTimes;
        }

        static IEnumerable<double> Dps(Matrix<double> matrix, Vector<double> probabilities)
        {
            int n = matrix.RowCount;

            double[] res = new double[n];

            for (int i = 0; i < n; i++)
            {
                double sum = 0;

                for (int j = 0; j < n; j++)
                    sum += i != j ? probabilities[j] * matrix[j, i] : probabilities[j] * (matrix[i, i] - matrix.RowSums()[i]);
                
                res[i] = sum * TimeDelta;
            }

            return res;
        }

        static Matrix<double> BuildAugmentationMatrix(int count)
        {
            Matrix<double> matrix = Matrix<double>.Build.Dense(count, 1);
            matrix[count - 1, 0] = 1;

            return matrix;
        }

        static Matrix<double> BuildCoefsMatrix(Matrix<double> matrix)
        {
            Matrix<double> res = Matrix<double>.Build.Dense(matrix.RowCount, matrix.ColumnCount);
            int n = matrix.RowCount;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n; j++)
                    res[i, i] -= matrix[i, j];
                for (int j = 0; j < n; j++)
                    res[i, j] += matrix[j, i];
            }

            for (int i = 0; i < n; i++)
                res[n - 1, i] = 1;

            return res;
        }
    }
}
