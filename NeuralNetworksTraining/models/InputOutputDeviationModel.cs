namespace NeuralNetworksTraining {
    public class InputOutputDeviationModel : InputOutputModel {
        private double deviance;
        private double predictedResult;

        public InputOutputDeviationModel(int[] inputValues, int[] outputValues) : base(inputValues, outputValues) {}

        public double getDeviance() {
            return deviance;
        }

        public double getPredictedResult() {
            return predictedResult;
        }

        public void setDeviance(double deviance) {
            this.deviance = deviance;
        }

        public void setPredictedResult(double predictedResult) {
            this.predictedResult = predictedResult;
        }
    }
}