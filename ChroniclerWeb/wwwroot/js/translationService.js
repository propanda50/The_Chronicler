const TranslationService = {
    originalContent: new Map(),
    currentLang: localStorage.getItem('preferredLanguage') || 'en',

    init() {
        this.currentLang = localStorage.getItem('preferredLanguage') || 'en';

        window.addEventListener('storage', (e) => {
            if (e.key === 'preferredLanguage') {
                this.currentLang = e.newValue || 'en';
                this.translateAllContent();
                this.translatePageContent(this.currentLang);
            }
        });

        window.addEventListener('languageChanged', (e) => {
            this.currentLang = e.detail.language || 'en';
            this.translateAllContent();
            this.translatePageContent(this.currentLang);
        });

        this.observeDynamicContent();
    },

    storeOriginal(key, content, originalLang) {
        this.originalContent.set(key, {
            content,
            originalLang: originalLang || 'en'
        });
    },

    async getContent(key) {
        const stored = this.originalContent.get(key);
        if (!stored) return null;

        if (stored.originalLang === this.currentLang) {
            return stored.content;
        }

        return await this.translateText(stored.content, stored.originalLang, this.currentLang);
    },

    async translateAllContent() {
        const elements = document.querySelectorAll('[data-translate-key]');

        for (const el of elements) {
            const key = el.dataset.translateKey;
            const stored = this.originalContent.get(key);
            if (!stored) continue;

            let output = stored.content;
            if (stored.originalLang !== this.currentLang) {
                output = await this.translateText(stored.content, stored.originalLang, this.currentLang);
            }

            if (el.tagName === 'INPUT' || el.tagName === 'TEXTAREA') {
                el.value = output;
            } else {
                el.textContent = output;
            }
        }

        window.dispatchEvent(new CustomEvent('contentTranslated', {
            detail: { language: this.currentLang }
        }));
    },

    async translateText(text, fromLang, toLang) {
        if (fromLang === toLang) return text;
        if (!text || !text.trim()) return text;

        const prompt = `Translate the following text from ${this.getLanguageName(fromLang)} to ${this.getLanguageName(toLang)}.
Only output the translated text, nothing else.

Text to translate:
${text}`;

        try {
            const result = await callOllama(prompt);
            return result.trim();
        } catch (error) {
            console.error('Translation failed:', error);
            return text;
        }
    },

    getLanguageName(code) {
        const languages = {
            'en': 'English', 'fr': 'French', 'nl': 'Dutch', 'de': 'German',
            'es': 'Spanish', 'it': 'Italian', 'pt': 'Portuguese', 'pl': 'Polish',
            'ru': 'Russian', 'ja': 'Japanese', 'zh': 'Chinese', 'ko': 'Korean',
            'ar': 'Arabic', 'tr': 'Turkish', 'cs': 'Czech', 'da': 'Danish',
            'el': 'Greek', 'fi': 'Finnish', 'he': 'Hebrew', 'hi': 'Hindi',
            'hu': 'Hungarian', 'id': 'Indonesian', 'ms': 'Malay', 'no': 'Norwegian',
            'ro': 'Romanian', 'sv': 'Swedish', 'th': 'Thai', 'uk': 'Ukrainian',
            'vi': 'Vietnamese'
        };
        return languages[code] || 'English';
    },

    autoStoreField(fieldId, lang) {
        const el = document.getElementById(fieldId);
        if (el && el.value) {
            this.storeOriginal(fieldId, el.value, lang || this.currentLang);
        }
    },

    async translatePageContent(targetLang) {
        if (!targetLang) targetLang = this.currentLang;

        let ollamaAvailable = false;
        try {
            if (typeof testOllamaStatus === 'function') {
                ollamaAvailable = await testOllamaStatus();
            }
        } catch (e) {}

        if (!ollamaAvailable && targetLang !== 'en') return;

        const elements = document.querySelectorAll('[data-translatable]');

        for (const el of elements) {
            const originalLang = el.dataset.originalLang || 'en';

            if (!el.dataset.originalText) {
                const currentText =
                    el.dataset.translateSource ||
                    el.value ||
                    el.textContent ||
                    '';

                const trimmed = currentText.trim();
                if (!trimmed) continue;

                el.dataset.originalText = trimmed;
                el.dataset.originalLang = originalLang;
            }

            const originalText = el.dataset.originalText;
            let translated = originalText;

            if (originalLang !== targetLang) {
                try {
                    translated = await this.translateText(originalText, originalLang, targetLang);
                } catch (e) {
                    console.error('Page translation failed:', e);
                    translated = originalText;
                }
            }

            if (el.matches('input, textarea')) {
                el.value = translated;
            } else {
                el.textContent = translated;
            }

            if (el.dataset.translatePlaceholder !== undefined) {
                el.placeholder = translated;
            }

            if (el.dataset.translateTitle !== undefined) {
                el.title = translated;
            }

            if (el.dataset.translateAriaLabel !== undefined) {
                el.setAttribute('aria-label', translated);
            }
        }
    },

    observeDynamicContent() {
        const observer = new MutationObserver(async (mutations) => {
            let shouldTranslate = false;
            for (const mutation of mutations) {
                if (mutation.addedNodes && mutation.addedNodes.length > 0) {
                    shouldTranslate = true;
                    break;
                }
            }
            if (shouldTranslate) {
                await this.translatePageContent(this.currentLang);
            }
        });

        observer.observe(document.body, {
            childList: true,
            subtree: true
        });
    }
};

document.addEventListener('DOMContentLoaded', () => {
    TranslationService.init();
});

async function applyTranslation(lang) {
    if (typeof TranslationService !== 'undefined') {
        TranslationService.currentLang = lang;
        localStorage.setItem('preferredLanguage', lang);
        await TranslationService.translateAllContent();
        await TranslationService.translatePageContent(lang);
    }
}

window.TranslationService = TranslationService;
window.applyTranslation = applyTranslation;
