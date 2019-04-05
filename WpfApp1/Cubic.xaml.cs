using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Cubic.xaml 的交互逻辑
    /// </summary>
    public partial class Cubic : Window
    {
        double temp = 0;

        public double Fabs(double x)//绝对值
        {
            return (x < 0) ? -x : x;
        }

        public double Cbrt(double x)//立方根
        {
            return (x < 0) ? (-Math.Pow(-x, 1.0 / 3)) : (Math.Pow(x, 1.0 / 3));
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
                textBox5.Text = textBox5.Text + "0\r\n" + x1.ToString() + "\r\n";
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
                    textBox5.Text = textBox5.Text + s1 + s2;
                }
                else if (delta == 0)
                {
                    x1 = -b / 2;
                    s1 = x1.ToString() + "\r\n";
                    textBox5.Text = textBox5.Text + s1 + s1;
                }
                else
                {
                    x1 = -b / 2;
                    x2 = Math.Sqrt(-delta) / 2;
                    if (Fabs(b) < 1e-15)
                    {
                        if (Fabs(x2 - 1) < 1e-15)
                        {
                            textBox5.Text += "i\r\n-i\r\n";
                        }
                        else
                        {
                            s2 = (Fabs(x2 - 1) < 1e-15) ? "i\r\n" : x2.ToString() + "i\r\n";
                            textBox5.Text = textBox5.Text + s2 + "-" + s2;
                        }
                    }
                    else
                    {
                        s1 = x1.ToString();
                        s2 = (Fabs(x2 - 1) < 1e-15) ? "i\r\n" : x2.ToString() + "i\r\n";
                        textBox5.Text = textBox5.Text + s1 + "+" + s2 + s1 + "-" + s2;
                    }
                }
            }
        }

        public double Fun3_subsidiary(double a, double b, double c, double d)//返回ax^3+bx^2+cx+d=0的一个实数根
        {
            double p, q, r, x, theta, delta;
            temp = a;
            a = b / temp;
            b = c / temp;
            c = d / temp;//x^3 + ax^2 + bx + c =0
            p = b - a * a / 3;
            q = c + a * (2 * a * a - 9 * b) / 27;
            p /= -3;
            q /= -2;
            delta = q * q - p * p * p;

            if (delta < 0)
            {
                r = p * Math.Sqrt(p);
                theta = Math.Acos(q / r) / 3;
                x = 2 * Cbrt(r) * Math.Cos(theta) - a / 3;
            }
            else if (delta == 0)
            {
                x = -a / 3 + 2 * Cbrt(q);
            }
            else
            {
                temp = Math.Sqrt(delta);
                x = -a / 3 + Cbrt(q + temp) + Cbrt(q - temp);//卡尔达诺公式
            }

            return x;
        }

        public void Fun3(double a, double b, double c, double d)//ax^3+bx^2+cx+d=0
        {
            double x;
            if (Fabs(d) < 1e-15)//x(ax^2+bx+c)=0
            {
                textBox5.Text += "0\r\n";
                Fun2(a, b, c);//降次
            }
            else
            {
                x = Fun3_subsidiary(a, b, c, d);
                textBox5.Text = textBox5.Text + x.ToString() + "\r\n";
                Fun2(a, a * x + b, x * (a * x + b) + c);
            }
        }

        public void Judgement(double a, double b, double c, double d)//判断次数
        {

            if (a != 0)//三次
            {
                textBox5.Text = "x1,x2,x3:\r\n";
                Fun3(a, b, c, d);
            }
            else if (b != 0)//二次
            {
                textBox5.Text = "x1,x2:\r\n";
                Fun2(b, c, d);
            }
            else if (c != 0)//一次
            {
                d /= (-c);
                textBox5.Text = "x = " + d.ToString();
            }
            else//常值
            {
                if (d == 0)
                {
                    textBox5.Text = "任意复数";
                }
                else
                {
                    textBox5.Text = "无解";
                }
            }
        }

        public Cubic()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            double a1, b1, c1, d1;
            textBox5.Text = "";
            if (Isdouble(textBox1.Text) && Isdouble(textBox2.Text) && Isdouble(textBox3.Text) && Isdouble(textBox4.Text))
            {
                a1 = Convert.ToDouble(textBox1.Text);
                b1 = Convert.ToDouble(textBox2.Text);
                c1 = Convert.ToDouble(textBox3.Text);
                d1 = Convert.ToDouble(textBox4.Text);
                temp = 0;
                Judgement(a1, b1, c1, d1);
                button2.Focus();

            }
            else
            {
                MessageBox.Show("请检查输入内容(Alt+F4)");
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            temp = 0;
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "";
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
                textBox4.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox2.Focus();
            }
        }
        private void TextBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                button1.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox3.Focus();
            }
        }
    }
}
