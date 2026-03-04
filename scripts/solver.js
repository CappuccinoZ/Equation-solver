import { Complex } from './complex.js'

// 解二次方程函数
function quadraticRoot(a, b, c) {
  /*
    ax^2 + bx + c = 0
    delta = b^2 - 4ac
    x1 = (-b + sqrt(delta))/(2a)
    x2 = (-b - sqrt(delta))/(2a)
  */
  let sqrt, x1, x2
  if ([a, b, c].every(num => Complex.isRealNumber(num))) {
    if (typeof a !== 'number') a = a.real
    if (typeof b !== 'number') b = b.real
    if (typeof c !== 'number') c = c.real
    sqrt = Complex.sqrt(b * b - 4 * a * c)
    x1 = new Complex(-b).add(sqrt).div(2 * a)
    x2 = new Complex(-b).sub(sqrt).div(2 * a)
  } else {
    if (typeof a === 'number') a = new Complex(a)
    if (typeof b === 'number') b = new Complex(b)
    if (typeof c === 'number') c = new Complex(c)
    sqrt = Complex.sqrt(b.mul(b).sub(a.mul(c).mul(4)))
    x1 = b.neg().add(sqrt).div(a.mul(2))
    x2 = b.neg().sub(sqrt).div(a.mul(2))
  }

  return [x1, x2]
}

// 获取三次方程的一个实根
function cubicRealRoot(den, ...nums) {
  /*
    将其他项系数除以最高项系数，y^3 + ay^2 + by + c = 0
    换元 y = x - a/3, x^3 + px = q
    p = b - a^2/3
    q = (9b - 2a^2)*a/27 - c
    delta = q^2/4 + p^3/27
    x = cbrt(q/2 + sqrt(delta)) + cbrt(q/2 - sqrt(delta))
  */
  const [a, b, c] = nums.map(num => num / den)
  const p = b - a * a / 3
  const q = (9 * b - 2 * a * a) * a / 27 - c
  const sqrt = Complex.sqrt(q * q / 4 + p * p * p / 27)
  const y = Complex.cbrt(new Complex(q / 2).add(sqrt)).real + Complex.cbrt(new Complex(q / 2).sub(sqrt)).real - a / 3

  return y
}

// 解三次方程函数
function cubicRoot(den, ...nums) {
  const [a, b, c] = nums.map(num => num / den)
  const x1 = cubicRealRoot(1, a, b, c)
  const [x2, x3] = quadraticRoot(1, x1 + a, x1 * (x1 + a) + b)

  return [new Complex(x1), x2, x3]
}

// 解四次方程函数
function quarticRoot(den, ...nums) {
  /*
    y^4 + ay^3 + by^2 + cy + d = 0
    换元 y = x - a/4, x^4 + px^2 + qx + r = 0
    p = b - 3a^2/8
    q = (a^3 - 4ab)/8 + c
    r = d - a(3a^3 - 16ab + 64c)/256
    (x^2 + kx + l)(x^2 - kx + m) = 0
  */
  const [a, b, c, d] = nums.map(num => num / den)
  const p = b - 3 * a * a / 8
  const q = a * (a * a - 4 * b) / 8 + c
  const r = d - a * (a * (3 * a * a - 16 * b) + 64 * c) / 256
  let k, l, m
  if (Complex.equals(q, 0)) {
    /*
      当 q = 0 时，k = 0
      m = (p + sqrt(p^2 - 4r))/2
      l = (p - sqrt(p^2 - 4r))/2
    */
    k = Complex.Zero;
    [m, l] = quadraticRoot(1, -p, r)
    // m = new Complex(p).add(Complex.sqrt(p * p - 4 * r)).div(2)
    // l = new Complex(p).sub(Complex.sqrt(p * p - 4 * r)).div(2)
  } else {
    /*
      k^2 满足方程 x^3 + 2px^2 + (p^2 - 4r)x - q^2 = 0
      m = (u^2 + p + q/u)/2
      l = (u^2 + p - q/u)/2
      不要改加减号顺序
    */
    k = Complex.sqrt(cubicRealRoot(1, 2 * p, p * p - 4 * r, -q * q))
    m = k.mul(k).add(p).add(new Complex(q).div(k)).div(2)
    l = k.mul(k).add(p).sub(new Complex(q).div(k)).div(2)
  }

  const [y1, y2] = quadraticRoot(1, k, l)
  const [y3, y4] = quadraticRoot(1, k.neg(), m)
  const x = [y1, y2, y3, y4].map(y => y.sub(a / 4))
  return x
}

export function solve(...nums) {
  const n = nums.length - 1
  let root
  switch (n) {
    case 2:
      root = quadraticRoot(...nums)
      break
    case 3:
      root = cubicRoot(...nums)
      break
    case 4:
      root = quarticRoot(...nums)
      break
    default:
      throw new Error('不支持的参数长度')
  }
  return root
}