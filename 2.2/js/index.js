#!/usr/bin/env node

const fs = require('fs');

const lines = fs.readFileSync('../../2.input', 'utf8').split('\n').filter(Boolean);

const sumPower = lines.map(calculateGamePower).reduce((acc, power) => acc + power, 0);
console.log(sumPower);

function calculateGamePower(line) {
  const { red, green, blue } =
    line
      .split(': ')[1].split('; ')
      .reduce(getMinColorsReducer, { red: 0, green: 0, blue: 0 });

  return red * green * blue;
}

function getMinColorsReducer(acc, cubeSet) {
  for (let cube of cubeSet.split(', ')) {
    const [count, color] = cube.split(' ');

    acc[color] = Math.max(acc[color], parseInt(count));
  }

  return acc;
}