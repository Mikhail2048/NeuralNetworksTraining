namespace NeuralNetworksTraining {
    public class InputOutputModel {
        
        private int[] inputValues;
        private int[] outputValues;

        public InputOutputModel(int[] inputValues, int[] outputValues) {
            this.outputValues = outputValues;
            this.inputValues = inputValues;
        }

        public int[] getInputValues() {
            return inputValues;
        }

        public int[] getOutputValues() {
            return outputValues;
        }
    }
}