using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Solver
{
    public class Complex
    {
        public double r, i;
        public Complex(double real, double imag)
        {
            r = real;//实部
            i = imag;//虚部
        }
        public static implicit operator Complex(double a) => new Complex(a, 0);//隐式转换
        public double Abs2 => r * r + i * i;//模的平方
        public double Abs => Math.Sqrt(Abs2);//模
        public double Arg => Math.Atan2(i, r);//辐角主值
        public Complex Conj => new Complex(r, -i);//共轭复数
        public static Complex operator +(Complex left, Complex right) => new Complex(left.r + right.r, left.i + right.i);
        public static Complex operator -(Complex left, Complex right) => new Complex(left.r - right.r, left.i - right.i);
        public static Complex operator *(Complex left, Complex right) => new Complex(left.r * right.r - left.i * right.i, left.r * right.i + left.i * right.r);
        public static Complex operator /(Complex left, Complex right) => left * right.Conj * (1 / right.Abs2);
        public static Complex operator -(Complex right) => new Complex(-right.r, -right.i);
        public override string ToString()
        {
            double a = Math.Round(r, 12), b = Math.Round(i, 12);
            string str = "";
            if (b == 0) return a.ToString();
            if (a != 0)
            {
                str += a;
                if (b > 0) str += "+";
            }
            switch (b)
            {
                case -1: str += "-i"; break;
                case 1: str += "i"; break;
                default: str += b + "i"; break;
            }
            return str;
        }
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Equal(Complex left, Complex right)
        {
            Complex t = left - right;
            return Math.Abs(t.r) < 1E-12 && Math.Abs(t.i) < 1E-12;
        }
        public Complex Sqrt(Complex z) => new Complex(Math.Cos(z.Arg / 2), Math.Sin(z.Arg / 2)) * Math.Sqrt(z.Abs);//平方根
        public Complex Cbrt(Complex z)//立方根
        {
            double t = (z.i == 0 && z.r < 0) ? Math.PI : z.Arg / 3;
            return new Complex(Math.Cos(t), Math.Sin(t)) * Math.Pow(z.Abs, 1.0 / 3);
        }
        public bool Isdouble(string target) => double.TryParse(target, out _);//判断String是否可以转换成Double
        public void Show<T>(T str) => textBox7.Text += str.ToString() + "\r\n";

        public void Fun2(Complex a, Complex b, Complex c)//ax^2+bx+c=0
        {
            Complex t = Sqrt(b * b - 4 * a * c);
            Show((-t - b) / (2 * a));
            Show((t - b) / (2 * a));
        }

        public double Fun3_root(double v, double a, double b, double c)//vx^3+ax^2+bx+c=0
        {
            a /= v;
            b /= v;
            c /= v;
            double p = b - a * a / 3, q = (2 * a * a - 9 * b) * a / 27 + c;
            Complex t = Sqrt(q * q / 4 + p * p * p / 27);
            return (Cbrt(t - q / 2) + Cbrt(-t - q / 2)).r - a / 3;
        }

        public void Fun3(double v, double a, double b, double c)//vx^3+ax^2+bx+c=0
        {
            a /= v;
            b /= v;
            c /= v;
            double x = Fun3_root(1, a, b, c);
            Show(Math.Round(x, 12));
            Fun2(1, x + a, x * (x + a) + b);
        }

        public List<Complex> Fun4_roots(double v, double a, double b, double c, double d)//vx^4+ax^3+bx^2+cx+d=0
        {
            a /= v;
            b /= v;
            c /= v;
            d /= v;
            double p, q, r;
            p = b - 3 * a * a / 8;
            q = a * (a * a - 4 * b) / 8 + c;
            r = d - a * (a * (3 * a * a - 16 * b) + 64 * c) / 256;
            Complex u, v1, v2, t;

            if (Equal(q, 0))
            {
                u = 0;
                v1 = (p + Sqrt(p * p - 4 * r)) / 2;
                v2 = p - v1;
            }
            else//笛卡尔法
            {
                u = Sqrt(Fun3_root(1, 2 * p, p * p - 4 * r, -q * q));
                v1 = (p + u * u - q / u) / 2;
                v2 = v1 + q / u;
            }

            List<Complex> list = new List<Complex>();
            t = Sqrt(u * u - 4 * v1);
            list.Add((-u + t) / 2 - a / 4);
            list.Add((-u - t) / 2 - a / 4);
            t = Sqrt(u * u - 4 * v2);
            list.Add((u + t) / 2 - a / 4);
            list.Add((u - t) / 2 - a / 4);

            return list;
        }
        public void Fun4(double a, double b, double c, double d, double e)//ax^4+bx^3+cx^2+dx+e=0
        {
            List<Complex> roots = Fun4_roots(a, b, c, d, e);
            foreach (var z in roots)
                Show(z);
        }

        public double Start(double a, double b, double c, double d, double e, List<double> roots)//迭代初始值
        {
            roots.Sort();
            return Fun5_calc(a, b, c, d, e, roots[0]) > 0 ? roots[0] - 0.5
                : Fun5_calc(a, b, c, d, e, roots[roots.Count - 1]) < 0 ? roots[roots.Count - 1] + 0.5
                    : (roots[1] + roots[2]) / 2;
        }

        public double Fun5_calc(double a, double b, double c, double d, double e, double x) => x * (x * (x * (x * (x + a) + b) + c) + d) + e;//计算x^5+ax^4+bx^3+cx^2+dx+e

        public double DFun5(double a, double b, double c, double d, double x) => x * (x * (x * (5 * x + 4 * a) + 3 * b) + 2 * c) + d;//计算5x^4+4ax^3+3bx^2+2cx+d

        public void Fun5(double a, double b, double c, double d, double e)//x^5+ax^4+bx^3+cx^2+dx+e=0
        {
            List<Complex> list = Fun4_roots(5, 4 * a, 3 * b, 2 * c, d);
            List<double> roots = new List<double>();
            foreach (var z in list)
                if (Math.Abs(z.i) < 1E-8)
                    roots.Add(z.r);
            double t, x = 0;
            int i = 0;

            foreach (var root in roots)
            {
                if (Equal(Fun5_calc(a, b, c, d, e, root), 0))
                {
                    x = root;
                    i = -1;
                    break;
                }
            }

            if (i == 0)//牛顿迭代法
            {
                if (roots.Count > 0)
                    x = Start(a, b, c, d, e, roots);
                if (!Equal(Fun5_calc(a, b, c, d, e, x), 0))
                    do
                    {
                        t = x;
                        x -= Fun5_calc(a, b, c, d, e, x) / DFun5(a, b, c, d, x);
                        i++;
                    } while (i < 6000 && Math.Abs(x - t) > 1E-15);
                Show("迭代次数：" + i);
            }

            t = Math.Round(x, 5);
            if (Equal(Fun5_calc(a, b, c, d, e, t), 0))
                x = t;

            Show(Math.Round(x, 12));
            Fun4(1, x + a, x * (x + a) + b, x * (x * (x + a) + b) + c, x * (x * (x * (x + a) + b) + c) + d);
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
            if (Isdouble(textBox1.Text) && Isdouble(textBox2.Text) && Isdouble(textBox3.Text) && Isdouble(textBox4.Text) && Isdouble(textBox5.Text) && Isdouble(textBox6.Text))
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
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)//重置
        {
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
        private void TextBox3_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox4);
        private void TextBox4_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox5);
        private void TextBox5_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox6);
        private void TextBox6_KeyUp(object sender, KeyEventArgs e) => FocusSet(e, textBox7, false);
    }
}
