#include <stdio.h>
#include <math.h>
#include <stdbool.h>
#include <stdlib.h>
#include <conio.h>
long double temp = 0;
long double root[4] = { 0 };

long double Maxof(long double x, long double y, long double z)//取最大值
{
	long double m = (x > y) ? x : y;
	return (m > z)? m : z;
}

void Fun2(long double a, long double b, long double c)//ax^2+bx+c=0
{
	long double x1, x2, delta;
	if (fabsl(c) < 1e-16)//x(ax+b)=0
	{
		x1 = -b / a;
		printf("\t0\n\t%.15Lf\n", x1);
	}
	else
	{
		b /= a;
		c /= a;
		delta = b * b - 4 * c;
		if (delta > 0)
		{
			temp = sqrtl(delta);
			x1 = (-b + temp) / 2;
			x2 = (-b - temp) / 2;
			printf("\t%.15Lf\n\t%.15Lf\n", x1, x2);
		}
		else if (delta == 0)
		{
			x1 = -b / 2;
			printf("\t%.15Lf\n\t%.15Lf\n", x1, x1);
		}
		else
		{
			x1 = -b / 2;
			x2 = sqrtl(-delta) / 2;
			if (fabsl(b) < 1e-16)
			{
				if (fabsl(x2 - 1) < 1e-16)
				{
					printf("\ti\n\t-i\n");
				}
				else
				{
					printf("\t%.15Lfi\n\t-%.15Lfi\n", x2, x2);
				}
			}
			else
			{
				if (fabsl(x2 - 1) < 1e-16)
				{
					printf("\t%.15Lf+i\n\t%.15Lf-i\n", x1, x1);
				}
				else
				{
					printf("\t%.15Lf+%.15Lfi\n\t%.15Lf-%.15Lfi\n", x1, x2, x1, x2);
				}
			}
		}
	}
}

long double Fun3_subsidiary(long double a, long double b, long double c, long double d)//返回ax^3+bx^2+cx+d=0的一个实数根
{
	long double p, q, r, x, theta, delta;
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
		r = p * sqrtl(p);
		theta = acosl(q / r) / 3;
		x = 2 * cbrtl(r) * cosl(theta) - a / 3;
	}
	else if (delta == 0)
	{
		x = -a / 3 + 2 * cbrtl(q);
	}
	else
	{
		temp = sqrtl(delta);
		x = -a / 3 + cbrtl(q + temp) + cbrtl(q - temp);//卡尔达诺公式
	}
	return x;
}

void Fun3(long double a, long double b, long double c, long double d)//ax^3+bx^2+cx+d=0
{
	long double x;
	if (fabsl(d) < 1e-16)//x(ax^2+bx+c)=0
	{
		printf("\t0\n");
		Fun2(a, b, c);//降次
	}
	else
	{
		x = Fun3_subsidiary(a, b, c, d);
		printf("\t%.15Lf\n", x);
		Fun2(a, a * x + b, x * (a * x + b) + c);
	}
}

