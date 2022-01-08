namespace NeuralNetworksTraining
{
    public class NeuralNetworkController {

        private NeuralNetwork? neuralNetwork;

        private static NeuralNetworkController? neuralNetworkController;

        public static NeuralNetworkController getInstance() {
            if (neuralNetworkController == null) {
                neuralNetworkController = new NeuralNetworkController();
            }
            return neuralNetworkController;
        }

        private NeuralNetworkController() {
            neuralNetwork = NeuralNetwork.getIntance();
        }

        public bool doStartNewTrainingEpoch(InputOutputModel inputOutputModel) {
            if (inputOutputModel.getInputValues().Length != inputOutputModel.getOutputValues().Length) {
                MessageBox.Show("Amount of values present in input: " +  inputOutputModel.getInputValues().Length +  ", in output : " + inputOutputModel.getOutputValues().Length + " - should be the same numbers");
                return false;
            } else {
                neuralNetwork.addInputValuesSet(inputOutputModel.getInputValues());
                neuralNetwork.addOutputValuesSet(inputOutputModel.getOutputValues());
                neuralNetwork.runTrainIteration();
                return true;
            }
        }

        public void predictValue(InputOutputDeviationModel inputOutputDeviationModel) {
            int testInputValue = inputOutputDeviationModel.getInputValues()[0];
            int expectedValidOutput = inputOutputDeviationModel.getOutputValues()[0];
            double predictedResultValue = neuralNetwork.Calculate(testInputValue);
            double deviation = Math.Abs(expectedValidOutput - predictedResultValue) / expectedValidOutput;
            inputOutputDeviationModel.setPredictedResult(predictedResultValue);
            inputOutputDeviationModel.setDeviance(deviation);
        }
    }
}