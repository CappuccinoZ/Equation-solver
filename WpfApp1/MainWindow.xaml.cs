using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        double temp = 0;
        double[] root = { 0, 0, 0, 0 };

        public double Fabs(double x)//绝对值
        {
            return (x < 0) ? -x : x;
        }

        public double Cbrt(double x)//立方根
        {
            return (x < 0) ? -Math.Pow(-x, 1.0 / 3) : Math.Pow(x, 1.0 / 3);
        }

        public double Maxof(double x, double y, double z)//取最大值
        {
            double m = (x > y) ? x : y;
            return (m > z) ? m : z;
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
                textBox7.Text += ("0\r\n" + x1.ToString() + "\r\n");
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
                    textBox7.Text += (s1 + s2);
                }
                else if (delta == 0)
                {
                    x1 = -b / 2;
                    s1 = x1.ToString() + "\r\n";
                    textBox7.Text += (s1 + s1);
                }
                else
                {
                    x1 = -b / 2;
                    x2 = Math.Sqrt(-delta) / 2;
                    if (Fabs(b) < 1e-15)
                    {
                        if (Fabs(x2 - 1) < 1e-15)
                        {
                            textBox7.Text += "i\r\n-i\r\n";
                        }
                        else
                        {
                            s2 = x2.ToString() + "i\r\n";
                            textBox7.Text += (s2 + "-" + s2);
                        }
                    }
                    else
                    {
                        s1 = x1.ToString();
                        s2 = (Fabs(x2 - 1) < 1e-15) ? "i\r\n" : x2.ToString() + "i\r\n";
                        textBox7.Text += (s1 + "+" + s2 + s1 + "-" + s2);
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
                textBox7.Text += "0\r\n";
                Fun2(a, b, c);//降次
            }
            else
            {
                x = Fun3_subsidiary(a, b, c, d);
                textBox7.Text += (x.ToString() + "\r\n");
                Fun2(a, a * x + b, x * (a * x + b) + c);
            }
        }

        public void Fun4(double a, double b, double c, double d, double e)//ax^4+bx^3+cx^2+dx+e=0
        {
            double y, p, q, r, delta, theta;
            string s1, s2;
            if (Fabs(e) < 1e-15)//x(ax^3+bx^2+cx+d)=0
            {
                textBox7.Text += "0\r\n";
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
                else if (Fabs(b) < 1e-15)//x^4+d=0
                {
                    if (d < 0)
                    {
                        y = Math.Pow(-d, 0.25);
                        s1 = y.ToString() + "\r\n";
                        s2 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                        textBox7.Text += (s1 + "-" + s1 + s2 + "-" + s2);
                    }
                    else
                    {
                        y = Math.Pow(d / 4, 0.25);
                        s1 = y.ToString();
                        s2 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                        textBox7.Text += (s1 + "+" + s2 + s1 + "-" + s2 + "-" + s1 + "+" + s2 + "-" + s1 + "-" + s2);
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
                            textBox7.Text += (s1 + "-" + s1);
                        }
                        else
                        {
                            y = Math.Sqrt(-y / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox7.Text += (s1 + "-" + s1);
                        }

                        y = b + temp;
                        if (y > 0)
                        {
                            y = Math.Sqrt(y / 2);
                            s1 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox7.Text += (s1 + "-" + s1);
                        }
                        else
                        {
                            y = Math.Sqrt(-y / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox7.Text += (s1 + "-" + s1);
                        }
                    }
                    else if (delta == 0)
                    {
                        if (b < 0)
                        {
                            y = Math.Sqrt(-b / 2);
                            s1 = y.ToString() + "\r\n";
                            textBox7.Text += (s1 + s1 + "-" + s1 + "-" + s1);
                        }
                        else
                        {
                            y = Math.Sqrt(b / 2);
                            s1 = (Fabs(y - 1) < 1e-15) ? "i\r\n" : y.ToString() + "i\r\n";
                            textBox7.Text += (s1 + s1 + "-" + s1 + "-" + s1);
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
                            textBox7.Text += (s1 + "-" + s2 + "-" + s1 + "+" + s2);
                        }
                        else
                        {
                            s1 = Fabs(p).ToString();
                            s2 = (Fabs(Fabs(q) - 1) < 1e-15) ? "i\r\n" : Fabs(q).ToString() + "i\r\n";
                            textBox7.Text += (s1 + "+" + s2 + "-" + s1 + "-" + s2);
                        }
                        theta = Math.Atan2(-Math.Sqrt(-delta) / 2, -b / 2) / 2;
                        p = r * Math.Cos(theta);
                        q = r * Math.Sin(theta);
                        if (p * q < 0)
                        {
                            s1 = Fabs(p).ToString();
                            s2 = (Fabs(Fabs(q) - 1) < 1e-15) ? "i\r\n" : Fabs(q).ToString() + "i\r\n";
                            textBox7.Text += (s1 + "-" + s2 + "-" + s1 + "+" + s2);
                        }
                        else
                        {
                            s1 = Fabs(p).ToString();
                            s2 = (Fabs(Fabs(q) - 1) < 1e-15) ? "i\r\n" : Fabs(q).ToString() + "i\r\n";
                            textBox7.Text += (s1 + "+" + s2 + "-" + s1 + "-" + s2);
                        }
                    }
                }
            }
        }

        public double Starter(double a, double b, double c, double d, double e)//x^5+ax^4+bx^3+cx^2+dx+e=0根的上界
        {
            double q, y;
            if (a > 0 && b > 0 && c > 0 && d > 0 && e > 0)
            {
                y = 0;
            }
            else
            {
                q = Maxof(Fabs(a), Fabs(b), Fabs(c));
                q = Maxof(q, Fabs(d), Fabs(e));
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
                    y = Math.Pow(q, 0.2);
                }
            }
            return y;
        }

        public double Fun5_calculation(double a, double b, double c, double d, double e, double x)//计算函数值
        {
            return x * (x * (x * (x * (x + a) + b) + c) + d) + e;//x^5+ax^4+bx^3+cx^2+dx+e            
        }

        public double Fun5_derivative(double a, double b, double c, double d, double x)//计算导数
        {
            return x * (x * (x * (5 * x + 4 * a) + 3 * b) + 2 * c) + d;//5x^4+4ax^3+3bx^2+2cx+d
        }

        public int Fun4_realroot(double a, double b, double c, double d, double e)//四次方程实根数量
        {
            int i = 0;
            double y, p, q, delta;
            if (Fabs(e) < 1e-15)//x(ax^3+bx^2+cx+)=0
            {
                root[1] = Fun3_subsidiary(a, b, c, d);
                delta = b * b - 4 * a * c - a * root[1] * (3 * a * root[1] + 2 * b);
                if (delta > 0)
                {
                    temp = Math.Sqrt(delta);
                    root[2] = -root[1] / 2 - (b - temp) / (2 * a);
                    root[3] = -root[1] / 2 - (b + temp) / (2 * a);
                    i = 4;
                }
                else if (delta == 0)
                {
                    root[2] = -root[1] / 2 - b / (2 * a);
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
                        root[0] = (p - a) / 4;
                        root[1] = -(p + a) / 4;
                        p = Math.Sqrt(a * a - 8 * (temp + y));
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
                            temp = Math.Sqrt(delta);
                            root[0] = (2 * p - a + temp) / 4;
                            root[1] = (2 * p - a - temp) / 4;
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
                            temp = Math.Sqrt(delta);
                            root[2] = -(a + 2 * p - temp) / 4;
                            root[3] = -(a + 2 * p + temp) / 4;
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
                else if (Fabs(b) < 1e-15)//x^4+d=0
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
                        temp = Math.Sqrt(delta);
                        y = b - temp;
                        if (y <= 0)
                        {
                            y = Math.Sqrt(-y / 2);
                            root[0] = y;
                            root[1] = -y;
                            i = 2;
                        }
                        y = b + temp;
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
                            root[1] = root[0];
                            root[2] = -y;
                            root[3] = root[2];
                            i = 4;
                        }
                    }
                }
            }
            return i;
        }

        public void Fun5(double a, double b, double c, double d, double e)//x^5+ax^4+bx^3+cx^2+dx+e=0
        {
            int i, k;
            double x;
            bool stationary = false;
            if (Fabs(e) < 1e-15)
            {
                textBox7.Text += "0\r\n";
                Fun4(1, a, b, c, d);
            }
            else
            {
                i = Fun4_realroot(5, 4 * a, 3 * b, 2 * c, d) - 1;

                while (i >= 0)
                {
                    if (Fabs(Fun5_calculation(a, b, c, d, e, root[i])) < 1e-15)
                    {
                        stationary = true;
                        break;
                    }
                    else
                    {
                        i--;
                    }
                }

                if (stationary)
                {
                    x = root[i];
                }
                else
                {
                    if (Starter(a, b, c, d, e) == 0)
                    {
                        x = -Starter(-a, b, -c, d, -e);
                    }
                    else if (Starter(-a, b, -c, d, -e) == 0)
                    {
                        x = Starter(a, b, c, d, e);
                    }
                    else
                    {
                        x = (Starter(a, b, c, d, e) - Starter(-a, b, -c, d, -e)) / 2;
                    }
                    while (Fabs(Fun5_derivative(a, b, c, d, x)) < 1e-15)
                    {
                        x += 0.1;
                    }
                    k = (int)Convert.ToDouble(textBox8.Text);
                    for (i = 0; i < k; i++)
                    {
                        x -= Fun5_calculation(a, b, c, d, e, x) / Fun5_derivative(a, b, c, d, x);
                    }
                    temp = Fun5_calculation(a, b, c, d, e, x);
                    if (Fabs(temp) > 1)
                    {
                        MessageBox.Show("误差范围过大!");
                    }
                    MessageBox.Show("L-R = " + temp.ToString());
                }
                textBox7.Text += (x.ToString() + "\r\n");
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
                f /= (-e);
                textBox7.Text = "x = " + f.ToString();
            }
            else//常值
            {
                if (f == 0)
                {
                    textBox7.Text = "任意复数";
                }
                else
                {
                    textBox7.Text = "无解";
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)//求解
        {
            double a1, b1, c1, d1, e1, f1;
            textBox7.Text = "";
            Array.Clear(root, 0, 4);
            if (Isdouble(textBox1.Text) && Isdouble(textBox2.Text) && Isdouble(textBox3.Text) && Isdouble(textBox4.Text) && Isdouble(textBox5.Text) && Isdouble(textBox6.Text))
            {
                a1 = Convert.ToDouble(textBox1.Text);
                b1 = Convert.ToDouble(textBox2.Text);
                c1 = Convert.ToDouble(textBox3.Text);
                d1 = Convert.ToDouble(textBox4.Text);
                e1 = Convert.ToDouble(textBox5.Text);
                f1 = Convert.ToDouble(textBox6.Text);
                if (Isdouble(textBox8.Text))
                {
                    temp = 0;
                    Judgement(a1, b1, c1, d1, e1, f1);
                    button2.Focus();
                }
                else
                {
                    textBox8.Focus();
                    MessageBox.Show("请检查输入内容(Alt+F4)");
                }
            }
            else
            {
                MessageBox.Show("请检查输入内容(Alt+F4)");
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)//重置
        {
            temp = 0;
            Array.Clear(root, 0, 4);
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "";
        }

        private void Button3_Click(object sender, RoutedEventArgs e)//说明
        {
            MessageBox.Show("误差范围随系数变化;\r\n双击bx^4/cx^3/dx^2标签可切换模式;\r\n最后修改日期:2019-4-19\r\n");
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                textBox2.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox8.Focus();
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
                textBox6.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox4.Focus();
            }
        }

        private void TextBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                button1.Focus();
            }
            else if (e.Key == Key.Up)
            {
                textBox5.Focus();
            }
        }

        private void TextBox8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                textBox1.Focus();
            }
            else if (e.Key == Key.Up)
            {
                button2.Focus();
            }
        }

        private void Label2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Quartic frm = new Quartic();
            frm.Show();
        }

        private void Label3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Cubic frm = new Cubic();
            frm.Show();
        }

        private void Label4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Quadratic frm = new Quadratic();
            frm.Show();
        }
    }
}
