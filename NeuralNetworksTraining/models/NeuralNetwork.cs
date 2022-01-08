using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace NeuralNetworksTraining
{
    public class NeuralNetwork {
        
        private int[] inputValues;
        private int currentInputValuesIndex;

        private int[] outputValues;
        private int currentOutputValuesIndex;

        private const int calculationMatrixOrder = 2;
        private Vector<double>? weigthsVector;
        
        private static NeuralNetwork? instance;

        public static NeuralNetwork getIntance() {
            if (instance == null) {
                instance = new NeuralNetwork(100, 100);
            }
            return instance;
        }

        private NeuralNetwork(int maxMountOfInputValues, int maxMountOfOutputValues) {
            this.inputValues = new int[maxMountOfInputValues]; 
            this.outputValues = new int[maxMountOfOutputValues];
            this.currentInputValuesIndex = 0;
            this.currentOutputValuesIndex = 0;
        }

        public void addInputValuesSet(int[] additionalInputValues) {
            for (int index = 0; index < additionalInputValues.Length && currentInputValuesIndex < inputValues.Length;) {
                inputValues[currentInputValuesIndex] = additionalInputValues[index];
                currentInputValuesIndex++;
                index++;
            }
        }

        public void addOutputValuesSet(int[] additionalOutputValues) {
            for (int index = 0; index < additionalOutputValues.Length && currentOutputValuesIndex < outputValues.Length;) {
                outputValues[currentOutputValuesIndex] = additionalOutputValues[index];
                currentOutputValuesIndex++;
                index++;
            }
        }

        public int[] getTotalInputValues() {
            return inputValues;
        }

        public int[] getTotalOutputValues() {
            return outputValues;
        }

        private double[] getOutputValuesAsDoubleArray() {
            double[] array = new double[outputValues.Length];
            for (int i = 0; i < outputValues.Length; i++) {
                array[i] = outputValues[i];
            }
            return array;
        }

        private double[] getInputValuesAsDoubleArray() {
            double[] array = new double[inputValues.Length];
            for (int i = 0; i < inputValues.Length; i++) {
                array[i] = inputValues[i];
            }
            return array;
        }

        public void runTrainIteration() {
            DenseVector input = new DenseVector(getInputValuesAsDoubleArray());
            DenseVector output = new DenseVector(getOutputValuesAsDoubleArray());

            var vandMatrix = new DenseMatrix(input.Count, calculationMatrixOrder + 1);
            for (int i = 0; i < input.Count; i++) {
                double mult = 1;
                for (int j = 0; j < calculationMatrixOrder + 1; j++) {
                    vandMatrix[i, j] = mult;
                    mult *= input[i];
                }
            }

            weigthsVector = vandMatrix.TransposeThisAndMultiply(vandMatrix).LU().Solve(TransposeAndMult(vandMatrix, output));
        }

        private Vector<double> VandermondeRow(double x) {
            double[] result = new double[calculationMatrixOrder + 1];
            double mult = 1;
            for (int i = 0; i <= calculationMatrixOrder; i++) {
                result[i] = mult;
                mult *= x;
            }
            return new DenseVector(result);
        }

        private static DenseVector TransposeAndMult(Matrix m, Vector v) {
            var result = new DenseVector(m.ColumnCount);
            for (int j = 0; j < m.RowCount; j++)
                for (int i = 0; i < m.ColumnCount; i++)
                    result[i] += m[j, i] * v[j];
            return result;
        }

        public double Calculate(int x) {
            return VandermondeRow(x) * weigthsVector;
        }

        public Vector<double>? getCurrentNetworkWeights() {
            return weigthsVector;
        }
    }
}