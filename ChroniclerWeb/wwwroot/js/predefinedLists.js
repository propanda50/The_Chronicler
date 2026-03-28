// ============ TheChronicler Random Name Generator ============
// Self-contained multi-language random generators

// Get current language code
function getLang() {
    return (localStorage.getItem('preferredLanguage') || document.documentElement.lang || 'en').split('-')[0];
}

// Multi-language dictionaries
const DICTS = {
    en: {
        prefixes: ['The', 'Ancient', 'Lost', 'Hidden', 'Dark', 'Golden', 'Silver', 'Frozen', 'Burning', 'Shadow', 'Crystal', 'Iron', 'Storm', 'Star', 'Moon', 'Sun', 'Blood', 'Bone', 'Eternal', 'Forbidden', 'Sacred', 'Divine', 'Verdant', 'Crimson', 'Rising', 'Fallen', 'Twisted', 'Shattered'],
        nouns: ['Kingdom', 'Realm', 'Empire', 'Chronicles', 'Saga', 'Quest', 'Journey', 'Legacy', 'Prophecy', 'Legend', 'Myth', 'Odyssey', 'Epic', 'Adventure', 'War', 'Peace', 'Dawn', 'Twilight', 'Eternity', 'Destiny', 'Fate', 'Glory', 'Victory', 'Conquest', 'Dominion', 'Genesis', 'Apocalypse'],
        suffixes: ['of Shadows', 'of Light', 'of Dreams', 'of Doom', 'of Hope', 'of the Ancients', 'of the Lost', 'of the Damned', 'of the Forgotten', 'of the Stars', 'of the Moon', 'of the Sun', 'of Winter', 'of Summer', 'of the Abyss', 'of the Heavens'],
        sessionNouns: ['Session', 'Chapter', 'Episode', 'Discovery', 'Journey', 'Quest', 'Trial', 'Challenge', 'Victory', 'Defeat', 'Turning Point', 'Climax', 'Resolution', 'Mystery', 'Encounter', 'Battle', 'Awakening', 'Finale', 'Prologue', 'Crossroads', 'Threshold'],
        sessionAdj: ['First', 'Opening', 'Initial', 'Discovery', 'Confrontation', 'Trial', 'Dark', 'Shadow', 'Hidden', 'Lost', 'Rising', 'Final', 'Turning'],
        eventTypes: ['Festival', 'Ceremony', 'Battle', 'Ritual', 'Gathering', 'Council', 'Tournament', 'Market', 'Invasion', 'Alliance', 'Treaty', 'Celebration', 'Rebellion', 'Revolution', 'Eclipse', 'Storm', 'Cataclysm', 'Summons', 'Hunt', 'Expedition'],
        locTypes: ['Citadel', 'Temple', 'Tavern', 'Forest', 'Cave', 'Tower', 'Keep', 'Sanctuary', 'Ruins', 'Port', 'Village', 'City', 'Dungeon', 'Shrine', 'Palace', 'Library', 'Arena', 'Mount', 'Valley', 'Desert', 'Island', 'Volcano', 'Swamp', 'Canyon', 'Waterfall', 'Lake', 'River', 'Ocean', 'Fortress', 'Haven'],
        charFirst: {human: ['Marcus', 'Elena', 'Gareth', 'Lyra', 'Aldric', 'Brenna', 'Cedric', 'Dara', 'Eldric', 'Freya', 'Helena', 'Idris', 'Kael', 'Luna', 'Magnus', 'Nadia', 'Orion', 'Petra', 'Rowan', 'Soren'], elf: ['Elara', 'Thalion', 'Aelindra', 'Silvanus', 'Miriel', 'Galadriel', 'Legolas', 'Arwen', 'Luthien', 'Beren', 'Elrond', 'Faelar'], dwarf: ['Thorin', 'Balin', 'Dwalin', 'Fili', 'Kili', 'Gimli', 'Durin', 'Gloin', 'Oin', 'Bofur', 'Bombur', 'Nori'], orc: ['Grom', 'Thrash', 'Ugluk', 'Mogul', 'Gashnak', 'Grishnak', 'Lurtz', 'Bolg', 'Azog', 'Boldog'], halfling: ['Pip', 'Rosie', 'Milo', 'Bramble', 'Frodo', 'Bilbo', 'Sam', 'Merry', 'Pippin', 'Cotton'], tiefling: ['Zariel', 'Xaphan', 'Vexia', 'Riftan', 'Morthos', 'Nemeia', 'Zarathos', 'Orpheus', 'Lilith'], dragonborn: ['Draconis', 'Sylvana', 'Pyrrus', 'Balasar', 'Kriv', 'Mishann', 'Nadarr', 'Torinn'], gnome: ['Fizzle', 'Pipwhistle', 'Tanglewood', 'Zizzle', 'Bimpnottin', 'Dimble', 'Eldon', 'Gimble'] },
        charLast: ['Sterling', 'Blackwood', 'Stone', 'Whitmore', 'Ironforge', 'Moonwhisper', 'Silverleaf', 'Starweave', 'Goldleaf', 'Shadowmere', 'Brightblade', 'Darkhollow', 'Frostborn', 'Hillcrest', 'Riverdale', 'Stoneheart', 'Stormwind', 'Thornwood', 'Winterborne'],
        pseudos: {fighter: ['The Iron Blade', 'Shield Breaker', 'Steel Wall', 'Battle Master', 'Warlord', 'Champion'], wizard: ['The Arcane Sage', 'Spell Weaver', 'Mystic Oracle', 'Lorekeeper', 'Archmage'], rogue: ['The Shadow Walker', 'Silent Step', 'Night Blade', 'Whisper', 'Ghost', 'Phantom'], cleric: ['The Holy Light', 'Faith Keeper', 'Divine Hand', 'The Blessed', 'Light Bringer'], paladin: ['The Sacred Shield', 'Light Bringer', 'Oathbound', 'The Righteous', 'Divine Protector'], ranger: ['The Wild Hunt', 'Forest Warden', 'Tracker', 'Pathfinder', 'The Lone Wolf'], barbarian: ['The Storm Rage', 'Fury Unleashed', 'Bonecrusher', 'War Cry', 'The Berserker'], bard: ['The Silver Tongue', 'Melody Weaver', 'Charmcaster', 'The Songbird', 'Talespinner'], druid: ['The Forest Voice', "Nature's Wrath", 'Root Warden', 'Grove Keeper', 'The Green'], monk: ['The Silent Fist', 'Inner Peace', 'Storm Palm', 'Zen Master', 'The Enlightened'], sorcerer: ['The Blood Weaver', 'Chaos Spark', 'Wild Magic', 'Arcane Storm', 'The Flames'], warlock: ['The Dark Covenant', 'Pact Keeper', 'Shadow Caller', 'Doom Whisper', 'The Damned'] },
        backgrounds: ['Born in a small village at the edge of the kingdom', 'Raised among the ancient ones of a distant land', 'The child of travelers, never knowing a permanent home', 'Once a member of a prestigious order', 'Raised in the mountains by reclusive masters'],
        stories: ['Through years of training and hardship, they developed exceptional skills', 'Their reputation precedes them, and many tales are told of their exploits', 'They set out to make a name for themselves', 'Their journey has taken them through dark forests and ancient ruins', 'Now they seek new adventures and greater challenges']
    },
    fr: {
        prefixes: ['Le', 'Ancien', 'Perdu', 'Cache', 'Sombre', 'Dore', 'Argente', 'Gele', 'Ardent', 'Ombre', 'Cristal', 'Fer', 'Tempete', 'Etoile', 'Lune', 'Soleil', 'Sang', 'Os', 'Eternel', 'Interdit', 'Sacré', 'Divin', 'Verdoyant', 'Cramoisi', 'Lever', 'Chute', 'Tordus', 'Brise'],
        nouns: ['Royaume', 'Empire', 'Chroniques', 'Saga', 'Quete', 'Voyage', 'Heritage', 'Prophetie', 'Legende', 'Mythe', 'Odyssee', 'Epopee', 'Aventure', 'Guerre', 'Paix', 'Aube', 'Crepuscule', 'Eternite', 'Destin', 'Gloire', 'Victoire', 'Conquete', 'Domination', 'Genese', 'Apocalypse'],
        suffixes: ['d\'Ombre', 'de Lumiere', 'de Reves', 'de Malheur', 'd\'Espoir', 'des Anciens', 'des Perdus', 'des Damnes', 'd\'Oubli', 'des Etoiles', 'de la Lune', 'du Soleil', 'd\'Hiver', 'd\'Ete', 'de l\'Abime', 'des Cieux'],
        sessionNouns: ['Session', 'Chapitre', 'Episode', 'Decouverte', 'Voyage', 'Quete', 'Epreuve', 'Defi', 'Victoire', 'Defaite', 'Tournant', 'Climax', 'Resolution', 'Mystere', 'Rencontre', 'Bataille', 'Eveil', 'Finale', 'Prologue', 'Carrefour', 'Seuil'],
        sessionAdj: ['Premier', 'Ouverture', 'Initial', 'Decouverte', 'Confrontation', 'Epreuve', 'Sombre', 'Ombre', 'Cache', 'Perdu', 'Montant', 'Final', 'Tournant'],
        eventTypes: ['Festival', 'Ceremonie', 'Bataille', 'Rituel', 'Rassemblement', 'Conseil', 'Tournoi', 'Marche', 'Invasion', 'Alliance', 'Traite', 'Celebration', 'Rebellion', 'Revolution', 'Eclipse', 'Tempete', 'Cataclysme', 'Convocation', 'Chasse', 'Expedition'],
        locTypes: ['Citadelle', 'Temple', 'Taverne', 'Foret', 'Caverne', 'Tour', 'Donjon', 'Sanctuaire', 'Ruines', 'Port', 'Village', 'Ville', 'Dungeon', 'Palais', 'Bibliotheque', 'Arene', 'Mont', 'Vallee', 'Desert', 'Ile', 'Volcan', 'Marais', 'Canyon', 'Cascade', 'Lac', 'Riviere', 'Ocean', 'Forteresse', 'Havre'],
        charFirst: {human: ['Marc', 'Eleanor', 'Gerard', 'Lyra', 'Aldric', 'Brenna', 'Cedric', 'Dara', 'Eldric', 'Freya', 'Helene', 'Idris', 'Kael', 'Luna', 'Magnus', 'Nadia', 'Orion', 'Petra', 'Rowan', 'Soren'], elf: ['Elara', 'Thalion', 'Aelindra', 'Silvanus', 'Miriel', 'Galadriel', 'Legolas', 'Arwen', 'Luthien', 'Beren', 'Elrond', 'Faelar'], dwarf: ['Thorin', 'Balin', 'Dwalin', 'Fili', 'Kili', 'Gimli', 'Durin', 'Gloin', 'Oin', 'Bofur', 'Bombur', 'Nori'], orc: ['Grom', 'Thrash', 'Ugluk', 'Mogul', 'Gashnak', 'Grishnak', 'Lurtz', 'Bolg', 'Azog', 'Boldog'], halfling: ['Pip', 'Rosie', 'Milo', 'Bramble', 'Frodon', 'Bilbo', 'Sam', 'Merry', 'Pippin', 'Cotton'], tiefling: ['Zariel', 'Xaphan', 'Vexia', 'Riftan', 'Morthos', 'Nemea', 'Zarathos', 'Orphee', 'Lilith'], dragonborn: ['Draconis', 'Sylvana', 'Pyrrhus', 'Balasar', 'Kriv', 'Mishann', 'Nadarr', 'Torinn'], gnome: ['Fizzle', 'Sifflet', 'Boissec', 'Zizzle', 'Bimpnottin', 'Dimble', 'Eldon', 'Gimble'] },
        charLast: ['Sterling', 'Boisnoir', 'Pierre', 'Blanchet', 'Ferforge', 'Lunemurmure', 'Feuillearg', 'Etoile', 'Feuilledor', 'Ombremer', 'Lamebrillante', 'Coeurpierre', 'Tempete', 'Thornwood', 'Winterborne'],
        pseudos: {fighter: ['La Lame de Fer', 'Briseur de Bouclier', 'Mur d\'Acier', 'Maitre de Bataille', 'Seigneur', 'Champion'], wizard: ['Le Sage Arcanique', 'Tisserand de Sorts', 'Oracle Mystique', 'Gardien du Savoir', 'Archimage'], rogue: ['Le Marcheur d\'Ombre', 'Pas Silencieux', 'Lame de Nuit', 'Murmure', 'Fantome', 'Ombre'], cleric: ['La Lumiere Sainte', 'Gardien de la Foi', 'Main Divine', 'Le Beni', 'Porteur de Lumiere'], paladin: ['Le Bouclier Sacre', 'Porteur de Lumiere', 'Lie par le Serment', 'Le Juste', 'Protecteur Divin'], ranger: ['La Chasse Sauvage', 'Gardien de la Foret', 'Pisteur', 'Cheminard', 'Le Loup Solitaire'], barbarian: ['La Rage de la Tempete', 'Furie Dechainee', 'Ecraseur d\'Os', 'Cri de Guerre', 'Le Berserker'], bard: ['La Langue d\'Argent', 'Tisserand de Melodie', 'Enchanteur', 'Le Chanteur', 'Conteur'], druid: ['La Voix de la Foret', 'Colere de la Nature', 'Gardien des Racines', 'Gardien du Bois', 'Le Vert'], monk: ['Le Poing Silencieux', 'Paix Interieure', 'Paume de Tempete', 'Maitre Zen', 'L\'Eveille'], sorcerer: ['Le Tisserand de Sang', 'Etincelle du Chaos', 'Magie Sauvage', 'Tempete Arcanique', 'Les Flammes'], warlock: ['Le Pacte Sombre', 'Gardien du Pacte', 'Appeleur d\'Ombre', 'Murmure de Malheur', 'Le Damne'] },
        backgrounds: ['Ne dans un petit village a la frontiere du royaume', 'Eleve parmi les anciens d\'une terre lointaine', 'Enfant de voyageurs, ne connaissant jamais de foyer permanent', 'Ancien membre d\'un ordre prestigieux', 'Eleve dans les montagnes par des maitres reclus'],
        stories: ['A travers des annees d\'entrainement et de misere, ils ont developpe des competences exceptionnelles', 'Leur reputation les precede, de nombreuses histoires sont racontees sur leurs exploits', 'Ils se sont lances pour se faire un nom', 'Leur voyage les a mene a travers des forets sombres et des ruines antiques', 'Maintenant, ils cherchent de nouvelles aventures et de plus grands defis']
    },
    de: {
        prefixes: ['Das', 'Der', 'Alte', 'Verlorene', 'Versteckte', 'Dunkle', 'Goldene', 'Silberne', 'Gefrorene', 'Schatten', 'Kristall', 'Eisen', 'Sturm', 'Stern', 'Mond', 'Sonne', 'Blut', 'Knochen', 'Ewig', 'Verboten', 'Heilig', 'Gottlich', 'Grun', 'Rot', 'Erwachende', 'Gefallen', 'Zerbrochen'],
        nouns: ['Konigreich', 'Reich', 'Imperium', 'Chronik', 'Saga', 'Quest', 'Reise', 'Vermachtnis', 'Weissagung', 'Legende', 'Mythos', 'Odyssee', 'Epos', 'Abenteuer', 'Krieg', 'Frieden', 'Morgenrote', 'Dammerung', 'Ewigkeit', 'Schicksal', 'Ruhm', 'Sieg', 'Eroberung', 'Genesis', 'Apokalypse'],
        suffixes: ['der Schatten', 'des Lichts', 'der Traume', 'des Verderbens', 'der Hoffnung', 'der Alten', 'der Verlorenen', 'der Verdammten', 'der Vergessenen', 'der Sterne', 'des Mondes', 'des Winters', 'des Sommers', 'des Abgrunds', 'der Himmel'],
        sessionNouns: ['Sitzung', 'Kapitel', 'Episode', 'Entdeckung', 'Reise', 'Quest', 'Prufung', 'Herausforderung', 'Sieg', 'Niederlage', 'Wendepunkt', 'Hohenpunkt', 'Auflosung', 'Geheimnis', 'Begegnung', 'Schlacht', 'Erweckung', 'Finale', 'Prolog', 'Kreuzweg', 'Schwelle'],
        sessionAdj: ['Erste', 'Eroffnung', 'Anfang', 'Entdeckung', 'Konfrontation', 'Prufung', 'Dunkle', 'Schatten', 'Versteckt', 'Verloren', 'Erwachende', 'Letzte', 'Wendepunkt'],
        eventTypes: ['Festival', 'Zeremonie', 'Schlacht', 'Ritual', 'Versammlung', 'Rat', 'Turnier', 'Markt', 'Invasion', 'Allianz', 'Vertrag', 'Feier', 'Aufruhr', 'Revolution', 'Finsternis', 'Sturm', 'Katastrophe', 'Ruf', 'Jagd', 'Expedition'],
        locTypes: ['Zitadelle', 'Tempel', 'Taverne', 'Wald', 'Hohle', 'Turm', 'Burg', 'Heiligtum', 'Ruinen', 'Hafen', 'Dorf', 'Stadt', 'Verlies', 'Schrein', 'Palast', 'Bibliothek', 'Arena', 'Berg', 'Tal', 'Wuste', 'Insel', 'Vulkan', 'Sumpf', 'Schlucht', 'Wasserfall', 'See', 'Fluss', 'Ozean', 'Festung', 'Hafen'],
        charFirst: {human: ['Marcus', 'Elena', 'Gareth', 'Lyra', 'Aldric', 'Brenna', 'Cedric', 'Dara', 'Eldric', 'Freya', 'Helena', 'Idris', 'Kael', 'Luna', 'Magnus', 'Nadia', 'Orion', 'Petra', 'Rowan', 'Soren'], elf: ['Elara', 'Thalion', 'Aelindra', 'Silvanus', 'Miriel', 'Galadriel', 'Legolas', 'Arwen', 'Luthien', 'Beren', 'Elrond', 'Faelar'], dwarf: ['Thorin', 'Balin', 'Dwalin', 'Fili', 'Kili', 'Gimli', 'Durin', 'Gloin', 'Oin', 'Bofur', 'Bombur', 'Nori'], orc: ['Grom', 'Thrash', 'Ugluk', 'Mogul', 'Gashnak', 'Grishnak', 'Lurtz', 'Bolg', 'Azog', 'Boldog'], halfling: ['Pip', 'Rosie', 'Milo', 'Bramble', 'Frodo', 'Bilbo', 'Sam', 'Merry', 'Pippin', 'Cotton'], tiefling: ['Zariel', 'Xaphan', 'Vexia', 'Riftan', 'Morthos', 'Nemeia', 'Zarathos', 'Orpheus', 'Lilith'], dragonborn: ['Draconis', 'Sylvana', 'Pyrrus', 'Balasar', 'Kriv', 'Mishann', 'Nadarr', 'Torinn'], gnome: ['Fizzle', 'Pfeifen', 'Tanglewood', 'Zizzle', 'Bimpnottin', 'Dimble', 'Eldon', 'Gimble'] },
        charLast: ['Sterling', 'Schwarzholz', 'Stein', 'Eisenberg', 'Mondflusterer', 'Silberblatt', 'Dusterumhang', 'Sternenweb', 'Goldblatt', 'Schattenmeer', 'Hellklinge', 'Dunkelhollow', 'Frostborn', 'Hugelkamm', 'Flusstal', 'Steinherz', 'Sturmwind', 'Dornholz', 'Winterborne'],
        pseudos: {fighter: ['Die Eisenklinge', 'Schildbrecher', 'Stahlwand', 'Kriegsmeister', 'Kriegsherr', 'Champion'], wizard: ['Der Arkane Weise', 'Zauberweber', 'Orakel', 'Weisheitswahrer', 'Erzmagier'], rogue: ['Der Schattenwandler', 'Stiller Schritt', 'Nachtklinge', 'Flustern', 'Geist', 'Phantom'], cleric: ['Das Heilige Licht', 'Glaubenshuter', 'Gottliche Hand', 'Der Gesegnete', 'Lichtbringer'], paladin: ['Der Heilige Schild', 'Lichtbringer', 'Eidgebunden', 'Der Rechtschaffende', 'Gottlicher Beschutzer'], ranger: ['Die Wilde Jagd', 'Waldhuter', 'Fahrtenleser', 'Pfadfinder', 'Der Einzelwolf'], barbarian: ['Der Sturmzorn', 'Entfesselte Wut', 'Knochenbrecher', 'Kriegsschrei', 'Der Berserker'], bard: ['Die Silberne Zunge', 'Melodienweber', 'Verzauberer', 'Der Sanger', 'Geschichtenerzahler'], druid: ['Die Waldstimme', 'Naturzorn', 'Wurzelwahrer', 'Hainwahrer', 'Der Grune'], monk: ['Die Stille Faust', 'Innerer Frieden', 'Sturmschlag', 'Zenmeister', 'Der Erleuchtete'], sorcerer: ['Der Blutweber', 'Chaosfunke', 'Wilde Magie', 'Arkane Sturm', 'Die Flammen'], warlock: ['Der Dunkle Pakt', 'Paktwahrer', 'Schattenrufer', 'Verderbenflusterer', 'Der Verdammt'] },
        backgrounds: ['In einem kleinen Dorf am Rande des Konigreichs geboren', 'Unter den Alten eines fernen Landes aufgewachsen', 'Als Kind von Reisenden, ohne je ein dauerhaftes Zuhause zu kennen', 'Einst Mitglied eines renommierten Ordens', 'In den Bergen von zuruckgezogenen Meistern aufgezogen'],
        stories: ['Durch Jahre des Trainings und der Entbehrung entwickelten sie aussergewohnliche Fahigkeiten', 'Ihr Ruf eilt ihnen voraus, viele Geschichten werden uber ihre Taten erzahlt', 'Sie machten sich auf, sich einen Namen zu machen', 'Ihre Reise fuhrte sie durch dunkle Walder und antike Ruinen', 'Nun suchen sie neue Abenteuer und grossere Herausforderungen']
    },
    es: {
        prefixes: ['El', 'Antiguo', 'Perdido', 'Oculto', 'Oscuro', 'Dorado', 'Plateado', 'Congelado', 'Ardiente', 'Sombra', 'Cristal', 'Hierro', 'Tormenta', 'Estrella', 'Luna', 'Sol', 'Sangre', 'Hueso', 'Eterno', 'Prohibido', 'Sagrado', 'Divino', 'Verde', 'Carmesi', 'Rising', 'Caido', 'Roto'],
        nouns: ['Reino', 'Imperio', 'Cronicas', 'Saga', 'Busqueda', 'Viaje', 'Legado', 'Profecia', 'Leyenda', 'Mito', 'Odisea', 'Epica', 'Aventura', 'Guerra', 'Paz', 'Amanecer', 'Crepusculo', 'Eternidad', 'Destino', 'Gloria', 'Victoria', 'Conquista', 'Dominacion', 'Genesis', 'Apocalipsis'],
        suffixes: ['de Sombras', 'de Luz', 'de Suenos', 'de Perdicion', 'de Esperanza', 'de los Antiguos', 'de los Perdidos', 'de los Condenados', 'del Olvido', 'de las Estrellas', 'de la Luna', 'de Invierno', 'de Verano', 'del Abismo', 'de los Cielos'],
        sessionNouns: ['Sesion', 'Capitulo', 'Episodio', 'Descubrimiento', 'Viaje', 'Busqueda', 'Prueba', 'Desafio', 'Victoria', 'Derrota', 'Punto de Inflexion', 'Climax', 'Resolucion', 'Misterio', 'Encuentro', 'Batalla', 'Despertar', 'Final', 'Prologo', 'Cruce', 'Umbral'],
        sessionAdj: ['Primera', 'Apertura', 'Inicial', 'Descubrimiento', 'Confrontacion', 'Prueba', 'Oscura', 'Sombra', 'Oculta', 'Perdida', 'Rising', 'Final', 'Giro'],
        eventTypes: ['Festival', 'Ceremonia', 'Batalla', 'Ritual', 'Reunion', 'Consejo', 'Torneo', 'Mercado', 'Invasion', 'Alianza', 'Tratado', 'Celebracion', 'Rebelion', 'Revolucion', 'Eclipse', 'Tormenta', 'Catastrofe', 'Convocatoria', 'Caza', 'Expedicion'],
        locTypes: ['Ciudadela', 'Templo', 'Taberna', 'Bosque', 'Cueva', 'Torre', 'Castillo', 'Santuario', 'Ruinas', 'Puerto', 'Aldea', 'Ciudad', 'Mazmorra', 'Santuario', 'Palacio', 'Biblioteca', 'Arena', 'Monte', 'Valle', 'Desierto', 'Isla', 'Volcan', 'Pantano', 'Canyon', 'Cascada', 'Lago', 'Rio', 'Oceano', 'Fortaleza', 'Refugio'],
        charFirst: {human: ['Marco', 'Elena', 'Gareth', 'Lyra', 'Aldric', 'Brenna', 'Cedric', 'Dara', 'Eldric', 'Freya', 'Helena', 'Idris', 'Kael', 'Luna', 'Magnus', 'Nadia', 'Orion', 'Petra', 'Rowan', 'Soren'], elf: ['Elara', 'Thalion', 'Aelindra', 'Silvanus', 'Miriel', 'Galadriel', 'Legolas', 'Arwen', 'Luthien', 'Beren', 'Elrond', 'Faelar'], dwarf: ['Thorin', 'Balin', 'Dwalin', 'Fili', 'Kili', 'Gimli', 'Durin', 'Gloin', 'Oin', 'Bofur', 'Bombur', 'Nori'], orc: ['Grom', 'Thrash', 'Ugluk', 'Mogul', 'Gashnak', 'Grishnak', 'Lurtz', 'Bolg', 'Azog', 'Boldog'], halfling: ['Pip', 'Rosie', 'Milo', 'Bramble', 'Frodo', 'Bilbo', 'Sam', 'Merry', 'Pippin', 'Cotton'], tiefling: ['Zariel', 'Xaphan', 'Vexia', 'Riftan', 'Morthos', 'Nemeia', 'Zarathos', 'Orpheus', 'Lilith'], dragonborn: ['Draconis', 'Sylvana', 'Pyrrus', 'Balasar', 'Kriv', 'Mishann', 'Nadarr', 'Torinn'], gnome: ['Fizzle', 'Silbato', 'Tanglewood', 'Zizzle', 'Bimpnottin', 'Dimble', 'Eldon', 'Gimble'] },
        charLast: ['Sterling', 'BosqueNegro', 'Piedra', 'HierroForja', 'LunaSusurro', 'PlataHoja', 'Crepusculo', 'TejidoEstrella', 'HojaDorada', 'Sombrerio', 'HojaBrillante', 'OscuroHollow', 'NacidoFrio', 'CimaColina', 'ValleRio', 'CorazonPiedra', 'VientoTormenta', 'BosqueEspino', 'Winterborne'],
        pseudos: {fighter: ['La Espada de Hierro', 'RompeEscudos', 'Pared de Acero', 'Maestro Batalla', 'Senor', 'Campeon'], wizard: ['El Sabio Arcano', 'Tejedor de Hechizos', 'Oraculo Mistico', 'Guardian Conocimiento', 'Archimago'], rogue: ['El Caminante de Sombras', 'Paso Silencioso', 'Espada de Noche', 'Susurro', 'Fantasma', 'Sombra'], cleric: ['La Luz Santa', 'Guardian de la Fe', 'Mano Divina', 'El Bendito', 'Portador de Luz'], paladin: ['El Escudo Sagrado', 'Portador de Luz', 'Vinculo Jurado', 'El Justiciero', 'Protector Divino'], ranger: ['La Caza Salvaje', 'Guardian del Bosque', 'Rastreador', 'Explorador', 'El Lobo Solitario'], barbarian: ['La Rabia de la Tormenta', 'Furia Desatada', 'Rompedor de Huesos', 'Grito de Guerra', 'El Berserker'], bard: ['La Lengua de Plata', 'Tejedor de Melodias', 'Encantador', 'El Cantor', 'Cuentista'], druid: ['La Voz del Bosque', 'Ira de la Naturaleza', 'Guardian Raices', 'Guardian del Bosque', 'El Verde'], monk: ['El Punho Silencioso', 'Paz Interior', 'Palma Tormenta', 'Maestro Zen', 'El Iluminado'], sorcerer: ['El Tejedor de Sangre', 'Chispa del Caos', 'Magia Salvaje', 'Tormenta Arcana', 'Las Llamas'], warlock: ['El Pacto Oscuro', 'Guardian del Pacto', 'Invocador de Sombras', 'Susurro de Perdicion', 'El Condenado'] },
        backgrounds: ['Nacido en una pequena aldea en el borde del reino', 'Criado entre los antiguos de una tierra lejana', 'Hijo de viajeros, sin conocer nunca un hogar permanente', 'Miembro alguna vez de una orden prestigiosa', 'Criado en las montanhas por maestros reclusos'],
        stories: ['A traves de ahos de entrenamiento y privacion, desarrollaron habilidades excepcionales', 'Su reputacion los precede, muchas historias se cuentan de sus hazanas', 'Se aventuraron para hacerse un nombre', 'Su viaje los llevo a traves de bosques oscuros y ruinas antiguas', 'Ahora buscan nuevas aventuras y mayores desafios']
    }
};

