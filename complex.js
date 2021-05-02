/*complex.js|CappuccinoZ.github.io|2021-5-2*/
class Complex {
    constructor(real, imag) {
        if (isNaN(real) || isNaN(imag)) throw new TypeError();
        this.r = real;
        this.i = imag
    }
    norm() {
        return this.r * this.r + this.i * this.i
    }
    abs() {
        return Math.hypot(this.r, this.i)
    }
    arg() {
        return Math.atan2(this.i, this.r)
    }
    conj() {
        return new Complex(this.r, -this.i)
    }
    neg() {
        return new Complex(-this.r, -this.i)
    }
    add(that) {
        if (typeof that == "number") return new Complex(this.r + that, this.i);
        return new Complex(this.r + that.r, this.i + that.i)
    }
    sub(that) {
        if (typeof that == "number") return new Complex(this.r - that, this.i);
        return new Complex(this.r - that.r, this.i - that.i)
    }
    mul(that) {
        if (typeof that == "number") return new Complex(this.r * that, this.i * that);
        return new Complex(this.r * that.r - this.i * that.i, this.r * that.i + this.i * that.r)
    }
    div(that) {
        if (typeof that == "number") return new Complex(this.r / that, this.i / that);
        return this.mul(that.conj()).div(that.norm())
    }
    log() {
        return new Complex(Math.log(this.norm()) * 0.5, this.arg())
    }
    exp() {
        return new Complex(Math.cos(this.i), Math.sin(this.i)).mul(Math.exp(this.r))
    }
    pow(that) {
        if (typeof that == "number") return new Complex(Math.cos(this.arg() * that), Math.sin(this.arg() * that)).mul(Math.pow(this.abs(), that));
        return this.log().mul(that).exp()
    }
    sqrt() {
        return this.pow(0.5)
    }
    cbrt() {
        if (equal(this.i, 0) && this.r < 0) return new Complex(Math.cbrt(this.r), 0);
        return this.pow(1.0 / 3)
    }
    toString() {
        var a = Number(this.r.toFixed(12)),
            b = Number(this.i.toFixed(12));
        var str = "";
        if (equal(a, 0) && equal(b, 0)) str = "0";
        else {
            if (a != 0) {
                str += format(a);
                if (b > 0) str += "+"
            }
            switch (b) {
                case 0:
                    break;
                case -1:
                    str += "-";
                case 1:
                    str += "i";
                    break;
                default:
                    str += format(b) + "i";
                    break
            }
        }
        return str
    }
}
const equal = (a, b) => Math.abs(a - b) <= 1e-12;

function show(text) {
    var str = (typeof text == "number") ? format(Number(text.toFixed(12))) : text.toString();
    document.getElementById("box").innerHTML += (str + "\\\\")
}

function calc(a) {
    document.getElementById(a).value = (document.getElementById(a).value == "") ? "0" : eval(document.getElementById(a).value);
    return Number(document.getElementById(a).value)
}

function solve() {
    document.getElementById("box").innerHTML = "";
    var a = calc("num5"),
        b = calc("num4"),
        c = calc("num3"),
        d = calc("num2"),
        e = calc("num1"),
        f = calc("num0");
    switch (true) {
        case a != 0:
            fun5(b / a, c / a, d / a, e / a, f / a);
            break;
        case b != 0:
            fun4(b, c, d, e, f);
            break;
        case c != 0:
            fun3(c, d, e, f);
            break;
        case d != 0:
            fun2(d, e, f);
            break;
        case e != 0:
            show(-f / e);
            break;
        case f != 0:
            show("无解");
            break;
        default:
            show("任意复数");
            break
    }
    katex.render(document.getElementById("box").innerHTML, document.getElementById("box"))
}

function fun2(a, b, c) {
    if (typeof c == "number") c = new Complex(c, 0);
    var t = c.mul(-4 * a).add(b * b).sqrt();
    show(t.neg().sub(b).div(2 * a));
    show(t.sub(b).div(2 * a))
}

