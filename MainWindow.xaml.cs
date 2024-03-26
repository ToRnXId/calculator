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

        string solution = null; 
        string stringDigit = "0";
        double intDigit = 0;
        int counter = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string stringButton = (e.OriginalSource as Button)?.Content as string;
            
            switch (stringButton)
            {
                case "%":  //not working

                    break;

                case "CE":
                    txtblNumber.Text = "0";

                    break;

                case "C":
                    txtblNumber.Text = "0";
                    txtblResult.Text = null;

                    break;

                case "Erase":
                    txtblNumber.Text = txtblNumber.Text.Length < 1
                        ? "0"
                        : txtblNumber.Text.Remove(txtblNumber.Text.Length - 1);

                    break;

                case "1/x":
                    if (txtblNumber.Text != "0")
                    {
                        intDigit = int.Parse(txtblNumber.Text);
                        txtblResult.Text += $"1/{intDigit}";
                        txtblNumber.Text = $"{1/intDigit}";
                    } else MessageBox.Show("Dividing by zero error");

                    break;

                case "x2":
                    intDigit = int.Parse(txtblNumber.Text);
                    txtblResult.Text += $"sqr{intDigit}";

                    break;

                case "sqrt2":
                    intDigit = int.Parse(txtblNumber.Text);
                    txtblResult.Text += $"sqr({intDigit})";

                    break;

                case "/":
                    CheckPreviousSign(stringButton);

                    break;

                case "*":
                    CheckPreviousSign(stringButton);

                    break;

                case "-":
                    CheckPreviousSign(stringButton);

                    break;

                case "+":
                    CheckPreviousSign(stringButton);

                    break;

                case "+/-":
                    txtblNumber.Text = txtblNumber.Text.StartsWith("-") 
                        ? txtblNumber.Text = txtblNumber.Text.Remove(0, 1) 
                        : txtblNumber.Text = "-" + txtblNumber.Text;

                    break;

                case ".":
                    _ = !txtblNumber.Text.Contains(".") ? txtblNumber.Text += "." : null;

                    break;

                case "=":
                    if (!txtblResult.Text.Contains('='))
                        counter = 0;
                        solution = $"{txtblResult.Text}{txtblNumber.Text}";
                        string value = new DataTable().Compute(solution, null).ToString();
                        value = value.Replace(',', '.');
                        txtblResult.Text += txtblNumber.Text + "=";
                        txtblNumber.Text = value;
                        solution = "";

                    break;

                default:
                    txtblNumber.Text = txtblNumber.Text.TrimStart('0');

                    if (txtblNumber.Text.Length < 15)
                        if (txtblResult.Text.Length > 1)
                        {
                            counter++;
                            stringDigit = stringButton;
                            txtblNumber.Text += stringDigit;
                        } else {
                            stringDigit = stringButton;
                            txtblNumber.Text += stringDigit;
                        } 

                    break;
                }
            }

        private void CheckPreviousSign(string sign)
        {
            if (counter > 0)
                solution = $"{txtblResult.Text}{txtblNumber.Text}";
                string value = new DataTable().Compute(solution, null).ToString();
                value = value.Replace(',', '.');
                txtblResult.Text = value + sign;
                txtblNumber.Text = value;

            if (txtblResult.Text.Length > 0)
            {
                if (txtblResult.Text.EndsWith("="))
                    txtblResult.Text = txtblNumber.Text + "=";
                txtblResult.Text = txtblResult.Text.Remove(txtblResult.Text.Length - 1);
                txtblResult.Text += sign;
                stringDigit = "";
                txtblNumber.Text = stringDigit;
                solution = $"{txtblResult.Text}";
            } else {
                txtblResult.Text = $"{txtblNumber.Text}{sign}";
                stringDigit = "";
                txtblNumber.Text = stringDigit;
            } 
        }
    }
}
