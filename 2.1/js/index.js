#!/usr/bin/env node

const fs = require('fs');

const lines = fs.readFileSync('../input', 'utf8').split('\n').filter(Boolean);

const filteredLines = lines.filter(line => filterLine(line));

const gameIdSum = filteredLines.reduce((acc, line) => {
  const gameId = line.split(': ')[0].slice(5);

  return acc + parseInt(gameId);
}, 0);

console.log(gameIdSum);

function filterLine(line, red = 12, green = 13, blue = 14) {
  const cubeSets = line.split(': ')[1].split('; ');

  return cubeSets.every(cubeSet => {
    const cubes = cubeSet.split(', ').reduce((acc, cube) => {
      const [count, color] = cube.split(' ');
      acc[color] = parseInt(count);

      return acc;
    }, { red: 0, green: 0, blue: 0 });

    return cubes['red'] <= red && cubes['green'] <= green && cubes['blue'] <= blue;
  });
}