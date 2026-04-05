<template>
  <div class="w-full max-w-4xl mx-auto md:p-4">
    <div class="glass-card w-full p-6 md:p-12 rounded-3xl">
      <!-- 卡片标题 -->
      <div class="mb-10 text-center">
        <h1 class="mb-2 text-3xl md:text-4xl font-semibold text-orange-500">一元四次方程计算器</h1>
        <div class="flex justify-center items-center">
          <a href="https://github.com/CappuccinoZ/equation-solver" target="_blank"
            class="px-2 py-1 flex justify-center items-center group">
            <svg viewBox="0 0 24 24" class="group-hover:text-blue-400 w-6" width="24" height="24" fill="currentColor">
              <path
                d="M10.226 17.284c-2.965-.36-5.054-2.493-5.054-5.256 0-1.123.404-2.336 1.078-3.144-.292-.741-.247-2.314.09-2.965.898-.112 2.111.36 2.83 1.01.853-.269 1.752-.404 2.853-.404 1.1 0 1.999.135 2.807.382.696-.629 1.932-1.1 2.83-.988.315.606.36 2.179.067 2.942.72.854 1.101 2 1.101 3.167 0 2.763-2.089 4.852-5.098 5.234.763.494 1.28 1.572 1.28 2.807v2.336c0 .674.561 1.056 1.235.786 4.066-1.55 7.255-5.615 7.255-10.646C23.5 6.188 18.334 1 11.978 1 5.62 1 .5 6.188.5 12.545c0 4.986 3.167 9.12 7.435 10.669.606.225 1.19-.18 1.19-.786V20.63a2.9 2.9 0 0 1-1.078.224c-1.483 0-2.359-.808-2.987-2.313-.247-.607-.517-.966-1.034-1.033-.27-.023-.359-.135-.359-.27 0-.27.45-.471.898-.471.652 0 1.213.404 1.797 1.235.45.651.921.943 1.483.943.561 0 .92-.202 1.437-.719.382-.381.674-.718.944-.943">
              </path>
            </svg>

            <span class="ml-2 text-gray-500/80 group-hover:text-blue-400">GitHub</span>
          </a>
        </div>

      </div>

      <!-- 卡片主体 -->
      <div class="flex flex-col gap-8">
        <!-- 输入区 -->
        <div class="p-6 bg-base-100/80 rounded-2xl border-base-300 border">
          <div class="mb-8 flex flex-col md:flex-row justify-center items-center gap-4 md:gap-8">
            <!-- 切换方程次数 -->
            <div role="tablist" class="tabs tabs-box">
              <a role="tab" class="tab md:px-6" data-degree="2">二次</a>
              <a role="tab" class="tab md:px-6" data-degree="3">三次</a>
              <a role="tab" class="tab tab-active md:px-6" data-degree="4">四次</a>
            </div>

            <button id="clearBtn" class="btn btn-sm px-4 py-2 rounded-full text-red-500 bg-white">
              <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                stroke="currentColor" class="size-6">
                <path stroke-linecap="round" stroke-linejoin="round"
                  d="M12 9.75 14.25 12m0 0 2.25 2.25M14.25 12l2.25-2.25M14.25 12 12 14.25m-2.58 4.92-6.374-6.375a1.125 1.125 0 0 1 0-1.59L9.42 4.83c.21-.211.497-.33.795-.33H19.5a2.25 2.25 0 0 1 2.25 2.25v10.5a2.25 2.25 0 0 1-2.25 2.25h-9.284c-.298 0-.585-.119-.795-.33Z" />
              </svg>
              清空
            </button>
          </div>

          <!-- 输入系数 -->
          <div class="flex flex-wrap items-center justify-center gap-3 text-lg md:text-xl text-gray-700">
            <div id="term-4" class="flex items-center gap-1 ">
              <input id="num4" type="number" placeholder="a" class="input input-lg w-16 md:w-20 text-center"
                value="1" />
              <label for="num4" class="font-semibold">x<sup>4</sup></label>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div id="term-3" class="flex items-center gap-1">
              <input id="num3" type="number" placeholder="b" class="input input-lg w-16 md:w-20 text-center" />
              <label for="num3" class="font-semibold">x<sup>3</sup></label>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div id="term-2" class="flex items-center gap-1">
              <input id="num2" type="number" placeholder="c" class="input input-lg w-16 md:w-20 text-center" />
              <label for="num2" class="font-semibold">x<sup>2</sup></label>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div id="term-1" class="flex items-center gap-1">
              <input id="num1" type="number" placeholder="d" class="input input-lg w-16 md:w-20 text-center" />
              <label for="num1" class="font-semibold mr-2">x</label>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div id="term-0" class="flex items-center gap-1">
              <input id="num0" type="number" placeholder="e" class="input input-lg w-16 md:w-20 text-center" />
              <span class="text-gray-400 ml-2">=</span>
              <span class="font-semibold mx-1">0</span>
            </div>
          </div>

          <!-- 计算按钮 -->
          <div class="mt-8 flex justify-center">
            <button class="btn btn-lg px-8 py-3 rounded-full text-white bg-orange-500" id="solve-btn">开始计算</button>
          </div>
        </div>

        <!-- 结果区 -->
        <div id="output-section" class="hidden">
          <div class="flex flex-col p-6 bg-base-100/80 rounded-2xl border-base-300 border">
            <h3 class="text-lg font-semibold text-gray-800 mb-4">结果：</h3>

            <div id="result-container" class="space-y-3">
              <!-- 这里显示结果 -->
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { solve } from './solver.js'
import { Complex } from './complex.js'
import { onMounted } from 'vue'

onMounted(() => {
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
      input4.value = '1'
      input3.value = ''
      input2.value = ''
      input1.value = ''
      input0.value = ''

    } else if (degree === 3) {
      input4.value = ''
      input3.value = '1'
      input2.value = ''
      input1.value = ''
      input0.value = ''

    } else if (degree === 2) {
      input4.value = ''
      input3.value = ''
      input2.value = '1'
      input1.value = ''
      input0.value = ''
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
      div.className = `p-4 rounded-xl border ${isReal ? 'bg-green-50 border-green-200' : 'bg-purple-50 border-purple-200'} transition-transform hover:scale-[1.02]`

      div.innerHTML = `
      <div class="flex items-center justify-between gap-3">
        <span class="font-mono text-sm font-bold opacity-50">x<sub>${index + 1}</sub></span>
        <div class="grow hidden md:block">
          <span class="font-mono text-lg font-medium text-gray-800">${valStr}</span>
        </div>        
        <span class="badge font-bold uppercase tracking-wider ${isReal ? 'bg-green-200 text-green-800' : 'bg-purple-200 text-purple-800'}">
          ${isReal ? 'Real' : 'Complex'}
        </span>
      </div>
      <div class="grow block md:hidden">
        <span class="font-mono text-lg font-medium text-gray-800">${valStr}</span>
      </div>
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
})
</script>