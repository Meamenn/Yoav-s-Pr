// game.js
window.startRace = function () {
    console.log("startRace function called");

    const canvas = document.getElementById('gameCanvas');
    console.log("canvas element:", canvas);

    if (!canvas) {
        console.error("Canvas element not found!");
        return;
    }

    const ctx = canvas.getContext('2d');
    console.log("canvas context:", ctx);

    if (!ctx) {
        console.error("Could not get 2D context!");
        return;
    }

    let car;
    let obstacles;
    let gameOver;
    let movingLeft;
    let movingRight;
    const carSpeed = 5;

    function initGame() {
        car = { x: 175, y: 500, width: 50, height: 100 };
        obstacles = [];
        gameOver = false;
        movingLeft = false;
        movingRight = false;
    }

    function drawCar() {
        ctx.fillStyle = 'blue';
        ctx.fillRect(car.x, car.y, car.width, car.height);
    }

    function drawObstacles() {
        ctx.fillStyle = 'red';
        obstacles.forEach(obs => {
            ctx.fillRect(obs.x, obs.y, obs.width, obs.height);
        });
    }

    function updateGame() {
        if (gameOver) return;

        ctx.clearRect(0, 0, canvas.width, canvas.height);

        if (movingLeft && car.x > 0) {
            car.x -= carSpeed;
        }
        if (movingRight && car.x < canvas.width - car.width) {
            car.x += carSpeed;
        }

        obstacles.forEach(obs => obs.y += 5);
        obstacles = obstacles.filter(obs => obs.y < canvas.height);

        for (let obs of obstacles) {
            if (obs.y + obs.height > car.y &&
                obs.y < car.y + car.height &&
                obs.x < car.x + car.width &&
                obs.x + obs.width > car.x) {
                gameOver = true;
                alert("Game Over! Press any arrow key to restart.");
                return;
            }
        }

        drawCar();
        drawObstacles();

        requestAnimationFrame(updateGame);
    }

    function spawnObstacle() {
        let x = Math.floor(Math.random() * (canvas.width - 50));
        obstacles.push({ x: x, y: 0, width: 50, height: 50 });
        if (!gameOver) setTimeout(spawnObstacle, 2000);
    }

    function handleKeyDown(event) {
        if (gameOver && (event.key === "ArrowLeft" || event.key === "ArrowRight")) {
            console.log("Restarting game...");
            initGame();
            spawnObstacle();
            updateGame();
        } else {
            if (event.key === "ArrowLeft") movingLeft = true;
            if (event.key === "ArrowRight") movingRight = true;
        }
    }

    function handleKeyUp(event) {
        if (event.key === "ArrowLeft") movingLeft = false;
        if (event.key === "ArrowRight") movingRight = false;
    }

    document.removeEventListener('keydown', handleKeyDown);
    document.removeEventListener('keyup', handleKeyUp);
    document.addEventListener('keydown', handleKeyDown);
    document.addEventListener('keyup', handleKeyUp);

    initGame();
    spawnObstacle();
    updateGame();
};

document.addEventListener('DOMContentLoaded', window.startRace);
