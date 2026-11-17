(() => {
    const toggle = document.querySelector('[data-nav-toggle]');
    const menu = document.querySelector('[data-nav-menu]');
    if (toggle && menu) {
        toggle.addEventListener('click', () => {
            const expanded = toggle.getAttribute('aria-expanded') === 'true';
            toggle.setAttribute('aria-expanded', String(!expanded));
            menu.classList.toggle('is-open', !expanded);
        });
    }

    const emailInput = document.querySelector('[data-email-check]');
    const feedback = document.getElementById('emailFeedback');
    let debounce;
    if (emailInput && feedback) {
        emailInput.addEventListener('input', () => {
            clearTimeout(debounce);
            feedback.textContent = '';
            const email = emailInput.value.trim();
            if (!email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) return;
            debounce = setTimeout(async () => {
                try {
                    const response = await fetch(`/Account/EmailAvailable?email=${encodeURIComponent(email)}`, {
                        headers: { 'X-Requested-With': 'XMLHttpRequest' }
                    });
                    if (!response.ok) throw new Error('Request failed');
                    const data = await response.json();
                    feedback.textContent = data.message;
                    feedback.className = `field-hint ${data.available ? 'hint-success' : 'hint-error'}`;
                } catch {
                    feedback.textContent = 'Email availability could not be checked right now.';
                    feedback.className = 'field-hint hint-error';
                }
            }, 450);
        });
    }
})();
