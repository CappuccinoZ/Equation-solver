<template>
  <div class="w-full max-w-4xl mx-auto p-4 md:p-8">
    <div class="glass-card w-full p-6 md:p-12 rounded-3xl">
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

      <div class="flex flex-col gap-8">
        <div class="p-6 bg-base-100/80 rounded-2xl border-base-300 border">
          <div class="mb-8 flex flex-col md:flex-row justify-center items-center gap-4 md:gap-8">
            <div role="tablist" class="tabs tabs-box">
              <a role="tab" class="tab md:px-6" :class="{ 'tab-active': currentDegree === 2 }"
                @click="setDegree(2)">二次</a>
              <a role="tab" class="tab md:px-6" :class="{ 'tab-active': currentDegree === 3 }"
                @click="setDegree(3)">三次</a>
              <a role="tab" class="tab md:px-6" :class="{ 'tab-active': currentDegree === 4 }"
                @click="setDegree(4)">四次</a>
            </div>

            <button class="btn btn-sm px-4 py-2 rounded-full text-red-500 bg-white" @click="clearNums">
              <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                stroke="currentColor" class="size-6">
                <path stroke-linecap="round" stroke-linejoin="round"
                  d="M12 9.75 14.25 12m0 0 2.25 2.25M14.25 12l2.25-2.25M14.25 12 12 14.25m-2.58 4.92-6.374-6.375a1.125 1.125 0 0 1 0-1.59L9.42 4.83c.21-.211.497-.33.795-.33H19.5a2.25 2.25 0 0 1 2.25 2.25v10.5a2.25 2.25 0 0 1-2.25 2.25h-9.284c-.298 0-.585-.119-.795-.33Z" />
              </svg>
              清空
            </button>
          </div>

          <div class="flex flex-wrap items-center justify-center gap-3 text-lg md:text-xl text-gray-700">
            <div v-show="currentDegree === 4" class="flex items-center gap-1">
              <input v-model="coefficients.num4" type="number" :placeholder="placeholders.num4"
                class="input input-lg w-16 md:w-20 text-center" />
              <span class="font-semibold">x<sup>4</sup></span>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div v-show="currentDegree >= 3" class="flex items-center gap-1">
              <input v-model="coefficients.num3" type="number" :placeholder="placeholders.num3"
                class="input input-lg w-16 md:w-20 text-center" />
              <span class="font-semibold">x<sup>3</sup></span>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div class="flex items-center gap-1">
              <input v-model="coefficients.num2" type="number" :placeholder="placeholders.num2"
                class="input input-lg w-16 md:w-20 text-center" />
              <span class="font-semibold">x<sup>2</sup></span>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div class="flex items-center gap-1">
              <input v-model="coefficients.num1" type="number" :placeholder="placeholders.num1"
                class="input input-lg w-16 md:w-20 text-center" />
              <span class="font-semibold mr-2">x</span>
              <span class="text-gray-400 mx-1">+</span>
            </div>

            <div class="flex items-center gap-1">
              <input v-model="coefficients.num0" type="number" :placeholder="placeholders.num0"
                class="input input-lg w-16 md:w-20 text-center" />
              <span class="text-gray-400 ml-2">=</span>
              <span class="font-semibold mx-1">0</span>
            </div>
          </div>

          <div class="mt-8 flex justify-center">
            <button class="btn btn-lg px-8 py-3 rounded-full text-white bg-orange-500" @click="solveEquation">
              开始计算
            </button>
          </div>
        </div>

        <div v-show="showResults" class="flex flex-col p-6 bg-base-100/80 rounded-2xl border-base-300 border">
          <h3 class="text-lg font-semibold text-gray-800 mb-4">结果：</h3>
          <div class="space-y-3">
            <div v-for="(root, index) in results" :key="index"
              class="p-4 rounded-xl border transition-transform hover:scale-[1.02]"
              :class="isRealNumber(root) ? 'bg-green-50 border-green-200' : 'bg-purple-50 border-purple-200'">
              <div class="flex items-center justify-between gap-3">
                <span class="font-mono text-sm font-bold opacity-50">x<sub>{{ index + 1 }}</sub></span>
                <div class="grow hidden md:block">
                  <span class="font-mono text-lg font-medium text-gray-800">{{ root.toString() }}</span>
                </div>
                <span class="badge font-bold uppercase tracking-wider"
                  :class="isRealNumber(root) ? 'bg-green-200 text-green-800' : 'bg-purple-200 text-purple-800'">
                  {{ isRealNumber(root) ? 'Real' : 'Complex' }}
                </span>
              </div>
              <div class="grow block md:hidden">
                <span class="font-mono text-lg font-medium text-gray-800">{{ root.toString() }}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { solve } from './solver.js'
import { Complex } from './complex.js'

const currentDegree = ref(4)
const coefficients = ref({
  num4: '1',
  num3: '',
  num2: '',
  num1: '',
  num0: ''
})
const results = ref([])
const showResults = ref(false)

const placeholders = computed(() => {
  if (currentDegree.value === 4) {
    return { num4: 'a', num3: 'b', num2: 'c', num1: 'd', num0: 'e' }
  } else if (currentDegree.value === 3) {
    return { num4: '', num3: 'a', num2: 'b', num1: 'c', num0: 'd' }
  } else {
    return { num4: '', num3: '', num2: 'a', num1: 'b', num0: 'c' }
  }
})

function clearNums() {
  if (currentDegree.value === 4) {
    coefficients.value = { num4: '1', num3: '', num2: '', num1: '', num0: '' }
  } else if (currentDegree.value === 3) {
    coefficients.value = { num4: '', num3: '1', num2: '', num1: '', num0: '' }
  } else {
    coefficients.value = { num4: '', num3: '', num2: '1', num1: '', num0: '' }
  }
  showResults.value = false
}

function setDegree(degree) {
  if (currentDegree.value === degree) return
  currentDegree.value = degree
  clearNums()
}

function isRealNumber(root) {
  return Complex.isRealNumber(root)
}

function solveEquation() {
  const getVal = (key) => Number(coefficients.value[key]) || 0

  const num4 = currentDegree.value === 4 ? getVal('num4') : 0
  const num3 = currentDegree.value >= 3 ? getVal('num3') : 0
  const num2 = getVal('num2')
  const num1 = getVal('num1')
  const num0 = getVal('num0')

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
    alert('计算出错: ' + err.message)
    return
  }

  results.value = roots
  showResults.value = true
}
</script>
