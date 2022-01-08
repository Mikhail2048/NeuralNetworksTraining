using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworksTraining
{
    internal class ArrayInputParser {
        private ArrayInputParser() { }

        private static ArrayInputParser? instance;

        public static ArrayInputParser getInstance() {
            if (instance == null) {
                instance = new ArrayInputParser();
            }
            return instance;
        }

        public int[]? extractIntValuesFromArray(string[] array) {
            int[] resultArray = new int[getAmountOfNonEmptyElements(array)];
            int curretIndex = 0;
            foreach (var item in array) {
                if (item.Trim().Length != 0) {
                    try {
                        int number = Int32.Parse(item.Trim());
                        resultArray[curretIndex] = number;
                        curretIndex++;
                    } catch (FormatException) {
                        MessageBox.Show("Unable to convert to integer number value : " + item);
                        return null;
                    }
                }
            }
            return resultArray;
        }

        public bool elementAlreadyPresentInArray(int[] array, int value) {
            if (array != null && array.Length != 0) {
                for (int i = 0; i < array.Length; i++) {
                    if (array[i] == value) {
                        return true;
                    } 
                }
            }
            return false;
        }

        private int getAmountOfNonEmptyElements(string[] array) {
            if (array != null) {
                int amountOfNonEmptyElementsInArray = 0;
                foreach (var item in array) {
                    if (!String.IsNullOrWhiteSpace(item)) {
                        amountOfNonEmptyElementsInArray++;
                    }
                }
                return amountOfNonEmptyElementsInArray;
            }
            return 0;
        }
    }
}
