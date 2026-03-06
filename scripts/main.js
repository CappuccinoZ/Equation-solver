import { solve } from './solver.js'
import { Complex } from './complex.js'

const degreeTablist = document.querySelector('.tabs')
const clearBtn = document.querySelector('#clearBtn')
const solveBtn = document.querySelector('#solve-btn')
const resultContainer = document.querySelector('#result-container')
const outputSection = document.querySelector('#output-section')

// 输入框
const term4 = document.querySelector('#term-4')
const term3 = document.querySelector('#term-3')
const input4 = document.querySelector('#num4')
const input3 = document.querySelector('#num3')
const input2 = document.querySelector('#num2')
const input1 = document.querySelector('#num1')
const input0 = document.querySelector('#num0')

let currentDegree = 4

// 清除系数
function clearNums(degree) {
  if (degree === 4) {
    input4.value = 1
    input3.value = 0
    input2.value = 0
    input1.value = 0
    input0.value = 0

  } else if (degree === 3) {
    input4.value = 0
    input3.value = 1
    input2.value = 0
    input1.value = 0
    input0.value = 0

  } else if (degree === 2) {
    input4.value = 0
    input3.value = 0
    input2.value = 1
    input1.value = 0
    input0.value = 0
  }
}

clearBtn.addEventListener('click', () => {
  clearNums(currentDegree)
})

// 切换方程次数
function setDegree(degree) {
  currentDegree = degree

  // 更新tab状态
  const beforeTab = degreeTablist.querySelector('.tab-active')
  if (beforeTab.dataset.degree == degree) return

  const afterTab = degreeTablist.querySelector(`[data-degree="${degree}"]`)
  beforeTab.classList.remove('tab-active')
  afterTab.classList.add('tab-active')

  // 更新输入框
  if (degree === 4) {
    term4.classList.remove('hidden')
    term3.classList.remove('hidden')

    input4.placeholder = 'a'
    input3.placeholder = 'b'
    input2.placeholder = 'c'
    input1.placeholder = 'd'
    input0.placeholder = 'e'

  } else if (degree === 3) {
    term4.classList.add('hidden')
    term3.classList.remove('hidden')

    input3.placeholder = 'a'
    input2.placeholder = 'b'
    input1.placeholder = 'c'
    input0.placeholder = 'd'

  } else if (degree === 2) {
    term4.classList.add('hidden')
    term3.classList.add('hidden')

    input2.placeholder = 'a'
    input1.placeholder = 'b'
    input0.placeholder = 'c'
  }

  // 重置系数
  clearNums(degree)

  // 隐藏结果区
  outputSection.classList.add('hidden')
}

degreeTablist.addEventListener('click', (e) => {
  if (e.target.tagName === 'A') {
    const degree = Number(e.target.dataset.degree)
    setDegree(degree)
  }
})

// 显示结果
function displayResults(roots) {
  resultContainer.innerHTML = ''

  roots.forEach((root, index) => {
    const isReal = Complex.isRealNumber(root)
    const valStr = root.toString()

    const div = document.createElement('div')
    div.className = `p-4 rounded-xl border ${isReal ? 'bg-green-50 border-green-200' : 'bg-purple-50 border-purple-200'} flex justify-between items-center transition-transform hover:scale-[1.02]`

    div.innerHTML = `
      <div class="flex items-center gap-3">
        <span class="font-mono text-sm font-bold opacity-50">x<sub>${index + 1}</sub></span>
        <span class="font-mono text-lg font-medium text-gray-800">${valStr}</span>
      </div>
      <span class="badge font-bold uppercase tracking-wider ${isReal ? 'bg-green-200 text-green-800' : 'bg-purple-200 text-purple-800'}">
        ${isReal ? 'Real' : 'Complex'}
      </span>
    `
    resultContainer.appendChild(div)
  })
}

function solveFn() {
  const getVal = (id) => Number(document.getElementById(id).value)

  let num4 = currentDegree === 4 ? getVal('num4') : 0
  let num3 = currentDegree >= 3 ? getVal('num3') : 0
  let num2 = getVal('num2')
  let num1 = getVal('num1')
  let num0 = getVal('num0')

  // 解方程
  let roots = []
  try {
    if (num4 !== 0) {
      roots = solve(num4, num3, num2, num1, num0)
    } else if (num3 !== 0) {
      roots = solve(num3, num2, num1, num0)
    } else if (num2 !== 0) {
      roots = solve(num2, num1, num0)
    } else {
      throw new Error('不支持的参数')
    }
  } catch (err) {
    console.error(err)
    alert("计算出错: " + err.message)
    outputSection.classList.add('hidden')
    return
  }

  // 显示结果
  displayResults(roots)
  outputSection.classList.remove('hidden')
}

solveBtn.addEventListener('click', function (e) {
  e.preventDefault()
  solveFn()
})