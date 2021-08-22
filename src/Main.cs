using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Calculator
{
    public partial class Main : Form
    {
        public static Button[] numericButtons = new Button[10];

        private const int textLimit = 10;

        private bool canUseOperation    = true;
        private bool canAddDecimal      = true;
        private bool hasDecimals        = false;
        private bool canUseNegate       = true;

        private List<bool> canUseOperationHistory   = new List<bool>();
        private List<bool> canAddDecimalHistory     = new List<bool>();
        private List<bool> hasDecimalsHistory       = new List<bool>();
        private List<bool> canUseNegateHistory      = new List<bool>();

        private string calculation;

        public Main()
        {
            InitializeComponent();
            SaveAction();
            SetCalculationText("0");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool AddToCalculationText(string input)
        {
            // Fix this

            if (calculation.Length + 1 != textLimit)
            {
                LabelCalculation.Text += input;
                calculation += input;
                return true;
            }
            else return false;
        }

        private void SetCalculationText(string input)
        {
            LabelCalculation.Text = input;
            calculation = input;
        }

        private void ClearCalculationText()
        {
            LabelCalculation.Text = "";
            calculation = "";
        }

        private void RemoveLastCalculationText()
        {
            LabelCalculation.Text = LabelCalculation.Text.Substring(0, LabelCalculation.Text.Length - 1);
            calculation = calculation.Substring(0, calculation.Length - 1);
        }

        private void SaveAction()
        {
            canAddDecimalHistory.Add(canAddDecimal);
            hasDecimalsHistory.Add(hasDecimals);
            canUseOperationHistory.Add(canUseOperation);
            canUseNegateHistory.Add(canUseNegate);
        }

        private void RemoveLastAction()
        {
            if (canAddDecimalHistory.Count - 1 > 0)
            {
                canAddDecimalHistory.RemoveLastItem();
                hasDecimalsHistory.RemoveLastItem();
                canUseOperationHistory.RemoveLastItem();
                canUseNegateHistory.RemoveLastItem();

                canAddDecimal = canAddDecimalHistory.LastItem();
                hasDecimals = hasDecimalsHistory.LastItem();
                canUseOperation = canUseOperationHistory.LastItem();
                canUseNegate = canUseNegateHistory.LastItem();
            }
            else
            {
                RemoveAllActions();
                SaveAction();
            }            
        }

        private void RemoveAllActions()
        {
            canAddDecimalHistory.Clear();
            hasDecimalsHistory.Clear();
            canUseOperationHistory.Clear();
            canUseNegateHistory.Clear();
        }

        private void Clear()
        {
            hasDecimals = false;
            canAddDecimal = true;
            canUseOperation = true;
            canUseNegate = true;

            RemoveAllActions();
            SaveAction();

            SetCalculationText("0");
        }

        private void AddNumberToLabel(string number)
        {
            if (LabelCalculation.Text == "0")
            {
                ClearCalculationText();
            }
            if (!canUseOperation) canUseOperation = true;
            if (!hasDecimals) canAddDecimal = true;
            else
            {
                hasDecimals = true;
                canAddDecimal = false;
            }
            canUseNegate = false;
            AddToCalculationText(number);

            SaveAction();
        }

        private void AddOperationToLabel(string operationText)
        {
            if (canUseOperation)
            {
                if (AddToCalculationText(operationText))
                {
                    canUseOperation = false;
                    canAddDecimal = false;
                    hasDecimals = false;
                    canUseNegate = true;

                    SaveAction();
                }               
            }
        }       

        private void AddDecimalToLabel()
        {
            if (canAddDecimal)
            {
                if (AddToCalculationText("."))
                {
                    canAddDecimal = false;
                    hasDecimals = true;
                    canUseOperation = false;
                    canUseNegate = false;

                    SaveAction();
                }            
            }
        }

        private void AddNegationToLabel()
        {
            if (canUseNegate)
            {
                if (LabelCalculation.Text == "0")
                {
                    SetCalculationText("-");

                    canUseNegate = false;
                    canUseOperation = false;

                    SaveAction();
                }
                else
                {
                    if (AddToCalculationText("-"))
                    {
                        canUseNegate = false;
                        canUseOperation = false;

                        SaveAction();
                    }
                }             
            }
        }

        public void RemoveLastEntryFromlabel()
        {
            int endIndex = LabelCalculation.Text.Length - 1;
            if (endIndex - 1 >= 0)
            {
                RemoveLastAction();
                RemoveLastCalculationText();
            }
            else
            {
                Clear();
            }
        }

        private void OnNumberButtonPressed(string number)
        {
            AddNumberToLabel(number);
        }

        private void buttonNegate_Click(object sender, EventArgs e)
        {
            AddNegationToLabel();
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            AddDecimalToLabel();
        }         

        private void buttonZero_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("0");
        }

        private void buttonOne_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("1");
        }

        private void buttonTwo_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("2");
        }

        private void buttonThree_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("3");
        }

        private void buttonFour_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("4");
        }

        private void buttonFive_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("5");
        }

        private void buttonSix_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("6");
        }

        private void buttonSeven_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("7");
        }

        private void buttonEight_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("8");
        }

        private void buttonNine_Click(object sender, EventArgs e)
        {
            OnNumberButtonPressed("9");
        }      

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddOperationToLabel("+");
        }             

        private void buttonSubtract_Click(object sender, EventArgs e)
        {
            AddOperationToLabel("-");
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            AddOperationToLabel("x");
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            AddOperationToLabel("/");
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            SetCalculationText(Calculator.Calculate(calculation));

            // Fix this

            hasDecimals = Calculator.HasDecimals(calculation);
            canAddDecimal = !hasDecimals;
            canUseOperation = true;
            canUseNegate = false;

            RemoveAllActions();
            SaveAction();
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (Calculator.IsUndefined(calculation))
            {
                Clear();
            }
            else if (calculation.Length > 0)
            {
                RemoveLastEntryFromlabel();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public static class ExtensionBoolList
    {
        public static bool LastItem(this List<bool> list)
        {
            if (list.Count - 1 < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else return list[list.Count - 1];
        }

        public static void RemoveLastItem(this List<bool> list)
        {
            if (list.Count - 1 < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else list.RemoveAt(list.Count - 1);
        }
    }
}
