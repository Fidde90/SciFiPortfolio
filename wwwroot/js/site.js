
const canvas = document.getElementById("space");
const ctx = canvas.getContext("2d");
const mobileBtn = document.querySelector('.mobile-nav-btn');
const mobileLinks = document.querySelector('.mobile-links');
const mobileOverlay = document.querySelector('.mobile-overlay');
const cookieSettings = document.getElementById('cookie-settings-btn');
const banner = document.getElementById("cookie-banner");

renderSpaceCanvas();
drawStars();
renderCarousel();
renderCookiebannerIfNoCookie();

window.addEventListener('resize', () => {
    if (window.innerWidth > 768) {
        mobileLinks.classList.remove('active');
        mobileOverlay.classList.remove('active');
    }

    redrawStars();
    setThemeSelectInput();
});

function renderSpaceCanvas(){
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
}

function renderCarousel() {
    const carousel = document.querySelector('.carousel');
    const carouselGroup = document.querySelector('.carousel-group');

    if (carousel) {
        carousel.innerHTML += carousel.innerHTML;
    }
}

function renderCookiebannerIfNoCookie() {
    if (!document.cookie.includes(`${banner.dataset.consentCookie}=`)) {
        banner.hidden = false;
    }
}

function closeCookieBanner() {
    banner.hidden = true;
}

cookieSettings.addEventListener('click', () => {
    const banner = document.getElementById('cookie-banner');
    banner.hidden = false;
})

mobileBtn.addEventListener('click', () => {
    mobileLinks.classList.toggle('active');
    mobileOverlay.classList.toggle('active');
});

mobileOverlay.addEventListener('click', () => {
    mobileLinks.classList.remove('active');
    mobileOverlay.classList.remove('active');
})

function drawStars() {
    for (let i = 0; i < 100; i++) {
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

function setThemeSelectInput() {
    document.querySelectorAll(".theme-select").forEach(themeSelect => {
        themeSelect.value = getTheme();
    })
}