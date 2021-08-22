using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static class Calculator
    {
        private static char[] digits = new char[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
        private static char[] binaryOperations = new char[]
        {
            'x', '/', '+', '-'
        };
        private static char decimalPoint = '.';

        public static bool IsUndefined(string input)
        {
            if (input == "Undefined") return true;
            else return false;
        }

        public static bool HasDecimals(string input)
        {
            foreach (char c in input)
            {
                if (c == decimalPoint) return true;
            }
            return false;
        }

        private static bool IsComputable(string equation)
        {
            foreach (char c in equation)
            {
                if (!IsDigit(c) && !IsDecimalPoint(c) && !IsBinaryOperation(c)) return false;
            }

            return true;
        }

        private static bool IsDigit(char c)
        {
            foreach (char digit in digits)
            {
                if (c == digit) return true;
            }
            return false;
        }

        private static bool IsBinaryOperation(char c)
        {
            foreach (char symbol in binaryOperations)
            {
                if (c == symbol) return true;
            }
            return false;
        }

        private static bool IsDecimalPoint(char c)
        {
            if (c == decimalPoint) return true;
            return false;
        }

        private static List<string> SplitEquation(string input)
        {
            List<string> splitInput = new List<string>();

            int i = 0;
            bool negateNextNumber = false;
            if (i != input.Length && input[i] == '-')
            {
                negateNextNumber = true;
                i++;
            }

            while (i != input.Length)
            {
                if (i != input.Length)
                {
                    string number = "";
                    while (i != input.Length && (IsDigit(input[i]) || IsDecimalPoint(input[i])))
                    {
                        if (negateNextNumber)
                        {
                            number += "-" + input[i];
                            negateNextNumber = false;
                        }
                        else
                        {
                            number += input[i];
                        }
                        i++;
                    }
                    if (number != "") splitInput.Add(number);

                    while (i != input.Length && IsBinaryOperation(input[i]))
                    {
                        splitInput.Add(Convert.ToString(input[i]));
                        if (i + 1 != input.Length && input[i + 1] == '-')
                        {
                            negateNextNumber = true;
                            i++;
                        }
                        i++;
                    }
                }              
            }

            return splitInput;
        }

        private static void CalculateBinaryOperation(List<string> equationList, char operation)
        {
            string operationString = Convert.ToString(operation);
            int operationIndex = equationList.FindIndex(s => s == operationString);
            while (operationIndex != -1)
            {
                if (operationIndex - 1 < 0)
                {
                    equationList.Add("0");
                    equationList.Add("+");
                    equationList.Add("0");
                }

                if (operationIndex + 1 >= equationList.Count)
                {
                    operation = '+';
                    equationList.Add("0");
                }

                double left = Convert.ToDouble(equationList[operationIndex - 1]);
                double right = Convert.ToDouble(equationList[operationIndex + 1]);

                switch (operation)
                {
                    case 'x':
                        equationList[operationIndex] = Convert.ToString(left * right);
                        break;
                    case '/':
                        equationList[operationIndex] = Convert.ToString(left / right);
                        break;
                    case '+':
                        equationList[operationIndex] = Convert.ToString(left + right);
                        break;
                    case '-':
                        equationList[operationIndex] = Convert.ToString(left - right);
                        break;
                }

                equationList.RemoveAt(operationIndex - 1);
                operationIndex--;
                equationList.RemoveAt(operationIndex + 1);
                operationIndex--;

                operationIndex = equationList.FindIndex(s => s == operationString);
            }
        }

        public static string Calculate(string equation)
        {
            if (IsComputable(equation))
            {
                List<string> equationList = SplitEquation(equation);

                foreach (char binaryOperation in binaryOperations)
                {
                    CalculateBinaryOperation(equationList, binaryOperation);
                }

                if (equationList.Count <= 0 || !IsComputable(equationList[0])) return "Undefined";
                else return equationList[0];
            }
            else
            {
                return "Undefined";
            }
        }
    }
}
