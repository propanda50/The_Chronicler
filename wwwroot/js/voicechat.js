// TheChronicler Voice Chat - WebRTC (100% Free P2P)
class VoiceChat {
    constructor(gameHub) {
        this.gameHub = gameHub;
        this.localStream = null;
        this.peerConnections = {};
        this.isMuted = false;
        this.isDeafened = false;
        this.isPushToTalk = false;
        this.isTalking = false;
        this.voiceActivityThreshold = -50; // dB
        this.peers = new Map();
    }

    async initialize() {
        try {
            this.localStream = await navigator.mediaDevices.getUserMedia({
                audio: {
                    echoCancellation: true,
                    noiseSuppression: true,
                    autoGainControl: true
                }
            });

            // Setup audio analysis for voice activity detection
            this.setupAudioAnalysis();
            
            // Register SignalR handlers
            this.registerSignalRHandlers();
            
            console.log('Voice chat initialized');
            return true;
        } catch (error) {
            console.error('Failed to initialize voice chat:', error);
            return false;
        }
    }

    setupAudioAnalysis() {
        const audioContext = new AudioContext();
        const source = audioContext.createMediaStreamSource(this.localStream);
        const analyser = audioContext.createAnalyser();
        analyser.fftSize = 256;
        source.connect(analyser);

        const dataArray = new Uint8Array(analyser.frequencyBinCount);
        
        const checkVoiceActivity = () => {
            analyser.getByteFrequencyData(dataArray);
            const average = dataArray.reduce((a, b) => a + b) / dataArray.length;
            const isTalking = average > 30; // Threshold

            if (isTalking !== this.isTalking) {
                this.isTalking = isTalking;
                this.updateVoiceIndicator(isTalking);
            }

            requestAnimationFrame(checkVoiceActivity);
        };

        checkVoiceActivity();
    }

    registerSignalRHandlers() {
        this.gameHub.on('voiceOffer', async (data) => {
            await this.handleOffer(data);
        });

        this.gameHub.on('voiceAnswer', async (data) => {
            await this.handleAnswer(data);
        });

        this.gameHub.on('voiceIceCandidate', async (data) => {
            await this.handleIceCandidate(data);
        });

        this.gameHub.on('playerJoined', async (data) => {
            if (data.userId !== this.gameHub.connection.connectionId) {
                await this.connectToPeer(data.userId);
            }
        });

        this.gameHub.on('playerLeft', (data) => {
            this.disconnectFromPeer(data.userId);
        });
    }

    async connectToPeer(peerId) {
        const pc = new RTCPeerConnection({
            iceServers: [
                { urls: 'stun:stun.l.google.com:19302' },
                { urls: 'stun:stun1.l.google.com:19302' }
            ]
        });

        this.peerConnections[peerId] = pc;

        // Add local stream to connection
        this.localStream.getTracks().forEach(track => {
            pc.addTrack(track, this.localStream);
        });

        // Handle remote stream
        pc.ontrack = (event) => {
            this.handleRemoteStream(peerId, event.streams[0]);
        };

        // Create and send offer
        const offer = await pc.createOffer();
        await pc.setLocalDescription(offer);

        this.gameHub.connection.invoke('SendVoiceOffer', peerId, offer);
    }

    async handleOffer(data) {
        const pc = new RTCPeerConnection({
            iceServers: [
                { urls: 'stun:stun.l.google.com:19302' },
                { urls: 'stun:stun1.l.google.com:19302' }
            ]
        });

        this.peerConnections[data.fromUserId] = pc;

        this.localStream.getTracks().forEach(track => {
            pc.addTrack(track, this.localStream);
        });

        pc.ontrack = (event) => {
            this.handleRemoteStream(data.fromUserId, event.streams[0]);
        };

        await pc.setRemoteDescription(new RTCSessionDescription(data.offer));
        const answer = await pc.createAnswer();
        await pc.setLocalDescription(answer);

        this.gameHub.connection.invoke('SendVoiceAnswer', data.fromUserId, answer);
    }

