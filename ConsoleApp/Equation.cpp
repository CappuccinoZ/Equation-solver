#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <math.h>
#include <float.h>
#include <algorithm>
double root[4] = { 0 };

bool approx(double a, double b)//判断是否约等
{
	return (b == 0) ? (fabs(a) < DBL_EPSILON) : (fabs(a - b) < DBL_EPSILON);
}

void fun2(double a, double b, double c)//ax^2+bx+c=0
{
	if (approx(c, 0))//x(ax+b)=0
	{
		printf("0\n%.15lf\n", -b / a);
	}
	else
	{
		double x1, x2, delta;
		b /= a;
		c /= a;
		delta = b * b - 4 * c;
		if (delta > 0)
		{
			double t = sqrt(delta);
			x1 = (-b + t) / 2;
			x2 = (-b - t) / 2;
			printf("%.15lf\n%.15lf\n", x1, x2);
		}
		else if (delta == 0)
		{
			x1 = -b / 2;
			printf("%.15lf\n%.15lf\n", x1, x1);
		}
		else
		{
			x1 = -b / 2;
			x2 = sqrt(-delta) / 2;
			if (approx(b, 0))
			{
				if (approx(x2, 1))
				{
					printf("i\n-i\n");
				}
				else
				{
					printf("%.15lfi\n-%.15lfi\n", x2, x2);
				}
			}
			else
			{
				if (approx(x2, 1))
				{
					printf("%.15lf+i\n%.15lf-i\n", x1, x1);
				}
				else
				{
					printf("%.15lf+%.15lfi\n%.15lf-%.15lfi\n", x1, x2, x1, x2);
				}
			}
		}
	}
}

double fun3_subsidiary(double a, double b, double c, double d)//返回ax^3+bx^2+cx+d=0的一个实数根
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
		r = p * sqrt(p);
		theta = acos(q / r) / 3;
		x = 2 * cbrt(r) * cos(theta) - a / 3;
	}
	else if (delta == 0)
	{
		x = 2 * cbrt(q) - a / 3;
	}
	else
	{
		t = sqrt(delta);
		x = cbrt(q + t) + cbrt(q - t) - a / 3;//卡尔达诺公式
	}
	return x;
}

void fun3(double a, double b, double c, double d)//ax^3+bx^2+cx+d=0
{
	if (approx(d, 0))//x(ax^2+bx+c)=0
	{
		puts("0");
		fun2(a, b, c);//降次
	}
	else
	{
		double x = fun3_subsidiary(a, b, c, d);
		printf("%.15lf\n", x);
		fun2(a, a * x + b, x * (a * x + b) + c);
	}
}

