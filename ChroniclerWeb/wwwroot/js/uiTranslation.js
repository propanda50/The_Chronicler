// Comprehensive Multi-Language System for TheChronicler
// Supports 30+ languages with UI translation

const UIDictionary = {
    // English (base language)
    "en": {
        // Navigation
        "Dashboard": "Dashboard", "Campaigns": "Campaigns", "Settings": "Settings",
        "Logout": "Logout", "Login": "Login", "Register": "Register",
        "Profile": "Profile", "Help": "Help", "Home": "Home",
        "Characters": "Characters", "Sessions": "Sessions", "Events": "Events",
        "Locations": "Locations", "Forum": "Forum", "Maps": "Maps",
        
        // Actions
        "Save": "Save", "Save Changes": "Save Changes", "Save Preferences": "Save Preferences",
        "Cancel": "Cancel", "Delete": "Delete", "Edit": "Edit", "Create": "Create",
        "Update": "Update", "Next": "Next", "Previous": "Previous", "Submit": "Submit",
        "Close": "Close", "Search": "Search", "Filter": "Filter", "Reset": "Reset",
        "Confirm": "Confirm", "Add": "Add", "Remove": "Remove", "Clear": "Clear",
        "Back": "Back", "Continue": "Continue", "Finish": "Finish",
        
        // Character
        "Create Character": "Create Character", "Character Name": "Character Name",
        "Name": "Name", "Surname / Pseudo": "Surname / Pseudo", "Pseudo": "Pseudo",
        "Description": "Description", "Race": "Race", "Class": "Class", "Role": "Role",
        "Type": "Type", "Status": "Status", "NPC Role": "NPC Role", "Stats": "Stats",
        "Points Remaining": "Points Remaining", "Randomize Stats": "Randomize Stats",
        "Character Image": "Character Image", "Upload Image": "Upload Image",
        "Generate with AI": "Generate with AI", "Regenerate": "Regenerate",
        "Character Basics": "Character Basics", "Full Name": "Full Name",
        
        // Stats
        "Strength": "Strength", "Agility": "Agility", "Health": "Health",
        "Intelligence": "Intelligence", "Charisma": "Charisma", "Endurance": "Endurance",
        
        // Character Types
        "Player Character": "Player Character", "NPC": "NPC",
        "Alive": "Alive", "Dead": "Dead", "Undead": "Undead",
        "Missing": "Missing", "Unknown": "Unknown",
        "None": "None", "Antagonist": "Antagonist", "Ally": "Ally",
        "Merchant": "Merchant", "Plot Driver": "Plot Driver",
        "Quest Giver": "Quest Giver", "Informant": "Informant", "Henchman": "Henchman",
        
        // Campaign
        "Create Campaign": "Create Campaign", "Campaign Name": "Campaign Name",
        "Game System": "Game System", "Campaign Cover": "Campaign Cover",
        
        // Sessions
        "New Session": "New Session", "Session Title": "Session Title",
        "Session Number": "Session Number", "Session Date": "Session Date",
        "Summary": "Summary", "Session Notes": "Session Notes",
        "Tag Characters": "Tag Characters", "Tag Locations": "Tag Locations",
        
        // Events
        "New Event": "New Event", "Event Title": "Event Title",
        "Event Date": "Event Date", "Mark as Key Event": "Mark as Key Event",
        
        // Locations
        "New Location": "New Location", "Location Name": "Location Name", "Region": "Region",
        
        // Forum
        "New Forum Post": "New Forum Post", "Title": "Title", "Content": "Content",
        "Category": "Category", "Attachments": "Attachments", "Transcript": "Transcript",
        "Create Post": "Create Post",
        
        // Categories
        "General": "General", "Looking for Group": "Looking for Group",
        "Recruitment": "Recruitment", "Tips & Help": "Tips & Help", "Discussion": "Discussion",
        
        // Settings
        "Preferences": "Preferences", "Security": "Security", "Notifications": "Notifications",
        "Language": "Language", "Theme": "Theme", "Save to apply language": "Save to apply language",
        "Avatar": "Avatar", "Account": "Account",
        
        // Misc
        "Loading": "Loading...", "Generating with AI": "Generating with AI...",
        "No results found": "No results found", "Click to select": "Click to select",
        "Required field": "Required field", "Random": "Random",
        "Generate Name": "Generate Name", "Generate Pseudo": "Generate Pseudo",
        "Generate Description": "Generate Description", "Generate Title": "Generate Title",
        "Step 1": "Step 1", "Step 2": "Step 2", "Step 3": "Step 3", "Step 4": "Step 4",
        "Basics": "Basics", "Image": "Image", "Attachments": "Attachments",
        "Voice Input": "Voice Input", "Listening": "Listening...",
        "Map Upload": "Map Upload", "Cover Image": "Cover Image",
        "Character Sheet": "Character Sheet", "Campaign Details": "Campaign Details",
        // Generator buttons
        "AI Generate": "AI Generate", "Generate AI Description": "Generate AI Description",
        "Regenerate": "Regenerate", "Generate with AI": "Generate with AI",
        "Randomize": "Randomize", "Random": "Random",
        "Create Campaign": "Create Campaign", "Create Session": "Create Session",
        "Create Event": "Create Event", "Create Location": "Create Location",
        "New Session": "New Session", "New Event": "New Event", "New Location": "New Location",
        
        // Map
        "Campaign Map": "Campaign Map", "Interactive Map": "Interactive Map",
        "Click on map to place pin": "Click on map to place pin",
        "No map": "No map", "Upload a map to get started": "Upload a map to get started",
        "Add pin": "Add pin", "Edit pin": "Edit pin", "Pin list": "Pin list",
        "Search": "Search", "Name": "Name", "Type": "Type", "Description": "Description",
        "Save": "Save", "Delete this pin": "Delete this pin", "Cancel": "Cancel",
        "Enter a name": "Enter a name", "Click on the map first": "Click on the map first",
        "Delete this pin?": "Delete this pin?", "Error": "Error",
        "Upload map": "Upload map", "Upload error": "Upload error"
    },
    
    // French
    "fr": {
        "Dashboard": "Tableau de Bord", "Campaigns": "Campagnes", "Settings": "Paramètres",
        "Logout": "Déconnexion", "Login": "Connexion", "Register": "S'inscrire",
        "Profile": "Profil", "Help": "Aide", "Home": "Accueil",
        "Characters": "Personnages", "Sessions": "Sessions", "Events": "Événements",
        "Locations": "Lieux", "Forum": "Forum", "Maps": "Cartes",
        "Save": "Enregistrer", "Save Changes": "Enregistrer", "Save Preferences": "Enregistrer les Préférences",
        "Cancel": "Annuler", "Delete": "Supprimer", "Edit": "Modifier", "Create": "Créer",
        "Update": "Mettre à jour", "Next": "Suivant", "Previous": "Précédent", "Submit": "Soumettre",
        "Close": "Fermer", "Search": "Rechercher", "Filter": "Filtrer", "Reset": "Réinitialiser",
        "Confirm": "Confirmer", "Add": "Ajouter", "Remove": "Supprimer", "Clear": "Effacer",
        "Back": "Retour", "Continue": "Continuer", "Finish": "Terminer",
        "Create Character": "Créer un Personnage", "Character Name": "Nom du Personnage",
        "Name": "Nom", "Surname / Pseudo": "Surnom / Pseudo", "Pseudo": "Pseudo",
        "Description": "Description", "Race": "Race", "Class": "Classe", "Role": "Rôle",
        "Type": "Type", "Status": "Statut", "NPC Role": "Rôle PNJ", "Stats": "Statistiques",
        "Points Remaining": "Points Restants", "Randomize Stats": "Stats Aléatoires",
        "Character Image": "Image du Personnage", "Upload Image": "Téléverser une Image",
        "Generate with AI": "Générer avec l'IA", "Regenerate": "Régénérer",
        "Character Basics": "Bases du Personnage", "Full Name": "Nom Complet",
        "Strength": "Force", "Agility": "Agilité", "Health": "Santé",
        "Intelligence": "Intelligence", "Charisma": "Charisme", "Endurance": "Endurance",
        "Player Character": "Personnage Joueur", "NPC": "PNJ",
        "Alive": "Vivant", "Dead": "Mort", "Undead": "Mort-vivant",
        "Missing": "Disparu", "Unknown": "Inconnu",
        "None": "Aucun", "Antagonist": "Antagoniste", "Ally": "Allié",
        "Merchant": "Marchand", "Plot Driver": "Catalyseur d'Histoire",
        "Quest Giver": "Donneur de Quêtes", "Informant": "Informateur", "Henchman": "Laquais",
        "Create Campaign": "Créer une Campagne", "Campaign Name": "Nom de la Campagne",
        "Game System": "Système de Jeu", "Campaign Cover": "Couverture de Campagne",
        "New Session": "Nouvelle Session", "Session Title": "Titre de la Session",
        "Session Number": "Numéro de Session", "Session Date": "Date de Session",
        "Summary": "Résumé", "Session Notes": "Notes de Session",
        "Tag Characters": "Taguer les Personnages", "Tag Locations": "Taguer les Lieux",
        "New Event": "Nouvel Événement", "Event Title": "Titre de l'Événement",
        "Event Date": "Date de l'Événement", "Mark as Key Event": "Marquer comme Événement Clé",
        "New Location": "Nouveau Lieu", "Location Name": "Nom du Lieu", "Region": "Région",
        "New Forum Post": "Nouveau Message", "Title": "Titre", "Content": "Contenu",
        "Category": "Catégorie", "Attachments": "Pièces Jointes", "Transcript": "Transcription",
        "Create Post": "Créer le Message",
        "General": "Général", "Looking for Group": "Recherche de Groupe",
        "Recruitment": "Recrutement", "Tips & Help": "Conseils & Aide", "Discussion": "Discussion",
        "Preferences": "Préférences", "Security": "Sécurité", "Notifications": "Notifications",
        "Language": "Langue", "Theme": "Thème", "Save to apply language": "Enregistrer pour appliquer la langue",
        "Avatar": "Avatar", "Account": "Compte",
        "Loading": "Chargement...", "Generating with AI": "Génération avec l'IA...",
        "No results found": "Aucun résultat trouvé", "Click to select": "Cliquez pour sélectionner",
        "Required field": "Champ requis", "Random": "Aléatoire",
        "Generate Name": "Générer Nom", "Generate Pseudo": "Générer Pseudo",
        "Generate Description": "Générer Description", "Generate Title": "Générer Titre",
        "Step 1": "Étape 1", "Step 2": "Étape 2", "Step 3": "Étape 3", "Step 4": "Étape 4",
        "Basics": "Bases", "Image": "Image", "Voice Input": "Entrée Vocale", "Listening": "Écoute...",
        "Map Upload": "Téléversement de Carte", "Cover Image": "Image de Couverture",
        "Character Sheet": "Fiche de Personnage", "Campaign Details": "Détails de Campagne",
        "AI Generate": "Aléatoire", "Generate AI Description": "Générer Description",
        "Regenerate": "Régénérer", "Generate Description": "Générer Description",
        "Randomize": "Aléatoire", "Random": "Aléatoire",
        "Create Campaign": "Créer Campagne", "Create Session": "Créer Session",
        "Create Event": "Créer Événement", "Create Location": "Créer Lieu",
        "New Session": "Nouvelle Session", "New Event": "Nouvel Événement", "New Location": "Nouveau Lieu",
        
        // Map
        "Campaign Map": "Carte de Campagne", "Interactive Map": "Carte Interactive",
        "Click on map to place pin": "Clique sur la carte pour placer un pin",
        "No map": "Aucune carte", "Upload a map to get started": "Uploade une carte pour commencer",
        "Add pin": "Ajouter un pin", "Edit pin": "Modifier le pin", "Pin list": "Liste des pins",
        "Search": "Rechercher", "Name": "Nom", "Type": "Type", "Description": "Description",
        "Save": "Sauvegarder", "Delete this pin": "Supprimer ce pin", "Cancel": "Annuler",
        "Enter a name": "Entre un nom", "Click on the map first": "Clique d'abord sur la carte",
        "Delete this pin?": "Supprimer ce pin ?", "Error": "Erreur",
        "Upload map": "Uploader carte", "Upload error": "Erreur upload"
    },
    
    // Spanish
    "es": {
        "Dashboard": "Panel", "Campaigns": "Campañas", "Settings": "Ajustes",
        "Logout": "Cerrar sesión", "Login": "Iniciar sesión", "Register": "Registrarse",
        "Profile": "Perfil", "Help": "Ayuda", "Home": "Inicio",
        "Characters": "Personajes", "Sessions": "Sesiones", "Events": "Eventos",
        "Locations": "Lugares", "Forum": "Foro", "Maps": "Mapas",
        "Save": "Guardar", "Save Changes": "Guardar Cambios", "Save Preferences": "Guardar Preferencias",
        "Cancel": "Cancelar", "Delete": "Eliminar", "Edit": "Editar", "Create": "Crear",
        "Update": "Actualizar", "Next": "Siguiente", "Previous": "Anterior", "Submit": "Enviar",
        "Close": "Cerrar", "Search": "Buscar", "Filter": "Filtrar", "Reset": "Restablecer",
        "Confirm": "Confirmar", "Add": "Agregar", "Remove": "Eliminar", "Clear": "Limpiar",
        "Back": "Volver", "Continue": "Continuar", "Finish": "Finalizar",
        "Create Character": "Crear Personaje", "Character Name": "Nombre del Personaje",
        "Name": "Nombre", "Surname / Pseudo": "Apellido / Pseudónimo", "Pseudo": "Pseudónimo",
        "Description": "Descripción", "Race": "Raza", "Class": "Clase", "Role": "Rol",
        "Type": "Tipo", "Status": "Estado", "NPC Role": "Rol PNJ", "Stats": "Estadísticas",
        "Points Remaining": "Puntos Restantes", "Randomize Stats": "Stats Aleatorios",
        "Character Image": "Imagen del Personaje", "Upload Image": "Subir Imagen",
        "Generate with AI": "Generar con IA", "Regenerate": "Regenerar",
        "Character Basics": "Básicos del Personaje", "Full Name": "Nombre Completo",
        "Strength": "Fuerza", "Agility": "Agilidad", "Health": "Salud",
        "Intelligence": "Inteligencia", "Charisma": "Carisma", "Endurance": "Resistencia",
        "Player Character": "Personaje Jugador", "NPC": "PNJ",
        "Alive": "Vivo", "Dead": "Muerto", "Undead": "No Muerto",
        "Missing": "Desaparecido", "Unknown": "Desconocido",
        "None": "Ninguno", "Antagonist": "Antagonista", "Ally": "Aliado",
        "Merchant": "Comerciante", "Plot Driver": "Motor de Trama",
        "Quest Giver": "Dador de Misiones", "Informant": "Informante", "Henchman": "Esbirro",
        "Create Campaign": "Crear Campaña", "Campaign Name": "Nombre de la Campaña",
        "Game System": "Sistema de Juego", "Campaign Cover": "Portada de Campaña",
        "New Session": "Nueva Sesión", "Session Title": "Título de la Sesión",
        "Session Number": "Número de Sesión", "Session Date": "Fecha de Sesión",
        "Summary": "Resumen", "Session Notes": "Notas de Sesión",
        "Tag Characters": "Etiquetar Personajes", "Tag Locations": "Etiquetar Lugares",
        "New Event": "Nuevo Evento", "Event Title": "Título del Evento",
        "Event Date": "Fecha del Evento", "Mark as Key Event": "Marcar como Evento Clave",
        "New Location": "Nuevo Lugar", "Location Name": "Nombre del Lugar", "Region": "Región",
        "New Forum Post": "Nueva Publicación", "Title": "Título", "Content": "Contenido",
        "Category": "Categoría", "Attachments": "Archivos Adjuntos", "Transcript": "Transcripción",
        "Create Post": "Crear Publicación",
        "General": "General", "Looking for Group": "Buscando Grupo",
        "Recruitment": "Reclutamiento", "Tips & Help": "Consejos y Ayuda", "Discussion": "Discusión",
        "Preferences": "Preferencias", "Security": "Seguridad", "Notifications": "Notificaciones",
        "Language": "Idioma", "Theme": "Tema", "Save to apply language": "Guardar para aplicar idioma",
        "Avatar": "Avatar", "Account": "Cuenta",
        "Loading": "Cargando...", "Generating with AI": "Generando con IA...",
        "No results found": "No se encontraron resultados", "Click to select": "Clic para seleccionar",
        "Required field": "Campo obligatorio", "Random": "Aleatorio",
        "Generate Name": "Generar Nombre", "Generate Pseudo": "Generar Pseudónimo",
        "Generate Description": "Generar Descripción", "Generate Title": "Generar Título",
        "Step 1": "Paso 1", "Step 2": "Paso 2", "Step 3": "Paso 3", "Step 4": "Paso 4",
        "Basics": "Básicos", "Image": "Imagen", "Voice Input": "Entrada de Voz", "Listening": "Escuchando...",
        "Map Upload": "Subir Mapa", "Cover Image": "Imagen de Portada",
        "Character Sheet": "Ficha de Personaje", "Campaign Details": "Detalles de Campaña",
        "AI Generate": "Aleatorio", "Generate AI Description": "Generar Descripción",
        "Regenerate": "Regenerar", "Generate Description": "Generar Descripción",
        "Randomize": "Aleatorio", "Random": "Aleatorio",
        "Create Campaign": "Crear Campaña", "Create Session": "Crear Sesión",
        "Create Event": "Crear Evento", "Create Location": "Crear Lugar",
        "New Session": "Nueva Sesión", "New Event": "Nuevo Evento", "New Location": "Nuevo Lugar",
        // Map
        "Campaign Map": "Mapa de Campaña", "Interactive Map": "Mapa Interactivo",
        "Click on map to place pin": "Haz clic en el mapa para colocar un pin",
        "No map": "Sin mapa", "Upload a map to get started": "Sube un mapa para comenzar",
        "Add pin": "Añadir pin", "Edit pin": "Editar pin", "Pin list": "Lista de pins",
        "Search": "Buscar", "Name": "Nombre", "Type": "Tipo", "Description": "Descripción",
        "Save": "Guardar", "Delete this pin": "Eliminar este pin", "Cancel": "Cancelar",
        "Enter a name": "Introduce un nombre", "Click on the map first": "Haz clic primero en el mapa",
        "Delete this pin?": "¿Eliminar este pin?", "Error": "Error",
        "Upload map": "Subir mapa", "Upload error": "Error al subir"
    },
    
    // German
    "de": {
        "Dashboard": "Dashboard", "Campaigns": "Kampagnen", "Settings": "Einstellungen",
        "Logout": "Abmelden", "Login": "Anmelden", "Register": "Registrieren",
        "Profile": "Profil", "Help": "Hilfe", "Home": "Startseite",
        "Characters": "Charaktere", "Sessions": "Sitzungen", "Events": "Ereignisse",
        "Locations": "Orte", "Forum": "Forum", "Maps": "Karten",
        "Save": "Speichern", "Save Changes": "Änderungen speichern", "Save Preferences": "Einstellungen speichern",
        "Cancel": "Abbrechen", "Delete": "Löschen", "Edit": "Bearbeiten", "Create": "Erstellen",
        "Update": "Aktualisieren", "Next": "Weiter", "Previous": "Zurück", "Submit": "Absenden",
        "Close": "Schließen", "Search": "Suchen", "Filter": "Filter", "Reset": "Zurücksetzen",
        "Confirm": "Bestätigen", "Add": "Hinzufügen", "Remove": "Entfernen", "Clear": "Löschen",
        "Back": "Zurück", "Continue": "Fortsetzen", "Finish": "Beenden",
        "Create Character": "Charakter erstellen", "Character Name": "Charaktername",
        "Name": "Name", "Surname / Pseudo": "Nachname / Pseudonym", "Pseudo": "Pseudonym",
        "Description": "Beschreibung", "Race": "Rasse", "Class": "Klasse", "Role": "Rolle",
        "Type": "Typ", "Status": "Status", "NPC Role": "NSC-Rolle", "Stats": "Werte",
        "Points Remaining": "Verbleibende Punkte", "Randomize Stats": "Zufällige Werte",
        "Strength": "Stärke", "Agility": "Beweglichkeit", "Health": "Gesundheit",
        "Intelligence": "Intelligenz", "Charisma": "Charisma", "Endurance": "Ausdauer",
        "Player Character": "Spielercharakter", "NPC": "NSC",
        "Alive": "Lebendig", "Dead": "Tot", "Undead": "Untot",
        "Loading": "Laden...", "Generating with AI": "Generiere mit KI...",
        "Language": "Sprache", "Avatar": "Avatar", "Account": "Konto",
        "Regenerate": "Regenerieren", "Generate Description": "Beschreibung generieren",
        "Random": "Zufällig", "Randomize": "Zufällig",
        "Create Campaign": "Kampagne erstellen", "Create Session": "Sitzung erstellen",
        "Create Event": "Ereignis erstellen", "Create Location": "Ort erstellen",
        "New Session": "Neue Sitzung", "New Event": "Neues Ereignis", "New Location": "Neuer Ort",
        // Map
        "Campaign Map": "Kampagnenkarte", "Interactive Map": "Interaktive Karte",
        "Click on map to place pin": "Klicke auf die Karte, um einen Pin zu setzen",
        "No map": "Keine Karte", "Upload a map to get started": "Lade eine Karte hoch um zu beginnen",
        "Add pin": "Pin hinzufügen", "Edit pin": "Pin bearbeiten", "Pin list": "Pin-Liste",
        "Search": "Suchen", "Name": "Name", "Type": "Typ", "Description": "Beschreibung",
        "Save": "Speichern", "Delete this pin": "Diesen Pin löschen", "Cancel": "Abbrechen",
        "Enter a name": "Name eingeben", "Click on the map first": "Zuerst auf die Karte klicken",
        "Delete this pin?": "Diesen Pin löschen?", "Error": "Fehler",
        "Upload map": "Karte hochladen", "Upload error": "Upload-Fehler"
    },
    
    // Italian
    "it": {
        "Dashboard": "Cruscotto", "Campaigns": "Campagne", "Settings": "Impostazioni",
        "Logout": "Esci", "Login": "Accedi", "Register": "Registrati",
        "Profile": "Profilo", "Help": "Aiuto", "Home": "Home",
        "Characters": "Personaggi", "Sessions": "Sessioni", "Events": "Eventi",
        "Locations": "Luoghi", "Forum": "Forum", "Maps": "Mappe",
        "Save": "Salva", "Save Changes": "Salva modifiche", "Cancel": "Annulla",
        "Delete": "Elimina", "Edit": "Modifica", "Create": "Crea",
        "Next": "Avanti", "Previous": "Indietro", "Submit": "Invia",
        "Create Character": "Crea Personaggio", "Character Name": "Nome Personaggio",
        "Name": "Nome", "Surname / Pseudo": "Cognome / Pseudonimo", "Pseudo": "Pseudonimo",
        "Description": "Descrizione", "Race": "Razza", "Class": "Classe", "Role": "Ruolo",
        "Strength": "Forza", "Agility": "Agilità", "Health": "Salute",
        "Intelligence": "Intelligenza", "Charisma": "Carisma", "Endurance": "Resistenza",
        "Player Character": "Personaggio Giocatore", "NPC": "PNG",
        "Alive": "Vivo", "Dead": "Morto", "Undead": "Non Morto",
        "Loading": "Caricamento...", "Language": "Lingua", "Avatar": "Avatar",
        "AI Generate": "Casuale", "Generate AI Description": "Genera Descrizione",
        "Randomize": "Casuale", "Regenerate": "Rigenera",
        "Generate Description": "Genera Descrizione", "Random": "Casuale",
        "Create Campaign": "Crea Campagna", "Create Session": "Crea Sessione",
        "Create Event": "Crea Evento", "Create Location": "Crea Luogo",
        "New Session": "Nuova Sessione", "New Event": "Nuovo Evento", "New Location": "Nuovo Luogo"
    },
    
    // Dutch
    "nl": {
        "Dashboard": "Dashboard", "Campaigns": "Campagnes", "Settings": "Instellingen",
        "Logout": "Uitloggen", "Login": "Inloggen", "Register": "Registreren",
        "Profile": "Profiel", "Help": "Help", "Home": "Home",
        "Characters": "Personages", "Sessions": "Sessies", "Events": "Evenementen",
        "Locations": "Locaties", "Forum": "Forum", "Maps": "Kaarten",
        "Save": "Opslaan", "Save Changes": "Wijzigingen opslaan", "Cancel": "Annuleren",
        "Delete": "Verwijderen", "Edit": "Bewerken", "Create": "Aanmaken",
        "Next": "Volgende", "Previous": "Vorige", "Submit": "Verzenden",
        "Create Character": "Personage maken", "Character Name": "Personagenaam",
        "Name": "Naam", "Description": "Beschrijving", "Race": "Ras", "Class": "Klasse", "Role": "Rol",
        "Strength": "Kracht", "Agility": "Behendigheid", "Health": "Gezondheid",
        "Intelligence": "Intelligentie", "Charisma": "Charisma", "Endurance": "Uithoudingsvermogen",
        "Loading": "Laden...", "Language": "Taal", "Avatar": "Avatar",
        "AI Generate": "Willekeurig", "Generate AI Description": "Genereer Beschrijving",
        "Randomize": "Willekeurig", "Regenerate": "Hergenereer",
        "Generate Description": "Beschrijving genereren", "Random": "Willekeurig",
        "Create Campaign": "Campagne aanmaken", "Create Session": "Sessie aanmaken",
        "Create Event": "Evenement aanmaken", "Create Location": "Locatie aanmaken",
        "New Session": "Nieuwe Sessie", "New Event": "Nieuw Evenement", "New Location": "Nieuwe Locatie"
    },
    
    // Portuguese
    "pt": {
        "Dashboard": "Painel", "Campaigns": "Campanhas", "Settings": "Configurações",
        "Logout": "Sair", "Login": "Entrar", "Register": "Registrar",
        "Profile": "Perfil", "Help": "Ajuda", "Home": "Início",
        "Characters": "Personagens", "Sessions": "Sessões", "Events": "Eventos",
        "Save": "Salvar", "Save Changes": "Salvar Alterações", "Cancel": "Cancelar",
        "Create Character": "Criar Personagem", "Character Name": "Nome do Personagem",
        "Name": "Nome", "Description": "Descrição", "Race": "Raça", "Class": "Classe", "Role": "Função",
        "Strength": "Força", "Agility": "Agilidade", "Health": "Saúde",
        "Intelligence": "Inteligência", "Charisma": "Carisma", "Endurance": "Resistência",
        "Loading": "Carregando...", "Language": "Idioma",
        "AI Generate": "Aleatório", "Generate AI Description": "Gerar Descrição",
        "Randomize": "Aleatório", "Regenerate": "Regenerar",
        "Generate Description": "Gerar Descrição", "Random": "Aleatório",
        "Create Campaign": "Criar Campanha", "Create Session": "Criar Sessão",
        "Create Event": "Criar Evento", "Create Location": "Criar Local",
        "New Session": "Nova Sessão", "New Event": "Novo Evento", "New Location": "Novo Local"
    },
    
    // Polish
    "pl": {
        "Dashboard": "Panel", "Campaigns": "Kampanie", "Settings": "Ustawienia",
        "Logout": "Wyloguj", "Login": "Zaloguj", "Register": "Zarejestruj",
        "Profile": "Profil", "Help": "Pomoc", "Home": "Strona główna",
        "Save": "Zapisz", "Save Changes": "Zapisz zmiany", "Cancel": "Anuluj",
        "Create Character": "Utwórz postać", "Name": "Nazwa", "Description": "Opis",
        "Race": "Rasa", "Class": "Klasa", "Role": "Rola",
        "Strength": "Siła", "Agility": "Zręczność", "Health": "Zdrowie",
        "Intelligence": "Inteligencja", "Charisma": "Charyzma", "Endurance": "Wytrzymałość",
        "Loading": "Ładowanie...", "Language": "Język",
        "AI Generate": "Losowy", "Generate AI Description": "Generuj Opis",
        "Randomize": "Losowy", "Regenerate": "Przegeneruj",
        "Generate Description": "Generuj Opis", "Random": "Losowy",
        "Create Campaign": "Utwórz Kampanię", "Create Session": "Utwórz Sesję",
        "Create Event": "Utwórz Wydarzenie", "Create Location": "Utwórz Lokację",
        "New Session": "Nowa Sesja", "New Event": "Nowe Wydarzenie", "New Location": "Nowa Lokacja"
    },
     
    // Russian
    "ru": {
        "Dashboard": "Панель управления", "Campaigns": "Кампании", "Settings": "Настройки",
        "Logout": "Выход", "Login": "Вход", "Register": "Регистрация",
        "Profile": "Профиль", "Help": "Помощь", "Home": "Главная",
        "Save": "Сохранить", "Save Changes": "Сохранить изменения", "Cancel": "Отмена",
        "Create Character": "Создать персонажа", "Name": "Имя", "Description": "Описание",
        "Race": "Раса", "Class": "Класс", "Role": "Роль",
        "Strength": "Сила", "Agility": "Ловкость", "Health": "Здоровье",
        "Intelligence": "Интеллект", "Charisma": "Харизма", "Endurance": "Выносливость",
        "Loading": "Загрузка...", "Language": "Язык",
        "AI Generate": "Случайное", "Generate AI Description": "Создать Описание",
        "Randomize": "Случайное", "Regenerate": "Пересоздать",
        "Generate Description": "Создать Описание", "Random": "Случайное",
        "Create Campaign": "Создать Кампанию", "Create Session": "Создать Сессию",
        "Create Event": "Создать Событие", "Create Location": "Создать Локацию",
        "New Session": "Новая Сессия", "New Event": "Новое Событие", "New Location": "Новая Локация"
    },
    
    // Japanese
    "ja": {
        "Dashboard": "ダッシュボード", "Campaigns": "キャンペーン", "Settings": "設定",
        "Logout": "ログアウト", "Login": "ログイン", "Register": "登録",
        "Profile": "プロフィール", "Help": "ヘルプ", "Home": "ホーム",
        "Save": "保存", "Save Changes": "変更を保存", "Cancel": "キャンセル",
        "Create Character": "キャラクター作成", "Name": "名前", "Description": "説明",
        "Race": "種族", "Class": "クラス", "Role": "役割",
        "Strength": "力", "Agility": "敏捷性", "Health": "体力",
        "Intelligence": "知力", "Charisma": "魅力", "Endurance": "耐久力",
        "Loading": "読み込み中...", "Language": "言語",
        "AI Generate": "ランダム", "Generate AI Description": "説明生成",
        "Randomize": "ランダム", "Regenerate": "再生成",
        "Generate Description": "説明生成", "Random": "ランダム",
        "Create Campaign": "キャンペーン作成", "Create Session": "セッション作成",
        "Create Event": "イベント作成", "Create Location": "ロケーション作成",
        "New Session": "新しいセッション", "New Event": "新しいイベント", "New Location": "新しいロケーション"
    },
    
    // Chinese
    "zh": {
        "Dashboard": "仪表板", "Campaigns": "战役", "Settings": "设置",
        "Logout": "退出", "Login": "登录", "Register": "注册",
        "Profile": "个人资料", "Help": "帮助", "Home": "首页",
        "Save": "保存", "Save Changes": "保存更改", "Cancel": "取消",
        "Create Character": "创建角色", "Name": "名称", "Description": "描述",
        "Race": "种族", "Class": "职业", "Role": "角色",
        "Strength": "力量", "Agility": "敏捷", "Health": "生命",
        "Intelligence": "智力", "Charisma": "魅力", "Endurance": "耐力",
        "Loading": "加载中...", "Language": "语言",
        "AI Generate": "随机", "Generate AI Description": "生成描述",
        "Randomize": "随机", "Regenerate": "重新生成",
        "Generate Description": "生成描述", "Random": "随机",
        "Create Campaign": "创建战役", "Create Session": "创建会议",
        "Create Event": "创建事件", "Create Location": "创建位置",
        "New Session": "新会议", "New Event": "新事件", "New Location": "新位置"
    },
    
    // Korean
    "ko": {
        "Dashboard": "대시보드", "Campaigns": "캠페인", "Settings": "설정",
        "Logout": "로그아웃", "Login": "로그인", "Register": "등록",
        "Profile": "프로필", "Help": "도움말", "Home": "홈",
        "Save": "저장", "Save Changes": "변경 사항 저장", "Cancel": "취소",
        "Create Character": "캐릭터 생성", "Name": "이름", "Description": "설명",
        "Race": "종족", "Class": "클래스", "Role": "역할",
        "Strength": "힘", "Agility": "민첩", "Health": "체력",
        "Intelligence": "지능", "Charisma": "매력", "Endurance": "지구력",
        "Loading": "로딩 중...", "Language": "언어",
        "AI Generate": "무작위", "Generate AI Description": "설명 생성",
        "Randomize": "무작위", "Regenerate": "재생성",
        "Generate Description": "설명 생성", "Random": "무작위",
        "Create Campaign": "캠페인 생성", "Create Session": "세션 생성",
        "Create Event": "이벤트 생성", "Create Location": "위치 생성",
        "New Session": "새 세션", "New Event": "새 이벤트", "New Location": "새 위치"
    },
    
    // Arabic
    "ar": {
        "Dashboard": "لوحة القيادة", "Campaigns": "الحملات", "Settings": "الإعدادات",
        "Logout": "تسجيل الخروج", "Login": "تسجيل الدخول", "Register": "التسجيل",
        "Profile": "الملف الشخصي", "Help": "مساعدة", "Home": "الرئيسية",
        "Save": "حفظ", "Save Changes": "حفظ التغييرات", "Cancel": "إلغاء",
        "Create Character": "إنشاء شخصية", "Name": "الاسم", "Description": "الوصف",
        "Race": "العرق", "Class": "الفئة", "Role": "الدور",
        "Strength": "القوة", "Agility": "الرشاقة", "Health": "الصحة",
        "Intelligence": "الذكاء", "Charisma": "الكاريزما", "Endurance": "التحمل",
        "Loading": "جاري التحميل...", "Language": "اللغة",
        "Random": "عشوائي", "Regenerate": "إعادة إنشاء", "Generate Description": "إنشاء وصف",
        "Create Campaign": "إنشاء حملة", "Create Session": "إنشاء جلسة",
        "Create Event": "إنشاء حدث", "Create Location": "إنشاء موقع",
        "New Session": "جلسة جديدة", "New Event": "حدث جديد", "New Location": "موقع جديد"
    },
    
    // Turkish
    "tr": {
        "Dashboard": "Kontrol Paneli", "Campaigns": "Kampanyalar", "Settings": "Ayarlar",
        "Logout": "Çıkış", "Login": "Giriş", "Register": "Kayıt",
        "Profile": "Profil", "Help": "Yardım", "Home": "Ana Sayfa",
        "Save": "Kaydet", "Save Changes": "Değişiklikleri Kaydet", "Cancel": "İptal",
        "Create Character": "Karakter Oluştur", "Name": "İsim", "Description": "Açıklama",
        "Race": "Irk", "Class": "Sınıf", "Role": "Rol",
        "Strength": "Güç", "Agility": "Çeviklik", "Health": "Sağlık",
        "Intelligence": "Zeka", "Charisma": "Karizma", "Endurance": "Dayanıklılık",
        "Loading": "Yükleniyor...", "Language": "Dil",
        "Random": "Rastgele", "Regenerate": "Yeniden Oluştur", "Generate Description": "Açıklama Oluştur",
        "Create Campaign": "Kampanya Oluştur", "Create Session": "Oturum Oluştur",
        "Create Event": "Etkinlik Oluştur", "Create Location": "Konum Oluştur",
        "New Session": "Yeni Oturum", "New Event": "Yeni Etkinlik", "New Location": "Yeni Konum"
    },
    
    // Czech
    "cs": {
        "Dashboard": "Nástěnka", "Campaigns": "Kampaně", "Settings": "Nastavení",
        "Logout": "Odhlásit", "Login": "Přihlásit", "Register": "Registrovat",
        "Profile": "Profil", "Help": "Nápověda", "Home": "Domů",
        "Save": "Uložit", "Save Changes": "Uložit změny", "Cancel": "Zrušit",
        "Create Character": "Vytvořit postavu", "Name": "Jméno", "Description": "Popis",
        "Race": "Rasa", "Class": "Třída", "Role": "Role",
        "Strength": "Síla", "Agility": "Obratnost", "Health": "Zdraví",
        "Intelligence": "Inteligence", "Charisma": "Charisma", "Endurance": "Vytrvalost",
        "Loading": "Načítání...", "Language": "Jazyk",
        "Random": "Náhodné", "Regenerate": "Regenerovat", "Generate Description": "Generovat popis",
        "Create Campaign": "Vytvořit kampaň", "Create Session": "Vytvořit relaci",
        "Create Event": "Vytvořit událost", "Create Location": "Vytvořit místo",
        "New Session": "Nová relace", "New Event": "Nová událost", "New Location": "Nové místo"
    },
    
    // Danish
    "da": {
        "Dashboard": "Kontrolpanel", "Campaigns": "Kampagner", "Settings": "Indstillinger",
        "Logout": "Log ud", "Login": "Log ind", "Register": "Registrer",
        "Save": "Gem", "Save Changes": "Gem ændringer", "Cancel": "Annuller",
        "Create Character": "Opret karakter", "Name": "Navn", "Description": "Beskrivelse",
        "Race": "Race", "Class": "Klasse", "Role": "Rolle",
        "Loading": "Indlæser...", "Language": "Sprog",
        "Random": "Tilfældig", "Regenerate": "Regenerer", "Generate Description": "Generer beskrivelse",
        "Create Campaign": "Opret kampagne", "Create Session": "Opret session",
        "Create Event": "Opret begivenhed", "Create Location": "Opret sted",
        "New Session": "Ny session", "New Event": "Ny begivenhed", "New Location": "Nyt sted"
    },
    
    // Greek
    "el": {
        "Dashboard": "Πίνακας Ελέγχου", "Campaigns": "Εκστρατείες", "Settings": "Ρυθμίσεις",
        "Logout": "Αποσύνδεση", "Login": "Σύνδεση", "Register": "Εγγραφή",
        "Save": "Αποθήκευση", "Save Changes": "Αποθήκευση αλλαγών", "Cancel": "Ακύρωση",
        "Create Character": "Δημιουργία Χαρακτήρα", "Name": "Όνομα", "Description": "Περιγραφή",
        "Race": "Ράτσα", "Class": "Τάξη", "Role": "Ρόλος",
        "Loading": "Φόρτωση...", "Language": "Γλώσσα"
    },
    
    // Finnish
    "fi": {
        "Dashboard": "Kojelauta", "Campaigns": "Kampanjat", "Settings": "Asetukset",
        "Logout": "Kirjaudu ulos", "Login": "Kirjaudu sisään", "Register": "Rekisteröidy",
        "Save": "Tallenna", "Save Changes": "Tallenna muutokset", "Cancel": "Peruuta",
        "Create Character": "Luo hahmo", "Name": "Nimi", "Description": "Kuvaus",
        "Race": "Rotu", "Class": "Luokka", "Role": "Rooli",
        "Loading": "Lataa...", "Language": "Kieli"
    },
    
    // Hebrew
    "he": {
        "Dashboard": "לוח בקרה", "Campaigns": "מערכות", "Settings": "הגדרות",
        "Logout": "התנתק", "Login": "התחבר", "Register": "הירשם",
        "Save": "שמור", "Save Changes": "שמור שינויים", "Cancel": "ביטול",
        "Create Character": "צור דמות", "Name": "שם", "Description": "תיאור",
        "Race": "גזע", "Class": "מחלק", "Role": "תפקיד",
        "Loading": "טוען...", "Language": "שפה"
    },
    
    // Hindi
    "hi": {
        "Dashboard": "डैशबोर्ड", "Campaigns": "अभियान", "Settings": "सेटिंग्स",
        "Logout": "लॉग आउट", "Login": "लॉग इन", "Register": "पंजीकरण",
        "Save": "सहेजें", "Save Changes": "परिवर्तन सहेजें", "Cancel": "रद्द करें",
        "Create Character": "चरित्र बनाएं", "Name": "नाम", "Description": "विवरण",
        "Race": "जाति", "Class": "वर्ग", "Role": "भूमिका",
        "Loading": "लोड हो रहा है...", "Language": "भाषा"
    },
    
    // Hungarian
    "hu": {
        "Dashboard": "Vezérlőpult", "Campaigns": "Kampányok", "Settings": "Beállítások",
        "Logout": "Kijelentkezés", "Login": "Bejelentkezés", "Register": "Regisztráció",
        "Save": "Mentés", "Save Changes": "Változások mentése", "Cancel": "Mégse",
        "Create Character": "Karakter létrehozása", "Name": "Név", "Description": "Leírás",
        "Race": "Faj", "Class": "Osztály", "Role": "Szerep",
        "Loading": "Betöltés...", "Language": "Nyelv"
    },
    
    // Indonesian
    "id": {
        "Dashboard": "Dasbor", "Campaigns": "Kampanye", "Settings": "Pengaturan",
        "Logout": "Keluar", "Login": "Masuk", "Register": "Daftar",
        "Save": "Simpan", "Save Changes": "Simpan Perubahan", "Cancel": "Batal",
        "Create Character": "Buat Karakter", "Name": "Nama", "Description": "Deskripsi",
        "Race": "Ras", "Class": "Kelas", "Role": "Peran",
        "Strength": "Kekuatan", "Agility": "Kelincahan", "Health": "Kesehatan",
        "Intelligence": "Kecerdasan", "Charisma": "Karakisma", "Endurance": "Ketahanan",
        "Loading": "Memuat...", "Language": "Bahasa"
    },
    
    // Malay
    "ms": {
        "Dashboard": "Papan Pemuka", "Campaigns": "Kempen", "Settings": "Tetapan",
        "Logout": "Log Keluar", "Login": "Log Masuk", "Register": "Daftar",
        "Save": "Simpan", "Save Changes": "Simpan Perubahan", "Cancel": "Batal",
        "Create Character": "Cipta Watak", "Name": "Nama", "Description": "Penerangan",
        "Race": "Bangsa", "Class": "Kelas", "Role": "Peranan",
        "Loading": "Memuatkan...", "Language": "Bahasa"
    },
    
    // Norwegian
    "no": {
        "Dashboard": "Kontrollpanel", "Campaigns": "Kampanjer", "Settings": "Innstillinger",
        "Logout": "Logg ut", "Login": "Logg inn", "Register": "Registrer",
        "Save": "Lagre", "Save Changes": "Lagre endringer", "Cancel": "Avbryt",
        "Create Character": "Opprett karakter", "Name": "Navn", "Description": "Beskrivelse",
        "Race": "Rase", "Class": "Klasse", "Role": "Rolle",
        "Loading": "Laster...", "Language": "Språk"
    },
    
    // Romanian
    "ro": {
        "Dashboard": "Panou de control", "Campaigns": "Campanii", "Settings": "Setări",
        "Logout": "Deconectare", "Login": "Conectare", "Register": "Înregistrare",
        "Save": "Salvează", "Save Changes": "Salvează modificările", "Cancel": "Anulare",
        "Create Character": "Creează personaj", "Name": "Nume", "Description": "Descriere",
        "Race": "Rasă", "Class": "Clasă", "Role": "Rol",
        "Loading": "Se încarcă...", "Language": "Limbă"
    },
    
    // Swedish
    "sv": {
        "Dashboard": "Kontrollpanel", "Campaigns": "Kampanjer", "Settings": "Inställningar",
        "Logout": "Logga ut", "Login": "Logga in", "Register": "Registrera",
        "Save": "Spara", "Save Changes": "Spara ändringar", "Cancel": "Avbryt",
        "Create Character": "Skapa karaktär", "Name": "Namn", "Description": "Beskrivning",
        "Race": "Ras", "Class": "Klass", "Role": "Roll",
        "Loading": "Laddar...", "Language": "Språk"
    },
    
    // Thai
    "th": {
        "Dashboard": "แดชบอร์ด", "Campaigns": "แคมเปญ", "Settings": "การตั้งค่า",
        "Logout": "ออกจากระบบ", "Login": "เข้าสู่ระบบ", "Register": "สมัครสมาชิก",
        "Save": "บันทึก", "Save Changes": "บันทึกการเปลี่ยนแปลง", "Cancel": "ยกเลิก",
        "Create Character": "สร้างตัวละคร", "Name": "ชื่อ", "Description": "คำอธิบาย",
        "Race": "เผ่าพันธุ์", "Class": "คลาส", "Role": "บทบาท",
        "Loading": "กำลังโหลด...", "Language": "ภาษา"
    },
    
    // Ukrainian
    "uk": {
        "Dashboard": "Панель управління", "Campaigns": "Кампанії", "Settings": "Налаштування",
        "Logout": "Вийти", "Login": "Увійти", "Register": "Реєстрація",
        "Save": "Зберегти", "Save Changes": "Зберегти зміни", "Cancel": "Скасувати",
        "Create Character": "Створити персонажа", "Name": "Ім'я", "Description": "Опис",
        "Race": "Раса", "Class": "Клас", "Role": "Роль",
        "Loading": "Завантаження...", "Language": "Мова"
    },
    
    // Vietnamese
    "vi": {
        "Dashboard": "Bảng điều khiển", "Campaigns": "Chiến dịch", "Settings": "Cài đặt",
        "Logout": "Đăng xuất", "Login": "Đăng nhập", "Register": "Đăng ký",
        "Save": "Lưu", "Save Changes": "Lưu thay đổi", "Cancel": "Hủy",
        "Create Character": "Tạo nhân vật", "Name": "Tên", "Description": "Mô tả",
        "Race": "Chủng tộc", "Class": "Lớp", "Role": "Vai trò",
        "Loading": "Đang tải...", "Language": "Ngôn ngữ"
    }
};

