// ============================================
// THE CHRONICLER - Premium UI Effects
// ============================================

(function() {
    'use strict';

    // Feature detection
    const isTouch = window.matchMedia('(pointer: coarse)').matches;
    const reduceMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
    const prefersReducedMotion = reduceMotion;

    // ============================================
    // THEME MANAGEMENT
    // ============================================
    
    function initTheme() {
        // Apply saved theme preference
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme === 'light') {
            document.body.classList.add('light-theme');
        } else {
            document.body.classList.remove('light-theme');
        }
    }

    // Global function to toggle theme
    window.toggleTheme = function() {
        const isLight = document.body.classList.toggle('light-theme');
        localStorage.setItem('theme', isLight ? 'light' : 'dark');
        
        // Update toggle button if exists
        updateThemeToggleUI(isLight);
    };

    function updateThemeToggleUI(isLight) {
        const toggleBtn = document.getElementById('themeToggle');
        if (toggleBtn) {
            const icon = toggleBtn.querySelector('i');
            const text = toggleBtn.querySelector('span');
            if (icon) {
                icon.className = isLight ? 'bi bi-moon-stars me-2' : 'bi bi-sun me-2';
            }
            if (text) {
                text.textContent = isLight ? 'Dark Mode' : 'Light Mode';
            }
        }
    }

    // Main initialization
    document.addEventListener('DOMContentLoaded', function() {
        initTheme();
        initParticles();
        initSpotlight();
        initReveal();
        initPageTransition();
        
        // Update toggle UI on load
        updateThemeToggleUI(document.body.classList.contains('light-theme'));
        
        if (!isTouch && !prefersReducedMotion) {
            initCustomCursor();
            initMagnetic();
            initTilt();
            document.body.classList.add('cursor-ready');
        }
    });

    // ============================================
    // BACKGROUND PARTICLES
    // ============================================
    
    function initParticles() {
        const container = document.getElementById('bgParticles');
        if (!container || prefersReducedMotion) return;

        const particleCount = window.innerWidth < 768 ? 15 : 30;
        
        for (let i = 0; i < particleCount; i++) {
            createParticle(container);
        }
    }

    function createParticle(container) {
        const particle = document.createElement('div');
        particle.className = 'bg-particle';
        
        // Random properties
        const size = Math.random() * 3 + 1;
        const left = Math.random() * 100;
        const duration = Math.random() * 20 + 15;
        const delay = Math.random() * 20;
        const opacity = Math.random() * 0.5 + 0.2;
        
        // Random color (purple, cyan, or gold)
        const colors = [
            'rgba(139, 92, 246, 0.6)',
            'rgba(79, 209, 255, 0.5)',
            'rgba(242, 198, 109, 0.4)'
        ];
        const color = colors[Math.floor(Math.random() * colors.length)];
        
        particle.style.cssText = `
            width: ${size}px;
            height: ${size}px;
            left: ${left}%;
            background: ${color};
            animation-duration: ${duration}s;
            animation-delay: -${delay}s;
            opacity: ${opacity};
        `;
        
        container.appendChild(particle);
    }

    // ============================================
    // CURSOR FOLLOWER
    // ============================================
    
    function initCustomCursor() {
        const dot = document.getElementById('cursorDot');
        const ring = document.getElementById('cursorRing');
        if (!dot || !ring) return;

        let mouseX = -100;
        let mouseY = -100;
        let ringX = -100;
        let ringY = -100;
        let isHovering = false;

        // Track mouse position
        document.addEventListener('pointermove', function(e) {
            mouseX = e.clientX;
            mouseY = e.clientY;
            
            // Update dot immediately
            dot.style.left = mouseX + 'px';
            dot.style.top = mouseY + 'px';
        }, { passive: true });

        // Smooth ring follow
        function animateRing() {
            ringX += (mouseX - ringX) * 0.15;
            ringY += (mouseY - ringY) * 0.15;
            
            ring.style.left = ringX + 'px';
            ring.style.top = ringY + 'px';
            
            requestAnimationFrame(animateRing);
        }
        animateRing();

        // Hover effects on interactive elements
        const hoverTargets = document.querySelectorAll('a, button, .btn, .card, .nav-link, .dropdown-item, .entity-card, .character-card, .campaign-card, .feature-card, .interactive-link');
        
        hoverTargets.forEach(function(el) {
            el.addEventListener('mouseenter', function() {
                isHovering = true;
                ring.style.transform = 'translate(-50%, -50%) scale(1.4)';
                ring.style.borderColor = 'rgba(242, 198, 109, 0.8)';
                ring.style.boxShadow = '0 0 40px rgba(242, 198, 109, 0.3)';
                dot.style.transform = 'translate(-50%, -50%) scale(1.5)';
            });
            
            el.addEventListener('mouseleave', function() {
                isHovering = false;
                ring.style.transform = 'translate(-50%, -50%) scale(1)';
                ring.style.borderColor = 'rgba(255, 255, 255, 0.35)';
                ring.style.boxShadow = '0 0 30px rgba(79, 209, 255, 0.18)';
                dot.style.transform = 'translate(-50%, -50%) scale(1)';
            });
        });

        // Hide cursor when leaving window
        document.addEventListener('mouseleave', function() {
            dot.style.opacity = '0';
            ring.style.opacity = '0';
        });

        document.addEventListener('mouseenter', function() {
            dot.style.opacity = '1';
            ring.style.opacity = '1';
        });

        // Click effect
        document.addEventListener('mousedown', function() {
            ring.style.transform = 'translate(-50%, -50%) scale(0.9)';
        });

        document.addEventListener('mouseup', function() {
            ring.style.transform = isHovering 
                ? 'translate(-50%, -50%) scale(1.4)' 
                : 'translate(-50%, -50%) scale(1)';
        });
    }

    // ============================================
    // MOUSE SPOTLIGHT
    // ============================================
    
    function initSpotlight() {
        if (prefersReducedMotion) return;

        let ticking = false;
        
        document.addEventListener('pointermove', function(e) {
            if (!ticking) {
                requestAnimationFrame(function() {
                    document.documentElement.style.setProperty('--spot-x', e.clientX + 'px');
                    document.documentElement.style.setProperty('--spot-y', e.clientY + 'px');
                    ticking = false;
                });
                ticking = true;
            }
        }, { passive: true });
    }

    // ============================================
    // MAGNETIC HOVER EFFECT
    // ============================================
    
    function initMagnetic() {
        if (prefersReducedMotion) return;

        const magneticElements = document.querySelectorAll('.btn-chronicle, .btn-outline-chronicle, .nav-link, .feature-card-icon');
        
        magneticElements.forEach(function(el) {
            el.addEventListener('mousemove', function(e) {
                const rect = el.getBoundingClientRect();
                const centerX = rect.left + rect.width / 2;
                const centerY = rect.top + rect.height / 2;
                
                const deltaX = (e.clientX - centerX) * 0.15;
                const deltaY = (e.clientY - centerY) * 0.15;
                
                el.style.transform = 'translate(' + deltaX + 'px, ' + deltaY + 'px)';
            });

            el.addEventListener('mouseleave', function() {
                el.style.transform = '';
            });
        });
    }

    // ============================================
    // TILT EFFECT FOR CARDS
    // ============================================
    
    function initTilt() {
        if (prefersReducedMotion) return;

        const tiltElements = document.querySelectorAll('.card, .stat-card, .hero-section, .entity-card, .character-card, .campaign-card, .feature-card');
        
        tiltElements.forEach(function(el) {
            el.classList.add('tilt-card');

            el.addEventListener('mousemove', function(e) {
                const rect = el.getBoundingClientRect();
                const percentX = (e.clientX - rect.left) / rect.width;
                const percentY = (e.clientY - rect.top) / rect.height;
                
                const rotateX = (0.5 - percentY) * 8;
                const rotateY = (percentX - 0.5) * 10;
                
                el.style.transform = 'perspective(1000px) rotateX(' + rotateX + 'deg) rotateY(' + rotateY + 'deg) translateY(-4px)';
            });

            el.addEventListener('mouseleave', function() {
                el.style.transform = '';
            });
        });
    }

    // ============================================
    // REVEAL ON SCROLL
    // ============================================
    
    function initReveal() {
        const revealElements = document.querySelectorAll(
            '.card, .hero-section, .page-header, .stat-card, .timeline-item, .entity-card, .character-card, .campaign-card, .feature-card, .campaign-launcher, .activity-item'
        );
        
        if (!revealElements.length) return;

        revealElements.forEach(function(el) {
            el.classList.add('page-reveal');
        });

        if (prefersReducedMotion) {
            revealElements.forEach(function(el) {
                el.classList.add('is-visible');
            });
            return;
        }

        const observer = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if (entry.isIntersecting) {
                    // Add staggered delay for siblings
                    const parent = entry.target.parentElement;
                    if (parent) {
                        const siblings = parent.querySelectorAll('.page-reveal');
                        const index = Array.from(siblings).indexOf(entry.target);
                        entry.target.style.transitionDelay = (index * 0.05) + 's';
                    }
                    
                    entry.target.classList.add('is-visible');
                    observer.unobserve(entry.target);
                }
            });
        }, { 
            threshold: 0.1,
            rootMargin: '0px 0px -50px 0px'
        });

        revealElements.forEach(function(el) {
            observer.observe(el);
        });
    }

    // ============================================
    // PAGE TRANSITION
    // ============================================
    
    function initPageTransition() {
        var overlay = document.getElementById('pageTransition');
        if (!overlay) return;

        // Handle clicks on internal links
        document.addEventListener('click', function(e) {
            // Find closest link or button
            var target = e.target.closest('a.interactive-link, .btn-chronicle, .btn-outline-chronicle, .nav-link, a[href]');
            if (!target) return;
            
            // Skip if modifier keys pressed
            if (e.ctrlKey || e.metaKey || e.shiftKey || e.altKey) return;
            
            // Skip external links
            var href = target.getAttribute('href');
            if (!href) return;
            if (href.startsWith('#') || href.startsWith('javascript:') || href.startsWith('mailto:') || href.startsWith('tel:')) return;
            if (target.hasAttribute('download')) return;
            if (target.target === '_blank') return;
            
            // Check if same origin
            try {
                var url = new URL(href, window.location.origin);
                if (url.origin !== window.location.origin) return;
                
                // Skip if same page (only hash difference)
                if (url.pathname === window.location.pathname && !url.search) return;
            } catch (err) {
                return;
            }

            // Trigger transition
            e.preventDefault();
            overlay.classList.add('is-active');
            
            setTimeout(function() {
                window.location.href = href;
            }, 350);
        });

        // Handle back/forward navigation
        window.addEventListener('pageshow', function(e) {
            if (e.persisted) {
                overlay.classList.remove('is-active');
            }
        });
    }

    // ============================================
    // SMOOTH SCROLL FOR ANCHOR LINKS
    // ============================================
    
    document.addEventListener('click', function(e) {
        var anchor = e.target.closest('a[href^="#"]');
        if (!anchor) return;
        
        var targetId = anchor.getAttribute('href').slice(1);
        var targetElement = document.getElementById(targetId);
        
        if (targetElement) {
            e.preventDefault();
            targetElement.scrollIntoView({
                behavior: prefersReducedMotion ? 'auto' : 'smooth',
                block: 'start'
            });
        }
    });

    // ============================================
    // IMAGE LAZY LOADING
    // ============================================
    
    if ('IntersectionObserver' in window) {
        const imageObserver = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    if (img.dataset.src) {
                        img.src = img.dataset.src;
                        img.removeAttribute('data-src');
                    }
                    imageObserver.unobserve(img);
                }
            });
        }, { rootMargin: '100px' });

        document.querySelectorAll('img[data-src]').forEach(function(img) {
            imageObserver.observe(img);
        });
    }

    // ============================================
    // FORM ENHANCEMENT
    // ============================================
    
    // Add floating label behavior
    document.querySelectorAll('.form-floating input, .form-floating textarea, .form-floating select').forEach(function(input) {
        input.addEventListener('focus', function() {
            this.parentElement.classList.add('is-focused');
        });
        input.addEventListener('blur', function() {
            this.parentElement.classList.remove('is-focused');
        });
    });

    // ============================================
    // KEYBOARD NAVIGATION
    // ============================================
    
    document.addEventListener('keydown', function(e) {
        // ESC to close modals
        if (e.key === 'Escape') {
            var openModals = document.querySelectorAll('.modal.show, .image-modal.open');
            openModals.forEach(function(modal) {
                if (modal.classList.contains('image-modal')) {
                    modal.classList.remove('open');
                    document.body.style.overflow = '';
                }
            });
        }
    });

    // ============================================
    // TOOLTIP INITIALIZATION (Bootstrap)
    // ============================================
    
    if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
        document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function(el) {
            new bootstrap.Tooltip(el);
        });
    }

    // ============================================
    // PERFORMANCE: CLEANUP ON PAGE UNLOAD
    // ============================================
    
    window.addEventListener('beforeunload', function() {
        // Clean up observers and event listeners would happen automatically
        // This is mainly for any cleanup that might be needed
    });

})();
