using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Quartic.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class Quartic : Window
    {
        double temp = 0;

        public double Fabs(double x)//绝对值
        {
            return (x < 0) ? -x : x;
        }

        public double Cbrt(double x)//立方根
        {
            return (x < 0) ? -Math.Pow(-x, 1.0 / 3) : Math.Pow(x, 1.0 / 3);
        }

        public bool Isdouble(string target)//判断String是否可以转换成Double
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
                textBox6.Text += ("0\r\n" + x1.ToString() + "\r\n");
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
                    textBox6.Text += (s1 + s2);
                }
                else if (delta == 0)
                {
                    x1 = -b / 2;
                    s1 = x1.ToString() + "\r\n";
                    textBox6.Text += (s1 + s1);
                }
                else
                {
                    x1 = -b / 2;
                    x2 = Math.Sqrt(-delta) / 2;
                    if (Fabs(b) < 1e-15)
                    {
                        if (Fabs(x2 - 1) < 1e-15)
                        {
                            textBox6.Text += "i\r\n-i\r\n";
                        }
                        else
                        {
                            s2 = x2.ToString() + "i\r\n";
                            textBox6.Text += (s2 + "-" + s2);
                        }
                    }
                    else
                    {
                        s1 = x1.ToString();
                        s2 = (Fabs(x2 - 1) < 1e-15) ? "i\r\n" : x2.ToString() + "i\r\n";
                        textBox6.Text += (s1 + "+" + s2 + s1 + "-" + s2);
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
                textBox6.Text += "0\r\n";
                Fun2(a, b, c);//降次
            }
            else
            {
                x = Fun3_subsidiary(a, b, c, d);
                textBox6.Text += (x.ToString() + "\r\n");
                Fun2(a, a * x + b, x * (a * x + b) + c);
            }
        }

        public void Fun4(double a, double b, double c, double d, double e)//ax^4+bx^3+cx^2+dx+e=0
        {
            double y, p, q, r, delta, theta;
            string s1, s2;
            if (Fabs(e) < 1e-15)//x(ax^3+bx^2+cx+d)=0
            {
                textBox6.Text += "0\r\n";
                Fun3(a, b, c, d);
            }
            else
            {
                temp = a;
                a = b / temp;
                b = c / temp;
                c = d / temp;
                d = e / temp;
                if (a != 0 || c != 0)
                {
                    y = Fun3_subsidiary(1, -b, a * c - 4 * d, 4 * b * d - a * a * d - c * c);//费拉里法
                    if (Fabs(a * a - 4 * b + 4 * y) < 1e-15)
                    {
                        temp = Math.Sqrt(y * y - 4 * d);
                        p = Math.Sqrt(a * a + 8 * (temp - y));
                        q = (p - a) / 4;
                        s1 = q.ToString() + "\r\n";
                        q = -(p + a) / 4;
                        s1 = s1 + q.ToString() + "\r\n";
                        p = Math.Sqrt(a * a - 8 * (temp + y));
                        q = (p - a) / 4;
                        s1 = s1 + q.ToString() + "\r\n";
                        q = -(p + a) / 4;
                        s1 = s1 + q.ToString() + "\r\n";
                        textBox6.Text += s1;
                    }
                    else
                    {
                        p = Math.Sqrt(a * a / 4 - b + y);
                        q = (a * y - 2 * c) / (a * a - 4 * b + 4 * y);
                        Fun2(1, a / 2 - p, y / 2 - p * q);
                        Fun2(1, a / 2 + p, y / 2 + p * q);
                    }
                }
                else if (Fabs(b) < 1e-15)//x^4+d=0
                {
                    if (d < 0)
                    {
                        y = Math.Pow(-d, 0.25);
                        s1 = y.ToString() + "\r\n";
                        s2 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                        textBox6.Text += (s1 + "-" + s1 + s2 + "-" + s2);
                    }
                    else
                    {
                        y = Math.Pow(d / 4, 0.25);
                        s1 = y.ToString();
                        s2 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                        textBox6.Text += (s1 + "+" + s2 + s1 + "-" + s2 + "-" + s1 + "+" + s2 + "-" + s1 + "-" + s2);
                    }
                }
                else //x^4+bx^2+d=0
                {
                    delta = b * b - 4 * d;
                    if (delta > 0)
                    {
                        temp = Math.Sqrt(delta);
                        y = b - temp;
                        if (y > 0)
                        {
                            y = Math.Sqrt(y / 2);
                            s1 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox6.Text += (s1 + "-" + s1);
                        }
                        else
                        {
                            y = Math.Sqrt(-y / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox6.Text += (s1 + "-" + s1);
                        }

                        y = b + temp;
                        if (y > 0)
                        {
                            y = Math.Sqrt(y / 2);
                            s1 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox6.Text += (s1 + "-" + s1);
                        }
                        else
                        {
                            y = Math.Sqrt(-y / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox6.Text += (s1 + "-" + s1);
                        }
                    }
                    else if (delta == 0)
                    {
                        if (b < 0)
                        {
                            y = Math.Sqrt(-b / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox6.Text += (s1 + s1 + "-" + s1 + "-" + s1);
                        }
                        else
                        {
                            y = Math.Sqrt(b / 2);
                            s1 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox6.Text += (s1 + s1 + "-" + s1 + "-" + s1);
                        }
                    }
                    else
                    {
                        r = Math.Pow(d, 0.25);
                        theta = Math.Atan2(Math.Sqrt(-delta) / 2, -b / 2) / 2;
                        p = r * Math.Cos(theta);
                        q = r * Math.Sin(theta);
                        if (p * q < 0)
                        {
                            s1 = Fabs(p).ToString();
                            s2 = (Fabs(Fabs(q) - 1) < 1e-15) ? "i\r\n" : Fabs(q).ToString() + "i\r\n";
                            textBox6.Text += (s1 + "-" + s2 + "-" + s1 + "+" + s2);
                        }
                        else
                        {
                            s1 = Fabs(p).ToString();
                            s2 = (Fabs(Fabs(q) - 1) < 1e-15) ? "i\r\n" : Fabs(q).ToString() + "i\r\n";
                            textBox6.Text += (s1 + "+" + s2 + "-" + s1 + "-" + s2);
                        }
                        theta = Math.Atan2(-Math.Sqrt(-delta) / 2, -b / 2) / 2;
                        p = r * Math.Cos(theta);
                        q = r * Math.Sin(theta);
                        if (p * q < 0)
                        {
                            s1 = Fabs(p).ToString();
                            s2 = (Fabs(Fabs(q) - 1) < 1e-15) ? "i\r\n" : Fabs(q).ToString() + "i\r\n";
                            textBox6.Text += (s1 + "-" + s2 + "-" + s1 + "+" + s2);
                        }
                        else
                        {
                            s1 = Fabs(p).ToString();
                            s2 = (Fabs(Fabs(q) - 1) < 1e-15) ? "i\r\n" : Fabs(q).ToString() + "i\r\n";
                            textBox6.Text += (s1 + "+" + s2 + "-" + s1 + "-" + s2);
                        }
                    }
                }
            }
        }

        public void Judgement(double a, double b, double c, double d, double e)//判断次数
        {
            if (a != 0)//四次
            {
                textBox6.Text = "x1,x2,x3,x4:\r\n";
                Fun4(a, b, c, d, e);
            }
            else if (b != 0)//三次
            {
                textBox6.Text = "x1,x2,x3:\r\n";
                Fun3(b, c, d, e);
            }
            else if (c != 0)//二次
            {
                textBox6.Text = "x1,x2:\r\n";
                Fun2(c, d, e);
            }
            else if (d != 0)//一次
            {
                e /= (-d);
                textBox6.Text = "x = " + e.ToString();
            }
            else//常值
            {
                if (e == 0)
                {
                    textBox6.Text = "任意复数";
                }
                else
                {
                    textBox6.Text = "无解";
                }
            }
        }

        public Quartic()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            double a1, b1, c1, d1, e1;
            textBox6.Text = "";
            if (Isdouble(textBox1.Text) && Isdouble(textBox2.Text) && Isdouble(textBox3.Text) && Isdouble(textBox4.Text) && Isdouble(textBox5.Text))
            {
                a1 = Convert.ToDouble(textBox1.Text);
                b1 = Convert.ToDouble(textBox2.Text);
                c1 = Convert.ToDouble(textBox3.Text);
                d1 = Convert.ToDouble(textBox4.Text);
                e1 = Convert.ToDouble(textBox5.Text);
                temp = 0;
                Judgement(a1, b1, c1, d1, e1);
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
            textBox5.Text = "0";
            textBox6.Text = "";
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
                textBox5.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox3.Focus();
            }
        }

        private void TextBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                button1.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox4.Focus();
            }
        }
    }
}
