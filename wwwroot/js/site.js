const canvas = document.getElementById("space");
const ctx = canvas.getContext("2d");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;
drawStars();

const mobileBtn = document.querySelector('.mobile-nav-btn');
const mobileLinks = document.querySelector('.mobile-links');
const mobileOverlay = document.querySelector('.mobile-overlay');

window.addEventListener('resize', () => {
    if (window.innerWidth > 768) {
        mobileLinks.classList.remove('active');
        mobileOverlay.classList.remove('active');
    }

    redrawStars();
});

window.addEventListener('load', () => {
    const carousel = document.querySelector('.carousel');
    const carouselGroup = document.querySelector('.carousel-group');

    if (carousel) {
        carousel.innerHTML += carousel.innerHTML;
    }
});

mobileBtn.addEventListener('click', () => {
    mobileLinks.classList.toggle('active');
    mobileOverlay.classList.toggle('active');
});

mobileOverlay.addEventListener('click', () => {
    mobileLinks.classList.remove('active');
    mobileOverlay.classList.remove('active');
})

function drawStars() {
    for (let i = 0; i < 80; i++) {
        const x = Math.random() * canvas.width;
        const y = Math.random() * canvas.height;

        ctx.fillStyle = "white";
        ctx.beginPath();
        ctx.arc(x, y, Math.random() * 2, 0, Math.PI * 2);
        ctx.fill();
    }
}

function redrawStars() {
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    drawStars();
}

function setTheme(theme) {
    document.documentElement.dataset.theme = theme;
    document.cookie =
        `theme=${theme};path=/;max-age=31536000;SameSite=Lax`;
}