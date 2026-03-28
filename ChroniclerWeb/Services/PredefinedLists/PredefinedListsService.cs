using System.Collections.Generic;

namespace ChroniclerWeb.Services.PredefinedLists
{
    public static class PredefinedLists
    {
        private static readonly Random _random = new();

        public static readonly Dictionary<string, Dictionary<string, string>> Classes = new()
        {
            // Core Classes
            { "Fighter", new() { { "en", "Fighter" }, { "fr", "Guerrier" }, { "es", "Luchador" }, { "de", "Kämpfer" }, { "it", "Guerriero" } } },
            { "Wizard", new() { { "en", "Wizard" }, { "fr", "Sorcier" }, { "es", "Mago" }, { "de", "Zauberer" }, { "it", "Stregone" } } },
            { "Rogue", new() { { "en", "Rogue" }, { "fr", "Voleur" }, { "es", "Pícaro" }, { "de", "Schurke" }, { "it", "Ladro" } } },
            { "Cleric", new() { { "en", "Cleric" }, { "fr", "Clerc" }, { "es", "Clérigo" }, { "de", "Priester" }, { "it", "Chierico" } } },
            { "Paladin", new() { { "en", "Paladin" }, { "fr", "Paladin" }, { "es", "Paladín" }, { "de", "Paladin" }, { "it", "Paladino" } } },
            { "Ranger", new() { { "en", "Ranger" }, { "fr", "Rôdeur" }, { "es", "Montaraz" }, { "de", "Waldläufer" }, { "it", "Ranger" } } },
            { "Barbarian", new() { { "en", "Barbarian" }, { "fr", "Barbare" }, { "es", "Bárbaro" }, { "de", "Barbar" }, { "it", "Barbaro" } } },
            { "Bard", new() { { "en", "Bard" }, { "fr", "Barde" }, { "es", "Bardo" }, { "de", "Barde" }, { "it", "Bardo" } } },
            { "Druid", new() { { "en", "Druid" }, { "fr", "Druide" }, { "es", "Druida" }, { "de", "Druide" }, { "it", "Druido" } } },
            { "Monk", new() { { "en", "Monk" }, { "fr", "Moine" }, { "es", "Monje" }, { "de", "Mönch" }, { "it", "Monaco" } } },
            { "Sorcerer", new() { { "en", "Sorcerer" }, { "fr", "Ensorceleur" }, { "es", "Hechicero" }, { "de", "Hexenmeister" }, { "it", "Stregone" } } },
            { "Warlock", new() { { "en", "Warlock" }, { "fr", "Démoniste" }, { "es", "Brujo" }, { "de", "Kriegsmagier" }, { "it", "Stregone" } } },
            { "Artificer", new() { { "en", "Artificer" }, { "fr", "Artificier" }, { "es", "Artífice" }, { "de", "Artillerist" }, { "it", "Artificiere" } } },
            
            // Extended Classes
            { "Blood Hunter", new() { { "en", "Blood Hunter" }, { "fr", "Chasseur de Sang" }, { "es", "Cazador de Sangre" } } },
            { "Champion", new() { { "en", "Champion" }, { "fr", "Champion" }, { "es", "Campeón" } } },
            { "Guardian", new() { { "en", "Guardian" }, { "fr", "Gardien" }, { "es", "Guardián" } } },
            { "Templar", new() { { "en", "Templar" }, { "fr", "Templier" }, { "es", "Templario" } } },
            { "Inquisitor", new() { { "en", "Inquisitor" }, { "fr", "Inquisiteur" }, { "es", "Inquisidor" } } },
            { "Mystic", new() { { "en", "Mystic" }, { "fr", "Mystique" }, { "es", "Místico" } } },
            { "Necromancer", new() { { "en", "Necromancer" }, { "fr", "Nécromancien" }, { "es", "Nigromante" } } },
            { "Elementalist", new() { { "en", "Elementalist" }, { "fr", "Élémentaliste" }, { "es", "Elementalista" } } },
            { "Chronomancer", new() { { "en", "Chronomancer" }, { "fr", "Chronomancien" }, { "es", "Cronomante" } } },
            { "Bladesinger", new() { { "en", "Bladesinger" }, { "fr", "Chante-lame" }, { "es", "Cantante de Espadas" } } },
            { "Arcane Trickster", new() { { "en", "Arcane Trickster" }, { "fr", "Escamoteur des Arcanes" }, { "es", "Tramposo Arcano" } } },
            { "Assassin", new() { { "en", "Assassin" }, { "fr", "Assassin" }, { "es", "Asesino" } } },
            { "Shadowdancer", new() { { "en", "Shadowdancer" }, { "fr", "Danseur des Ombres" }, { "es", "Bailarín de Sombras" } } },
            { "Swashbuckler", new() { { "en", "Swashbuckler" }, { "fr", "Bretteur" }, { "es", "Espadachín" } } },
            { "Scout", new() { { "en", "Scout" }, { "fr", "Éclaireur" }, { "es", "Explorador" } } },
            { "Slayer", new() { { "en", "Slayer" }, { "fr", "Pourfendeur" }, { "es", "Asesino" } } },
            { "Warlord", new() { { "en", "Warlord" }, { "fr", "Seigneur de Guerre" }, { "es", "Señor de la Guerra" } } },
            { "Commander", new() { { "en", "Commander" }, { "fr", "Commandant" }, { "es", "Comandante" } } },
            { "Tactician", new() { { "en", "Tactician" }, { "fr", "Tacticien" }, { "es", "Táctico" } } },
            { "Strategist", new() { { "en", "Strategist" }, { "fr", "Stratège" }, { "es", "Estratega" } } },
            { "Berserker", new() { { "en", "Berserker" }, { "fr", "Berserker" }, { "es", "Bersérker" } } },
            { "Totem Warrior", new() { { "en", "Totem Warrior" }, { "fr", "Guerrier Totem" }, { "es", "Guerrero Totémico" } } },
            { "Storm Herald", new() { { "en", "Storm Herald" }, { "fr", "Héraut des Tempêtes" }, { "es", "Heraldo de la Tormenta" } } },
            { "Zealot", new() { { "en", "Zealot" }, { "fr", "Zélé" }, { "es", "Zelote" } } },
            { "Conquest Paladin", new() { { "en", "Conquest Paladin" }, { "fr", "Paladin de Conquête" }, { "es", "Paladín de Conquista" } } },
            { "Ancients Paladin", new() { { "en", "Ancients Paladin" }, { "fr", "Paladin des Anciens" }, { "es", "Paladín de los Ancestros" } } },
            { "Devotion Paladin", new() { { "en", "Devotion Paladin" }, { "fr", "Paladin de Dévotion" }, { "es", "Paladín de Devoción" } } },
            { "Vengeance Paladin", new() { { "en", "Vengeance Paladin" }, { "fr", "Paladin de Vengeance" }, { "es", "Paladín de Venganza" } } },
            { "Oathbreaker", new() { { "en", "Oathbreaker" }, { "fr", "Briseur de Serment" }, { "es", "Rompedor de Juramentos" } } },
            { "Moon Druid", new() { { "en", "Moon Druid" }, { "fr", "Druide de la Lune" }, { "es", "Druida de la Luna" } } },
            { "Land Druid", new() { { "en", "Land Druid" }, { "fr", "Druide de la Terre" }, { "es", "Druida de la Tierra" } } },
            { "Shepherd Druid", new() { { "en", "Shepherd Druid" }, { "fr", "Druide Berger" }, { "es", "Druida Pastor" } } },
            { "Stars Druid", new() { { "en", "Stars Druid" }, { "fr", "Druide des Étoiles" }, { "es", "Druida de las Estrellas" } } },
            { "Spores Druid", new() { { "en", "Spores Druid" }, { "fr", "Druide des Spores" }, { "es", "Druida de Esporas" } } },
            { "Lore Bard", new() { { "en", "Lore Bard" }, { "fr", "Barde du Savoir" }, { "es", "Bardo del Saber" } } },
            { "Valor Bard", new() { { "en", "Valor Bard" }, { "fr", "Barde de la Vaillance" }, { "es", "Bardo del Valor" } } },
            { "Swords Bard", new() { { "en", "Swords Bard" }, { "fr", "Barde des Épées" }, { "es", "Bardo de las Espadas" } } },
            { "Whispers Bard", new() { { "en", "Whispers Bard" }, { "fr", "Barde des Murmures" }, { "es", "Bardo de los Susurros" } } },
            { "Eloquence Bard", new() { { "en", "Eloquence Bard" }, { "fr", "Barde de l'Éloquence" }, { "es", "Bardo de la Eloquencia" } } },
            { "Creation Bard", new() { { "en", "Creation Bard" }, { "fr", "Barde de la Création" }, { "es", "Bardo de la Creación" } } },
            { "Glamour Bard", new() { { "en", "Glamour Bard" }, { "fr", "Barde du Glamour" }, { "es", "Bardo del Glamour" } } },
            { "Spirits Bard", new() { { "en", "Spirits Bard" }, { "fr", "Barde des Esprits" }, { "es", "Bardo de los Espíritus" } } },
            { "Open Hand Monk", new() { { "en", "Open Hand Monk" }, { "fr", "Moine de la Main Ouverte" }, { "es", "Monje de la Mano Abierta" } } },
            { "Shadow Monk", new() { { "en", "Shadow Monk" }, { "fr", "Moine de l'Ombre" }, { "es", "Monje de la Sombra" } } },
            { "Four Elements Monk", new() { { "en", "Four Elements Monk" }, { "fr", "Moine des Quatre Éléments" }, { "es", "Monje de los Cuatro Elementos" } } },
            { "Long Death Monk", new() { { "en", "Long Death Monk" }, { "fr", "Moine de la Longue Mort" }, { "es", "Monje de la Larga Muerte" } } },
            { "Sun Soul Monk", new() { { "en", "Sun Soul Monk" }, { "fr", "Moine de l'Âme Solaire" }, { "es", "Monje del Alma Solar" } } },
            { "Kensei Monk", new() { { "en", "Kensei Monk" }, { "fr", "Moine Kensei" }, { "es", "Monje Kensei" } } },
            { "Drunken Master Monk", new() { { "en", "Drunken Master Monk" }, { "fr", "Moine Ivrogne" }, { "es", "Monje Maestro Ebrio" } } },
            { "Mercy Monk", new() { { "en", "Mercy Monk" }, { "fr", "Moine de la Miséricorde" }, { "es", "Monje de la Misericordia" } } },
            { "Berserker Barbarian", new() { { "en", "Berserker Barbarian" }, { "fr", "Barbare Berserker" }, { "es", "Bárbaro Bersérker" } } },
            { "Ancestral Guardian", new() { { "en", "Ancestral Guardian" }, { "fr", "Gardien Ancestral" }, { "es", "Guardián Ancestral" } } },
            { "Battle Rager", new() { { "en", "Battle Rager" }, { "fr", "Furieux de Bataille" }, { "es", "Furioso de Batalla" } } },
            { "Wild Magic Barbarian", new() { { "en", "Wild Magic Barbarian" }, { "fr", "Barbare de Magie Sauvage" }, { "es", "Bárbaro de Magia Salvaje" } } },
            { "Beast Barbarian", new() { { "en", "Beast Barbarian" }, { "fr", "Barbare Bête" }, { "es", "Bárbaro Bestia" } } },
            { "Evocation Wizard", new() { { "en", "Evocation Wizard" }, { "fr", "Sorcier d'Évocation" }, { "es", "Mago de Evocación" } } },
            { "Abjuration Wizard", new() { { "en", "Abjuration Wizard" }, { "fr", "Sorcier d'Abjuration" }, { "es", "Mago de Abjuración" } } },
            { "Conjuration Wizard", new() { { "en", "Conjuration Wizard" }, { "fr", "Sorcier de Conjuration" }, { "es", "Mago de Conjuro" } } },
            { "Divination Wizard", new() { { "en", "Divination Wizard" }, { "fr", "Sorcier de Divination" }, { "es", "Mago de Adivinación" } } },
            { "Enchantment Wizard", new() { { "en", "Enchantment Wizard" }, { "fr", "Sorcier d'Enchantement" }, { "es", "Mago de Encantamiento" } } },
            { "Illusion Wizard", new() { { "en", "Illusion Wizard" }, { "fr", "Sorcier d'Illusion" }, { "es", "Mago de Ilusión" } } },
            { "Transmutation Wizard", new() { { "en", "Transmutation Wizard" }, { "fr", "Sorcier de Transmutation" }, { "es", "Mago de Transmutación" } } },
            { "Bladesinger Wizard", new() { { "en", "Bladesinger Wizard" }, { "fr", "Sorcier Chante-lame" }, { "es", "Mago Cantante de Espadas" } } },
            { "War Magic Wizard", new() { { "en", "War Magic Wizard" }, { "fr", "Sorcier de Guerre" }, { "es", "Mago de Guerra" } } },
            { "Grave Cleric", new() { { "en", "Grave Cleric" }, { "fr", "Clerc de la Tombe" }, { "es", "Clérigo de la Tumba" } } },
            { "Life Cleric", new() { { "en", "Life Cleric" }, { "fr", "Clerc de la Vie" }, { "es", "Clérigo de la Vida" } } },
            { "Light Cleric", new() { { "en", "Light Cleric" }, { "fr", "Clerc de la Lumière" }, { "es", "Clérigo de la Luz" } } },
            { "War Cleric", new() { { "en", "War Cleric" }, { "fr", "Clerc de la Guerre" }, { "es", "Clérigo de la Guerra" } } },
            { "Nature Cleric", new() { { "en", "Nature Cleric" }, { "fr", "Clerc de la Nature" }, { "es", "Clérigo de la Naturaleza" } } },
            { "Forge Cleric", new() { { "en", "Forge Cleric" }, { "fr", "Clerc de la Forge" }, { "es", "Clérigo de la Forja" } } },
            { "Tempest Cleric", new() { { "en", "Tempest Cleric" }, { "fr", "Clerc de la Tempête" }, { "es", "Clérigo de la Tormenta" } } },
            { "Trickery Cleric", new() { { "en", "Trickery Cleric" }, { "fr", "Clerc de la Tricherie" }, { "es", "Clérigo del Engaño" } } },
            { "Knowledge Cleric", new() { { "en", "Knowledge Cleric" }, { "fr", "Clerc de la Connaissance" }, { "es", "Clérigo del Conocimiento" } } },
            { "Death Cleric", new() { { "en", "Death Cleric" }, { "fr", "Clerc de la Mort" }, { "es", "Clérigo de la Muerte" } } },
            { "Peace Cleric", new() { { "en", "Peace Cleric" }, { "fr", "Clerc de la Paix" }, { "es", "Clérigo de la Paz" } } },
            { "Twilight Cleric", new() { { "en", "Twilight Cleric" }, { "fr", "Clerc du Crépuscule" }, { "es", "Clérigo del Crepúsculo" } } },
            { "Order Cleric", new() { { "en", "Order Cleric" }, { "fr", "Clerc de l'Ordre" }, { "es", "Clérigo del Orden" } } },
            { "Thief Rogue", new() { { "en", "Thief Rogue" }, { "fr", "Voleur" }, { "es", "Ladrón" } } },
            { "Assassin Rogue", new() { { "en", "Assassin Rogue" }, { "fr", "Voleur Assassin" }, { "es", "Pícaro Asesino" } } },
            { "Arcane Trickster Rogue", new() { { "en", "Arcane Trickster Rogue" }, { "fr", "Voleur des Arcanes" }, { "es", "Pícaro Arcano" } } },
            { "Inquisitive Rogue", new() { { "en", "Inquisitive Rogue" }, { "fr", "Voleur Inquisitif" }, { "es", "Pícaro Inquisitivo" } } },
            { "Mastermind Rogue", new() { { "en", "Mastermind Rogue" }, { "fr", "Voleur Maître" }, { "es", "Pícaro Maestro" } } },
            { "Scout Rogue", new() { { "en", "Scout Rogue" }, { "fr", "Voleur Éclaireur" }, { "es", "Pícaro Explorador" } } },
            { "Swashbuckler Rogue", new() { { "en", "Swashbuckler Rogue" }, { "fr", "Voleur Bretteur" }, { "es", "Pícaro Espadachín" } } },
            { "Phantom Rogue", new() { { "en", "Phantom Rogue" }, { "fr", "Voleur Fantôme" }, { "es", "Pícaro Fantasma" } } },
            { "Soulknife Rogue", new() { { "en", "Soulknife Rogue" }, { "fr", "Voleur Couteau d'Âme" }, { "es", "Pícaro Cuchillo de Alma" } } },
            { "Champion Fighter", new() { { "en", "Champion Fighter" }, { "fr", "Guerrier Champion" }, { "es", "Luchador Campeón" } } },
            { "Battle Master Fighter", new() { { "en", "Battle Master Fighter" }, { "fr", "Guerrier Maître de Bataille" }, { "es", "Luchador Maestro de Batalla" } } },
            { "Eldritch Knight Fighter", new() { { "en", "Eldritch Knight Fighter" }, { "fr", "Guerrier Chevalier Céleste" }, { "es", "Luchador Caballero Arcano" } } },
            { "Arcane Archer Fighter", new() { { "en", "Arcane Archer Fighter" }, { "fr", "Guerrier Archer des Arcanes" }, { "es", "Luchador Arquero Arcano" } } },
            { "Cavalier Fighter", new() { { "en", "Cavalier Fighter" }, { "fr", "Guerrier Cavalier" }, { "es", "Luchador Jinete" } } },
            { "Samurai Fighter", new() { { "en", "Samurai Fighter" }, { "fr", "Guerrier Samouraï" }, { "es", "Luchador Samurái" } } },
            { "Psi Warrior Fighter", new() { { "en", "Psi Warrior Fighter" }, { "fr", "Guerrier Psi" }, { "es", "Luchador Psi" } } },
            { "Rune Knight Fighter", new() { { "en", "Rune Knight Fighter" }, { "fr", "Guerrier Chevalier des Runes" }, { "es", "Luchador Caballero Rúnico" } } },
            { "Hunter Ranger", new() { { "en", "Hunter Ranger" }, { "fr", "Rôdeur Chasseur" }, { "es", "Montaraz Cazador" } } },
            { "Gloom Stalker Ranger", new() { { "en", "Gloom Stalker Ranger" }, { "fr", "Rôdeur Traqueur Ténébreux" }, { "es", "Montaraz Acechador Sombrío" } } },
            { "Horizon Walker Ranger", new() { { "en", "Horizon Walker Ranger" }, { "fr", "Rôdeur Marcheur d'Horizon" }, { "es", "Montaraz Caminante del Horizonte" } } },
            { "Monster Slayer Ranger", new() { { "en", "Monster Slayer Ranger" }, { "fr", "Rôdeur Tueur de Monstres" }, { "es", "Montaraz Cazador de Monstruos" } } },
            { "Fey Wanderer Ranger", new() { { "en", "Fey Wanderer Ranger" }, { "fr", "Rôdeur Errant Féerique" }, { "es", "Montaraz Errante Feérico" } } },
            { "Swarmkeeper Ranger", new() { { "en", "Swarmkeeper Ranger" }, { "fr", "Rôdeur Gardien d'Essaim" }, { "es", "Montaraz Guardián de Enjambre" } } },
            { "Drake Warden Ranger", new() { { "en", "Drake Warden Ranger" }, { "fr", "Rôdeur Gardien de Dracol" }, { "es", "Montaraz Guardián de Drake" } } },
            { "Beast Master Ranger", new() { { "en", "Beast Master Ranger" }, { "fr", "Rôdeur Maître des Bêtes" }, { "es", "Montaraz Amo de Bestias" } } },
            { "Ancient One Warlock", new() { { "en", "Ancient One Warlock" }, { "fr", "Démoniste de l'Ancien" }, { "es", "Brujo del Anciano" } } },
            { "Fiend Warlock", new() { { "en", "Fiend Warlock" }, { "fr", "Démoniste du Démon" }, { "es", "Brujo del Demonio" } } },
            { "Great Old One Warlock", new() { { "en", "Great Old One Warlock" }, { "fr", "Démoniste du Grand Ancien" }, { "es", "Brujo del Gran Anciano" } } },
            { "Archfey Warlock", new() { { "en", "Archfey Warlock" }, { "fr", "Démoniste de l'Archifee" }, { "es", "Brujo del Archifé" } } },
            { "Celestial Warlock", new() { { "en", "Celestial Warlock" }, { "fr", "Démoniste Céleste" }, { "es", "Brujo Celestial" } } },
            { "Hexblade Warlock", new() { { "en", "Hexblade Warlock" }, { "fr", "Démoniste de la Malédiction" }, { "es", "Brujo de la Hexahoja" } } },
            { "Undying Warlock", new() { { "en", "Undying Warlock" }, { "fr", "Démoniste Immortel" }, { "es", "Brujo Inmortal" } } },
            { "Fathomless Warlock", new() { { "en", "Fathomless Warlock" }, { "fr", "Démoniste des Profondeurs" }, { "es", "Brujo de las Profundidades" } } },
            { "Genie Warlock", new() { { "en", "Genie Warlock" }, { "fr", "Démoniste du Génie" }, { "es", "Brujo del Genio" } } },
            { "Aberrant Mind Sorcerer", new() { { "en", "Aberrant Mind Sorcerer" }, { "fr", "Ensorceleur Esprit Aberrant" }, { "es", "Hechicero Mente Aberrante" } } },
            { "Draconic Bloodline Sorcerer", new() { { "en", "Draconic Bloodline Sorcerer" }, { "fr", "Ensorceleur Lignée Dragon" }, { "es", "Hechicero Linaje Dracónico" } } },
            { "Wild Magic Sorcerer", new() { { "en", "Wild Magic Sorcerer" }, { "fr", "Ensorceleur Magie Sauvage" }, { "es", "Hechicero Magia Salvaje" } } },
            { "Shadow Magic Sorcerer", new() { { "en", "Shadow Magic Sorcerer" }, { "fr", "Ensorceleur Magie de l'Ombre" }, { "es", "Hechicero Magia de Sombras" } } },
            { "Storm Sorcery Sorcerer", new() { { "en", "Storm Sorcery Sorcerer" }, { "fr", "Ensorceleur Sorcellerie de Tempête" }, { "es", "Hechicero Tormentoso" } } },
            { "Divine Soul Sorcerer", new() { { "en", "Divine Soul Sorcerer" }, { "fr", "Ensorceleur Âme Divine" }, { "es", "Hechicero Alma Divina" } } },
            { "Clockwork Soul Sorcerer", new() { { "en", "Clockwork Soul Sorcerer" }, { "fr", "Ensorceleur Âme d'Horlogerie" }, { "es", "Hechicero Alma de Relojería" } } },
            { "Lunar Sorcery Sorcerer", new() { { "en", "Lunar Sorcery Sorcerer" }, { "fr", "Ensorceleur Sorcellerie Lunaire" }, { "es", "Hechicero Magia Lunar" } } },
            { "Alchemist Artificer", new() { { "en", "Alchemist Artificer" }, { "fr", "Artificier Alchimiste" }, { "es", "Artífice Alquimista" } } },
            { "Armorer Artificer", new() { { "en", "Armorer Artificer" }, { "fr", "Artificier Armurier" }, { "es", "Artífice Armador" } } },
            { "Artillerist Artificer", new() { { "en", "Artillerist Artificer" }, { "fr", "Artificier Artilleur" }, { "es", "Artífice Artillero" } } },
            { "Battle Smith Artificer", new() { { "en", "Battle Smith Artificer" }, { "fr", "Artificier Forgeron de Bataille" }, { "es", "Artífice Herrero de Batalla" } } },
            { "Mechanic Artificer", new() { { "en", "Mechanic Artificer" }, { "fr", "Artificier Mécanicien" }, { "es", "Artífice Mecánico" } } }
        };

