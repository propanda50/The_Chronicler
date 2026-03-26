// TheChronicler 3D Miniatures Viewer - Three.js (FREE)
class Miniature3DViewer {
    constructor(containerId) {
        this.container = document.getElementById(containerId);
        this.scene = null;
        this.camera = null;
        this.renderer = null;
        this.miniatures = new Map();
        this.selectedMiniature = null;
        this.isDragging = false;
        this.raycaster = new THREE.Raycaster();
        this.mouse = new THREE.Vector2();
    }

    initialize() {
        // Scene
        this.scene = new THREE.Scene();
        this.scene.background = new THREE.Color(0x1a1a2e);

        // Camera
        this.camera = new THREE.PerspectiveCamera(
            60,
            this.container.clientWidth / this.container.clientHeight,
            0.1,
            1000
        );
        this.camera.position.set(0, 10, 15);
        this.camera.lookAt(0, 0, 0);

        // Renderer
        this.renderer = new THREE.WebGLRenderer({ antialias: true });
        this.renderer.setSize(this.container.clientWidth, this.container.clientHeight);
        this.renderer.shadowMap.enabled = true;
        this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;
        this.container.appendChild(this.renderer.domElement);

        // Lights
        const ambientLight = new THREE.AmbientLight(0xffffff, 0.4);
        this.scene.add(ambientLight);

        const directionalLight = new THREE.DirectionalLight(0xffffff, 0.8);
        directionalLight.position.set(10, 20, 10);
        directionalLight.castShadow = true;
        this.scene.add(directionalLight);

        // Grid floor
        this.createGrid();

        // Controls
        this.setupControls();

        // Start render loop
        this.animate();

        // Handle resize
        window.addEventListener('resize', () => this.onResize());
    }

    createGrid() {
        const gridHelper = new THREE.GridHelper(20, 20, 0x3a3a5a, 0x2a2a4a);
        this.scene.add(gridHelper);

        // Add ground plane for raycasting
        const groundGeometry = new THREE.PlaneGeometry(20, 20);
        const groundMaterial = new THREE.MeshBasicMaterial({ 
            visible: false 
        });
        const ground = new THREE.Mesh(groundGeometry, groundMaterial);
        ground.rotation.x = -Math.PI / 2;
        ground.name = 'ground';
        this.scene.add(ground);
    }

    setupControls() {
        const canvas = this.renderer.domElement;
        
        // Mouse controls
        canvas.addEventListener('mousedown', (e) => this.onMouseDown(e));
        canvas.addEventListener('mousemove', (e) => this.onMouseMove(e));
        canvas.addEventListener('mouseup', (e) => this.onMouseUp(e));
        canvas.addEventListener('wheel', (e) => this.onMouseWheel(e));

        // Touch controls
        canvas.addEventListener('touchstart', (e) => this.onTouchStart(e));
        canvas.addEventListener('touchmove', (e) => this.onTouchMove(e));
        canvas.addEventListener('touchend', (e) => this.onTouchEnd(e));
    }

    onMouseDown(e) {
        this.isDragging = true;
        this.lastMouseX = e.clientX;
        this.lastMouseY = e.clientY;

        // Check for miniature selection
        this.mouse.x = (e.clientX / this.container.clientWidth) * 2 - 1;
        this.mouse.y = -(e.clientY / this.container.clientHeight) * 2 + 1;

        this.raycaster.setFromCamera(this.mouse, this.camera);
        const miniaturesArray = Array.from(this.miniatures.values());
        const intersects = this.raycaster.intersectObjects(miniaturesArray, true);

        if (intersects.length > 0) {
            this.selectMiniature(intersects[0].object.parent?.userData?.id || intersects[0].object.userData?.id);
        }
    }

    onMouseMove(e) {
        if (!this.isDragging) return;

        const deltaX = e.clientX - this.lastMouseX;
        const deltaY = e.clientY - this.lastMouseY;

        if (e.shiftKey) {
            // Pan
            this.camera.position.x -= deltaX * 0.02;
            this.camera.position.z -= deltaY * 0.02;
        } else if (e.ctrlKey) {
            // Rotate
            const angle = deltaX * 0.01;
            this.camera.position.x = this.camera.position.x * Math.cos(angle) - this.camera.position.z * Math.sin(angle);
            this.camera.position.z = this.camera.position.x * Math.sin(angle) + this.camera.position.z * Math.cos(angle);
            this.camera.lookAt(0, 0, 0);
        }

        this.lastMouseX = e.clientX;
        this.lastMouseY = e.clientY;
    }

    onMouseUp(e) {
        this.isDragging = false;
    }

    onMouseWheel(e) {
        e.preventDefault();
        const zoomSpeed = 0.001;
        const distance = this.camera.position.length();
        const newDistance = distance * (1 + e.deltaY * zoomSpeed);
        
        if (newDistance > 5 && newDistance < 50) {
            this.camera.position.normalize().multiplyScalar(newDistance);
        }
    }

    onTouchStart(e) {
        if (e.touches.length === 1) {
            this.lastMouseX = e.touches[0].clientX;
            this.lastMouseY = e.touches[0].clientY;
            this.isDragging = true;
        }
    }

    onTouchMove(e) {
        if (e.touches.length === 1 && this.isDragging) {
            const deltaX = e.touches[0].clientX - this.lastMouseX;
            const deltaY = e.touches[0].clientY - this.lastMouseY;
            
            this.camera.position.x -= deltaX * 0.02;
            this.camera.position.z -= deltaY * 0.02;
            
            this.lastMouseX = e.touches[0].clientX;
            this.lastMouseY = e.touches[0].clientY;
        }
    }

