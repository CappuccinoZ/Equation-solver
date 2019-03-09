#include <stdio.h>
#include <math.h>
#include <conio.h>
double temp;

void fun2(double a, double b, double c)//ax^2+bx+c=0
{
	double x1, x2, delta;
	if (c)
	{
		b /= a;
		c /= a;
		delta = b * b - 4 * c;
		if (delta > 0)
		{
			x1 = (-b + sqrt(delta)) / 2;
			x2 = (-b - sqrt(delta)) / 2;
			printf("\t%.12lf\n\t%.12lf\n", x1, x2);
		}
		else if (delta == 0)
		{
			x1 = -b / 2;
			printf("\t%.12lf\n\t%.12lf\n", x1, x1);
		}
		else
		{
			x1 = -b / 2;
			x2 = sqrt(-delta) / 2;
			printf("\t%.12lf + %.12lf i\n\t%.12lf - %.12lf i\n", x1, x2, x1, x2);
		}
	}
	else//x(ax+b)=0
	{
		x1 = 0;
		x2 = -b / a;
		printf("\t%.12lf\n\t%.12lf\n", x1, x2);
	}
}

double fun3_subsidiary(double a, double b, double c, double d)//返回ax^3+bx^2+cx+d=0的一个根
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
		r = p * sqrt(p);
		theta = acos(q / r) / 3;
		x = 2 * cbrt(r) * cos(theta) - a / 3;
	}
	else if (delta == 0)
	{
		x = -a / 3 + 2 * cbrt(q);
	}
	else
	{
		x = -a / 3 + cbrt(q + sqrt(delta)) + cbrt(q - sqrt(delta));//卡尔达诺公式
	}

	return x;
}

void fun3(double a, double b, double c, double d)//ax^3+bx^2+cx+d=0
{
	double x;
	if (d)
	{
		x = fun3_subsidiary(a, b, c, d);
		printf("\t%.12lf\n", x);
		fun2(1, b / a + x, -d / (a*x));//x1+x2+x3 = -b/a; x1*x2*x3 = -d/a
	}
	else//d = 0
	{
		printf("\t0.000000000000\n");//x = 0
		fun2(a, b, c);
	}
}

void fun4(double a, double b, double c, double d, double e)//ax^4+bx^3+cx^2+dx+e=0
{
	double y, p, q, r, delta, theta;
	if (e)
	{
		temp = a;
		a = b / temp;
		b = c / temp;
		c = d / temp;
		d = e / temp;
		if (a != 0 || c != 0)
		{
			y = fun3_subsidiary(1, -b, a*c - 4 * d, 4 * b*d - a * a*d - c * c);//费拉里法
			p = sqrt(a * a / 4 - b + y);
			q = (a * y - 2 * c) / (a*a - 4 * b + 4 * y);
			fun2(1, a / 2 - p, y / 2 - p * q);
			fun2(1, a / 2 + p, y / 2 + p * q);
		}
		else if (b)//x^4+bx^2+d=0
		{
			delta = b * b - 4 * d;
			if (delta > 0)
			{
				y = b - sqrt(delta);
				if (y > 0)
				{
					y = sqrt(y / 2);
					printf("\t%.12lf i\n\t-%.12lf i\n", y, y);
				}
				else
				{
					y = sqrt(-y / 2);
					printf("\t%.12lf\n\t-%.12lf\n", y, y);
				}

				y = b + sqrt(delta);
				if (y > 0)
				{
					y = sqrt(y / 2);
					printf("\t%.12lf i\n\t-%.12lf i\n", y, y);
				}
				else
				{
					y = sqrt(-y / 2);
					printf("\t%.12lf\n\t-%.12lf\n", y, y);
				}
			}
			else if (delta == 0)
			{
				if (b < 0)
				{
					y = sqrt(-b / 2);
					printf("\t%.12lf\n\t%.12lf\n\t-%.12lf\n\t-%.12lf\n", y, y, y, y);
				}
				else
				{
					y = sqrt(b / 2);
					printf("\t%.12lf i\n\t%.12lf i\n\t-%.12lf i\n\t-%.12lf i\n", y, y, y, y);
				}
			}
			else
			{
				r = sqrt(b*b - delta) / 2;
				theta = atan2(sqrt(-delta) / 2, -b / 2) / 2;
				p = sqrt(r)*cos(theta);
				q = sqrt(r)*sin(theta);
				printf("\t%+.12lf %+.12lf i\n\t%+.12lf %+.12lf i\n", p, q, -p, -q);

				theta = atan2(-sqrt(-delta) / 2, -b / 2) / 2;
				p = sqrt(r)*cos(theta);
				q = sqrt(r)*sin(theta);
				printf("\t%+.12lf %+.12lf i\n\t%+.12lf %+.12lf i\n", p, q, -p, -q);
			}
		}
		else if (d < 0)//x^4+d=0
		{
			y = pow(-d, 0.25);
			printf("\t%.12lf\n\t-%.12lf\n\t%.12lf i\n\t-%.12lf i\n", y, y, y, y);
		}
		else
		{
			y = pow(d / 4, 0.25);
			printf("\t%.12lf + %.12lf i\n\t%.12lf - %.12lf i\n", y, y, y, y);
			printf("\t-%.12lf + %.12lf i\n\t-%.12lf - %.12lf i\n", y, y, y, y);
		}
	}
	else
	{
		printf("\t0.000000000000\n");
		fun3(a, b, c, d);
	}
}

void judgement(double a, double b, double c, double d, double e)//判断方程最高次数
{
	if (a)//四次
	{
		printf("\n\tx1,x2,x3,x4:\n");
		fun4(a, b, c, d, e);
	}
	else if (b)//三次
	{
		printf("\n\tx1,x2,x3:\n");
		fun3(b, c, d, e);
	}
	else if (c)//二次
	{
		printf("\n\tx1,x2:\n");
		fun2(c, d, e);
	}
	else if (d)//一次
	{
		printf("\n\tx = %.12lf.\n", -e / d);
	}
	else if (e)//常值
	{
		printf("\n\t无解\n");
	}
	else
	{
		printf("\n\t任意复数\n");
	}
}

int main(void)
{
	double a, b, c, d, e;
	printf("求解方程ax^4+bx^3+cx^2+dx+e=0(系数均为实数):\n");
	printf("请输入四次项系数:\n");
	scanf_s("%lf", &a);
	printf("请输入三次项系数:\n");
	scanf_s("%lf", &b);
	printf("请输入二次项系数:\n");
	scanf_s("%lf", &c);
	printf("请输入一次项系数:\n");
	scanf_s("%lf", &d);
	printf("请输入常数项:\n");
	scanf_s("%lf", &e);

	judgement(a, b, c, d, e);
	_getch();
	return 0;
}
