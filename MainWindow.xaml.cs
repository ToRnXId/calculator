using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach(UIElement el in grMainRoot.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
        }

        string lblTextValue = null;
        string digits = "0123456789";
        string mathSigns = "+-*/";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string stringButton = (e.OriginalSource as Button).Content as string;

            switch (stringButton)
            {
                case "%":
                    CheckRepeatSign(stringButton);
                    break;
                case "CE":
                    lblText.Text = "0";
                    break;
                case "C":
                    lblText.Text = "0";
                    lblTextHistory.Text = null;
                    break;
                case "Erase":
                    if (lblText.Text.Length > 0)
                    {
                        lblText.Text = lblText.Text.Remove(lblText.Text.Length - 1);

                        if (lblTextHistory.Text.Contains("mathSigns"))
                        {
                            lblTextHistory.Text = lblText.Text;
                        }
                        lblTextHistory.Text = lblText.Text;
                    } else {
                        lblText.Text = "0";
                    }
                    
                    break;
                case "1/x":
                    if (!stringButton.Contains("+-*/"))
                    {
                        int digit = int.Parse(lblTextHistory.Text);
                        double x = 1 / digit;
                        lblTextHistory.Text = x.ToString();
                    } else
                    {
                        lblTextHistory.Text = "Error";
                    }
                    break;
                case "x2":
                    break;
                case "sqrt2":
                    break;
                case "/":
                    //CheckRepeatSign(stringButton);
                    ChechPreviousSign(stringButton);
                    break;
                case "*":
                    ChechPreviousSign(stringButton);
                    //CheckRepeatSign(stringButton);
                    break;
                case "-":
                    ChechPreviousSign(stringButton);
                    //CheckRepeatSign(stringButton);
                    //lblTextHistory.Text += stringButton;
                    //lblTextValue = "";
                    break;
                case "+":
                    ChechPreviousSign(stringButton);
                    //CheckRepeatSign(stringButton);
                    break;
                case "+/-":
                    break;
                case ",":
                    CheckRepeatSign(stringButton);
                    break;
                case "=":
                    string value = new DataTable().Compute(lblTextHistory.Text, null).ToString(); 
                    lblText.Text = null;
                    lblTextHistory.Text = value;
                    break;
                default:
                    lblText.Text = lblText.Text.TrimStart('0');
                    lblTextHistory.Text = lblTextHistory.Text.TrimStart('0');
                    if (lblTextHistory.Text.EndsWith("-"))
                    {
                        lblText.Text = "";
                    }
                    lblTextValue = stringButton;
                    lblText.Text += lblTextValue;
                    lblTextHistory.Text += stringButton;
                    break;
            }




            /*
            if (stringButton == "C") lblText.Text = "";
            else if (stringButton == "=")
            {
                string value = new DataTable().Compute(lblText.Text, null).ToString();
                lblText.Text = value;
            }
            else lblText.Text += stringButton;
            */

        }

        private void ChechPreviousSign(string sign)
        {
            lblTextValue = sign;
            int counter = 0;
            foreach (char el in mathSigns)
            {
                string strElement = el.ToString();
                if (lblTextHistory.Text.EndsWith(strElement))
                {
                    counter++;
                    lblTextHistory.Text = lblTextHistory.Text.Remove(lblTextHistory.Text.Length - 1);
                    lblTextHistory.Text += sign;
                }
            }

            if (counter == 0)
            {
                lblTextHistory.Text += sign;
            }

        }

        private void CheckRepeatSign(string sign)
        {
            lblTextValue = sign;
            if (lblTextHistory.Text.EndsWith(mathSigns))
            {
                lblTextHistory.Text += 1;
            } else
            {
                lblTextHistory.Text += sign;
            }
        }
    }
}
