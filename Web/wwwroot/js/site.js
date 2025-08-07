// Site-wide JavaScript for the Sample Web Application

// Add active class to current nav item
const currentLocation = location.href;
const menuItems = document.querySelectorAll('.nav-link');
const menuLength = menuItems.length;

for (let i = 0; i < menuLength; i++) {
    if (menuItems[i].href === currentLocation) {
        menuItems[i].classList.add('active');
        menuItems[i].setAttribute('aria-current', 'page');
    }
}

// Add fade-in effect to pages
document.addEventListener('DOMContentLoaded', function() {
    document.body.classList.add('fade-in');
});

// Add smooth scrolling to all links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});
