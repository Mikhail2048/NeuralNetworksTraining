namespace NeuralNetworksTraining
{
    public partial class Form1 : Form
    {

        private TextBox inputValues;
        private TextBox expectedResultTextBox;
        private NumericUpDown testInputValue;
        private NumericUpDown validOutputForTestValue;
        private Label runEpochCounter;
        private Label sampleErrorLabel;
        private Label predictedValueLabel;

        private int currentEpochCounter;
        private NeuralNetwork neuralNetwork;

        private static Form1 singletonInstace;

        public static Form1 getInstance()
        {
            return new Form1();
        }

        private Form1()
        {
            InitializeComponent();
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series.Insert(1, new Syncfusion.Windows.Forms.Chart.ChartSeries("Input"));
            chartControl1.Series.Add(new Syncfusion.Windows.Forms.Chart.ChartSeries("Predictions"));
            Text = "Neural network training window";
            BackColor = Color.Bisque;
            currentEpochCounter = 0;
            setUpInputValues(); 
        }

        private void setUpInputValues() {
            setUpInputFieldFormComponents();
            setUpExpectedFieldFormComponenets();
            setUpTestInputComponents();
            setUpSampleErrorLabel();
            setUpPredictedValueLabel();
            setUpRunEpochCounter();
            setUpButtonsOnTheForm();
            neuralNetwork = NeuralNetwork.getIntance();
        }

        private void setUpRunEpochCounter() {
            runEpochCounter = new Label();
            displayCurrentEpochCounter();
            runEpochCounter.Location = new Point(33, 290);
            runEpochCounter.TextAlign = ContentAlignment.MiddleRight;
            runEpochCounter.Size = new Size(175, 30);
            runEpochCounter.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            Controls.Add(runEpochCounter);
        }

        private void setUpInputFieldFormComponents() {
            Label label = new Label();
            label.Text = "Input values";
            label.Location = new Point(30, 32);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            Controls.Add(label);

            inputValues = new TextBox();
            inputValues.Width = 250;
            inputValues.Height = 50;
            inputValues.BackColor = Color.White;
            inputValues.Location = new Point(135, 30);
            Controls.Add(inputValues);
        }

        private void setUpExpectedFieldFormComponenets() {
            Label expectedResultLabel = new Label();
            expectedResultLabel.Text = "Expected ";
            expectedResultLabel.Location = new Point(33, 83);
            expectedResultLabel.TextAlign = ContentAlignment.MiddleRight;
            expectedResultLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            Controls.Add(expectedResultLabel);

            expectedResultTextBox = new TextBox();
            expectedResultTextBox.Width = 250;
            expectedResultTextBox.Height = 50;
            expectedResultTextBox.BackColor = Color.White;
            expectedResultTextBox.Location = new Point(135, 83);
            Controls.Add(expectedResultTextBox);
        }

        private void setUpTestInputComponents() {
            Label testInputLabel = new Label();
            testInputLabel.Text = "Test input";
            testInputLabel.Location = new Point(30, 132);
            testInputLabel.TextAlign = ContentAlignment.MiddleRight;
            testInputLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            Controls.Add(testInputLabel);

            testInputValue = new NumericUpDown();
            testInputValue.Width = 50;
            testInputValue.Height = 50;
            testInputValue.BackColor = Color.White;
            testInputValue.Location = new Point(135, 130);
            Controls.Add(testInputValue);

            Label validResultLabel = new Label();
            validResultLabel.Text = "Valid result";
            validResultLabel.Location = new Point(180, 132);
            validResultLabel.TextAlign = ContentAlignment.MiddleRight;
            validResultLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            Controls.Add(validResultLabel);

            validOutputForTestValue = new NumericUpDown();
            validOutputForTestValue.Width = 50;
            validOutputForTestValue.Height = 50;
            validOutputForTestValue.BackColor = Color.White;
            validOutputForTestValue.Location = new Point(285, 130);
            Controls.Add(validOutputForTestValue);
        }

        private void setUpButtonsOnTheForm() {
            Button startNewEpoch = new Button();
            startNewEpoch.Text = "Run new epoch";
            startNewEpoch.BackColor = Color.White;
            startNewEpoch.ForeColor = Color.Empty;
            startNewEpoch.Location = new Point(30, 345);
            startNewEpoch.Size = new Size(150, 30);
            startNewEpoch.Click += new EventHandler(this.startNewTrainingEpoch);
            this.Controls.Add(startNewEpoch);

            Button openCheckPrdictionsWindow = new Button();
            openCheckPrdictionsWindow.Text = "Test predictions";
            openCheckPrdictionsWindow.BackColor = Color.White;
            openCheckPrdictionsWindow.ForeColor = Color.Empty;
            openCheckPrdictionsWindow.Location = new Point(210, 345);
            openCheckPrdictionsWindow.Size = new Size(200, 30);
            openCheckPrdictionsWindow.Click += new EventHandler(this.getPrediction);
            this.Controls.Add(openCheckPrdictionsWindow);
        }

        private void setUpSampleErrorLabel() {
            sampleErrorLabel = new Label();
            displaySampleErrorValue(double.NaN);
            sampleErrorLabel.Location = new Point(50, 190);
            sampleErrorLabel.Size = new Size(175, 30);
            sampleErrorLabel.TextAlign = ContentAlignment.MiddleCenter;
            sampleErrorLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            Controls.Add(sampleErrorLabel);
        }

        private void setUpPredictedValueLabel() {
            predictedValueLabel = new Label();
            displayPredictedValue(double.NaN);
            predictedValueLabel.Location = new Point(50, 240);
            predictedValueLabel.Size = new Size(175, 30);
            predictedValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            predictedValueLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            Controls.Add(predictedValueLabel);
        }

        private void getPrediction(object? sender, EventArgs? eventArgs) {
            int testInputAsDecimal = (int) testInputValue.Value;
            int validResult = (int) validOutputForTestValue.Value;
            double predictedResultValue = neuralNetwork.Calculate(testInputAsDecimal);
            double deviation = Math.Abs(validResult - predictedResultValue) / validResult;
            displaySampleErrorValue(deviation);
            displayPredictedValue(predictedResultValue);
            this.chartControl1.Series[1].Points.Add(testInputAsDecimal, predictedResultValue);
            this.chartControl1.Series[0].Points.Add(testInputAsDecimal, validResult);
        }

        private void startNewTrainingEpoch(object? sender, EventArgs? eventArguments) {
            int[]? inputValuesAsInteger = extractIntValuesFromInputArray();
            int[]? outputValuesAsInteger = extractIntValuesFromOutputArray();

            if ((inputValuesAsInteger != null && outputValuesAsInteger != null) 
                && (inputValuesAsInteger.Length != 0 && outputValuesAsInteger.Length != 0)) {
                doStartNewTrainingEpoch(inputValuesAsInteger, outputValuesAsInteger);
            }
        }

        private void doStartNewTrainingEpoch(int[] inputValuesAsInteger, int[] outputValuesAsInteger) {
            if (inputValuesAsInteger.Length != outputValuesAsInteger.Length) {
                MessageBox.Show("Amount of values present in input: " + inputValuesAsInteger.Length + ", in output : " + outputValuesAsInteger.Length + " - should be the same numbers");
            } else {
                neuralNetwork.addInputValuesSet(inputValuesAsInteger);
                neuralNetwork.addOutputValuesSet(outputValuesAsInteger);
                neuralNetwork.runTrainIteration();
                currentEpochCounter++;
                displayCurrentEpochCounter();
                redrawChart();
            }
        }

        private void redrawChart() {
            int[] totalInputValues = neuralNetwork.getTotalInputValues();
            int[] totalOutputValues = neuralNetwork.getTotalOutputValues();

            chartControl1.Series[0].Points.Clear();
            for (int i = 0; i < totalInputValues.Length; i++) {
                chartControl1.Series[0].Points.Add(totalInputValues[i], totalOutputValues[i]);
            }
        }

        private int[]? extractIntValuesFromInputArray() {
            string[] inputValuesStringArray = inputValues.Text.Trim().Split(' ');
            if (inputValuesStringArray.Length != 0) {
                return new ArrayInputParser().extractIntValuesFromArray(inputValuesStringArray);
            }
            return null;
        }        

        private int[]? extractIntValuesFromOutputArray() {
            string[] outputValuesArray = expectedResultTextBox.Text.Split(' ');
            if (outputValuesArray.Length != 0) {
                return new ArrayInputParser().extractIntValuesFromArray(outputValuesArray);
            }
            return null;
        }

        private void displayCurrentEpochCounter() {
            if (runEpochCounter != null) {
                runEpochCounter.Text = "Run epoch counter : " + currentEpochCounter;
                runEpochCounter.Show();
            }
        }

        private void displaySampleErrorValue(double sampleErrorValue) {
            if (sampleErrorLabel != null) {
                sampleErrorLabel.Text = "Sample error : " + getRoundedDoubleValueElseDash(sampleErrorValue);
                sampleErrorLabel.Show();
            }
        }

        private void displayPredictedValue(double predictedValue) {
            if (predictedValueLabel != null) {
                predictedValueLabel.Text = "Predicted value: " + getRoundedDoubleValueElseDash(predictedValue);
                predictedValueLabel.Show();
            }
        }

        private string getRoundedDoubleValueElseDash(double sampleErrorValue) {
            return sampleErrorValue.Equals(double.NaN) ? "-" : Convert.ToString(Math.Round(sampleErrorValue, 2));
        }
    }
}