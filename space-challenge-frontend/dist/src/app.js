"use strict";
// app.ts
window.onload = () => {
    const canvas = document.getElementById('gameCanvas');
    const ctx = canvas.getContext('2d');
    if (!ctx) {
        console.error('Failed to get canvas context');
        return;
    }
    const map = [
        ['F', '.', '.', '#', '.', '.', '.', '.', '.', 'P'],
        ['#', '#', '.', '#', '.', '#', '.', '.', '.', '.'],
        ['#', '.', '.', '.', '.', '#', '#', '#', '.', '.'],
        ['#', '.', '#', '.', '.', '#', '.', '.', '.', '.'],
    ];
    const cellSize = 40;
    function drawMap() {
        if (!ctx) {
            console.error('Failed to get canvas context');
            return;
        }
        map.forEach((row, rowIndex) => {
            row.forEach((cell, colIndex) => {
                switch (cell) {
                    case 'F':
                        ctx.fillStyle = 'blue';
                        break;
                    case 'P':
                        ctx.fillStyle = 'green';
                        break;
                    case '#':
                        ctx.fillStyle = 'gray';
                        break;
                    default:
                        ctx.fillStyle = 'white';
                }
                ctx.fillRect(colIndex * cellSize, rowIndex * cellSize, cellSize, cellSize);
                ctx.strokeRect(colIndex * cellSize, rowIndex * cellSize, cellSize, cellSize);
            });
        });
    }
    drawMap();
};
