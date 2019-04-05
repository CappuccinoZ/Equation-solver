using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Quadratic.xaml 的交互逻辑
    /// </summary>
    public partial class Quadratic : Window
    {
        double temp = 0;

        public double Fabs(double x)//绝对值
        {
            return (x < 0) ? -x : x;
        }

        public bool Isdouble(string target)//String是否可以转换成Double
        {
            return (double.TryParse(target, out temp)) ? true : false;
        }

        public void Fun2(double a, double b, double c)//ax^2+bx+c=0
        {
            double x1, x2, delta;
            string s1, s2;

            if (Fabs(c) < 1e-15)//x(ax+b)=0
            {
                x1 = -b / a;
                textBox4.Text = textBox4.Text + "0\r\n" + x1.ToString() + "\r\n";
            }
            else
            {
                b /= a;
                c /= a;
                delta = b * b - 4 * c;
                if (delta > 0)
                {
                    temp = Math.Sqrt(delta);
                    x1 = (-b + temp) / 2;
                    x2 = (-b - temp) / 2;
                    s1 = x1.ToString() + "\r\n";
                    s2 = x2.ToString() + "\r\n";
                    textBox4.Text = textBox4.Text + s1 + s2;
                }
                else if (delta == 0)
                {
                    x1 = -b / 2;
                    s1 = x1.ToString() + "\r\n";
                    textBox4.Text = textBox4.Text + s1 + s1;
                }
                else
                {
                    x1 = -b / 2;
                    x2 = Math.Sqrt(-delta) / 2;
                    if (Fabs(b) < 1e-15)
                    {
                        if (Fabs(x2 - 1) < 1e-15)
                        {
                            textBox4.Text += "i\r\n-i\r\n";
                        }
                        else
                        {
                            s2 = (Fabs(x2 - 1) < 1e-15) ? "i\r\n" : x2.ToString() + "i\r\n";
                            textBox4.Text = textBox4.Text + s2 + "-" + s2;
                        }
                    }
                    else
                    {
                        s1 = x1.ToString();
                        s2 = (Fabs(x2 - 1) < 1e-15) ? "i\r\n" : x2.ToString() + "i\r\n";
                        textBox4.Text = textBox4.Text + s1 + "+" + s2 + s1 + "-" + s2;
                    }
                }
            }
        }

        public void Judgement(double a, double b, double c)//判断次数
        {
            if (a != 0)//二次
            {
                textBox4.Text = "x1,x2:\r\n";
                Fun2(a, b, c);
            }
            else if (b != 0)//一次
            {
                c /= (-b);
                textBox4.Text = "x = " + c.ToString();
            }
            else//常值
            {
                if (c == 0)
                {
                    textBox4.Text = "任意复数";
                }
                else
                {
                    textBox4.Text = "无解";
                }
            }
        }
        public Quadratic()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)//Solve
        {
            double a1, b1, c1;
            textBox4.Text = "";
            if (Isdouble(textBox1.Text) && Isdouble(textBox2.Text) && Isdouble(textBox3.Text))
            {
                a1 = Convert.ToDouble(textBox1.Text);
                b1 = Convert.ToDouble(textBox2.Text);
                c1 = Convert.ToDouble(textBox3.Text);
                temp = 0;
                Judgement(a1, b1, c1);
                button2.Focus();
            }
            else
            {
                MessageBox.Show("请检查输入内容(Alt+F4)");
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)//Reset
        {
            temp = 0;
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "";
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                textBox2.Focus();
            }
            else if (e.Key == Key.Up)
            {
                button2.Focus();
            }
        }

        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                textBox3.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox1.Focus();
            }
        }

        private void TextBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                button1.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox2.Focus();
            }
        }
    }
}
