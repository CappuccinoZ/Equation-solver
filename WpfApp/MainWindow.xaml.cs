using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Solver
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly double DBL_EPSILON = 2.2204460492503131E-16;//零跨度值
        double[] root = { 0, 0, 0, 0 };

        public bool Approx(double a, double b) => (b == 0) ? (Math.Abs(a) < DBL_EPSILON) : (Math.Abs(a - b) < DBL_EPSILON);//判断是否约等
        public double Cbrt(double x) => (x < 0) ? -Math.Pow(-x, 1.0 / 3) : Math.Pow(x, 1.0 / 3);//立方根
        public bool Isdouble(string target) => double.TryParse(target, out _) ? true : false;//判断String是否可以转换成Double

        public void Fun2(double a, double b, double c)//ax^2+bx+c=0
        {
            if (Approx(c, 0))//x(ax+b)=0
            {
                textBox7.Text += "0\r\n" + (-b / a).ToString() + "\r\n";
            }
            else
            {
                double x1, x2, delta;
                string s1, s2;
                b /= a;
                c /= a;
                delta = b * b - 4 * c;
                if (delta > 0)
                {
                    double t = Math.Sqrt(delta);
                    x1 = (-b + t) / 2;
                    x2 = (-b - t) / 2;
                    s1 = x1.ToString() + "\r\n";
                    s2 = x2.ToString() + "\r\n";
                    textBox7.Text += s1 + s2;
                }
                else if (delta == 0)
                {
                    x1 = -b / 2;
                    s1 = x1.ToString() + "\r\n";
                    textBox7.Text += s1 + s1;
                }
                else
                {
                    x1 = -b / 2;
                    x2 = Math.Sqrt(-delta) / 2;
                    if (Approx(b, 0))
                    {
                        s2 = Approx(x2, 1) ? "i\r\n" : x2.ToString() + "i\r\n";
                        textBox7.Text += s2 + "-" + s2;
                    }
                    else
                    {
                        s1 = x1.ToString();
                        s2 = Approx(x2, 1) ? "i\r\n" : x2.ToString() + "i\r\n";
                        textBox7.Text += s1 + "+" + s2 + s1 + "-" + s2;
                    }
                }
            }
        }

        public double Fun3_subsidiary(double a, double b, double c, double d)//返回ax^3+bx^2+cx+d=0的一个实数根
        {
            double p, q, r, x, theta, delta;
            double t = 1 / a;
            a = b * t;
            b = c * t;
            c = d * t;//x^3 + ax^2 + bx + c =0
            t = a / 3;
            p = t * t - b / 3;
            q = t * (b / 2 - t * t) - c / 2;
            delta = q * q - p * p * p;
            if (delta < 0)
            {
                r = p * Math.Sqrt(p);
                theta = Math.Acos(q / r) / 3;
                x = 2 * Cbrt(r) * Math.Cos(theta) - a / 3;
            }
            else if (delta == 0)
            {
                x = 2 * Cbrt(q) - a / 3;
            }
            else
            {
                t = Math.Sqrt(delta);
                x = Cbrt(q + t) + Cbrt(q - t) - a / 3;//卡尔达诺公式
            }
            return x;
        }

        public void Fun3(double a, double b, double c, double d)//ax^3+bx^2+cx+d=0
        {
            if (Approx(d, 0))//x(ax^2+bx+c)=0
            {
                textBox7.Text += "0\r\n";
                Fun2(a, b, c);//降次
            }
            else
            {
                double x = Fun3_subsidiary(a, b, c, d);
                textBox7.Text += x.ToString() + "\r\n";
                Fun2(a, a * x + b, x * (a * x + b) + c);
            }
        }

        public void Fun4(double a, double b, double c, double d, double e)//ax^4+bx^3+cx^2+dx+e=0
        {
            if (Approx(e, 0))//x(ax^3+bx^2+cx+d)=0
            {
                textBox7.Text += "0\r\n";
                Fun3(a, b, c, d);
            }
            else
            {
                double y, p, q, r, t, delta, theta;
                string s1, s2;
                t = 1 / a;
                a = b * t;
                b = c * t;
                c = d * t;
                d = e * t;
                if (a != 0 || c != 0)
                {
                    y = Fun3_subsidiary(1, -b, a * c - 4 * d, 4 * b * d - a * a * d - c * c);//费拉里法
                    if (Approx(a * a - 4 * b + 4 * y, 0))
                    {
                        t = Math.Sqrt(y * y - 4 * d);
                        p = Math.Sqrt(a * a + 8 * (t - y));
                        q = (p - a) / 4;
                        s1 = q.ToString() + "\r\n";
                        q = -(p + a) / 4;
                        s1 += q.ToString() + "\r\n";
                        p = Math.Sqrt(a * a - 8 * (t + y));
                        q = (p - a) / 4;
                        s1 += q.ToString() + "\r\n";
                        q = -(p + a) / 4;
                        s1 += q.ToString() + "\r\n";
                        textBox7.Text += s1;
                    }
                    else
                    {
                        p = Math.Sqrt(a * a / 4 - b + y);
                        q = (a * y - 2 * c) / (a * a - 4 * b + 4 * y);
                        Fun2(1, a / 2 - p, y / 2 - p * q);
                        Fun2(1, a / 2 + p, y / 2 + p * q);
                    }
                }
                else if (Approx(b, 0))//x^4+d=0
                {
                    if (d < 0)
                    {
                        y = Math.Pow(-d, 0.25);
                        s1 = y.ToString() + "\r\n";
                        s2 = Approx(y, 1) ? "i\r\n" : y.ToString() + "i\r\n";
                        textBox7.Text += s1 + "-" + s1 + s2 + "-" + s2;
                    }
                    else
                    {
                        y = Math.Pow(d / 4, 0.25);
                        s1 = y.ToString();
                        s2 = Approx(y, 1) ? "i\r\n" : y.ToString() + "i\r\n";
                        textBox7.Text += s1 + "+" + s2 + s1 + "-" + s2 + "-" + s1 + "+" + s2 + "-" + s1 + "-" + s2;
                    }
                }
                else //x^4+bx^2+d=0
                {
                    delta = b * b - 4 * d;
                    if (delta > 0)
                    {
                        t = Math.Sqrt(delta);
                        y = b - t;
                        if (y > 0)
                        {
                            y = Math.Sqrt(y / 2);
                            s1 = Approx(y, 1) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox7.Text += s1 + "-" + s1;
                        }
                        else
                        {
                            y = Math.Sqrt(-y / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox7.Text += s1 + "-" + s1;
                        }
                        y = b + t;
                        if (y > 0)
                        {
                            y = Math.Sqrt(y / 2);
                            s1 = Approx(y, 1) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox7.Text += s1 + "-" + s1;
                        }
                        else
                        {
                            y = Math.Sqrt(-y / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox7.Text += s1 + "-" + s1;
                        }
                    }
                    else if (delta == 0)
                    {
                        y = Math.Sqrt(Math.Abs(b) / 2);
                        if (b < 0)
                        {
                            s1 = y.ToString() + "\r\n";
                            textBox7.Text += s1 + s1 + "-" + s1 + "-" + s1;
                        }
                        else
                        {
                            s1 = Approx(y, 1) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox7.Text += s1 + s1 + "-" + s1 + "-" + s1;
                        }
                    }
                    else
                    {
                        r = Math.Pow(d, 0.25);
                        theta = Math.Atan2(Math.Sqrt(-delta) / 2, -b / 2) / 2;
                        p = r * Math.Cos(theta);
                        q = r * Math.Sin(theta);
                        s1 = p.ToString();
                        s2 = Approx(q, 1) ? "i\r\n" : q.ToString() + "i\r\n";
                        textBox7.Text += s1 + "+" + s2 + s1 + "-" + s2;
                        textBox7.Text += "-" + s1 + "+" + s2 + "-" + s1 + "-" + s2;
                    }
                }
            }
        }

        public int Fun4_realroot(double a, double b, double c, double d, double e)//四次方程实根数量
        {
            int i = 0;
            double y, p, q, t, delta;
            if (Approx(e, 0))//x(ax^3+bx^2+cx+)=0
            {
                root[1] = Fun3_subsidiary(a, b, c, d);
                delta = b * b - 4 * a * c - a * root[1] * (3 * a * root[1] + 2 * b);
                if (delta > 0)
                {
                    t = Math.Sqrt(delta);
                    root[2] = -(root[1] + (b - t) / a) / 2;
                    root[3] = -(root[1] + (b + t) / a) / 2;
                    i = 4;
                }
                else if (delta == 0)
                {
                    root[2] = -(root[1] + b / a) / 2;
                    root[3] = root[2];
                    i = 4;
                }
                else
                {
                    i = 2;
                }
            }
            else
            {
                t = 1 / a;
                a = b * t;
                b = c * t;
                c = d * t;
                d = e * t;
                if (a != 0 || c != 0)
                {
                    y = Fun3_subsidiary(1, -b, a * c - 4 * d, 4 * b * d - a * a * d - c * c);//费拉里法
                    if (Approx(a * a - 4 * b + 4 * y, 0))
                    {
                        t = Math.Sqrt(y * y - 4 * d);
                        p = Math.Sqrt(a * a + 8 * (t - y));
                        root[0] = (p - a) / 4;
                        root[1] = -(p + a) / 4;
                        p = Math.Sqrt(a * a - 8 * (t + y));
                        root[2] = (p - a) / 4;
                        root[3] = -(p + a) / 4;
                        i = 4;
                    }
                    else
                    {
                        p = Math.Sqrt(a * a / 4 - b + y);
                        q = (a * y - 2 * c) / (a * a - 4 * b + 4 * y);
                        delta = 4 * p * (4 * q - a + p) + a * a - 8 * y;
                        if (delta > 0)
                        {
                            t = Math.Sqrt(delta);
                            root[0] = (2 * p - a + t) / 4;
                            root[1] = (2 * p - a - t) / 4;
                            i = 2;
                        }
                        else if (delta == 0)
                        {
                            root[0] = (2 * p - a) / 4;
                            root[1] = root[0];
                            i = 2;
                        }
                        delta = 4 * p * (a + p - 4 * q) + a * a - 8 * y;
                        if (delta > 0)
                        {
                            t = Math.Sqrt(delta);
                            root[2] = -(a + 2 * p - t) / 4;
                            root[3] = -(a + 2 * p + t) / 4;
                            i += 2;
                        }
                        else if (delta == 0)
                        {
                            root[2] = -(a + 2 * p) / 4;
                            root[3] = root[2];
                            i += 2;
                        }
                    }
                }
                else if (Approx(b, 0))//x^4+d=0
                {
                    if (d < 0)
                    {
                        y = Math.Pow(-d, 0.25);
                        root[0] = y;
                        root[1] = -y;
                        i = 2;
                    }
                }
                else //x^4+bx^2+d=0
                {
                    delta = b * b - 4 * d;
                    if (delta > 0)
                    {
                        t = Math.Sqrt(delta);
                        y = b - t;
                        if (y <= 0)
                        {
                            y = Math.Sqrt(-y / 2);
                            root[0] = y;
                            root[1] = -y;
                            i = 2;
                        }
                        y = b + t;
                        if (y <= 0)
                        {
                            y = Math.Sqrt(-y / 2);
                            root[2] = y;
                            root[3] = -y;
                            i += 2;
                        }
                    }
                    else if (delta == 0)
                    {
                        if (b < 0)
                        {
                            y = Math.Sqrt(-b / 2);
                            root[0] = y;
                            root[1] = -y;
                            root[2] = root[0];
                            root[3] = root[1];
                            i = 4;
                        }
                    }
                }
            }
            return i;
        }

        public double Start(double a, double b, double c, double d, double e)//x^5+ax^4+bx^3+cx^2+dx+e=0根的上界
        {
            double q, y;
            if (a > 0 && b > 0 && c > 0 && d > 0 && e > 0)
            {
                y = 0;
            }
            else
            {
                List<double> doubles = new List<double> { a, b, c, d, e };
                q = doubles.Max();
                if (a < 0)
                {
                    y = q;
                }
                else if (b < 0)
                {
                    y = Math.Sqrt(q);
                }
                else if (c < 0)
                {
                    y = Math.Pow(q, 1.0 / 3);
                }
                else if (d < 0)
                {
                    y = Math.Pow(q, 0.25);
                }
                else
                {
                    y = Math.Pow(q, 1.0 / 5);
                }
            }
            return y;
        }

        public double Fun5_calculation(double a, double b, double c, double d, double e, double x) => x * (x * (x * (x * (x + a) + b) + c) + d) + e;//计算函数值x^5+ax^4+bx^3+cx^2+dx+e

        public double Fun5_derivative(double a, double b, double c, double d, double x) => x * (x * (x * (5 * x + 4 * a) + 3 * b) + 2 * c) + d;//计算导数5x^4+4ax^3+3bx^2+2cx+d

        public void Fun5(double a, double b, double c, double d, double e)//x^5+ax^4+bx^3+cx^2+dx+e=0
        {
            if (Approx(e, 0))
            {
                textBox7.Text += "0\r\n";
                Fun4(1, a, b, c, d);
            }
            else
            {
                double x, t;
                bool tangents = true;//切线法是否可用
                int i = Fun4_realroot(5, 4 * a, 3 * b, 2 * c, d);
                while (i > 0)
                {
                    i--;
                    if (Approx(Fun5_calculation(a, b, c, d, e, root[i]), 0))
                    {
                        tangents = false;
                        break;
                    }
                }
                if (tangents)
                {
                    t = Start(a, b, c, d, e);
                    x = -Start(-a, b, -c, d, -e);
                    if (t != 0)
                    {
                        x = (x == 0) ? t : (t + x) / 2;
                    }
                    while (Approx(Fun5_derivative(a, b, c, d, x), 0))
                    {
                        x += 0.125;
                    }
                    for (i = 1; i < 120000000; i++)
                    {
                        t = x;
                        x -= Fun5_calculation(a, b, c, d, e, x) / Fun5_derivative(a, b, c, d, x);
                        if (Math.Abs(x - t) < 1e-5)
                        {
                            break;
                        }
                    }
                    t = Fun5_calculation(a, b, c, d, e, x);
                    if (Math.Abs(t) > 1)
                    {
                        MessageBox.Show("误差较大!");
                    }
                    MessageBox.Show("迭代次数:" + i.ToString() + "\r\nL-R = " + t.ToString());
                }
                else
                {
                    x = root[i];
                }
                textBox7.Text += x.ToString() + "\r\n";
                Fun4(1, x + a, x * (x + a) + b, x * (x * (x + a) + b) + c, x * (x * (x * (x + a) + b) + c) + d);//降次
            }
        }

        public void Judgement(double a, double b, double c, double d, double e, double f)//判断次数
        {
            if (a != 0)//五次
            {
                textBox7.Text = "x1,x2,x3,x4,x5:\r\n";
                Fun5(b / a, c / a, d / a, e / a, f / a);
            }
            else if (b != 0)//四次
            {
                textBox7.Text = "x1,x2,x3,x4:\r\n";
                Fun4(b, c, d, e, f);
            }
            else if (c != 0)//三次
            {
                textBox7.Text = "x1,x2,x3:\r\n";
                Fun3(c, d, e, f);
            }
            else if (d != 0)//二次
            {
                textBox7.Text = "x1,x2:\r\n";
                Fun2(d, e, f);
            }
            else if (e != 0)//一次
            {
                textBox7.Text = "x = " + (-f / e).ToString();
            }
            else//常值
            {
                textBox7.Text = (f == 0) ? "任意复数" : "无解";
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)//求解
        {
            textBox7.Text = "";
            Array.Clear(root, 0, 4);
            if (Isdouble(textBox1.Text) && Isdouble(textBox2.Text) && Isdouble(textBox3.Text) && Isdouble(textBox4.Text) && Isdouble(textBox5.Text) && Isdouble(textBox6.Text))
            {
                if (comboBoxItem1.IsSelected)
                {
                    Judgement(
                        Convert.ToDouble(textBox1.Text),
                        Convert.ToDouble(textBox2.Text),
                        Convert.ToDouble(textBox3.Text),
                        Convert.ToDouble(textBox4.Text),
                        Convert.ToDouble(textBox5.Text),
                        Convert.ToDouble(textBox6.Text)
                        );
                    button2.Focus();
                }
                else if (comboBoxItem2.IsSelected)
                {
                    Judgement(
                            0,
                            Convert.ToDouble(textBox1.Text),
                            Convert.ToDouble(textBox2.Text),
                            Convert.ToDouble(textBox3.Text),
                            Convert.ToDouble(textBox4.Text),
                            Convert.ToDouble(textBox5.Text)
                            );
                }
                else if (comboBoxItem3.IsSelected)
                {
                    Judgement(
                            0,
                            0,
                            Convert.ToDouble(textBox1.Text),
                            Convert.ToDouble(textBox2.Text),
                            Convert.ToDouble(textBox3.Text),
                            Convert.ToDouble(textBox4.Text)
                            );
                }
                else
                {
                    Judgement(
                            0,
                            0,
                            0,
                            Convert.ToDouble(textBox1.Text),
                            Convert.ToDouble(textBox2.Text),
                            Convert.ToDouble(textBox3.Text)
                            );
                }
            }
            else
            {
                MessageBox.Show("请检查输入内容(Alt+F4)");
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)//重置
        {
            Array.Clear(root, 0, 4);
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "";
        }

        private void FocusSet(KeyEventArgs e, TextBox t, bool c = true)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                if (c)
                {
                    t.Focus();
                }
                else
                {
                    button1.Focus();
                }
            }
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox2);
        private void TextBox2_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox3);
        private void TextBox3_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox4, !comboBoxItem4.IsSelected);
        private void TextBox4_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox5, !comboBoxItem3.IsSelected);
        private void TextBox5_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox6, !comboBoxItem2.IsSelected);
        private void TextBox6_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox7, false);

        private void ComboBoxSet(int a)
        {
            label4.Visibility = (a == 2) ? Visibility.Hidden : Visibility.Visible;
            label5.Visibility = (a < 4) ? Visibility.Hidden : Visibility.Visible;
            label6.Visibility = (a < 5) ? Visibility.Hidden : Visibility.Visible;
            textBox4.Visibility = (a == 2) ? Visibility.Hidden : Visibility.Visible;
            textBox5.Visibility = (a < 4) ? Visibility.Hidden : Visibility.Visible;
            textBox6.Visibility = (a < 5) ? Visibility.Hidden : Visibility.Visible;
            button1.Margin = new Thickness(8, 99 + 33 * a, 0, 0);
            button2.Margin = new Thickness(8, 160 + 33 * a, 0, 0);
            textBox7.Margin = new Thickness(58, 71 + 33 * a, 0, 0);
            Height = 300 + 33 * a;
        }

        private void LabelSet(string[] s)
        {
            label7.Content = s[0];
            label1.Content = s[1];
            label2.Content = s[2];
            label3.Content = s[3];
            label4.Content = s[4];
            label5.Content = s[5];
        }

        private void ComboBoxItem1_Selected(object sender, RoutedEventArgs e)
        {
            string[] s = { "ax^5+bx^4+cx^3+dx^2+ex+f=0", "ax^5", "bx^4", "cx^3", "dx^2", "ex" };
            LabelSet(s);
            ComboBoxSet(5);
        }

        private void ComboBoxItem2_Selected(object sender, RoutedEventArgs e)
        {
            string[] s = { "ax^4+bx^3+cx^2+dx+e=0", "ax^4", "bx^3", "cx^2", "dx", "e" };
            LabelSet(s);
            ComboBoxSet(4);
        }

        private void ComboBoxItem3_Selected(object sender, RoutedEventArgs e)
        {
            string[] s = { "ax^3+bx^2+cx+d=0", "ax^3", "bx^2", "cx", "d", "" };
            LabelSet(s);
            ComboBoxSet(3);
        }

        private void ComboBoxItem4_Selected(object sender, RoutedEventArgs e)
        {
            string[] s = { "ax^2+bx+c=0", "ax^2", "bx", "c", "", "" };
            LabelSet(s);
            ComboBoxSet(2);
        }
    }
}
