/*complex.js|CappuccinoZ.github.io|2021-7-22*/
class Complex {
    constructor(real, imag) {
        if (isNaN(real) || isNaN(imag)) throw new TypeError();
        this.r = real;
        this.i = imag;
    }
    abs2() { return this.r * this.r + this.i * this.i; }
    abs() { return Math.hypot(this.r, this.i); }
    arg() { return Math.atan2(this.i, this.r); }
    conj() { return new Complex(this.r, -this.i); }
    neg() { return new Complex(-this.r, -this.i); }
    add(that) {
        if (typeof that == "number") return new Complex(this.r + that, this.i);
        return new Complex(this.r + that.r, this.i + that.i);
    }
    sub(that) {
        if (typeof that == "number") return new Complex(this.r - that, this.i);
        return new Complex(this.r - that.r, this.i - that.i);
    }
    mul(that) {
        if (typeof that == "number") return new Complex(this.r * that, this.i * that);
        return new Complex(this.r * that.r - this.i * that.i, this.r * that.i + this.i * that.r);
    }
    div(that) {
        if (typeof that == "number") return new Complex(this.r / that, this.i / that);
        return this.mul(that.conj()).mul(1 / that.abs2());
    }
    toString() {
        var a = Number(this.r.toFixed(12)), b = Number(this.i.toFixed(12));
        var str = "";
        if (equal(b, 0)) return a.toString();
        if (a != 0) {
            str += a;
            if (b > 0) str += "+";
        }
        switch (b) {
            case -1: str += "-i"; break;
            case 1: str += "i"; break;
            default: str += b + "i"; break;
        }
        return str;
    }
}

function equal(a, b) {
    if (typeof a == "number") a = new Complex(a, 0);
    if (typeof b == "number") b = new Complex(b, 0);
    var t = a.sub(b);
    return Math.abs(t.r) < 1E-12 && Math.abs(t.i) < 1E-12;
}

function sqrt(z) {
    if (typeof z == "number") z = new Complex(z, 0);
    return new Complex(Math.cos(z.arg() / 2), Math.sin(z.arg() / 2)).mul(Math.sqrt(z.abs()));
}
function cbrt(z) {
    if (typeof z == "number") z = new Complex(z, 0);
    var t = (equal(z.i, 0) && z.r < 0) ? Math.PI : z.arg() / 3;
    return new Complex(Math.cos(t), Math.sin(t)).mul(Math.pow(z.abs(), 1.0 / 3));
}

function fun2(a, b, c) {
    if (typeof a == "number") a = new Complex(a, 0);
    if (typeof b == "number") b = new Complex(b, 0);
    if (typeof c == "number") c = new Complex(c, 0);
    var t = sqrt(b.mul(b).sub(a.mul(c).mul(4)));
    show(b.neg().add(t).div(a.mul(2)));
    show(b.neg().sub(t).div(a.mul(2)));
}

function fun3_root(a, b, c, d) {
    var k = 1 / a;
    a = b * k;
    b = c * k;
    c = d * k;
    var p = b - a * a / 3, q = (2 * a * a - 9 * b) * a / 27 + c;
    var t = sqrt(q * q / 4 + p * p * p / 27);
    return cbrt(t.sub(q / 2)).r + cbrt(t.neg().sub(q / 2)).r - a / 3;
}
function fun3(a, b, c, d) {
    var k = 1 / a;
    a = b * k;
    b = c * k;
    c = d * k;
    var x = fun3_root(1, a, b, c);
    show(x);
    fun2(1, x + a, x * (x + a) + b);
}

