// Global Image Processing Utility
const ImageProcessor = {
    /**
     * Process image file: resize, compress, and return base64
     * @param {File} file - The image file to process
     * @param {Object} options - Processing options
     * @returns {Promise<{dataUrl: string, base64: string, contentType: string, width: number, height: number}>}
     */
    async processImage(file, options = {}) {
        const {
            maxWidth = 1024,
            maxHeight = 1024,
            quality = 0.85,
            contentType = 'image/jpeg',
            cropToSquare = false,
            maintainAspectRatio = true
        } = options;

        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            
            reader.onload = (e) => {
                const img = new Image();
                
                img.onload = () => {
                    try {
                        let { width, height } = img;
                        let sourceX = 0, sourceY = 0, sourceWidth = width, sourceHeight = height;
                        
                        // Calculate new dimensions maintaining aspect ratio
                        if (maintainAspectRatio && (width > maxWidth || height > maxHeight)) {
                            const ratio = Math.min(maxWidth / width, maxHeight / height);
                            width = Math.round(width * ratio);
                            height = Math.round(height * ratio);
                        }
                        
                        // Optional: crop to square
                        let finalWidth = width;
                        let finalHeight = height;
                        
                        if (cropToSquare) {
                            const size = Math.min(width, height);
                            finalWidth = size;
                            finalHeight = size;
                            sourceX = (width - size) / 2;
                            sourceY = (height - size) / 2;
                            sourceWidth = size;
                            sourceHeight = size;
                        }
                        
                        const canvas = document.createElement('canvas');
                        canvas.width = cropToSquare ? finalWidth : width;
                        canvas.height = cropToSquare ? finalHeight : height;
                        
                        const ctx = canvas.getContext('2d');
                        
                        // Enable high quality rendering
                        ctx.imageSmoothingEnabled = true;
                        ctx.imageSmoothingQuality = 'high';
                        
                        // Draw image
                        if (cropToSquare) {
                            ctx.drawImage(img, sourceX, sourceY, sourceWidth, sourceHeight, 0, 0, finalWidth, finalHeight);
                        } else {
                            ctx.drawImage(img, 0, 0, width, height);
                        }
                        
                        const dataUrl = canvas.toDataURL(contentType, quality);
                        const base64 = dataUrl.split(',')[1];
                        
                        // Validate base64 size (limit to 1MB)
                        if (base64.length > 1000000) {
                            // Try lower quality
                            const smallerDataUrl = canvas.toDataURL(contentType, 0.6);
                            const smallerBase64 = smallerDataUrl.split(',')[1];
                            
                            resolve({
                                dataUrl: smallerDataUrl,
                                base64: smallerBase64,
                                contentType,
                                width: canvas.width,
                                height: canvas.height,
                                size: smallerBase64.length
                            });
                        } else {
                            resolve({
                                dataUrl,
                                base64,
                                contentType,
                                width: canvas.width,
                                height: canvas.height,
                                size: base64.length
                            });
                        }
                    } catch (err) {
                        reject(err);
                    }
                };
                
                img.onerror = () => reject(new Error('Failed to load image'));
                img.src = e.target.result;
            };
            
            reader.onerror = () => reject(new Error('Failed to read file'));
            reader.readAsDataURL(file);
        });
    },
    
    /**
     * Show image preview
     */
    showPreview(containerId, previewId, dataUrl) {
        const container = document.getElementById(containerId);
        const preview = document.getElementById(previewId);
        
        if (container && preview) {
            preview.src = dataUrl;
            container.style.display = 'block';
        }
    },
    
    /**
     * Clear image preview
     */
    clearPreview(containerId, previewId) {
        const container = document.getElementById(containerId);
        const preview = document.getElementById(previewId);
        
        if (container && preview) {
            preview.src = '';
            container.style.display = 'none';
        }
    },
    
    /**
     * Setup drag and drop for file upload
     */
    setupDragAndDrop(dropZoneId, fileInputId, callback, options = {}) {
        const dropZone = document.getElementById(dropZoneId);
        const fileInput = document.getElementById(fileInputId);
        
        if (!dropZone || !fileInput) return;
        
        dropZone.addEventListener('click', () => fileInput.click());
        
        dropZone.addEventListener('dragover', (e) => {
            e.preventDefault();
            dropZone.classList.add('dragover');
        });
        
        dropZone.addEventListener('dragleave', () => {
            dropZone.classList.remove('dragover');
        });
        
        dropZone.addEventListener('drop', async (e) => {
            e.preventDefault();
            dropZone.classList.remove('dragover');
            
            const files = e.dataTransfer.files;
            if (files.length > 0 && files[0].type.startsWith('image/')) {
                const processed = await this.processImage(files[0], options);
                if (callback) callback(processed);
            }
        });
        
        fileInput.addEventListener('change', async (e) => {
            if (e.target.files.length > 0 && e.target.files[0].type.startsWith('image/')) {
                const processed = await this.processImage(e.target.files[0], options);
                if (callback) callback(processed);
            }
        });
    },
    
    /**
     * Validate file type
     */
    isValidImage(file) {
        const validTypes = ['image/jpeg', 'image/png', 'image/gif', 'image/webp'];
        return file && validTypes.includes(file.type);
    },
    
    /**
     * Format file size
     */
    formatFileSize(bytes) {
        if (bytes < 1024) return bytes + ' B';
        if (bytes < 1024 * 1024) return (bytes / 1024).toFixed(1) + ' KB';
        return (bytes / (1024 * 1024)).toFixed(1) + ' MB';
    }
};

// Make globally available
window.ImageProcessor = ImageProcessor;