void Fun4(long double a, long double b, long double c, long double d, long double e)//ax^4+bx^3+cx^2+dx+e=0
{
	long double y, p, q, r, delta, theta;
	if (fabsl(e) < 1e-16)//x(ax^3+bx^2+cx+d)=0
	{
		printf("\t0\n");
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
			if (fabsl(a * a - 4 * b + 4 * y) < 1e-16)
			{
				temp = sqrtl(y * y - 4 * d);
				p = sqrtl(a * a + 8 * (temp - y));
				q = (p - a) / 4;
				printf("\t%.15Lf\n", q);
				q = -(p + a) / 4;
				printf("\t%.15Lf\n", q);
				p = sqrtl(a * a - 8 * (temp + y));
				q = (p - a) / 4;
				printf("\t%.15Lf\n", q);
				q = -(p + a) / 4;
				printf("\t%.15Lf\n", q);
			}
			else
			{
				p = sqrtl(a * a / 4 - b + y);
				q = (a * y - 2 * c) / (a * a - 4 * b + 4 * y);
				Fun2(1, a / 2 - p, y / 2 - p * q);
				Fun2(1, a / 2 + p, y / 2 + p * q);
			}
		}
		else if (fabsl(b) < 1e-16)//x^4+d=0
		{
			if (d < 0)
			{
				y = powl(-d, 0.25);
				if (fabsl(y - 1) < 1e-16)
				{
					printf("\t1\n\t-1\n\ti\n\t-i\n");
				}
				else
				{
					printf("\t%.15Lf\n\t-%.15Lf\n\t%.15Lfi\n\t-%.15Lfi\n", y, y, y, y);
				}
			}
			else
			{
				y = powl(d / 4, 0.25);
				if (fabsl(y - 1) < 1e-16)
				{
					printf("\t1+i\n\t1-i\n\t-1+i\n\t-1-i");
				}
				else
				{
					printf("\t%.15Lf+%.15Lf\n\t%.15Lf-%.15Lf\n", y, y, y, y);
					printf("\t-%.15Lf+%.15Lf\n\t-%.15Lf-%.15Lf\n", y, y, y, y);
				}
			}
		}
		else //x^4+bx^2+d=0
		{
			delta = b * b - 4 * d;
			if (delta > 0)
			{
				temp = sqrtl(delta);
				y = b - temp;
				if (y > 0)
				{
					y = sqrtl(y / 2);
					if (fabsl(y - 1) < 1e-16)
					{
						printf("\ti\n\t-i\n");
					}
					else
					{
						printf("\t%.15Lfi\n\t-%.15Lfi\n", y, y);
					}
				}
				else
				{
					y = sqrtl(-y / 2);
					printf("\t%.15Lf\n\t-%.15Lf\n", y, y);
				}
				y = b + temp;
				if (y > 0)
				{
					y = sqrtl(y / 2);
					if (fabsl(y - 1) < 1e-16)
					{
						printf("\ti\n\t-i\n");
					}
					else
					{
						printf("\t%.15Lfi\n\t-%.15Lfi\n", y, y);
					}
				}
				else
				{
					y = sqrtl(-y / 2);
					printf("\t%.15Lf\n\t-%.15Lf\n", y, y);
				}
			}
			else if (delta == 0)
			{
				if (b < 0)
				{
					y = sqrtl(-b / 2);
					printf("\t%.15Lf\n\t%.15Lf\n\t-%.15Lf\n\t-%.15Lf\n", y, y, y, y);
				}
				else
				{
					y = sqrtl(b / 2);
					if (fabsl(y - 1) < 1e-16)
					{
						printf("\ti\n\ti\n\t-i\n\t-i\n");
					}
					else
					{
						printf("\t%.15Lfi\n\t%.15Lfi\n\t-%.15Lfi\n\t-%.15Lfi\n", y, y, y, y);
					}
				}
			}
			else
			{
				r = powl(d, 0.25);
				theta = atan2l(sqrtl(-delta) / 2, -b / 2) / 2;
				p = r * cosl(theta);
				q = r * sinl(theta);
				if (p * q < 0)
				{
					if (fabsl(fabsl(q) - 1) < 1e-16)
					{
						printf("\t%.15Lf-i\n\t-%.15Lf+i\n", fabsl(p), fabsl(p));
					}
					else
					{
						printf("\t%.15Lf-%.15Lfi\n\t-%.15Lf+%.15Lfi\n", fabsl(p), fabsl(q), fabsl(p), fabsl(q));
					}
				}
				else
				{
					if (fabsl(fabsl(q) - 1) < 1e-16)
					{
						printf("\t%.15Lf+i\n\t-%.15Lf-i\n", fabsl(p), fabsl(p));
					}
					else
					{
						printf("\t%.15Lf+%.15Lfi\n\t-%.15Lf-%.15Lfi\n", fabsl(p), fabsl(q), fabsl(p), fabsl(q));
					}
				}
				theta = atan2l(-sqrtl(-delta) / 2, -b / 2) / 2;
				p = r * cosl(theta);
				q = r * sinl(theta);
				if (p * q < 0)
				{
					if (fabsl(fabsl(q) - 1) < 1e-16)
					{
						printf("\t%.15Lf-i\n\t-%.15Lf+i\n", fabsl(p), fabsl(p));
					}
					else
					{
						printf("\t%.15Lf-%.15Lfi\n\t-%.15Lf+%.15Lfi\n", fabsl(p), fabsl(q), fabsl(p), fabsl(q));
					}
				}
				else
				{
					if (fabsl(fabsl(q) - 1) < 1e-16)
					{
						printf("\t%.15Lf+i\n\t-%.15Lf-i\n", fabsl(p), fabsl(p));
					}
					else
					{
						printf("\t%.15Lf+%.15Lfi\n\t-%.15Lf-%.15Lfi\n", fabsl(p), fabsl(q), fabsl(p), fabsl(q));
					}
				}
			}
		}
	}
}

