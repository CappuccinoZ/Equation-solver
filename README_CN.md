# 一元四次方程计算器

<p>
  <a href="https://kitakita.top/equation-solver/">Demo</a> |
  <a href="./README.md">English</a> |
  中文
</p>

[![peCLvY6.md.png](https://s41.ax1x.com/2026/03/06/peCLvY6.md.png)](https://imgchr.com/i/peCLvY6)

一个使用JavaScript的方程计算器，可以解以下方程：

1. 一元四次方程

   - 表达式： $ax^4+bx^3+cx^2+dx+e=0$
   - 解法： 笛卡尔法

2. 一元三次方程

   - 表达式： $ax^3+bx^2+cx+d=0$
   - 解法： 卡尔达诺公式

3. 一元二次方程

   - 表达式： $ax^2+bx+c=0$

---

## 快速开始

安装依赖项，启动开发服务器

```sh
npm install
npm run dev
```

浏览器访问： `http://localhost:5173/equation-solver/`

---

## 算法文件

- `src/complex.js`： 定义复数及其运算
- `src/solver.js`： 定义求解方程的函数