void fun4(double a, double b, double c, double d, double e)//ax^4+bx^3+cx^2+dx+e=0
{
	double y, p, q, r, t, delta, theta;
	if (approx(e, 0))//x(ax^3+bx^2+cx+d)=0
	{
		puts("0");
		fun3(a, b, c, d);
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
			y = fun3_subsidiary(1, -b, a * c - 4 * d, 4 * b * d - a * a * d - c * c);//费拉里法
			if (approx(a * a - 4 * b + 4 * y, 0))
			{
				t = sqrt(y * y - 4 * d);
				p = sqrt(a * a + 8 * (t - y));
				q = (p - a) / 4;
				printf("%.15lf\n", q);
				q = -(p + a) / 4;
				printf("%.15lf\n", q);
				p = sqrt(a * a - 8 * (t + y));
				q = (p - a) / 4;
				printf("%.15lf\n", q);
				q = -(p + a) / 4;
				printf("%.15lf\n", q);
			}
			else
			{
				p = sqrt(a * a / 4 - b + y);
				q = (a * y - 2 * c) / (a * a - 4 * b + 4 * y);
				fun2(1, a / 2 - p, y / 2 - p * q);
				fun2(1, a / 2 + p, y / 2 + p * q);
			}
		}
		else if (approx(b, 0))//x^4+d=0
		{
			if (d < 0)
			{
				y = pow(-d, 0.25);
				if (approx(y, 1))
				{
					printf("1\n-1\ni\n-i\n");
				}
				else
				{
					printf("%.15lf\n-%.15lf\n%.15lfi\n-%.15lfi\n", y, y, y, y);
				}
			}
			else
			{
				y = pow(d / 4, 0.25);
				if (approx(y, 1))
				{
					printf("1+i\n1-i\n-1+i\n-1-i");
				}
				else
				{
					printf("%.15lf+%.15lfi\n%.15lf-%.15lfi\n", y, y, y, y);
					printf("-%.15lf+%.15lfi\n-%.15lf-%.15lfi\n", y, y, y, y);
				}
			}
		}
		else //x^4+bx^2+d=0
		{
			delta = b * b - 4 * d;
			if (delta > 0)
			{
				t = sqrt(delta);
				y = b - t;
				if (y > 0)
				{
					y = sqrt(y / 2);
					if (approx(y, 1))
					{
						printf("i\n-i\n");
					}
					else
					{
						printf("%.15lfi\n-%.15lfi\n", y, y);
					}
				}
				else
				{
					y = sqrt(-y / 2);
					printf("%.15lf\n-%.15lf\n", y, y);
				}
				y = b + t;
				if (y > 0)
				{
					y = sqrt(y / 2);
					if (approx(y, 1))
					{
						printf("i\n-i\n");
					}
					else
					{
						printf("%.15lfi\n-%.15lfi\n", y, y);
					}
				}
				else
				{
					y = sqrt(-y / 2);
					printf("%.15lf\n-%.15lf\n", y, y);
				}
			}
			else if (delta == 0)
			{
				if (b < 0)
				{
					y = sqrt(-b / 2);
					printf("%.15lf\n%.15lf\n-%.15lf\n-%.15lf\n", y, y, y, y);
				}
				else
				{
					y = sqrt(b / 2);
					if (approx(y, 1))
					{
						printf("i\ni\n-i\n-i\n");
					}
					else
					{
						printf("%.15lfi\n%.15lfi\n-%.15lfi\n-%.15lfi\n", y, y, y, y);
					}
				}
			}
			else
			{
				r = pow(d, 0.25);
				theta = atan2(sqrt(-delta) / 2, -b / 2) / 2;
				p = r * cos(theta);
				q = r * sin(theta);
				if (approx(q, 1))
				{
					printf("%.15lf+i\n%.15lf-i\n", p, p);
					printf("-%.15lf+i\n-%.15lf-i\n", p, p);
				}
				else
				{
					printf("%.15lf+%.15lfi\n%.15lf-%.15lfi\n", p, q, p, q);
					printf("-%.15lf+%.15lfi\n-%.15lf-%.15lfi\n", p, q, p, q);
				}

			}
		}
	}
}