// Pick random item from array
function pick(arr) {
    return arr[Math.floor(Math.random() * arr.length)];
}

// Generate random name
function randName() {
    const d = DICTS[getLang()] || DICTS.en;
    const r = Math.random();
    if (r < 0.3) return pick(d.prefixes) + ' ' + pick(d.nouns);
    if (r < 0.5) return pick(d.prefixes) + ' ' + pick(d.nouns) + ' ' + pick(d.suffixes);
    if (r < 0.7) return pick(d.nouns) + ' ' + pick(d.suffixes);
    return pick(d.nouns);
}

// Exported functions for pages
function generateRandomCampaignName() { return randName(); }
function generateRandomSessionName() {
    const d = DICTS[getLang()] || DICTS.en;
    if (Math.random() > 0.5) return pick(d.sessionAdj) + ' ' + pick(d.sessionNouns);
    return 'Chapter ' + (Math.floor(Math.random() * 50) + 1) + ': ' + pick(d.sessionNouns);
}
function generateRandomEventName() {
    const d = DICTS[getLang()] || DICTS.en;
    const r = Math.random();
    if (r < 0.5) return pick(d.eventTypes) + ' ' + pick(d.suffixes);
    return pick(d.eventTypes);
}
function generateRandomLocationName() {
    const d = DICTS[getLang()] || DICTS.en;
    const r = Math.random();
    if (r < 0.3) return pick(d.prefixes) + ' ' + pick(d.locTypes);
    if (r < 0.5) return pick(d.prefixes) + ' ' + pick(d.locTypes) + ' ' + pick(d.suffixes);
    if (r < 0.7) return pick(d.locTypes) + ' ' + pick(d.suffixes);
    return pick(d.locTypes);
}
function generateRandomForumTitle() {
    const d = DICTS[getLang()] || DICTS.en;
    const topics = ['Help', 'Question', 'Discussion', 'Tips', 'Guide', 'Story', 'Idea', 'Lore', 'Build', 'Strategy'];
    const themes = ['Needed', 'Discussion', 'Question', 'Tips', 'Share'];
    if (Math.random() > 0.5) return pick(topics) + ': ' + pick(themes);
    return pick(topics) + ' ' + pick(themes);
}

