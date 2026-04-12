# Quartic Equation Solver

<p>
  <a href="https://kitakita.top/equation-solver/">Demo</a> |
  English |
  <a href="./README_CN.md">中文</a>
</p>

[![peCLvY6.md.png](https://s41.ax1x.com/2026/03/06/peCLvY6.md.png)](https://imgchr.com/i/peCLvY6)

A JavaScript-based equation solver capable of solving the following equations:

1. Quartic Equation (Fourth-Degree)

   - Expression: $ax^4+bx^3+cx^2+dx+e=0$
   - Method: Descartes's method

2. Cubic Equation (Third-Degree)

   - Expression: $ax^3+bx^2+cx+d=0$
   - Method: Cardano formula

3. Quadratic Equation (Second-Degree)

   - Expression: $ax^2+bx+c=0$

---

## Quick Start

Install dependencies and start the development server:

```sh
npm install
npm run dev
```

Open your browser and navigate to: `http://localhost:5173/equation-solver/`

---

## Core Algorithms

- `src/complex.js`: Defines complex number structures and their mathematical operations
- `src/solver.js`: Contains the core logic and functions for solving the equations
