export class Complex {
  constructor(real = 0, imag = 0) {
    if (isNaN(real) || isNaN(imag)) throw new Error('参数必须是数字')
    // 定义实部与虚部
    this.real = real
    this.imag = imag
  }

  // 获取模与幅角属性
  get abs() { return Math.hypot(this.real, this.imag) }
  get arg() { return Math.atan2(this.imag, this.real) }

  // 四则运算
  add(that) {
    if (typeof that === 'number') return new Complex(this.real + that, this.imag)
    return new Complex(this.real + that.real, this.imag + that.imag)
  }
  sub(that) {
    if (typeof that === 'number') return new Complex(this.real - that, this.imag)
    return new Complex(this.real - that.real, this.imag - that.imag)
  }
  mul(that) {
    if (typeof that === 'number') return new Complex(this.real * that, this.imag * that)
    const [a, b, c, d] = [this.real, this.imag, that.real, that.imag]
    return new Complex(a * c - b * d, a * d + b * c)
  }
  div(that) {
    if (typeof that === 'number') return new Complex(this.real / that, this.imag / that)
    const [a, b, c, d] = [this.real, this.imag, that.real, that.imag]
    const den = c * c + d * d
    return new Complex((a * c + b * d) / den, (b * c - a * d) / den)
  }
  neg() { return new Complex(-this.real, -this.imag) }

  // 常用的复数
  static I = new Complex(0, 1)
  static Zero = new Complex(0, 0)
  static One = new Complex(1, 0)

  // 从极坐标创建复数
  static fromPolar(r, theta) {
    return new Complex(r * Math.cos(theta), r * Math.sin(theta))
  }

  // 平方根函数
  static sqrt(z) {
    if (typeof z === 'number') return z >= 0 ? new Complex(Math.sqrt(z)) : new Complex(0, Math.sqrt(-z))
    return Complex.fromPolar(Math.sqrt(z.abs), z.arg / 2)
  }
  // 立方根函数
  static cbrt(z) {
    if (typeof z === 'number') return new Complex(Math.cbrt(z))
    if (Complex.isRealNumber(z)) return new Complex(Math.cbrt(z.real))
    return Complex.fromPolar(Math.cbrt(z.abs), z.arg / 3)
  }

  // 判断两个数是否相等
  static equals(x, y) {
    const fn = (a, b) => Math.abs(a - b) <= 1E-12
    if (typeof x === 'number' && typeof y === 'number') return fn(x, y)
    if (typeof x === 'number') x = new Complex(x)
    if (typeof y === 'number') y = new Complex(y)
    return fn(x.real, y.real) && fn(x.imag, y.imag)
  }

  // 判断是否为实数
  static isRealNumber(z) {
    if (typeof z === 'number') return true
    return Complex.equals(z.imag, 0)
  }

  toString() {
    const [a, b] = [+this.real.toFixed(6), +this.imag.toFixed(6)]

    // 如果是实数则直接返回
    if (Complex.isRealNumber(this)) return a.toString()

    let str = ''

    // 处理实部，补充加号
    if (!Complex.equals(this.real, 0)) {
      str += this.imag > 0 ? a + '+' : a
    }

    // 处理虚部
    if (Complex.equals(this.imag, 1)) {
      str += 'i'
    } else if (Complex.equals(this.imag, -1)) {
      str += '-i'
    } else {
      str += b + 'i'
    }

    return str
  }
}