// Character functions
function generateRandomCharacterFullName(race) {
    const d = DICTS[getLang()] || DICTS.en;
    const rk = (race || document.getElementById('CharacterRace')?.value || 'human').toLowerCase();
    const firsts = d.charFirst[rk] || d.charFirst.human;
    return pick(firsts) + ' ' + pick(d.charLast);
}

function generateRandomPseudo(cls) {
    const d = DICTS[getLang()] || DICTS.en;
    const ck = (cls || document.getElementById('CharacterClass')?.value || 'fighter').toLowerCase();
    const list = d.pseudos[ck] || d.pseudos.fighter;
    return pick(list);
}

function generateRandomCampaignDesc() {
    const d = DICTS[getLang()] || DICTS.en;
    const descs = [
        pick(d.prefixes) + ' ' + pick(d.nouns) + '. ' + pick(d.stories) + '. ' + pick(d.stories) + '.',
        pick(d.prefixes) + ' ' + pick(d.nouns) + ' ' + pick(d.suffixes) + '. ' + pick(d.stories) + '.',
        pick(d.nouns) + ' ' + pick(d.suffixes) + '. ' + pick(d.stories) + '. ' + pick(d.stories) + '.'
    ];
    return pick(descs);
}

function generateRandomCharacterBackground() {
    const d = DICTS[getLang()] || DICTS.en;
    return pick(d.backgrounds) + '. ' + pick(d.stories) + '.';
}