int Fun4_realroot(long double a, long double b, long double c, long double d, long double e)//四次方程实根数量
{
	int i = 0;
	long double y, p, q, delta;
	if (fabsl(e) < 1e-16)//x(ax^3+bx^2+cx+d)=0
	{
		root[1] = Fun3_subsidiary(a, b, c, d);
		delta = b * b - 4 * a * c - a * root[1] * (3 * a * root[1] + 2 * b);
		if (delta > 0)
		{
			temp = sqrtl(delta);
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
			if (fabsl(a * a - 4 * b + 4 * y) < 1e-16)
			{
				temp = sqrtl(y * y - 4 * d);
				p = sqrtl(a * a + 8 * (temp - y));
				root[0] = (p - a) / 4;
				root[1] = -(p + a) / 4;
				p = sqrtl(a * a - 8 * (temp + y));
				root[2] = (p - a) / 4;
				root[3] = -(p + a) / 4;
				i = 4;
			}
			else
			{
				p = sqrtl(a * a / 4 - b + y);
				q = (a * y - 2 * c) / (a * a - 4 * b + 4 * y);
				delta = 4 * p * (4 * q - a + p) + a * a - 8 * y;
				if (delta > 0)
				{
					temp = sqrtl(delta);
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
					temp = sqrtl(delta);
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
		else if (fabsl(b) < 1e-16)//x^4+d=0
		{
			if (d < 0)
			{
				y = powl(-d, 0.25);
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
				temp = sqrtl(delta);
				y = b - temp;
				if (y <= 0)
				{
					y = sqrtl(-y / 2);
					root[0] = y;
					root[1] = -y;
					i = 2;
				}
				y = b + temp;
				if (y <= 0)
				{
					y = sqrtl(-y / 2);
					root[2] = y;
					root[3] = -y;
					i += 2;
				}
			}
			else if (delta == 0)
			{
				if (b < 0)
				{
					y = sqrtl(-b / 2);
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

long double Starter(long double a, long double b, long double c, long double d, long double e)//x^5+ax^4+bx^3+cx^2+dx+e=0根的上界
{
	long double k, q, y;
	if (a > 0 && b > 0 && c > 0 && d > 0 && e > 0)
	{
		y = 0;
	}
	else
	{
		if (a < 0)
		{
			k = 1;
		}
		else if (b < 0)
		{
			k = 2;
		}
		else if (c < 0)
		{
			k = 3;
		}
		else if (d < 0)
		{
			k = 4;
		}
		else
		{
			k = 5;
		}
		q = Maxof(fabsl(a), fabsl(b), fabsl(c));
		q = Maxof(q, fabsl(d), fabsl(e));
		y = pow(q, 1.0 / k);
	}
	return y;
}

long double Fun5_calculation(long double a, long double b, long double c, long double d, long double e, long double x)//计算函数值
{
	return x * (x * (x * (x * (x + a) + b) + c) + d) + e;//x^5+ax^4+bx^3+cx^2+dx+e            
}

long double Fun5_derivative(long double a, long double b, long double c, long double d, long double x)//计算导数
{
	return x * (x * (x * (5 * x + 4 * a) + 3 * b) + 2 * c) + d;//5x^4+4ax^3+3bx^2+2cx+d
}

void Fun5(long double a, long double b, long double c, long double d, long double e)//x^5+ax^4+bx^3+cx^2+dx+e=0
{
	int i, j;
	long double x;
	bool stationary = false;
	if (fabsl(e) < 1e-16)
	{
		printf("x1,x2,x3,x4,x5:\n\t0\n");
		Fun4(1, a, b, c, d);
	}
	else
	{
		i = Fun4_realroot(5, 4 * a, 3 * b, 2 * c, d) - 1;
		while (i >= 0)
		{
			if (fabsl(Fun5_calculation(a, b, c, d, e, root[i])) < 1e-16)
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
			while (fabsl(Fun5_derivative(a, b, c, d, x)) < 1e-16)
			{
				x += 0.1;
			}
			printf("请输入迭代次数(默认值:1000)\n");
			scanf_s("%d", &j);
			for (i = 0; i < j; i++)
			{
				x -= Fun5_calculation(a, b, c, d, e, x) / Fun5_derivative(a, b, c, d, x);
			}
			printf("[L-R = %E]\n", Fun5_calculation(a, b, c, d, e, x));
		}
		printf("x1, x2, x3, x4, x5:\n\t%.15Lf\n", x);
		Fun4(1, x + a, x * (x + a) + b, x * (x * (x + a) + b) + c, x * (x * (x * (x + a) + b) + c) + d);//降次
	}
}

void Judgement(long double a, long double b, long double c, long double d, long double e, long double f)//判断次数
{
	if (a != 0)//五次
	{
		Fun5(b / a, c / a, d / a, e / a, f / a);
	}
	else if (b != 0)//四次
	{
		printf("x1,x2,x3,x4:\n");
		Fun4(b, c, d, e, f);
	}
	else if (c != 0)//三次
	{
		printf("x1,x2,x3:\n");
		Fun3(c, d, e, f);
	}
	else if (d != 0)//二次
	{
		printf("x1,x2:\n");
		Fun2(d, e, f);
	}
	else if (e != 0)//一次
	{
		f /= (-e);
		printf("x = %.15Lf\n", f);
	}
	else//常值
	{
		if (f == 0)
		{
			printf("任意复数\n");
		}
		else
		{
			printf("无解\n");
		}
	}
}

int main(void)
{
	long double a, b, c, d, e, f;
	printf("请输入方程ax^5+bx^4+cx^3+dx^2+ex+f=0的系数\n");
	scanf_s("%Lf%Lf%Lf%Lf%Lf%Lf", &a, &b, &c, &d, &e, &f);
	Judgement(a, b, c, d, e, f);
	system("pause");
	return 0;
}
