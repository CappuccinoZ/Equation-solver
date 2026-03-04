import { solve } from './solver.js'

const btn = document.querySelector('.btn')
btn.addEventListener('click', function(e) {
  e.preventDefault()
  const num4 = Number(document.querySelector('#num4').value)
  const num3 = Number(document.querySelector('#num3').value)
  const num2 = Number(document.querySelector('#num2').value)
  const num1 = Number(document.querySelector('#num1').value)
  const num0 = Number(document.querySelector('#num0').value)

  let result
  if (num4 !== 0) {
    result = solve(num4, num3, num2, num1, num0)
  } else if (num3 !== 0) {
    result = solve(num3, num2, num1, num0)
  } else if (num2 !== 0) {
    result = solve(num2, num1, num0)
  } else {
    console.log('参数过少')
  }

  const str = result.join('\n')
  document.querySelector('#result').value = str
})