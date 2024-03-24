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
        string digit = "";
        string digits = "0123456789";
        string mathSigns = "+-*/";
        string temp = "";
        string[] arrayValues = {};

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string stringButton = (e.OriginalSource as Button).Content as string;

            switch (stringButton)
            {
                case "%":
                    CheckRepeatSign(stringButton);
                    break;
                case "CE":
                    txtblNumber.Text = "0";
                    break;
                case "C":
                    txtblNumber.Text = "0";
                    txtblResultat.Text = null;
                    break;
                case "Erase":
                    if (txtblNumber.Text.Length >= 1)
                    {
                        txtblNumber.Text = txtblNumber.Text.Remove(txtblNumber.Text.Length - 1);

                        //if (lblTextHistory.Text.Contains("mathSigns"))
                        //{
                        //    lblTextHistory.Text = lblText.Text;
                        //}
                        txtblResultat.Text = txtblNumber.Text;
                    } else if (txtblNumber.Text.Length == 0 || txtblNumber.Text == null){
                        txtblNumber.Text = "0";
                    }
                    
                    break;
                case "1/x":
                    if (!stringButton.Contains(mathSigns))
                    {
                        int digit = int.Parse(txtblResultat.Text);
                        double x = 1 / digit;
                        txtblResultat.Text = x.ToString();
                    } else
                    {
                        txtblResultat.Text = "Error";
                    }
                    break;
                case "x2":
                    break;
                case "sqrt2":
                    break;
                case "/":
                    if (!txtblResultat.Text.EndsWith("-"))
                        txtblResultat.Text += txtblNumber.Text;
                    //CheckRepeatSign(stringButton);
                    CheckPreviousSign(stringButton);
                    break;
                case "*":
                    if (!txtblResultat.Text.EndsWith("-"))
                        txtblResultat.Text += txtblNumber.Text;
                    CheckPreviousSign(stringButton);
                    //CheckRepeatSign(stringButton);
                    break;
                case "-":
                    if (!txtblResultat.Text.EndsWith("-"))
                        txtblResultat.Text += txtblNumber.Text;

                    CheckPreviousSign(stringButton);
                    
                    //CheckPreviousSign(stringButton);
                    //CheckRepeatSign(stringButton);
                    //lblTextHistory.Text += stringButton;
                    //lblTextValue = "";
                    break;
                case "+":
                    if (!txtblResultat.Text.EndsWith("-"))
                        txtblResultat.Text += txtblNumber.Text;
                    CheckPreviousSign(stringButton);
                    //CheckRepeatSign(stringButton);
                    break;
                case "+/-":
                    break;
                case ",":
                    CheckRepeatSign(stringButton);
                    break;
                case "=":
                    if (!txtblResultat.Text.Contains("="))
                    {
                        string value = new DataTable().Compute(txtblResultat.Text, null).ToString();
                        txtblNumber.Text = value;
                        temp = value;
                        txtblResultat.Text += ("=" + value);
                    }
                    else
                    {
                        txtblResultat.Text = temp;
                    }
                    break;
                default:
                    if (txtblResultat.Text.Contains('-'))
                        txtblNumber.Text = stringButton;
                    else         
                        txtblNumber.Text += stringButton;
                    if (txtblNumber.Text.Length > 1)
                        txtblNumber.Text = txtblNumber.Text.TrimStart('0');
                    else if (txtblNumber.Text.Length < 1)
                        txtblNumber.Text = "0";
                    //lblTextHistory.Text = lblTextHistory.Text.TrimStart('0');
                    digit = stringButton;



                    /*if (lblTextHistory.Text.EndsWith("-"))
                    {
                        lblText.Text = "";
                    }

                    lblTextValue = stringButton;
                    lblText.Text += lblTextValue;
                    lblTextHistory.Text += stringButton;*/
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

        private void CheckPreviousSign(string sign)
        {
            //lblTextValue = sign;
            int counter = 0;

            foreach (char el in mathSigns)
            {
                string strElement = el.ToString();
                if (txtblResultat.Text.EndsWith(strElement))
                {
                    counter++;
                    txtblResultat.Text = txtblResultat.Text.Remove(txtblResultat.Text.Length - 1);
                    txtblResultat.Text += sign;
                    txtblNumber.Text = digit;
                }
            }

            if (counter == 0)
            {
                txtblResultat.Text += sign;
            }

        }

        private void CheckRepeatSign(string sign)
        {
            lblTextValue = sign;
            if (txtblResultat.Text.EndsWith(mathSigns))
            {
                txtblResultat.Text += 1;
            } else
            {
                txtblResultat.Text += sign;
            }
        }
    }
}
