using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Calculator
{
    public partial class Form1 : Form
    {
        #region private
        private string result;
        private double firstNumber;
        private double secondNumber;
        private string symbolOperation;
        private string symbolSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private bool firstNumSet = false;
        private bool secondNumSet = false;
        private bool resultSet = false;
        private bool symbolSet = false;
        private bool separatorUsed = false;
        private bool toClear = false;

        private string boxResult2 = "0";
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void drawResults()
        {
            textBoxResult.Text = "";
            if (firstNumSet)
            {
                textBoxResult.Text += firstNumber.ToString();
                if (symbolSet)
                {
                    textBoxResult.Text += " " + symbolOperation + " ";
                    if(secondNumSet)
                    {
                        textBoxResult.Text += secondNumber.ToString();
                    }
                }
            }
            textBoxResult2.Text = boxResult2;
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (resultSet)
            {
                separatorUsed = false;
                firstNumSet = false;
                secondNumSet = false;
                resultSet = false;
                symbolSet = false;
            }
            if (toClear == true)
            {
                boxResult2 = "";
                toClear = false;
            }
            if (boxResult2.Length <= 15)
            {
                resultSet = false;
                if (boxResult2 == "0")
                {
                    boxResult2 = btn.Text;
                }
                else boxResult2 += btn.Text;
            }
            drawResults();
        }

        private void buttonSymbol_Click(object sender, EventArgs e)
        {
            resultSet = false;
            Button btn = (Button)sender;
            if (!symbolSet)
            {
                if (!firstNumSet)
                {
                    if(boxResult2 != "")
                    {
                        Double.TryParse(boxResult2, out firstNumber);
                        firstNumSet = true;
                        symbolOperation = btn.Text;
                        symbolSet = true;
                        toClear = true;
                        separatorUsed = false;
                    }
                }
            }
            else
            {
                if (toClear)
                {
                    symbolOperation = btn.Text;
                    secondNumSet = false;
                }
                else
                {
                    Double.TryParse(boxResult2, out secondNumber);
                    calculate();
                    boxResult2 = result;
                    Double.TryParse(result, out firstNumber);
                    toClear = true;
                    separatorUsed = false;
                    symbolOperation = btn.Text;
                }

            }
            drawResults();
        }

        private void calculate()
        {
            if (symbolOperation == "+")
            {
                result = (firstNumber + secondNumber).ToString();
            }
            else if (symbolOperation == "-")
            {
                result = (firstNumber - secondNumber).ToString();
            }
            else if (symbolOperation == "x")
            {
                result = (firstNumber * secondNumber).ToString();
            }
            else if (symbolOperation == "/")
            {
                if(secondNumber == 0)
                {
                    result = "You cannot divide by zero.";
                }else result = (firstNumber / secondNumber).ToString();
            }
        }
        private void buttonEqual_Click(object sender, EventArgs e)
        {
           if(firstNumSet && symbolSet)
           {
                if (boxResult2 != "" && secondNumSet == false)
                {
                    Double.TryParse(boxResult2, out secondNumber);
                    secondNumSet = true;
                }
                calculate();
                boxResult2 = result;
                toClear = true;
                resultSet = true;
                separatorUsed = false;
            }
            drawResults();
            Double.TryParse(result, out firstNumber);
        }

        private void buttonSeparate_Click(object sender, EventArgs e)
        {
            if (resultSet)
            {
                separatorUsed = false;
                firstNumSet = false;
                secondNumSet = false;
                resultSet = false;
                symbolSet = false;
            }
            if (toClear == true)
            {
                boxResult2 = "";
                toClear = false;
            }
            if (separatorUsed == false)
            {
                if (boxResult2 == "")
                {
                    boxResult2 += "0" + symbolSeparator;
                }else
                {
                    boxResult2 += symbolSeparator;
                }
                separatorUsed = true;
            }
            drawResults();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (!resultSet)
            {
                if(boxResult2 != "")
                {
                    if(boxResult2[boxResult2.Length -1].ToString() == symbolSeparator)
                    {
                        boxResult2 = boxResult2.Remove(textBoxResult2.Text.Length - 1);
                        separatorUsed = false;
                    }else
                    {
                        boxResult2 = boxResult2.Remove(boxResult2.Length - 1);
                    }
                }
                if (boxResult2 == "") boxResult2 = "0";
                drawResults();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            boxResult2 = "0";
            separatorUsed = false;
            firstNumSet = false;
            secondNumSet = false;
            resultSet = false;
            symbolSet = false;
            drawResults();
        }

        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            double temp;
            Double.TryParse(boxResult2, out temp);
            temp = -temp;
            boxResult2 = temp.ToString();
            if(resultSet)
            {
                secondNumSet = false;
                firstNumber = temp;
                resultSet = false;
            }
            drawResults();
        }
    }
}