// Stats functions
function generateRandomStats() {
    const MAX = 60;
    const stats = ['Strength', 'Agility', 'Health', 'Intelligence', 'Charisma', 'Endurance'];
    let remaining = MAX;
    
    stats.forEach((stat, i) => {
        if (i === stats.length - 1) {
            document.getElementById(stat).value = remaining;
        } else {
            const maxVal = Math.min(20, remaining - (stats.length - i - 1));
            const val = Math.floor(Math.random() * (maxVal + 1));
            document.getElementById(stat).value = val;
            remaining -= val;
        }
        if (typeof updateRadarChart === 'function') updateRadarChart();
    });
    if (typeof calculateUsedPoints === 'function') calculateUsedPoints();
}

function resetStats() {
    ['Strength', 'Agility', 'Health', 'Intelligence', 'Charisma', 'Endurance'].forEach(stat => {
        document.getElementById(stat).value = 0;
    });
    if (typeof calculateUsedPoints === 'function') calculateUsedPoints();
    if (typeof updateRadarChart === 'function') updateRadarChart();
}

function randomizeRace() {
    const races = ['Human', 'Elf', 'Dwarf', 'Orc', 'Halfling', 'Tiefling', 'Dragonborn', 'Gnome'];
    document.getElementById('CharacterRace').value = races[Math.floor(Math.random() * races.length)];
}