        public static readonly Dictionary<string, Dictionary<string, string>> Races = new()
        {
            // Core Races
            { "Human", new() { { "en", "Human" }, { "fr", "Humain" }, { "es", "Humano" }, { "de", "Mensch" }, { "it", "Umano" } } },
            { "Elf", new() { { "en", "Elf" }, { "fr", "Elfe" }, { "es", "Elfo" }, { "de", "Elf" }, { "it", "Elfo" } } },
            { "Dwarf", new() { { "en", "Dwarf" }, { "fr", "Nain" }, { "es", "Enano" }, { "de", "Zwerg" }, { "it", "Nano" } } },
            { "Halfling", new() { { "en", "Halfling" }, { "fr", "Halfelin" }, { "es", "Mediano" }, { "de", "Halbling" }, { "it", "Halfling" } } },
            { "Orc", new() { { "en", "Orc" }, { "fr", "Orc" }, { "es", "Orco" }, { "de", "Ork" }, { "it", "Orco" } } },
            { "Tiefling", new() { { "en", "Tiefling" }, { "fr", "Tieffelin" }, { "es", "Tiflín" }, { "de", "Tiefling" }, { "it", "Tiefling" } } },
            { "Dragonborn", new() { { "en", "Dragonborn" }, { "fr", "Drakéide" }, { "es", "Dracónido" }, { "de", "Drachenmensch" }, { "it", "Dragonide" } } },
            { "Gnome", new() { { "en", "Gnome" }, { "fr", "Gnome" }, { "es", "Gnomo" }, { "de", "Gnom" }, { "it", "Gnomo" } } },
            { "Half-Elf", new() { { "en", "Half-Elf" }, { "fr", "Demi-Elfe" }, { "es", "Semielfo" }, { "de", "Halbelf" }, { "it", "Mezzelfo" } } },
            { "Half-Orc", new() { { "en", "Half-Orc" }, { "fr", "Demi-Orc" }, { "es", "Semiorco" }, { "de", "Halbork" }, { "it", "Mezzorco" } } },
            { "Goblin", new() { { "en", "Goblin" }, { "fr", "Gobelin" }, { "es", "Goblin" }, { "de", "Goblin" }, { "it", "Goblin" } } },
            
            // Extended Races
            { "Aasimar", new() { { "en", "Aasimar" }, { "fr", "Aasimar" }, { "es", "Aasimar" } } },
            { "Firbolg", new() { { "en", "Firbolg" }, { "fr", "Firbolg" }, { "es", "Firbolg" } } },
            { "Genasi", new() { { "en", "Genasi" }, { "fr", "Génasi" }, { "es", "Genasi" } } },
            { "Goliath", new() { { "en", "Goliath" }, { "fr", "Goliath" }, { "es", "Goliath" } } },
            { "Kenku", new() { { "en", "Kenku" }, { "fr", "Kenku" }, { "es", "Kenku" } } },
            { "Lizardfolk", new() { { "en", "Lizardfolk" }, { "fr", "Lézardien" }, { "es", "Hombre Lagarto" } } },
            { "Tabaxi", new() { { "en", "Tabaxi" }, { "fr", "Tabaxi" }, { "es", "Tabaxi" } } },
            { "Triton", new() { { "en", "Triton" }, { "fr", "Triton" }, { "es", "Tritón" } } },
            { "Changeling", new() { { "en", "Changeling" }, { "fr", "Changeant" }, { "es", "Cambiante" } } },
            { "Kalashtar", new() { { "en", "Kalashtar" }, { "fr", "Kalashtar" }, { "es", "Kalashtar" } } },
            { "Shifter", new() { { "en", "Shifter" }, { "fr", "Mutant" }, { "es", "Cambiante" } } },
            { "Warforged", new() { { "en", "Warforged" }, { "fr", "Forgé-guerre" }, { "es", "Forjado de Guerra" } } },
            { "Bugbear", new() { { "en", "Bugbear" }, { "fr", "Bogeyman" }, { "es", "Hombre del Saco" } } },
            { "Hobgoblin", new() { { "en", "Hobgoblin" }, { "fr", "Hobgobelin" }, { "es", "Hobgoblin" } } },
            { "Kobold", new() { { "en", "Kobold" }, { "fr", "Kobold" }, { "es", "Kobold" } } },
            { "Yuan-Ti Pureblood", new() { { "en", "Yuan-Ti Pureblood" }, { "fr", "Yuan-Ti Pur-Sang" }, { "es", "Yuan-Ti Puro" } } },
            { "Githyanki", new() { { "en", "Githyanki" }, { "fr", "Githyanki" }, { "es", "Githyanki" } } },
            { "Githzerai", new() { { "en", "Githzerai" }, { "fr", "Githzerai" }, { "es", "Githzerai" } } },
            { "Duergar", new() { { "en", "Duergar" }, { "fr", "Duergar" }, { "es", "Duergar" } } },
            { "Drow", new() { { "en", "Drow" }, { "fr", "Drow" }, { "es", "Drow" } } },
            { "Eladrin", new() { { "en", "Eladrin" }, { "fr", "Éladrin" }, { "es", "Eladrin" } } },
            { "Sea Elf", new() { { "en", "Sea Elf" }, { "fr", "Elfe de Mer" }, { "es", "Elfo Marino" } } },
            { "Shadar-Kai", new() { { "en", "Shadar-Kai" }, { "fr", "Shadar-Kai" }, { "es", "Shadar-Kai" } } },
            { "Owlin", new() { { "en", "Owlin" }, { "fr", "Hiboulin" }, { "es", "Lechuza" } } },
            { "Leonin", new() { { "en", "Leonin" }, { "fr", "Léonin" }, { "es", "Leonino" } } },
            { "Minotaur", new() { { "en", "Minotaur" }, { "fr", "Minotaure" }, { "es", "Minotauro" } } },
            { "Centaur", new() { { "en", "Centaur" }, { "fr", "Centaure" }, { "es", "Centauro" } } },
            { "Satyr", new() { { "en", "Satyr" }, { "fr", "Faune" }, { "es", "Sátiro" } } },
            { "Verdan", new() { { "en", "Verdan" }, { "fr", "Verdan" }, { "es", "Verdan" } } },
            { "Tortle", new() { { "en", "Tortle" }, { "fr", "Tortle" }, { "es", "Tortuga" } } },
            { "Locathah", new() { { "en", "Locathah" }, { "fr", "Locathah" }, { "es", "Locathah" } } },
            { "Grung", new() { { "en", "Grung" }, { "fr", "Grung" }, { "es", "Grung" } } },
            { "Thri-Kreen", new() { { "en", "Thri-Kreen" }, { "fr", "Thri-Kreen" }, { "es", "Thri-Kreen" } } },
            { "Aarakocra", new() { { "en", "Aarakocra" }, { "fr", "Aarakocra" }, { "es", "Aarakocra" } } },
            { "Autognome", new() { { "en", "Autognome" }, { "fr", "Autognome" }, { "es", "Autognomo" } } },
            { "Hadozee", new() { { "en", "Hadozee" }, { "fr", "Hadozee" }, { "es", "Hadozee" } } },
            { "Plasmoid", new() { { "en", "Plasmoid" }, { "fr", "Plasmoïde" }, { "es", "Plasmoide" } } },
            { "Simic Hybrid", new() { { "en", "Simic Hybrid" }, { "fr", "Hybride Simique" }, { "es", "Híbrido Simic" } } },
            { "Vedalken", new() { { "en", "Vedalken" }, { "fr", "Vedalken" }, { "es", "Vedalken" } } },
            { "Meteor Aasimar", new() { { "en", "Meteor Aasimar" }, { "fr", "Aasimar Météore" }, { "es", "Aasimar Meteoro" } } },
            { "Fallen Aasimar", new() { { "en", "Fallen Aasimar" }, { "fr", "Aasimar Déchu" }, { "es", "Aasimar Caído" } } },
            { "Protector Aasimar", new() { { "en", "Protector Aasimar" }, { "fr", "Aasimar Protecteur" }, { "es", "Aasimar Protector" } } },
            { "Scourge Aasimar", new() { { "en", "Scourge Aasimar" }, { "fr", "Aasimar Fléau" }, { "es", "Aazsimar Azote" } } },
            { "Black Dragonborn", new() { { "en", "Black Dragonborn" }, { "fr", "Drakéide Noir" }, { "es", "Dracónido Negro" } } },
            { "Blue Dragonborn", new() { { "en", "Blue Dragonborn" }, { "fr", "Drakéide Bleu" }, { "es", "Dracónido Azul" } } },
            { "Brass Dragonborn", new() { { "en", "Brass Dragonborn" }, { "fr", "Drakéide d'Airain" }, { "es", "Dracónido de Latón" } } },
            { "Bronze Dragonborn", new() { { "en", "Bronze Dragonborn" }, { "fr", "Drakéide de Bronze" }, { "es", "Dracónido de Bronce" } } },
            { "Copper Dragonborn", new() { { "en", "Copper Dragonborn" }, { "fr", "Drakéide de Cuivre" }, { "es", "Dracónido de Cobre" } } },
            { "Gold Dragonborn", new() { { "en", "Gold Dragonborn" }, { "fr", "Drakéide d'Or" }, { "es", "Dracónido de Oro" } } },
            { "Green Dragonborn", new() { { "en", "Green Dragonborn" }, { "fr", "Drakéide Vert" }, { "es", "Dracónido Verde" } } },
            { "Red Dragonborn", new() { { "en", "Red Dragonborn" }, { "fr", "Drakéide Rouge" }, { "es", "Dracónido Rojo" } } },
            { "Silver Dragonborn", new() { { "en", "Silver Dragonborn" }, { "fr", "Drakéide d'Argent" }, { "es", "Dracónido de Plata" } } },
            { "White Dragonborn", new() { { "en", "White Dragonborn" }, { "fr", "Drakéide Blanc" }, { "es", "Dracónido Blanco" } } },
            { "Forest Gnome", new() { { "en", "Forest Gnome" }, { "fr", "Gnome des Forêts" }, { "es", "Gnomo del Bosque" } } },
            { "Rock Gnome", new() { { "en", "Rock Gnome" }, { "fr", "Gnome des Roches" }, { "es", "Gnomo de Roca" } } },
            { "Deep Gnome", new() { { "en", "Deep Gnome" }, { "fr", "Gnome des Profondeurs" }, { "es", "Gnomo Profundo" } } },
            { "High Elf", new() { { "en", "High Elf" }, { "fr", "Haut Elfe" }, { "es", "Alto Elfo" } } },
            { "Wood Elf", new() { { "en", "Wood Elf" }, { "fr", "Elfe des Bois" }, { "es", "Elfo del Bosque" } } },
            { "Dark Elf", new() { { "en", "Dark Elf" }, { "fr", "Elfe Noir" }, { "es", "Elfo Oscuro" } } },
            { "Mountain Dwarf", new() { { "en", "Mountain Dwarf" }, { "fr", "Nain des Montagnes" }, { "es", "Enano de Montaña" } } },
            { "Hill Dwarf", new() { { "en", "Hill Dwarf" }, { "fr", "Nain des Collines" }, { "es", "Enano de Colina" } } },
            { "Lightfoot Halfling", new() { { "en", "Lightfoot Halfling" }, { "fr", "Halfelin Pied-Léger" }, { "es", "Mediano Pie Ligero" } } },
            { "Stout Halfling", new() { { "en", "Stout Halfling" }, { "fr", "Halfelin Mémorial" }, { "es", "Mediano Robusto" } } },
            { "Ghostwise Halfling", new() { { "en", "Ghostwise Halfling" }, { "fr", "Halfelin Esprit-Fantôme" }, { "es", "Mediano Fantasmal" } } }
        };