    async handleAnswer(data) {
        const pc = this.peerConnections[data.fromUserId];
        if (pc) {
            await pc.setRemoteDescription(new RTCSessionDescription(data.answer));
        }
    }

    async handleIceCandidate(data) {
        const pc = this.peerConnections[data.fromUserId];
        if (pc && data.candidate) {
            await pc.addIceCandidate(new RTCIceCandidate(data.candidate));
        }
    }

    handleRemoteStream(peerId, stream) {
        // Create audio element for remote stream
        let audio = document.getElementById(`voice-${peerId}`);
        if (!audio) {
            audio = document.createElement('audio');
            audio.id = `voice-${peerId}`;
            audio.autoplay = true;
            document.body.appendChild(audio);
        }
        audio.srcObject = stream;
        audio.volume = this.isDeafened ? 0 : 1;
        
        this.peers.set(peerId, { stream, audio });
        this.updatePeerList();
    }

    disconnectFromPeer(peerId) {
        const pc = this.peerConnections[peerId];
        if (pc) {
            pc.close();
            delete this.peerConnections[peerId];
        }

        const audio = document.getElementById(`voice-${peerId}`);
        if (audio) audio.remove();
        
        this.peers.delete(peerId);
        this.updatePeerList();
    }

    toggleMute() {
        this.isMuted = !this.isMuted;
        this.localStream.getAudioTracks().forEach(track => {
            track.enabled = !this.isMuted;
        });
        this.updateMuteButton();
        return this.isMuted;
    }

    toggleDeafen() {
        this.isDeafened = !this.isDeafened;
        this.peers.forEach(({ audio }) => {
            audio.volume = this.isDeafened ? 0 : 1;
        });
        this.updateDeafenButton();
        return this.isDeafened;
    }

    togglePushToTalk() {
        this.isPushToTalk = !this.isPushToTalk;
        if (this.isPushToTalk) {
            this.localStream.getAudioTracks().forEach(track => {
                track.enabled = false;
            });
        }
        return this.isPushToTalk;
    }

    startPushToTalk() {
        if (this.isPushToTalk) {
            this.localStream.getAudioTracks().forEach(track => {
                track.enabled = true;
            });
        }
    }

    stopPushToTalk() {
        if (this.isPushToTalk) {
            this.localStream.getAudioTracks().forEach(track => {
                track.enabled = false;
            });
        }
    }

    updateVoiceIndicator(isTalking) {
        const indicator = document.getElementById('voiceIndicator');
        if (indicator) {
            indicator.classList.toggle('active', isTalking);
        }
    }

    updateMuteButton() {
        const btn = document.getElementById('muteBtn');
        if (btn) {
            btn.innerHTML = this.isMuted ? 
                '<i class="bi bi-mic-mute-fill"></i>' : 
                '<i class="bi bi-mic-fill"></i>';
            btn.classList.toggle('btn-danger', this.isMuted);
        }
    }

    updateDeafenButton() {
        const btn = document.getElementById('deafenBtn');
        if (btn) {
            btn.innerHTML = this.isDeafened ? 
                '<i class="bi bi-volume-mute-fill"></i>' : 
                '<i class="bi bi-volume-up-fill"></i>';
            btn.classList.toggle('btn-danger', this.isDeafened);
        }
    }

    updatePeerList() {
        const list = document.getElementById('voicePeerList');
        if (list) {
            list.innerHTML = '';
            this.peers.forEach((data, peerId) => {
                const item = document.createElement('div');
                item.className = 'voice-peer';
                item.innerHTML = `<i class="bi bi-person-fill"></i> ${peerId}`;
                list.appendChild(item);
            });
        }
    }

    async disconnect() {
        if (this.localStream) {
            this.localStream.getTracks().forEach(track => track.stop());
        }
        Object.keys(this.peerConnections).forEach(peerId => {
            this.disconnectFromPeer(peerId);
        });
    }
}

// Global voice chat instance
window.voiceChat = null;