    onTouchEnd(e) {
        this.isDragging = false;
    }

    onResize() {
        this.camera.aspect = this.container.clientWidth / this.container.clientHeight;
        this.camera.updateProjectionMatrix();
        this.renderer.setSize(this.container.clientWidth, this.container.clientHeight);
    }

    animate() {
        requestAnimationFrame(() => this.animate());
        
        // Animate miniatures (idle rotation)
        this.miniatures.forEach((mini) => {
            if (mini !== this.selectedMiniature) {
                mini.rotation.y += 0.002;
            }
        });

        this.renderer.render(this.scene, this.camera);
    }

    // Add a miniature to the scene
    addMiniature(id, options = {}) {
        const {
            name = 'Character',
            color = 0xd4a843,
            position = { x: 0, y: 0, z: 0 },
            size = 1,
            imageUrl = null
        } = options;

        const group = new THREE.Group();
        group.userData = { id, name };

        // Base cylinder (token style)
        const baseGeometry = new THREE.CylinderGeometry(0.5 * size, 0.5 * size, 0.1 * size, 32);
        const baseMaterial = new THREE.MeshStandardMaterial({ 
            color: color,
            metalness: 0.3,
            roughness: 0.7
        });
        const base = new THREE.Mesh(baseGeometry, baseMaterial);
        base.position.y = 0.05 * size;
        base.castShadow = true;
        base.receiveShadow = true;
        group.add(base);

        // Figure (simple humanoid shape)
        // Body
        const bodyGeometry = new THREE.CylinderGeometry(0.2 * size, 0.25 * size, 0.5 * size, 16);
        const bodyMaterial = new THREE.MeshStandardMaterial({ color: 0x4a4a6a });
        const body = new THREE.Mesh(bodyGeometry, bodyMaterial);
        body.position.y = 0.4 * size;
        body.castShadow = true;
        group.add(body);

        // Head
        const headGeometry = new THREE.SphereGeometry(0.15 * size, 16, 16);
        const headMaterial = new THREE.MeshStandardMaterial({ color: 0xdeb887 });
        const head = new THREE.Mesh(headGeometry, headMaterial);
        head.position.y = 0.8 * size;
        head.castShadow = true;
        group.add(head);

        // Name plate
        const canvas = document.createElement('canvas');
        canvas.width = 256;
        canvas.height = 64;
        const ctx = canvas.getContext('2d');
        ctx.fillStyle = '#1a1a2e';
        ctx.fillRect(0, 0, 256, 64);
        ctx.fillStyle = '#d4a843';
        ctx.font = 'bold 24px Arial';
        ctx.textAlign = 'center';
        ctx.fillText(name, 128, 40);

        const texture = new THREE.CanvasTexture(canvas);
        const plateGeometry = new THREE.PlaneGeometry(1, 0.25);
        const plateMaterial = new THREE.MeshBasicMaterial({ map: texture, transparent: true });
        const plate = new THREE.Mesh(plateGeometry, plateMaterial);
        plate.position.y = 1.1 * size;
        plate.rotation.x = -Math.PI / 4;
        group.add(plate);

        group.position.set(position.x, position.y, position.z);
        this.scene.add(group);
        this.miniatures.set(id, group);

        return group;
    }

    // Remove a miniature
    removeMiniature(id) {
        const mini = this.miniatures.get(id);
        if (mini) {
            this.scene.remove(mini);
            this.miniatures.delete(id);
        }
    }

    // Move a miniature
    moveMiniature(id, x, y, z) {
        const mini = this.miniatures.get(id);
        if (mini) {
            mini.position.set(x, y, z);
        }
    }

    // Select a miniature
    selectMiniature(id) {
        // Deselect previous
        if (this.selectedMiniature) {
            this.highlightMiniature(this.selectedMiniature, false);
        }

        const mini = this.miniatures.get(id);
        if (mini) {
            this.selectedMiniature = mini;
            this.highlightMiniature(mini, true);
            this.onMiniatureSelected?.(id, mini.userData.name);
        }
    }

    highlightMiniature(miniature, highlight) {
        miniature.traverse((child) => {
            if (child.isMesh && child.material) {
                if (highlight) {
                    child.material.emissive = new THREE.Color(0x444444);
                } else {
                    child.material.emissive = new THREE.Color(0x000000);
                }
            }
        });
    }

    // Take screenshot
    takeScreenshot() {
        this.renderer.render(this.scene, this.camera);
        return this.renderer.domElement.toDataURL('image/png');
    }

    // Reset camera
    resetCamera() {
        this.camera.position.set(0, 10, 15);
        this.camera.lookAt(0, 0, 0);
    }

    // Load GLB model (for custom miniatures)
    async loadGLBModel(url, id, options = {}) {
        const loader = new THREE.GLTFLoader();
        
        return new Promise((resolve, reject) => {
            loader.load(url, (gltf) => {
                const model = gltf.scene;
                model.userData = { id, name: options.name || 'Character' };
                
                // Scale and position
                model.scale.set(options.scale || 1, options.scale || 1, options.scale || 1);
                model.position.set(
                    options.position?.x || 0,
                    options.position?.y || 0,
                    options.position?.z || 0
                );

                // Add shadow
                model.traverse((child) => {
                    if (child.isMesh) {
                        child.castShadow = true;
                        child.receiveShadow = true;
                    }
                });

                this.scene.add(model);
                this.miniatures.set(id, model);
                resolve(model);
            }, undefined, reject);
        });
    }

    // Callback for when miniature is selected
    onMiniatureSelected = null;
}

// Global viewer instance
window.miniatureViewer = null;