int fun4_realroot(double a, double b, double c, double d, double e)//四次方程实根数量
{
	int i = 0;
	double y, p, q, t, delta;
	if (approx(e, 0))//x(ax^3+bx^2+cx+d)=0
	{
		root[1] = fun3_subsidiary(a, b, c, d);
		delta = b * b - 4 * a * c - a * root[1] * (3 * a * root[1] + 2 * b);
		if (delta > 0)
		{
			t = sqrt(delta);
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
			y = fun3_subsidiary(1, -b, a * c - 4 * d, 4 * b * d - a * a * d - c * c);//费拉里法
			if (approx(a * a - 4 * b + 4 * y, 0))
			{
				t = sqrt(y * y - 4 * d);
				p = sqrt(a * a + 8 * (t - y));
				root[0] = (p - a) / 4;
				root[1] = -(p + a) / 4;
				p = sqrt(a * a - 8 * (t + y));
				root[2] = (p - a) / 4;
				root[3] = -(p + a) / 4;
				i = 4;
			}
			else
			{
				p = sqrt(a * a / 4 - b + y);
				q = (a * y - 2 * c) / (a * a - 4 * b + 4 * y);
				delta = 4 * p * (4 * q - a + p) + a * a - 8 * y;
				if (delta > 0)
				{
					t = sqrt(delta);
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
					t = sqrt(delta);
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
		else if (approx(b, 0))//x^4+d=0
		{
			if (d < 0)
			{
				y = pow(-d, 0.25);
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
				t = sqrt(delta);
				y = b - t;
				if (y <= 0)
				{
					y = sqrt(-y / 2);
					root[0] = y;
					root[1] = -y;
					i = 2;
				}
				y = b + t;
				if (y <= 0)
				{
					y = sqrt(-y / 2);
					root[2] = y;
					root[3] = -y;
					i += 2;
				}
			}
			else if (delta == 0)
			{
				if (b < 0)
				{
					y = sqrt(-b / 2);
					root[0] = y;
					root[1] = y;
					root[2] = -y;
					root[3] = root[2];
					i = 4;
				}
			}
		}
	}
	return i;
}

double start(double a, double b, double c, double d, double e)//x^5+ax^4+bx^3+cx^2+dx+e=0根的上界
{
	double q, y;
	double k[5] = { a,b,c,d,e };
	if (a > 0 && b > 0 && c > 0 && d > 0 && e > 0)
	{
		y = 0;
	}
	else
	{
		q = *std::max_element(k, k + 5);
		if (a < 0)
		{
			y = q;
		}
		else if (b < 0)
		{
			y = sqrt(q);
		}
		else if (c < 0)
		{
			y = cbrt(q);
		}
		else if (d < 0)
		{
			y = pow(q, 0.25);
		}
		else
		{
			y = pow(q, 0.2);
		}
	}
	return y;
}

double fun5_calculation(double a, double b, double c, double d, double e, double x)//计算函数值
{
	return x * (x * (x * (x * (x + a) + b) + c) + d) + e;//x^5+ax^4+bx^3+cx^2+dx+e            
}

double fun5_derivative(double a, double b, double c, double d, double x)//计算导数
{
	return x * (x * (x * (5 * x + 4 * a) + 3 * b) + 2 * c) + d;//5x^4+4ax^3+3bx^2+2cx+d
}

void fun5(double a, double b, double c, double d, double e)//x^5+ax^4+bx^3+cx^2+dx+e=0
{
	int i, j;
	double x;
	bool stationary = false;
	if (approx(e, 0))
	{
		printf("x1,x2,x3,x4,x5:\n0\n");
		fun4(1, a, b, c, d);
	}
	else
	{
		i = fun4_realroot(5, 4 * a, 3 * b, 2 * c, d) - 1;
		while (i >= 0)
		{
			if (approx(fun5_calculation(a, b, c, d, e, root[i]), 0))
			{
				stationary = true;
				break;
			}
			i--;
		}
		if (stationary)
		{
			x = root[i];
		}
		else
		{
			if (start(a, b, c, d, e) == 0)
			{
				x = -start(-a, b, -c, d, -e);
			}
			else if (start(-a, b, -c, d, -e) == 0)
			{
				x = start(a, b, c, d, e);
			}
			else
			{
				x = (start(a, b, c, d, e) - start(-a, b, -c, d, -e)) / 2;
			}
			while (approx(fun5_derivative(a, b, c, d, x), 0))
			{
				x += 0.125;
			}
			puts("请输入迭代次数(e.g.1000)");
			scanf_s("%d", &j);
			for (i = 0; i < j; i++)
			{
				x -= fun5_calculation(a, b, c, d, e, x) / fun5_derivative(a, b, c, d, x);
			}
			double t = fun5_calculation(a, b, c, d, e, x);
			if (fabs(t) > 1)
			{
				puts("******误差范围过大******");
			}
			printf("[L-R = %E]\n", t);
		}
		printf("x1, x2, x3, x4, x5:\n%.15lf\n", x);
		fun4(1, x + a, x * (x + a) + b, x * (x * (x + a) + b) + c, x * (x * (x * (x + a) + b) + c) + d);//降次
	}
}

void judge(double a, double b, double c, double d, double e, double f)//判断次数
{
	if (a != 0)//五次
	{
		fun5(b / a, c / a, d / a, e / a, f / a);
	}
	else if (b != 0)//四次
	{
		puts("x1,x2,x3,x4:");
		fun4(b, c, d, e, f);
	}
	else if (c != 0)//三次
	{
		puts("x1,x2,x3:");
		fun3(c, d, e, f);
	}
	else if (d != 0)//二次
	{
		puts("x1,x2:");
		fun2(d, e, f);
	}
	else if (e != 0)//一次
	{
		printf("x = %.15lf\n", -f / e);
	}
	else//常值
	{
		if (f == 0)
		{
			puts("任意复数");
		}
		else
		{
			puts("无解");
		}
	}
}

int main(void)
{
	double a, b, c, d, e, f;
	puts("请输入方程系数(ax^5+bx^4+cx^3+dx^2+ex+f=0)");
	printf("a=");
	scanf_s("%lf", &a);
	printf("b=");
	scanf_s("%lf", &b);
	printf("c=");
	scanf_s("%lf", &c);
	printf("d=");
	scanf_s("%lf", &d);
	printf("e=");
	scanf_s("%lf", &e);
	printf("f=");
	scanf_s("%lf", &f);
	judge(a, b, c, d, e, f);
	system("pause");
	return 0;
}
