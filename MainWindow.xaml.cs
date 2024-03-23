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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string stringButton = (e.OriginalSource as Button).Content as string;
            string lblTextValue = null;
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
                    if (lblText.Text.Length > 1)
                    {
                        lblText.Text = lblText.Text.Remove(lblText.Text.Length - 1);

                    }
                    else
                    {
                        lblText.Text = "0";
                    }
                    lblTextHistory.Text = lblText.Text;
                    break;
                case "1/x":
                    if (!stringButton.Contains("+-*/"))
                    {
                        int digit = int.Parse(lblText.Text);
                        double x = 1 / digit;
                        lblText.Text = x.ToString();
                    }
                    break;
                case "x2":
                    break;
                case "sqrt2":
                    break;
                case "/":
                    CheckRepeatSign(stringButton);
                    break;
                case "x":
                    CheckRepeatSign(stringButton);
                    break;
                case "-":
                    lblTextHistory.Text += stringButton;
                    CheckRepeatSign(stringButton);
                    lblTextValue = "";
                    break;
                case "+":
                    CheckRepeatSign(stringButton);
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

        private void CheckRepeatSign(string sign)
        {
            if (!lblTextHistory.Text.EndsWith(sign)) lblTextHistory.Text += sign;
        }
    }
}