class UITranslator {
    constructor() {
        this.currentLang = localStorage.getItem('preferredLanguage') || 'en';
        this.init();
    }

    init() {
        // Apply language on load
        this.applyLanguage(this.currentLang);
        
        // Listen for storage changes (multi-tab)
        window.addEventListener('storage', (e) => {
            if (e.key === 'preferredLanguage' && e.newValue !== this.currentLang) {
                this.applyLanguage(e.newValue);
            }
        });
        
        // Listen for custom language change events
        window.addEventListener('languageChanged', (e) => {
            if (e.detail && e.detail.language) {
                this.applyLanguage(e.detail.language);
            }
        });
    }

    applyLanguage(lang) {
        this.currentLang = lang || 'en';
        localStorage.setItem('preferredLanguage', this.currentLang);
        document.documentElement.lang = this.currentLang;
        document.documentElement.setAttribute('dir', 
            this.currentLang === 'ar' || this.currentLang === 'he' ? 'rtl' : 'ltr');
        
        // Translate page
        this.translatePage();
        
        // Dispatch event
        window.dispatchEvent(new CustomEvent('languageApplied', { 
            detail: { language: this.currentLang } 
        }));
    }

    translatePage() {
        const dict = UIDictionary[this.currentLang] || UIDictionary['en'];
        const fallbackDict = UIDictionary['en'];
        
        // Translate elements with data-t attribute (most reliable)
        document.querySelectorAll('[data-t]').forEach(el => {
            const key = el.getAttribute('data-t');
            if (dict[key]) {
                el.textContent = dict[key];
            }
        });
        
        // Walk through all text nodes
        const walker = document.createTreeWalker(
            document.body,
            NodeFilter.SHOW_TEXT,
            {
                acceptNode: function(node) {
                    if (node.parentElement && 
                        (node.parentElement.tagName === 'SCRIPT' || 
                         node.parentElement.tagName === 'STYLE' ||
                         node.parentElement.tagName === 'CODE' ||
                         node.parentElement.tagName === 'PRE')) {
                        return NodeFilter.FILTER_REJECT;
                    }
                    return NodeFilter.FILTER_ACCEPT;
                }
            },
            false
        );

        let node;
        const textNodes = [];
        while (node = walker.nextNode()) {
            textNodes.push(node);
        }

        textNodes.forEach(textNode => {
            const text = textNode.nodeValue.trim();
            if (!text) return;
            
            // Check if this is a translatable string
            if (fallbackDict[text]) {
                const translated = dict[text] || fallbackDict[text];
                if (translated !== text) {
                    textNode.nodeValue = textNode.nodeValue.replace(text, translated);
                }
            }
        });

        // Translate placeholders
        document.querySelectorAll('[placeholder]').forEach(el => {
            const original = el.dataset.originalPlaceholder || el.getAttribute('placeholder');
            if (original && fallbackDict[original]) {
                if (!el.dataset.originalPlaceholder) {
                    el.dataset.originalPlaceholder = original;
                }
                const translated = dict[original] || fallbackDict[original];
                el.setAttribute('placeholder', translated);
            }
        });
        
        // Translate title attributes
        document.querySelectorAll('[title]').forEach(el => {
            const original = el.dataset.originalTitle || el.getAttribute('title');
            if (original && fallbackDict[original]) {
                if (!el.dataset.originalTitle) {
                    el.dataset.originalTitle = original;
                }
                const translated = dict[original] || fallbackDict[original];
                el.setAttribute('title', translated);
            }
        });
        
        // Translate aria-label attributes
        document.querySelectorAll('[aria-label]').forEach(el => {
            const original = el.dataset.originalAriaLabel || el.getAttribute('aria-label');
            if (original && fallbackDict[original]) {
                if (!el.dataset.originalAriaLabel) {
                    el.dataset.originalAriaLabel = original;
                }
                const translated = dict[original] || fallbackDict[original];
                el.setAttribute('aria-label', translated);
            }
        });
        
        // Translate button values
        document.querySelectorAll('input[type="submit"], input[type="button"]').forEach(el => {
            const original = el.dataset.originalValue || el.value;
            if (original && fallbackDict[original]) {
                if (!el.dataset.originalValue) {
                    el.dataset.originalValue = original;
                }
                const translated = dict[original] || fallbackDict[original];
                el.value = translated;
            }
        });
    }
    
    // Get translated text
    translate(key) {
        const dict = UIDictionary[this.currentLang] || UIDictionary['en'];
        return dict[key] || UIDictionary['en'][key] || key;
    }
    
    // Add translations dynamically
    addTranslations(lang, translations) {
        if (!UIDictionary[lang]) {
            UIDictionary[lang] = {};
        }
        Object.assign(UIDictionary[lang], translations);
    }
}

// Initialize translator
const Translator = new UITranslator();
window.UITranslator = Translator;
window.UIDictionary = UIDictionary;