function fun3_one(a, b, c, d) {
    var k = 1 / a;
    a = b * k;
    b = c * k;
    c = d * k;
    var e = (3 * b - a * a) / 9,
        f = ((2 * a * a - 9 * b) * a / 27 + c) / 2;
    var t = new Complex(f * f + e * e * e, 0).sqrt();
    return t.sub(f).cbrt().add(t.neg().sub(f).cbrt()).r - a / 3
}

function fun3(a, b, c, d) {
    var x = equal(d, 0) ? 0 : fun3_one(a, b, c, d);
    show(x);
    fun2(a, a * x + b, (a * x + b) * x + c)
}

function fun4(a, b, c, d, e) {
    if (equal(e, 0)) {
        show(0);
        fun3(a, b, c, d)
    } else {
        var k = 1 / a;
        a = b * k;
        b = c * k;
        c = d * k;
        d = e * k;
        if (equal(a, 0) && equal(c, 0)) {
            var t = new Complex(b * b - 4 * d, 0).sqrt();
            fun2(1, 0, t.add(b).div(2));
            fun2(1, 0, t.neg().add(b).div(2))
        } else {
            var u = a / 2,
                v = (4 * b - a * a) / 8;
            if (c == 2 * u * v && d == v * v) {
                fun2(1, u, v);
                fun2(1, u, v);
            } else {
                var y = fun3_one(1, -b, a * c - 4 * d, 4 * b * d - a * a * d - c * c);
                k = a * a / 4 - b + y;
                if (equal(k, 0)) {
                    var t = new Complex(y * y / 4 - d, 0).sqrt();
                    fun2(1, a / 2, t.add(y / 2));
                    fun2(1, a / 2, t.neg().add(y / 2))
                } else {
                    var p = Math.sqrt(k), q = (a * y - 2 * c) / (4 * p * p);
                    fun2(1, a / 2 - p, y / 2 - p * q);
                    fun2(1, a / 2 + p, y / 2 + p * q)
                }
            }
        }
    }
}

var root = new Array();

function fun4_realroot(a, b, c, d, e) {
    var y, p, q, t, delta;
    t = 1 / a;
    a = b * t;
    b = c * t;
    c = d * t;
    d = e * t;
    y = fun3_one(1, -b, a * c - 4 * d, 4 * b * d - a * a * d - c * c);
    t = a * a / 4 - b + y;
    if (equal(t, 0)) {
        if (y * y - 4 * d >= 0) {
            delta = a * a / 16 - y / 2 + Math.sqrt(y * y / 4 - d);
            if (delta >= 0) {
                t = Math.sqrt(delta);
                root.push(-a / 4 + t);
                root.push(-a / 4 - t)
            }
            delta = a * a / 16 - y / 2 - Math.sqrt(y * y / 4 - d);
            if (delta >= 0) {
                t = Math.sqrt(delta);
                root.push(-a / 4 + t);
                root.push(-a / 4 - t)
            }
        }
    } else {
        p = Math.sqrt(t);
        q = (a * y - 2 * c) / (4 * p * p);
        delta = 4 * p * (p + 4 * q - a) + a * a - 8 * y;
        if (delta >= 0) {
            t = Math.sqrt(delta);
            root.push((2 * p - a + t) / 4);
            root.push((2 * p - a - t) / 4)
        }
        delta = 4 * p * (p - 4 * q + a) + a * a - 8 * y;
        if (delta >= 0) {
            t = Math.sqrt(delta);
            root.push((-2 * p - a + t) / 4);
            root.push((-2 * p - a - t) / 4)
        }
    }
}

function start(a, b, c, d, e) {
    var y = 0;
    if (Math.min(a, b, c, d, e) < 0) {
        var q = Math.max(a, b, c, d, e);
        switch (true) {
            case a < 0:
                y = q;
                break;
            case b < 0:
                y = Math.sqrt(q);
                break;
            case c < 0:
                y = Math.cbrt(q);
                break;
            case d < 0:
                y = Math.pow(q, 0.25);
                break;
            case e < 0:
                y = Math.pow(q, 0.2);
                break;
            default:
                break
        }
    }
    return y
}
const fun5_calc = (a, b, c, d, e, x) => x * (x * (x * (x * (x + a) + b) + c) + d) + e;
const fun5_derivative = (a, b, c, d, x) => x * (x * (x * (5 * x + 4 * a) + 3 * b) + 2 * c) + d;

