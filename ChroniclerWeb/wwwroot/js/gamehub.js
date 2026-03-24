// TheChronicler Real-time Client (SignalR)
class GameHubClient {
    constructor() {
        this.connection = null;
        this.campaignId = null;
        this.handlers = {};
        this.connected = false;
    }

    async start(campaignId) {
        this.campaignId = campaignId;
        
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("/gamehub")
            .withAutomaticReconnect([0, 2000, 5000, 10000, 30000])
            .build();

        // Register event handlers
        this.registerHandlers();

        // Connection events
        this.connection.onreconnecting((error) => {
            console.log('Reconnecting...', error);
            this.updateConnectionStatus('reconnecting');
        });

        this.connection.onreconnected((connectionId) => {
            console.log('Reconnected:', connectionId);
            this.updateConnectionStatus('connected');
            this.joinCampaign();
        });

        this.connection.onclose((error) => {
            console.log('Connection closed:', error);
            this.updateConnectionStatus('disconnected');
            this.connected = false;
        });

        try {
            await this.connection.start();
            console.log('SignalR Connected!');
            this.connected = true;
            this.updateConnectionStatus('connected');
            await this.joinCampaign();
        } catch (err) {
            console.error('Connection failed:', err);
            this.updateConnectionStatus('error');
        }
    }

    registerHandlers() {
        // Player events
        this.connection.on("PlayerJoined", (data) => {
            console.log('Player joined:', data.userName);
            this.trigger('playerJoined', data);
            this.showNotification(`${data.userName} joined the campaign`, 'success');
        });

        this.connection.on("PlayerLeft", (data) => {
            console.log('Player left:', data.userName);
            this.trigger('playerLeft', data);
            this.showNotification(`${data.userName} left the campaign`, 'info');
        });

        // Chat events
        this.connection.on("NewMessage", (data) => {
            this.trigger('message', data);
            this.addChatMessage(data);
        });

        // Dice roll events
        this.connection.on("DiceRolled", (data) => {
            this.trigger('diceRoll', data);
            this.showDiceRoll(data);
        });

        // Map events
        this.connection.on("MapUpdated", (data) => {
            this.trigger('mapUpdate', data);
        });

        // Turn tracker events
        this.connection.on("TurnTrackerUpdated", (data) => {
            this.trigger('turnTrackerUpdate', data);
        });

        // Cursor events
        this.connection.on("CursorMoved", (data) => {
            this.trigger('cursorMoved', data);
        });

        // Fog of war events
        this.connection.on("FogUpdated", (data) => {
            this.trigger('fogUpdate', data);
        });

        // Character events
        this.connection.on("CharacterUpdated", (data) => {
            this.trigger('characterUpdate', data);
        });
    }

    async joinCampaign() {
        if (this.connection && this.campaignId) {
            await this.connection.invoke("JoinCampaign", this.campaignId);
        }
    }

    async leaveCampaign() {
        if (this.connection && this.campaignId) {
            await this.connection.invoke("LeaveCampaign", this.campaignId);
        }
    }

    // Send chat message
    async sendMessage(message, type = 'chat') {
        if (this.connection && this.connected) {
            await this.connection.invoke("SendMessage", this.campaignId, message, type);
        }
    }

    // Broadcast dice roll
    async broadcastRoll(notation, result, modifierNote = null) {
        if (this.connection && this.connected) {
            await this.connection.invoke("BroadcastRoll", this.campaignId, notation, result, modifierNote);
        }
    }

    // Update map
    async updateMap(mapUpdate) {
        if (this.connection && this.connected) {
            await this.connection.invoke("UpdateMap", this.campaignId, mapUpdate);
        }
    }

    // Update turn tracker
    async updateTurnTracker(trackerState) {
        if (this.connection && this.connected) {
            await this.connection.invoke("UpdateTurnTracker", this.campaignId, trackerState);
        }
    }

    // Update cursor position
    async updateCursor(lat, lng) {
        if (this.connection && this.connected) {
            await this.connection.invoke("UpdateCursor", this.campaignId, lat, lng);
        }
    }

    // Update fog of war
    async updateFogOfWar(fogData) {
        if (this.connection && this.connected) {
            await this.connection.invoke("UpdateFogOfWar", this.campaignId, fogData);
        }
    }

    // Update character status
    async updateCharacterStatus(characterId, currentHp, condition = null) {
        if (this.connection && this.connected) {
            await this.connection.invoke("UpdateCharacterStatus", this.campaignId, characterId, currentHp, condition);
        }
    }

    // Event system
    on(event, handler) {
        if (!this.handlers[event]) {
            this.handlers[event] = [];
        }
        this.handlers[event].push(handler);
    }

    trigger(event, data) {
        if (this.handlers[event]) {
            this.handlers[event].forEach(handler => handler(data));
        }
    }

    // UI helpers
    updateConnectionStatus(status) {
        const indicator = document.getElementById('connectionStatus');
        if (indicator) {
            indicator.className = `connection-status ${status}`;
            indicator.title = `Connection: ${status}`;
        }
    }

    showNotification(message, type = 'info') {
        // Create toast notification
        const toast = document.createElement('div');
        toast.className = `toast-notification toast-${type}`;
        toast.innerHTML = `
            <i class="bi bi-${type === 'success' ? 'check-circle' : 'info-circle'}"></i>
            <span>${message}</span>
        `;
        document.body.appendChild(toast);
        
        setTimeout(() => toast.classList.add('show'), 10);
        setTimeout(() => {
            toast.classList.remove('show');
            setTimeout(() => toast.remove(), 300);
        }, 3000);
    }

    addChatMessage(data) {
        const chatBox = document.getElementById('gameChat');
        if (chatBox) {
            const msgDiv = document.createElement('div');
            msgDiv.className = 'chat-message';
            msgDiv.innerHTML = `
                <span class="chat-user">${data.userName}</span>
                <span class="chat-text">${data.message}</span>
                <span class="chat-time">${new Date(data.timestamp).toLocaleTimeString()}</span>
            `;
            chatBox.appendChild(msgDiv);
            chatBox.scrollTop = chatBox.scrollHeight;
        }
    }

    showDiceRoll(data) {
        const rollBox = document.getElementById('diceRolls');
        if (rollBox) {
            const rollDiv = document.createElement('div');
            rollDiv.className = 'dice-roll-result';
            rollDiv.innerHTML = `
                <span class="roll-user">${data.userName}</span>
                <span class="roll-notation">${data.notation}</span>
                <span class="roll-result">${data.result}</span>
            `;
            rollBox.appendChild(rollDiv);
        }
    }

    async stop() {
        if (this.connection) {
            await this.leaveCampaign();
            await this.connection.stop();
        }
    }
}

// Global instance
window.gameHub = new GameHubClient();
