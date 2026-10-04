document.addEventListener('DOMContentLoaded', function () {

    // Smooth-scroll for in-page anchor links
    document.querySelectorAll('a[href^="#"]').forEach(function (link) {
        link.addEventListener('click', function (e) {
            var targetId = this.getAttribute('href');
            var target = targetId.length > 1 ? document.querySelector(targetId) : null;
            if (target) {
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth' });
                var nav = document.querySelector('.site-nav nav');
                if (nav && nav.classList.contains('is-open')) nav.classList.remove('is-open');
            }
        });
    });

    // Mobile nav toggle
    var toggle = document.querySelector('.nav-toggle');
    var nav = document.querySelector('.site-nav nav');
    if (toggle && nav) {
        toggle.addEventListener('click', function () {
            nav.classList.toggle('is-open');
        });
    }

    // Scroll-reveal animations
    var revealEls = document.querySelectorAll('.reveal');
    if ('IntersectionObserver' in window && revealEls.length) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('in-view');
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.15, rootMargin: '0px 0px -40px 0px' });

        revealEls.forEach(function (el) { observer.observe(el); });
    } else {
        revealEls.forEach(function (el) { el.classList.add('in-view'); });
    }

    // Animate skill proficiency bars from 0 to their target width once visible
    var bars = document.querySelectorAll('.bar-fill[data-target]');
    if ('IntersectionObserver' in window && bars.length) {
        var barObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    var el = entry.target;
                    el.style.width = el.getAttribute('data-target') + '%';
                    barObserver.unobserve(el);
                }
            });
        }, { threshold: 0.4 });
        bars.forEach(function (el) {
            el.style.width = '0%';
            barObserver.observe(el);
        });
    }

    // Highlight the active nav link based on scroll position
    var sections = document.querySelectorAll('section[id]');
    var navLinks = document.querySelectorAll('.site-nav nav a[href^="#"]');
    if (sections.length && navLinks.length && 'IntersectionObserver' in window) {
        var navObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    navLinks.forEach(function (l) { l.classList.remove('active-link'); });
                    var match = document.querySelector('.site-nav nav a[href="#' + entry.target.id + '"]');
                    if (match) match.classList.add('active-link');
                }
            });
        }, { threshold: 0.4 });
        sections.forEach(function (s) { navObserver.observe(s); });
    }
});