function fun5(a, b, c, d, e) {
    if (equal(e, 0)) {
        show(0);
        fun4(1, a, b, c, d)
    } else {
        var i, x, t;
        var f = true;
        fun4_realroot(5, 4 * a, 3 * b, 2 * c, d);
        for (i in root)
            if (equal(fun5_calc(a, b, c, d, e, root[i]), 0)) {
                f = false;
                x = root[i];
                break
            }
        root.length = 0;
        if (f) {
            t = start(a, b, c, d, e);
            x = -start(-a, b, -c, d, -e);
            if (t != 0) x = equal(x, 0) ? t : (t + x) / 2;
            while (equal(fun5_derivative(a, b, c, d, x), 0)) x += 0.125;
            i = 0;
            do {
                t = x;
                x -= fun5_calc(a, b, c, d, e, x) / fun5_derivative(a, b, c, d, x);
                i++
            } while (i < 6000 && Math.abs(x - t) > 1e-15)
            show("迭代次数:" + i);
            t = fun5_calc(a, b, c, d, e, x);
            if (Math.abs(t) > 1) show("误差较大");
            show("L-R=" + t)
        }
        show(x);
        fun4(1, x + a, x * (x + a) + b, x * (x * (x + a) + b) + c, x * (x * (x * (x + a) + b) + c) + d)
    }
}

class Fraction {
    constructor(a, b, c, d, e) {
        this.A = "" + a;
        this.B = (b == 1 && c != 1) ? "" : "" + b;
        this.C = c == 1 ? "" : '\\sqrt{' + c + '}';
        if (d > 0) {
            this.D = (d == 1 && e != 1) ? '+' : '+' + d;
        } else {
            this.D = (d == -1 && e != 1) ? '-' : "" + d;
        }
        this.E = e == 1 ? "" : '\\sqrt{' + e + '}';
    }
    toString() {
        var t = this.B + this.C + this.D + this.E;
        return this.A == "1" ? '(' + t + ')' : '\\frac{' + t + '}{' + this.A + '}';
    }
}
function isInteger(x) {
    return Math.abs(x - Math.round(x)) < 1e-7;
}
function format(x) {
    var a, b, c, d, f, t, y, z;
    var str = "";
    if (!isInteger(x) && Math.abs(x) < 25 && Math.abs(x) > 0.01) {
        if (x < 0) {
            str += "-";
            x = -x;
        }
        for (a = 1; a < 25; a++) {
            f = x * a;
            if (isInteger(f)) {
                str += '\\frac{' + Math.round(f) + '}{' + a + '}';
                return str;
            } else if (isInteger(f * f)) {
                for (b = Math.floor(f); b > 1 && !isInteger(f * f / (b * b)); b--);
                t = (b == 1 ? '' : b) + '\\sqrt{' + Math.round(f * f / (b * b)) + '}';
                str += (a == 1) ? t : '\\frac{' + t + '}{' + a + '}';
                return str;
            }
            else {
                for (z = 1; z < 1000; z++) {
                    y = Math.pow(f - Math.sqrt(z), 2);
                    if (isInteger(y)) {
                        for (b = Math.round(Math.sqrt(y)); b > 0; b--) {
                            c = y / (b * b);
                            if (isInteger(c)) {
                                for (d = Math.round(Math.sqrt(z)); d > 0; d--) {
                                    e = z / (d * d);
                                    if (isInteger(e)) {
                                        str += new Fraction(a, b, Math.round(c), d, Math.round(e));
                                        return str;
                                    }
                                }
                            }
                        }
                    }
                    y = Math.pow(f + Math.sqrt(z), 2);
                    if (isInteger(y)) {
                        for (b = Math.round(Math.sqrt(y)); b > 0; b--) {
                            c = y / (b * b);
                            if (isInteger(c)) {
                                for (d = Math.round(Math.sqrt(z)); d > 0; d--) {
                                    e = z / (d * d);
                                    if (isInteger(e)) {
                                        str += new Fraction(a, b, Math.round(c), -d, Math.round(e));
                                        return str;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    str += x;
    return str;
}