function randomizeClass() {
    const classes = ['Fighter', 'Wizard', 'Rogue', 'Cleric', 'Paladin', 'Ranger', 'Barbarian', 'Bard', 'Druid', 'Monk', 'Sorcerer', 'Warlock'];
    document.getElementById('CharacterClass').value = classes[Math.floor(Math.random() * classes.length)];
}

function randomizeRole() {
    const roles = ['None', 'Ally', 'Antagonist', 'Merchant', 'Quest Giver', 'Informant', 'Henchman'];
    document.getElementById('CharacterRole').value = roles[Math.floor(Math.random() * roles.length)];
}

function regenerateDescription() {
    document.getElementById('CharacterDescription').value = generateRandomCharacterBackground();
}

// Global exposure
window.generateRandomCampaignName = generateRandomCampaignName;
window.generateRandomSessionName = generateRandomSessionName;
window.generateRandomEventName = generateRandomEventName;
window.generateRandomLocationName = generateRandomLocationName;
window.generateRandomForumTitle = generateRandomForumTitle;
window.generateRandomCharacterFullName = generateRandomCharacterFullName;
window.generateRandomPseudo = generateRandomPseudo;
window.generateRandomCampaignDesc = generateRandomCampaignDesc;
window.generateRandomCharacterBackground = generateRandomCharacterBackground;
window.generateRandomStats = generateRandomStats;
window.resetStats = resetStats;
window.randomizeRace = randomizeRace;
window.randomizeClass = randomizeClass;
window.randomizeRole = randomizeRole;
window.regenerateDescription = regenerateDescription;

// Aliases for backward compat
window.generateCharacterName = generateRandomCharacterFullName;
window.generateCharacterPseudo = generateRandomPseudo;
window.generateCharacterDescription = generateRandomCharacterBackground;