function fun4_roots(a, b, c, d, e) {
    var k = 1 / a;
    a = b * k;
    b = c * k;
    c = d * k;
    d = e * k;
    var p, q, r, u, v1, v2, t;
    p = b - 3 * a * a / 8;
    q = a * (a * a - 4 * b) / 8 + c;
    r = d - a * (a * (3 * a * a - 16 * b) + 64 * c) / 256;
    if (equal(q, 0)) {
        u = new Complex(0, 0);
        v1 = sqrt(p * p - 4 * r).add(p).div(2);
        v2 = v1.neg().add(p);
    }
    else {
        u = sqrt(fun3_root(1, 2 * p, p * p - 4 * r, -q * q));
        v1 = u.mul(u).sub(new Complex(q, 0).div(u)).add(p).div(2);
        v2 = v1.add(new Complex(q, 0).div(u));
    }
    var list = [];
    t = sqrt(u.mul(u).sub(v1.mul(4)));
    list.push(u.neg().add(t).div(2).sub(a / 4));
    list.push(u.neg().sub(t).div(2).sub(a / 4));
    t = sqrt(u.mul(u).sub(v2.mul(4)));
    list.push(u.add(t).div(2).sub(a / 4));
    list.push(u.sub(t).div(2).sub(a / 4));
    return list;
}
function fun4(a, b, c, d, e) {
    var roots = fun4_roots(a, b, c, d, e);
    for (let i = 0; i < roots.length; i++)
        show(roots[i]);
}

function start(a, b, c, d, e, roots) {
    roots.sort();
    return fun5_calc(a, b, c, d, e, roots[0]) > 0 ? roots[0] - 0.5
        : fun5_calc(a, b, c, d, e, roots[roots.length - 1]) < 0 ? roots[0] + 0.5
            : (roots[1] + roots[2]) / 2;
}
const fun5_calc = (a, b, c, d, e, x) => x * (x * (x * (x * (x + a) + b) + c) + d) + e;
const fun5_derivative = (a, b, c, d, x) => x * (x * (x * (5 * x + 4 * a) + 3 * b) + 2 * c) + d;
function fun5(a, b, c, d, e, f) {
    var k = 1 / a;
    a = b * k;
    b = c * k;
    c = d * k;
    d = e * k;
    e = f * k;
    var list = fun4_roots(5, 4 * a, 3 * b, 2 * c, d);
    var roots = [];
    for (let j = 0; j < list.length; j++)
        if (equal(list[j].i, 0))
            roots.push(list[j].r);
    var t, x = 0;
    var i = 0;
    for (let j = 0; j < roots.length; j++)
        if (equal(fun5_calc(a, b, c, d, e, roots[j]), 0)) {
            x = roots[j];
            i = -1;
            break;
        }
    if (i == 0) {
        if (roots.length > 0)
            x = start(a, b, c, d, e, roots);
        if (!equal(fun5_calc(a, b, c, d, e, x), 0))
            do {
                t = x;
                x -= fun5_calc(a, b, c, d, e, x) / fun5_derivative(a, b, c, d, x);
                i++;
            } while (i < 6000 && Math.abs(x - t) > 1e-15);
        show("迭代次数:" + i);
    }
    show(x);
    fun4(1, x + a, x * (x + a) + b, x * (x * (x + a) + b) + c, x * (x * (x * (x + a) + b) + c) + d);
}

function show(text) {
    var str = (typeof text == "number") ? Number(text.toFixed(12)) : text.toString();
    document.getElementById("box").innerHTML += (str + "\\\\");
}
function calc(a) {
    if (document.getElementById(a).value == "") document.getElementById(a).value = "0";
    return Number(eval(document.getElementById(a).value));
}

function solve() {
    document.getElementById("box").innerHTML = "";
    var a = calc("num5"), b = calc("num4"), c = calc("num3"),
        d = calc("num2"), e = calc("num1"), f = calc("num0");
    a != 0 ? fun5(a, b, c, d, e, f)
        : b != 0 ? fun4(b, c, d, e, f)
            : c != 0 ? fun3(c, d, e, f)
                : d != 0 ? fun2(d, e, f)
                    : e != 0 ? show(-f / e)
                        : f != 0 ? show("无解")
                            : show("任意复数");
    box.innerHTML = '$$\\begin{array}{c}' + box.innerHTML + '\\end{array}$$';
    MathJax.typeset();
}
