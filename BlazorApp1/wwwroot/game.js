window.startRace = function () {
    const canvas = document.getElementById('raceCanvas');
    const ctx = canvas.getContext('2d');

    let car = { x: 175, y: 500, width: 50, height: 100 };
    let obstacles = [];
    let gameOver = false;

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

        // Move obstacles down
        obstacles.forEach(obs => obs.y += 5);
        obstacles = obstacles.filter(obs => obs.y < canvas.height);

        // Check for collisions
        for (let obs of obstacles) {
            if (obs.y + obs.height > car.y &&
                obs.y < car.y + car.height &&
                obs.x < car.x + car.width &&
                obs.x + obs.width > car.x) {
                gameOver = true;
                alert("Game Over!");
                return;
            }
        }

        // Draw game objects
        drawCar();
        drawObstacles();

        requestAnimationFrame(updateGame);
    }

    function spawnObstacle() {
        let x = Math.random() * (canvas.width - 50);
        obstacles.push({ x: x, y: 0, width: 50, height: 50 });
        if (!gameOver) setTimeout(spawnObstacle, 2000);
    }

    function moveCar(event) {
        if (event.key === "ArrowLeft" && car.x > 0) {
            car.x -= 25;
        } else if (event.key === "ArrowRight" && car.x < canvas.width - car.width) {
            car.x += 25;
        }
    }

    document.addEventListener('keydown', moveCar);
    spawnObstacle();
    updateGame();
};
