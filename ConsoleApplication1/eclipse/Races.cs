using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eclipse
{

    enum EclipseRace
    {
        Unknown = 0,
            
        Terran,

        Mechanema,
        Eridani,
        Orion,
        Hydran,
        Draco,
        Planta,

        Magellan,
        Exiles,
        Syndicate,
        Enlightened,

        Elders,
    }


    static class EclipseRaceParser
    {

        public static EclipseRace categorizeByRace(String comparison)
        {
            comparison = comparison.ToLower();
            if (comparison.Equals(""))
            {
                return EclipseRace.Unknown;
            }
            else if (comparison.Contains("terran") || comparison.Contains("human") || _knownTerran.Contains(comparison))
            {
                return EclipseRace.Terran;
            }
            else if ((comparison.Contains("alien") && comparison.Contains("white")) ||comparison.Contains("mech") || _knownMechanema.Contains(comparison))
            {
                return EclipseRace.Mechanema;
            }
            else if ((comparison.Contains("alien") && comparison.Contains("black")) || comparison.Contains("orion") || comparison.Contains("hegemony") || _knownOrion.Contains(comparison))
            {
                return EclipseRace.Orion;
            }
            else if ((comparison.Contains("alien") && comparison.Contains("blue")) || comparison.Contains("hydran") || comparison.Contains("progress") || _knownHydran.Contains(comparison))
            {
                return EclipseRace.Hydran;
            }
            else if ((comparison.Contains("alien") && comparison.Contains("gree")) || comparison.Contains("plant") || _knownPlanta.Contains(comparison))
            {
                return EclipseRace.Planta;
            }
            else if ((comparison.Contains("alien") && comparison.Contains("yellow")) || comparison.Contains("descendants") || comparison.Contains("draco") || _knownDraco.Contains(comparison))
            {
                return EclipseRace.Draco;
            }
            else if ((comparison.Contains("alien") && comparison.Contains("red")) || comparison.Contains("eridani") || comparison.Contains("empire") || _knownEridani.Contains(comparison))
            {
                return EclipseRace.Eridani;
            }
            else if (comparison.Contains("magellan") || comparison.Contains("keepers") || comparison.Contains("seekers") || comparison.Contains("wardens") || _knownMagellan.Contains(comparison))
            {
                return EclipseRace.Magellan;
            }
            else if (comparison.Contains("rho indi") || comparison.Contains("syndicate") || _knownSyndicate.Contains(comparison))
            {
                return EclipseRace.Syndicate;
            }
            else if (comparison.Contains("enlightened") || comparison.Contains("lyra") || _knownEnlightened.Contains(comparison))
            {
                return EclipseRace.Enlightened;
            }
            else if (comparison.Contains("exiles") || _knownExiles.Contains(comparison))
            {
                return EclipseRace.Exiles;
            }
            else if (comparison.Contains("elders") || comparison.Contains("solstice") || _knownElders.Contains(comparison))
            {
                return EclipseRace.Elders;
            }
            else if (_knownUnknowns.Contains(comparison))
            {
                return EclipseRace.Unknown;
            }
            else
            {
                _unknownUnknowns.Add(comparison);
                return EclipseRace.Unknown;
            }
        }

        private static HashSet<String> _unknownUnknowns = new HashSet<String>();

        private static string[] _knownUnknowns = new string[] {
"green/hydra",
"æµ·å“æ‹‰",
"blanco - hedonema",
"è—ï¼œæ¤ç‰©",
"ç´…ï¼œæ³¢æ±ÿåº§",
"é»ƒï¼œæ©ÿæ¢°",
"ç™½ï¼œäººé¡ž",
"negro",
"amarillo",
"åœ°çƒäººãƒ»èµ¤",
"æ©ÿæ¢°ï¼œç™½",
"ç§‘æš€ï¼œè—",
"å¤©é¾äººï¼œç´…",
"çµæˆ¶åº§ï¼œé»‘",
"negros",
"æ©ÿæ¢°",
"äººé¡ž",
"æ¤ç‰©",
"geel",
"çµæˆ¶åº§",
"æ³¢æ±ÿåº§",
"rot (mensch)",
"æµ·å“æ©",
"black/??",
"gelb",
"blau",
"red/blue",
"ã‚¨ãƒ«ãƒ€ãƒ‹",
"ã‚³ã‚±",
"é’(äººé–“)",
"èµ¤(äººé–“)",
"ç·‘(äººé–“)",
"yello",
"blue ",
"red civ",
"blue civ",
"white/red (w/g/b)",
"green/black",
"military",
"techie",
"dr. mindbender",
"sisco",
"vihreã¤",
"punainen",
"keltainen",
"valkoinen",
"violetti",
"biaå‚y",
"zielony",
"å¼ã³å‚ty",
"niebieski",
"23",
"czerwony",
"czarny",
"glucose1998",
"red ",
"blue?",
"amarillos",
"azules",
"rojos",
"vit+svart",
"white ",
"red, green, blue",
"gul",
"blue vs red",
"alien",
"blanc",
"lost souls of argalon",
"37",
" amarillo",
"rojo",
"blanco",
"verde",
"blu",
"bianco",
"rosso",
"giallo",
"zwart",
"wit",
"groen",
"black",
"purple",
"(krieger)",
"(pflanzen)",
"menschen",
"green ",
"gray",
"pink",
"plain",
"grey",
"allied with ryan",
"allied with brian",
"allied w/ mark/dave",
"allied w/ dennis/dave",
"allied w/ dennis/mark",
"blauw",
"?",
"cream",
"tan",
"harbingers",
"terricola",
"epsilon",
"bleu",
"noir",
"rouge",
"jaune",
"vert",
"red",
"vermelho",
"azul",
"preto",
"branco",
"green",
"white",
"yellow",
"blue",
"rot",
"grã¼n",
"schwarz",};


        private static string[] _knownTerran = new string[] {
"union terrienne, prof d'allemand",
"blue / earth",
"terra",
"humain noir",
"yellow / earth", 
"republic",
"federation",
"alliance terrienne (blanc)",
"terrain",};
        private static string[] _knownMechanema = new string[] { 
"alien blanc",
"machanema",
"mecanema",
"machena",
"borg",
"white - alien",
"white (alien)",
"white / spam aliens",
"white alien",
"white aliens",
"white cyborg women",
"white robots",
"white robot",
"white aliens (borg)",
"white - cyborg",};
        private static string[] _knownOrion = new string[] {
"war guys (black)",
"negro - hegemonã­a de oriã³n",
"hegemonie",
"black / alien",
"black - armour", 
"black alien",};
        private static string[] _knownEridani = new string[] {
"eridiani",
"eridane",
"erdani",
"alien rouge",
"red - alien",
"red (alien)",
"red / ? [money]",
"red / alien",
"red/eridian",
"red aliens",
"red/eredani",
"red alien",
"red - pepper pots" };
        private static string[] _knownPlanta = new string[] {
"green - elien",
"green (alien)",
"green - tentacles", 
"green alien",};
        private static string[] _knownDraco = new string[] { 
"ancients",
"descendents/yellow",
"ancient lovers / yellow",
"yellow - descendents of drako",
"yellow (alien)",
"yellow / ancient lovers",
"yellow ancient race lovers",
"yellow/descents of dracho",
"yellow aliens",
"los descendientes de nosequiã©n",
"decendants - yellow",
"yellow / alien",
"yellow alien",};
        private static string[] _knownHydran = new string[] {
"alien bleu",
"hydron / blue",
"hyran",
"technocratie",
"hydrian",
"research guys (blue)",
"blue / tech aliens",
"blue / tech guys",
"blue aliens",
"blue scientist race",
"blue water aliens",
"blue tech alien",
"hydra",
"blue alien",
"hydraxans",
"tech",
"blue / alien",
"dark blue - ghost",
"hydra (forscher)",
"blue/hydrian", };
        private static string[] _knownMagellan = new string[] { 
"mageran",};
        private static string[] _knownExiles = new string[] { 
"exiliados",};
        private static string[] _knownEnlightened = new string[] { 
"tan alien",
"lynas",};
        private static string[] _knownSyndicate = new string[] {
"sindicato rho",
"gray cyborg",
            "rhoindi",
"grey/rohi",
"sindicate",
"rho pirates",};
        private static string[] _knownElders = new string[] { };

        public static IEnumerable<string> unknownRaces { get {return _unknownUnknowns;} }
    }
}
