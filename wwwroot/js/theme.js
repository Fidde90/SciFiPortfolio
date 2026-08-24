
function getTheme() {
    const activeTheme = document.cookie
        .split("; ")
        .find(c => c.startsWith("theme="))
        ?.split("=")[1] ?? 'sci-fi';

    return activeTheme;
}

function loadTheme() {
    const activeTheme = getTheme();
    document.documentElement.dataset.theme = activeTheme;
}

function setTheme(theme) {
    document.documentElement.dataset.theme = theme;
    document.cookie =
        `theme=${theme};path=/;max-age=31536000;SameSite=Lax;Secure`;
}

function toggleWalls() {
    const walls = document.querySelector('.walls');
    const hidden = !walls.hidden;
    walls.hidden = hidden;
    document.cookie = `WallsHidden=${hidden}; path=/; max-age=31536000`;
}

loadTheme();