        public static readonly Dictionary<string, Dictionary<string, string>> Roles = new()
        {
            // Combat Roles
            { "Tank", new() { { "en", "Tank" }, { "fr", "Tank" }, { "es", "Tanque" } } },
            { "DPS", new() { { "en", "DPS" }, { "fr", "Dégâts" }, { "es", "DPS" } } },
            { "Healer", new() { { "en", "Healer" }, { "fr", "Guérisseur" }, { "es", "Sanador" } } },
            { "Support", new() { { "en", "Support" }, { "fr", "Support" }, { "es", "Soporte" } } },
            { "Controller", new() { { "en", "Controller" }, { "fr", "Contrôleur" }, { "es", "Controlador" } } },
            { "Off-Tank", new() { { "en", "Off-Tank" }, { "fr", "Off-Tank" }, { "es", "Subtanque" } } },
            { "Glass Cannon", new() { { "en", "Glass Cannon" }, { "fr", "Canon de Verre" }, { "es", "Cañón de Cristal" } } },
            { "Ranged DPS", new() { { "en", "Ranged DPS" }, { "fr", "Dégâts à Distance" }, { "es", "DPS a Distancia" } } },
            { "Melee DPS", new() { { "en", "Melee DPS" }, { "fr", "Dégâts au Corps à Corps" }, { "es", "DPS Cuerpo a Cuerpo" } } },
            { "Debuffer", new() { { "en", "Debuffer" }, { "fr", "Affaiblisseur" }, { "es", "Debilitador" } } },
            { "Buffer", new() { { "en", "Buffer" }, { "fr", "Amplificateur" }, { "es", "Potenciador" } } },
            
            // Narrative Roles
            { "Protagonist", new() { { "en", "Protagonist" }, { "fr", "Protagoniste" }, { "es", "Protagonista" } } },
            { "Antagonist", new() { { "en", "Antagonist" }, { "fr", "Antagoniste" }, { "es", "Antagonista" } } },
            { "Deuteragonist", new() { { "en", "Deuteragonist" }, { "fr", "Deutéragoniste" }, { "es", "Deuteragonista" } } },
            { "Foil", new() { { "en", "Foil" }, { "fr", "Contrepoint" }, { "es", "Contrapunto" } } },
            { "Comic Relief", new() { { "en", "Comic Relief" }, { "fr", "Soulagement Comique" }, { "es", "Alivio Cómico" } } },
            
            // Social Roles
            { "Leader", new() { { "en", "Leader" }, { "fr", "Chef" }, { "es", "Líder" } } },
            { "Diplomat", new() { { "en", "Diplomat" }, { "fr", "Diplomate" }, { "es", "Diplomático" } } },
            { "Mediator", new() { { "en", "Mediator" }, { "fr", "Médiateur" }, { "es", "Mediador" } } },
            { "Negotiator", new() { { "en", "Negotiator" }, { "fr", "Négociateur" }, { "es", "Negociador" } } },
            { "Spokesperson", new() { { "en", "Spokesperson" }, { "fr", "Porte-parole" }, { "es", "Portavoz" } } },
            
            // Exploration Roles
            { "Scout", new() { { "en", "Scout" }, { "fr", "Éclaireur" }, { "es", "Explorador" } } },
            { "Pathfinder", new() { { "en", "Pathfinder" }, { "fr", "Éclaireur de Chemin" }, { "es", "Abre Caminos" } } },
            { "Navigator", new() { { "en", "Navigator" }, { "fr", "Navigateur" }, { "es", "Navegante" } } },
            { "Cartographer", new() { { "en", "Cartographer" }, { "fr", "Cartographe" }, { "es", "Cartógrafo" } } },
            { "Survivalist", new() { { "en", "Survivalist" }, { "fr", "Survivaliste" }, { "es", "Superviviente" } } },
            
            // Knowledge Roles
            { "Scholar", new() { { "en", "Scholar" }, { "fr", "Érudit" }, { "es", "Erudito" } } },
            { "Researcher", new() { { "en", "Researcher" }, { "fr", "Chercheur" }, { "es", "Investigador" } } },
            { "Analyst", new() { { "en", "Analyst" }, { "fr", "Analyste" }, { "es", "Analista" } } },
            { "Librarian", new() { { "en", "Librarian" }, { "fr", "Bibliothécaire" }, { "es", "Bibliotecario" } } },
            { "Historian", new() { { "en", "Historian" }, { "fr", "Historien" }, { "es", "Historiador" } } },
            
            // Criminal Roles
            { "Thief", new() { { "en", "Thief" }, { "fr", "Voleur" }, { "es", "Ladrón" } } },
            { "Assassin", new() { { "en", "Assassin" }, { "fr", "Assassin" }, { "es", "Asesino" } } },
            { "Smuggler", new() { { "en", "Smuggler" }, { "fr", "Contrebandier" }, { "es", "Contrabandista" } } },
            { "Spy", new() { { "en", "Spy" }, { "fr", "Espion" }, { "es", "Espía" } } },
            { "Informant", new() { { "en", "Informant" }, { "fr", "Informateur" }, { "es", "Informante" } } },
            
            // Martial Roles
            { "Soldier", new() { { "en", "Soldier" }, { "fr", "Soldat" }, { "es", "Soldado" } } },
            { "Knight", new() { { "en", "Knight" }, { "fr", "Chevalier" }, { "es", "Caballero" } } },
            { "Mercenary", new() { { "en", "Mercenary" }, { "fr", "Mercenaire" }, { "es", "Mercenario" } } },
            { "Gladiator", new() { { "en", "Gladiator" }, { "fr", "Gladiateur" }, { "es", "Gladiador" } } },
            { "Champion", new() { { "en", "Champion" }, { "fr", "Champion" }, { "es", "Campeón" } } },
            
            // Arcane Roles
            { "Spellcaster", new() { { "en", "Spellcaster" }, { "fr", "Lanceur de Sorts" }, { "es", "Lanzador de Hechizos" } } },
            { "Enchanter", new() { { "en", "Enchanter" }, { "fr", "Enchanteur" }, { "es", "Encantador" } } },
            { "Elementalist", new() { { "en", "Elementalist" }, { "fr", "Élémentaliste" }, { "es", "Elementalista" } } },
            { "Ritualist", new() { { "en", "Ritualist" }, { "fr", "Ritualiste" }, { "es", "Ritualista" } } },
            { "Summoner", new() { { "en", "Summoner" }, { "fr", "Invocateur" }, { "es", "Invocador" } } },
            
            // Divine Roles
            { "Cleric", new() { { "en", "Cleric" }, { "fr", "Prêtre" }, { "es", "Clérigo" } } },
            { "Priest", new() { { "en", "Priest" }, { "fr", "Prêtre" }, { "es", "Sacerdote" } } },
            { "Paladin", new() { { "en", "Paladin" }, { "fr", "Paladin" }, { "es", "Paladín" } } },
            { "Templar", new() { { "en", "Templar" }, { "fr", "Templier" }, { "es", "Templario" } } },
            { "Inquisitor", new() { { "en", "Inquisitor" }, { "fr", "Inquisiteur" }, { "es", "Inquisidor" } } },
            
            // Nature Roles
            { "Druid", new() { { "en", "Druid" }, { "fr", "Druide" }, { "es", "Druida" } } },
            { "Ranger", new() { { "en", "Ranger" }, { "fr", "Rôdeur" }, { "es", "Montaraz" } } },
            { "Beastmaster", new() { { "en", "Beastmaster" }, { "fr", "Maître des Bêtes" }, { "es", "Domador de Bestias" } } },
            { "Herbalist", new() { { "en", "Herbalist" }, { "fr", "Herboriste" }, { "es", "Herbolario" } } },
            { "Shaman", new() { { "en", "Shaman" }, { "fr", "Chaman" }, { "es", "Chamán" } } },
            
            // Undead Roles
            { "Necromancer", new() { { "en", "Necromancer" }, { "fr", "Nécromancien" }, { "es", "Nigromante" } } },
            { "Death Knight", new() { { "en", "Death Knight" }, { "fr", "Chevalier de la Mort" }, { "es", "Caballero de la Muerte" } } },
            { "Undead Hunter", new() { { "en", "Undead Hunter" }, { "fr", "Chasseur de Mort-vivant" }, { "es", "Cazador de No Muertos" } } },
            
            // Specialized Roles
            { "Artisan", new() { { "en", "Artisan" }, { "fr", "Artisan" }, { "es", "Artesano" } } },
            { "Merchant", new() { { "en", "Merchant" }, { "fr", "Marchand" }, { "es", "Comerciante" } } },
            { "Entertainer", new() { { "en", "Entertainer" }, { "fr", "Artiste" }, { "es", "Artista" } } },
            { "Sage", new() { { "en", "Sage" }, { "fr", "Sage" }, { "es", "Sabio" } } },
            { "Guard", new() { { "en", "Guard" }, { "fr", "Garde" }, { "es", "Guardia" } } },
            { "Tracker", new() { { "en", "Tracker" }, { "fr", "Pisteur" }, { "es", "Rastreador" } } },
            { "Beast Tamer", new() { { "en", "Beast Tamer" }, { "fr", "Dompteur de Bêtes" }, { "es", "Domador de Bestias" } } },
            
            // Leadership
            { "Captain", new() { { "en", "Captain" }, { "fr", "Capitaine" }, { "es", "Capitán" } } },
            { "Commander", new() { { "en", "Commander" }, { "fr", "Commandant" }, { "es", "Comandante" } } },
            { "Warlord", new() { { "en", "Warlord" }, { "fr", "Seigneur de Guerre" }, { "es", "Señor de la Guerra" } } },
            { "Tactician", new() { { "en", "Tactician" }, { "fr", "Tacticien" }, { "es", "Táctico" } } },
            { "Strategist", new() { { "en", "Strategist" }, { "fr", "Stratège" }, { "es", "Estratega" } } },
            
            // Miscellaneous
            { "Mentor", new() { { "en", "Mentor" }, { "fr", "Mentor" }, { "es", "Mentor" } } },
            { "Student", new() { { "en", "Student" }, { "fr", "Étudiant" }, { "es", "Estudiante" } } },
            { "Wanderer", new() { { "en", "Wanderer" }, { "fr", "Vagabond" }, { "es", "Errante" } } },
            { "Hermit", new() { { "en", "Hermit" }, { "fr", "Ermite" }, { "es", "Ermitaño" } } },
            { "Pilgrim", new() { { "en", "Pilgrim" }, { "fr", "Pèlerin" }, { "es", "Peregrino" } } },
            { "Neutral", new() { { "en", "Neutral" }, { "fr", "Neutre" }, { "es", "Neutral" } } },
            { "Trickster", new() { { "en", "Trickster" }, { "fr", "Filou" }, { "es", "Tramposo" } } },
            { "Follower", new() { { "en", "Follower" }, { "fr", "Suiveur" }, { "es", "Seguidor" } } },
            { "Ally", new() { { "en", "Ally" }, { "fr", "Allié" }, { "es", "Aliado" } } },
            { "Enemy", new() { { "en", "Enemy" }, { "fr", "Ennemi" }, { "es", "Enemigo" } } }
        };

        public static string GetRandomClass()
        {
            var keys = new List<string>(Classes.Keys);
            return keys[_random.Next(keys.Count)];
        }

        public static string GetRandomRace()
        {
            var keys = new List<string>(Races.Keys);
            return keys[_random.Next(keys.Count)];
        }

        public static string GetRandomRole()
        {
            var keys = new List<string>(Roles.Keys);
            return keys[_random.Next(keys.Count)];
        }
    }
}
