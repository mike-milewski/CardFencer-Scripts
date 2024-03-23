using UnityEngine;

public class BattleCardParticleEffect : MonoBehaviour
{
    [SerializeField]
    private Card card;

    [SerializeField]
    private ParticleSystem slash, fireBolt, lightning, heal, gale, potion, ether, fireBlaze, hellFire, holyBolt, lightningBlade, judgement, piercingLight, tempest, lightningStorm, thunderStorm, shadowBlight, shadowScourge,
                           dualStrike, rendingSlash, shadowBlade, shadowSpike, cometStorm, rend, revive, toxin, flameTongue, courage, protection, cure, restore, triAttack, airCutter, storm, tornado, healingGuard,
                           braveThrust, fangWave, flareWave, strengthPotion, healingCounter, retain, holyFire, darkLight, consecration, hurricaneThrust, defensePotion, frostBolt, coldSteel, aquaBullet, lacerate,
                           liquidMetal, ultimateGuard, dagger, frozenOrb, waterPulse, toxicNova, poisonPoint, toxicBlade, flarePotion, gash, skyBlade, daggerRain, daggerBarrage, mend, nurse, recover, nourish, embrace,
                           revitalize, boilingMist, earthSpike, stalagmites, gaiasSplinters, moltenFireballs, waterfall, waterspout, frozenShard, frigidStorm, frostBite, energyBlast, terraTremor, slagCrush, sootCutter,
                           mightyExplosion, ultimatus, skyFall, flameRend, burningSky, upperCut, magicShower, cometShard, cometChunk, cometRain, powerCut, lifeShaver, banish, channel, surge, halt, doubleCast, panacea,
                           shockPotion, dragonMaw, somersault, darkWind, lightningBlast, gustBlade, shockBlade, chargedDagger, blazingDagger, blastPotion, risingStones, maelstrom, freezingWater, coldWave, absoluteZero,
                           blastingComets, frozenComets, waterPressure, earthenClaw, gaiasDagger, snowClaw, paralyzingClaw, thunderRing, flameRing, darkFire, coldAssault, shadowRing, braveCharge, shred, pulverize,
                           skyDive, skyQuake, braveOnslaught, curvet, dropkick, maim, malevolentEdge, impact, voidCut, shadowEdge, thrash, decimationStorm, spatialDistortion, giantBlade, energize, embolden, darkScythe,
                           shockThrust, earthRend, thunderRend, frigidRend, devastation, energyThrust, rendingThrust, crushingEarth, radiantWind, shadowThrust, darkBolt, vampiricBlade, brilliantBolt, scorchingWind,
                           bloodyImpale, flareTornado, blackHole, darkWave, eruption, radiantBlades, energyBlade, acidPulse, fieryExplosion, shockWave, blackInk, shadowBurst, darkMagicRain, blastBeam, vindicate,
                           violentVolt, poisonShot, sluice, iceTornado, coldEmbrace, discharge, levinStorm, KunaiWave, gaiaBlast, energySurge, holyRay, explosiveAssault, destructiveAssault, gaiaAssault, aquaRing,
                           dragonFall, subdue, whirlwindStrike, twistingBlade, crescentMoon, blackEclipse, plungingSky, skyAnnihilation, sanguineSlash, blizzardBite, stingingDust, freezingTempest, ravagingWaters,
                           evilBind, unstableVoid, iceWall, makiu, explosion, shadowDrops, drenchedSky, mudshot, muddyWaters, lavaToss, dragonFire, flareStorm, torrentialVortex, ultimateCards, shadowKnife, shadowBarrage,
                           conflagrationBarrage, volticBarrage, earthenBarrage, zephyrDagger, mistralBarrage, azureSky, soakingDagger, seaBarrage, marineCut, freezingDagger, arcticBarrage, darkIce, blackIceBerg,
                           zealousDagger, energyBarrage, dragonFrost, shadowComet, hotIce, shimmeringBarrage, poisonApples, appleOfEden, volatileSky, mysticsWrath, electricStorm, puritySabres, sinSpikes, trailingTumult,
                           usheringBlade, oblivion, puncture, blossomKick, thunderousBoom, weakeningThrust, raijinsBolt, cascadingRuin, cleft, furyBlade;

    private void Start()
    {
        SetUpParticle();
    }

    public ParticleSystem SetUpParticle()
    {
        switch(card._cardTemplate.cardTemplateName)
        {
            case (CardTemplateName.Slash):
                card.ParticleEffect = slash;
                break;
            case (CardTemplateName.FireBolt):
                card.ParticleEffect = fireBolt;
                break;
            case (CardTemplateName.Lightning):
                card.ParticleEffect = lightning;
                break;
            case (CardTemplateName.Heal):
                card.ParticleEffect = heal;
                break;
            case (CardTemplateName.Gale):
                card.ParticleEffect = gale;
                break;
            case (CardTemplateName.Potion):
                card.ParticleEffect = potion;
                break;
            case (CardTemplateName.Ether):
                card.ParticleEffect = ether;
                break;
            case (CardTemplateName.FlameTongue):
                card.ParticleEffect = flameTongue;
                break;
            case (CardTemplateName.LightningBlade):
                card.ParticleEffect = lightningBlade;
                break;
            case (CardTemplateName.PiercingLight):
                card.ParticleEffect = piercingLight;
                break;
            case (CardTemplateName.FireBlaze):
                card.ParticleEffect = fireBlaze;
                break;
            case (CardTemplateName.HellFire):
                card.ParticleEffect = hellFire;
                break;
            case (CardTemplateName.HolyBolt):
                card.ParticleEffect = holyBolt;
                break;
            case (CardTemplateName.Judgement):
                card.ParticleEffect = judgement;
                break;
            case (CardTemplateName.Tempest):
                card.ParticleEffect = tempest;
                break;
            case (CardTemplateName.ThunderStorm):
                card.ParticleEffect = thunderStorm;
                break;
            case (CardTemplateName.LightningStorm):
                card.ParticleEffect = lightningStorm;
                break;
            case (CardTemplateName.ShadowSpike):
                card.ParticleEffect = shadowSpike;
                break;
            case (CardTemplateName.ShadowBlight):
                card.ParticleEffect = shadowBlight;
                break;
            case (CardTemplateName.ShadowScourge):
                card.ParticleEffect = shadowScourge;
                break;
            case (CardTemplateName.ShadowBlade):
                card.ParticleEffect = shadowBlade;
                break;
            case (CardTemplateName.DualStrike):
                card.ParticleEffect = dualStrike;
                break;
            case (CardTemplateName.Rend):
                card.ParticleEffect = rend;
                break;
            case (CardTemplateName.RendingSlash):
                card.ParticleEffect = rendingSlash;
                break;
            case (CardTemplateName.CometStorm):
                card.ParticleEffect = cometStorm;
                break;
            case (CardTemplateName.Revive):
                card.ParticleEffect = revive;
                break;
            case (CardTemplateName.Toxin):
                card.ParticleEffect = toxin;
                break;
            case (CardTemplateName.Courage):
                card.ParticleEffect = courage;
                break;
            case (CardTemplateName.Protection):
                card.ParticleEffect = protection;
                break;
            case (CardTemplateName.Cure):
                card.ParticleEffect = cure;
                break;
            case (CardTemplateName.Restore):
                card.ParticleEffect = restore;
                break;
            case (CardTemplateName.TriAttack):
                card.ParticleEffect = triAttack;
                break;
            case (CardTemplateName.AirCutter):
                card.ParticleEffect = airCutter;
                break;
            case (CardTemplateName.Storm):
                card.ParticleEffect = storm;
                break;
            case (CardTemplateName.Tornado):
                card.ParticleEffect = tornado;
                break;
            case (CardTemplateName.HealingGuard):
                card.ParticleEffect = healingGuard;
                break;
            case (CardTemplateName.FangWave):
                card.ParticleEffect = fangWave;
                break;
            case (CardTemplateName.FlareWave):
                card.ParticleEffect = flareWave;
                break;
            case (CardTemplateName.StrengthPotion):
                card.ParticleEffect = strengthPotion;
                break;
            case (CardTemplateName.BraveThrust):
                card.ParticleEffect = braveThrust;
                break;
            case (CardTemplateName.HealingCounter):
                card.ParticleEffect = healingCounter;
                break;
            case (CardTemplateName.Reserve):
                card.ParticleEffect = retain;
                break;
            case (CardTemplateName.HolyFire):
                card.ParticleEffect = holyFire;
                break;
            case (CardTemplateName.DarkLight):
                card.ParticleEffect = darkLight;
                break;
            case (CardTemplateName.consecration):
                card.ParticleEffect = consecration;
                break;
            case (CardTemplateName.HurricaneThrust):
                card.ParticleEffect = hurricaneThrust;
                break;
            case (CardTemplateName.DefensePotion):
                card.ParticleEffect = defensePotion;
                break;
            case (CardTemplateName.FrostBolt):
                card.ParticleEffect = frostBolt;
                break;
            case (CardTemplateName.ColdSteel):
                card.ParticleEffect = coldSteel;
                break;
            case (CardTemplateName.AquaBullet):
                card.ParticleEffect = aquaBullet;
                break;
            case (CardTemplateName.Lacerate):
                card.ParticleEffect = lacerate;
                break;
            case (CardTemplateName.LiquidMetal):
                card.ParticleEffect = liquidMetal;
                break;
            case (CardTemplateName.UltimateGuard):
                card.ParticleEffect = ultimateGuard;
                break;
            case (CardTemplateName.Dagger):
                card.ParticleEffect = dagger;
                break;
            case (CardTemplateName.FrozenOrb):
                card.ParticleEffect = frozenOrb;
                break;
            case (CardTemplateName.WaterPulse):
                card.ParticleEffect = waterPulse;
                break;
            case (CardTemplateName.ToxicNova):
                card.ParticleEffect = toxicNova;
                break;
            case (CardTemplateName.PoisonPoint):
                card.ParticleEffect = poisonPoint;
                break;
            case (CardTemplateName.ToxicBlade):
                card.ParticleEffect = toxicBlade;
                break;
            case (CardTemplateName.FlarePotion):
                card.ParticleEffect = flarePotion;
                break;
            case (CardTemplateName.Gash):
                card.ParticleEffect = gash;
                break;
            case (CardTemplateName.SkyBlade):
                card.ParticleEffect = skyBlade;
                break;
            case (CardTemplateName.DaggerRain):
                card.ParticleEffect = daggerRain;
                break;
            case (CardTemplateName.DaggerBarrage):
                card.ParticleEffect = daggerBarrage;
                break;
            case (CardTemplateName.Mend):
                card.ParticleEffect = mend;
                break;
            case (CardTemplateName.Nurse):
                card.ParticleEffect = nurse;
                break;
            case (CardTemplateName.Recover):
                card.ParticleEffect = recover;
                break;
            case (CardTemplateName.Nourish):
                card.ParticleEffect = nourish;
                break;
            case (CardTemplateName.Embrace):
                card.ParticleEffect = embrace;
                break;
            case (CardTemplateName.Revitalize):
                card.ParticleEffect = revitalize;
                break;
            case (CardTemplateName.BoilingMist):
                card.ParticleEffect = boilingMist;
                break;
            case (CardTemplateName.EarthSpike):
                card.ParticleEffect = earthSpike;
                break;
            case (CardTemplateName.Stalagmites):
                card.ParticleEffect = stalagmites;
                break;
            case (CardTemplateName.GaiasSplinters):
                card.ParticleEffect = gaiasSplinters;
                break;
            case (CardTemplateName.MoltenFireballs):
                card.ParticleEffect = moltenFireballs;
                break;
            case (CardTemplateName.Waterfall):
                card.ParticleEffect = waterfall;
                break;
            case (CardTemplateName.Waterspout):
                card.ParticleEffect = waterspout;
                break;
            case (CardTemplateName.FrozenShard):
                card.ParticleEffect = frozenShard;
                break;
            case (CardTemplateName.FrigidStorm):
                card.ParticleEffect = frigidStorm;
                break;
            case (CardTemplateName.Frostbite):
                card.ParticleEffect = frostBite;
                break;
            case (CardTemplateName.EnergyBlast):
                card.ParticleEffect = energyBlast;
                break;
            case (CardTemplateName.TerraTremor):
                card.ParticleEffect = terraTremor;
                break;
            case (CardTemplateName.SlagCrush):
                card.ParticleEffect = slagCrush;
                break;
            case (CardTemplateName.SootCutter):
                card.ParticleEffect = sootCutter;
                break;
            case (CardTemplateName.MightyExplosion):
                card.ParticleEffect = mightyExplosion;
                break;
            case (CardTemplateName.Ultimatus):
                card.ParticleEffect = ultimatus;
                break;
            case (CardTemplateName.Skyfall):
                card.ParticleEffect = skyFall;
                break;
            case (CardTemplateName.FlameRend):
                card.ParticleEffect = flameRend;
                break;
            case (CardTemplateName.BurningSky):
                card.ParticleEffect = burningSky;
                break;
            case (CardTemplateName.Uppercut):
                card.ParticleEffect = upperCut;
                break;
            case (CardTemplateName.MagicShower):
                card.ParticleEffect = magicShower;
                break;
            case (CardTemplateName.CometShard):
                card.ParticleEffect = cometShard;
                break;
            case (CardTemplateName.CometChunk):
                card.ParticleEffect = cometChunk;
                break;
            case (CardTemplateName.CometRain):
                card.ParticleEffect = cometRain;
                break;
            case (CardTemplateName.PowerCut):
                card.ParticleEffect = powerCut;
                break;
            case (CardTemplateName.LifeShaver):
                card.ParticleEffect = lifeShaver;
                break;
            case (CardTemplateName.Banish):
                card.ParticleEffect = banish;
                break;
            case (CardTemplateName.Channel):
                card.ParticleEffect = channel;
                break;
            case (CardTemplateName.Surge):
                card.ParticleEffect = surge;
                break;
            case (CardTemplateName.Halt):
                card.ParticleEffect = halt;
                break;
            case (CardTemplateName.DoubleCast):
                card.ParticleEffect = doubleCast;
                break;
            case (CardTemplateName.Panacea):
                card.ParticleEffect = panacea;
                break;
            case (CardTemplateName.ShockPotion):
                card.ParticleEffect = shockPotion;
                break;
            case (CardTemplateName.DragonMaw):
                card.ParticleEffect = dragonMaw;
                break;
            case (CardTemplateName.Somersault):
                card.ParticleEffect = somersault;
                break;
            case (CardTemplateName.DarkWind):
                card.ParticleEffect = darkWind;
                break;
            case (CardTemplateName.LightningBlast):
                card.ParticleEffect = lightningBlast;
                break;
            case (CardTemplateName.GustBlade):
                card.ParticleEffect = gustBlade;
                break;
            case (CardTemplateName.ShockBlade):
                card.ParticleEffect = shockBlade;
                break;
            case (CardTemplateName.ChargedDagger):
                card.ParticleEffect = chargedDagger;
                break;
            case (CardTemplateName.BlazingDagger):
                card.ParticleEffect = blazingDagger;
                break;
            case (CardTemplateName.BlastPotion):
                card.ParticleEffect = blastPotion;
                break;
            case (CardTemplateName.RisingStones):
                card.ParticleEffect = risingStones;
                break;
            case (CardTemplateName.Maelstrom):
                card.ParticleEffect = maelstrom;
                break;
            case (CardTemplateName.FreezingWater):
                card.ParticleEffect = freezingWater;
                break;
            case (CardTemplateName.ColdWave):
                card.ParticleEffect = coldWave;
                break;
            case (CardTemplateName.AbsoluteZero):
                card.ParticleEffect = absoluteZero;
                break;
            case (CardTemplateName.BlastingComets):
                card.ParticleEffect = blastingComets;
                break;
            case (CardTemplateName.FrozenComets):
                card.ParticleEffect = frozenComets;
                break;
            case (CardTemplateName.WaterPressure):
                card.ParticleEffect = waterPressure;
                break;
            case (CardTemplateName.EarthenClaw):
                card.ParticleEffect = earthenClaw;
                break;
            case (CardTemplateName.GaiasDagger):
                card.ParticleEffect = gaiasDagger;
                break;
            case (CardTemplateName.SnowClaw):
                card.ParticleEffect = snowClaw;
                break;
            case (CardTemplateName.ParalyzingClaw):
                card.ParticleEffect = paralyzingClaw;
                break;
            case (CardTemplateName.ThunderRing):
                card.ParticleEffect = thunderRing;
                break;
            case (CardTemplateName.FlameRing):
                card.ParticleEffect = flameRing;
                break;
            case (CardTemplateName.DarkFire):
                card.ParticleEffect = darkFire;
                break;
            case (CardTemplateName.ColdAssault):
                card.ParticleEffect = coldAssault;
                break;
            case (CardTemplateName.ShadowRing):
                card.ParticleEffect = shadowRing;
                break;
            case (CardTemplateName.BraveCharge):
                card.ParticleEffect = braveCharge;
                break;
            case (CardTemplateName.Shred):
                card.ParticleEffect = shred;
                break;
            case (CardTemplateName.Pulverize):
                card.ParticleEffect = pulverize;
                break;
            case (CardTemplateName.Skydive):
                card.ParticleEffect = skyDive;
                break;
            case (CardTemplateName.SkyQuake):
                card.ParticleEffect = skyQuake;
                break;
            case (CardTemplateName.BraveOnslaught):
                card.ParticleEffect = braveOnslaught;
                break;
            case (CardTemplateName.Curvet):
                card.ParticleEffect = curvet;
                break;
            case (CardTemplateName.Dropkick):
                card.ParticleEffect = dropkick;
                break;
            case (CardTemplateName.Maim):
                card.ParticleEffect = maim;
                break;
            case (CardTemplateName.MalevolentEdge):
                card.ParticleEffect = malevolentEdge;
                break;
            case (CardTemplateName.Impact):
                card.ParticleEffect = impact;
                break;
            case (CardTemplateName.VoidCut):
                card.ParticleEffect = voidCut;
                break;
            case (CardTemplateName.ShadowEdge):
                card.ParticleEffect = shadowEdge;
                break;
            case (CardTemplateName.Thrash):
                card.ParticleEffect = thrash;
                break;
            case (CardTemplateName.DecimationStorm):
                card.ParticleEffect = decimationStorm;
                break;
            case (CardTemplateName.SpatialDistortion):
                card.ParticleEffect = spatialDistortion;
                break;
            case (CardTemplateName.GiantBlade):
                card.ParticleEffect = giantBlade;
                break;
            case (CardTemplateName.Energize):
                card.ParticleEffect = energize;
                break;
            case (CardTemplateName.Embolden):
                card.ParticleEffect = embolden;
                break;
            case (CardTemplateName.DarkScythe):
                card.ParticleEffect = darkScythe;
                break;
            case (CardTemplateName.ShockThrust):
                card.ParticleEffect = shockThrust;
                break;
            case (CardTemplateName.EarthRend):
                card.ParticleEffect = earthRend;
                break;
            case (CardTemplateName.ThunderRend):
                card.ParticleEffect = thunderRend;
                break;
            case (CardTemplateName.FrigidRend):
                card.ParticleEffect = frigidRend;
                break;
            case (CardTemplateName.Devastation):
                card.ParticleEffect = devastation;
                break;
            case (CardTemplateName.EnergyThrust):
                card.ParticleEffect = energyThrust;
                break;
            case (CardTemplateName.PiercingStrike):
                card.ParticleEffect = rendingThrust;
                break;
            case (CardTemplateName.crushingEarth):
                card.ParticleEffect = crushingEarth;
                break;
            case (CardTemplateName.RadiantWind):
                card.ParticleEffect = radiantWind;
                break;
            case (CardTemplateName.ShadowThrust):
                card.ParticleEffect = shadowThrust;
                break;
            case (CardTemplateName.DarkBolt):
                card.ParticleEffect = darkBolt;
                break;
            case (CardTemplateName.VampiricBlade):
                card.ParticleEffect = vampiricBlade;
                break;
            case (CardTemplateName.BrilliantBolt):
                card.ParticleEffect = brilliantBolt;
                break;
            case (CardTemplateName.ScorchingWind):
                card.ParticleEffect = scorchingWind;
                break;
            case (CardTemplateName.BloodyImpale):
                card.ParticleEffect = bloodyImpale;
                break;
            case (CardTemplateName.FlareTornado):
                card.ParticleEffect = flareTornado;
                break;
            case (CardTemplateName.BlackHole):
                card.ParticleEffect = blackHole;
                break;
            case (CardTemplateName.DarkWave):
                card.ParticleEffect = darkWave;
                break;
            case (CardTemplateName.Eruption):
                card.ParticleEffect = eruption;
                break;
            case (CardTemplateName.RadiantBlades):
                card.ParticleEffect = radiantBlades;
                break;
            case (CardTemplateName.EnergyBlade):
                card.ParticleEffect = energyBlade;
                break;
            case (CardTemplateName.AcidPulse):
                card.ParticleEffect = acidPulse;
                break;
            case (CardTemplateName.FieryExplosion):
                card.ParticleEffect = fieryExplosion;
                break;
            case (CardTemplateName.ShockWave):
                card.ParticleEffect = shockWave;
                break;
            case (CardTemplateName.BlackInk):
                card.ParticleEffect = blackInk;
                break;
            case (CardTemplateName.ShadowBurst):
                card.ParticleEffect = shadowBurst;
                break;
            case (CardTemplateName.DarkMagicRain):
                card.ParticleEffect = darkMagicRain;
                break;
            case (CardTemplateName.BladeBeam):
                card.ParticleEffect = blastBeam;
                break;
            case (CardTemplateName.Vindicate):
                card.ParticleEffect = vindicate;
                break;
            case (CardTemplateName.ViolentVolt):
                card.ParticleEffect = violentVolt;
                break;
            case (CardTemplateName.PoisonShot):
                card.ParticleEffect = poisonShot;
                break;
            case (CardTemplateName.Sluice):
                card.ParticleEffect = sluice;
                break;
            case (CardTemplateName.IceTornado):
                card.ParticleEffect = iceTornado;
                break;
            case (CardTemplateName.ColdEmbrace):
                card.ParticleEffect = coldEmbrace;
                break;
            case (CardTemplateName.Discharge):
                card.ParticleEffect = discharge;
                break;
            case (CardTemplateName.LevinStorm):
                card.ParticleEffect = levinStorm;
                break;
            case (CardTemplateName.KunaiWave):
                card.ParticleEffect = KunaiWave;
                break;
            case (CardTemplateName.GaiaBlast):
                card.ParticleEffect = gaiaBlast;
                break;
            case (CardTemplateName.EnergySurge):
                card.ParticleEffect = energySurge;
                break;
            case (CardTemplateName.HolyRay):
                card.ParticleEffect = holyRay;
                break;
            case (CardTemplateName.ExplosiveAssault):
                card.ParticleEffect = explosiveAssault;
                break;
            case (CardTemplateName.DestructiveAssault):
                card.ParticleEffect = destructiveAssault;
                break;
            case (CardTemplateName.GaiaAssault):
                card.ParticleEffect = gaiaAssault;
                break;
            case (CardTemplateName.AquaRing):
                card.ParticleEffect = aquaRing;
                break;
            case (CardTemplateName.DragonFall):
                card.ParticleEffect = dragonFall;
                break;
            case (CardTemplateName.Subdue):
                card.ParticleEffect = subdue;
                break;
            case (CardTemplateName.WhirlwindStrike):
                card.ParticleEffect = whirlwindStrike;
                break;
            case (CardTemplateName.TwistingBlade):
                card.ParticleEffect = twistingBlade;
                break;
            case (CardTemplateName.CrescentMoon):
                card.ParticleEffect = crescentMoon;
                break;
            case (CardTemplateName.BlackEclipse):
                card.ParticleEffect = blackEclipse;
                break;
            case (CardTemplateName.PlungingSky):
                card.ParticleEffect = plungingSky;
                break;
            case (CardTemplateName.SkyAnnihilation):
                card.ParticleEffect = skyAnnihilation;
                break;
            case (CardTemplateName.SanguineSlash):
                card.ParticleEffect = sanguineSlash;
                break;
            case (CardTemplateName.BlizzardBite):
                card.ParticleEffect = blizzardBite;
                break;
            case (CardTemplateName.StingingDust):
                card.ParticleEffect = stingingDust;
                break;
            case (CardTemplateName.FreezingTempest):
                card.ParticleEffect = freezingTempest;
                break;
            case (CardTemplateName.RavagingWaters):
                card.ParticleEffect = ravagingWaters;
                break;
            case (CardTemplateName.EvilBind):
                card.ParticleEffect = evilBind;
                break;
            case (CardTemplateName.UnstableVoid):
                card.ParticleEffect = unstableVoid;
                break;
            case (CardTemplateName.IceWall):
                card.ParticleEffect = iceWall;
                break;
            case (CardTemplateName.Makiu):
                card.ParticleEffect = makiu;
                break;
            case (CardTemplateName.Explosion):
                card.ParticleEffect = explosion;
                break;
            case (CardTemplateName.ShadowDrops):
                card.ParticleEffect = shadowDrops;
                break;
            case (CardTemplateName.DrenchedSky):
                card.ParticleEffect = drenchedSky;
                break;
            case (CardTemplateName.Mudshot):
                card.ParticleEffect = mudshot;
                break;
            case (CardTemplateName.MuddyWaters):
                card.ParticleEffect = muddyWaters;
                break;
            case (CardTemplateName.LavaToss):
                card.ParticleEffect = lavaToss;
                break;
            case (CardTemplateName.DragonFire):
                card.ParticleEffect = dragonFire;
                break;
            case (CardTemplateName.FlareStorm):
                card.ParticleEffect = flareStorm;
                break;
            case (CardTemplateName.TorrentialVortex):
                card.ParticleEffect = torrentialVortex;
                break;
            case (CardTemplateName.UltimateCards):
                card.ParticleEffect = ultimateCards;
                break;
            case (CardTemplateName.ShadowKnife):
                card.ParticleEffect = shadowKnife;
                break;
            case (CardTemplateName.ShadowBarrage):
                card.ParticleEffect = shadowBarrage;
                break;
            case (CardTemplateName.ConflagrationBarrage):
                card.ParticleEffect = conflagrationBarrage;
                break;
            case (CardTemplateName.VolticBarrage):
                card.ParticleEffect = volticBarrage;
                break;
            case (CardTemplateName.EarthenBarrage):
                card.ParticleEffect = earthenBarrage;
                break;
            case (CardTemplateName.ZephyrDagger):
                card.ParticleEffect = zephyrDagger;
                break;
            case (CardTemplateName.MistralBarrage):
                card.ParticleEffect = mistralBarrage;
                break;
            case (CardTemplateName.AzureSky):
                card.ParticleEffect = azureSky;
                break;
            case (CardTemplateName.SoakingDagger):
                card.ParticleEffect = soakingDagger;
                break;
            case (CardTemplateName.SeaBarrage):
                card.ParticleEffect = seaBarrage;
                break;
            case (CardTemplateName.MarineCut):
                card.ParticleEffect = marineCut;
                break;
            case (CardTemplateName.FreezingDagger):
                card.ParticleEffect = freezingDagger;
                break;
            case (CardTemplateName.ArcticBarrage):
                card.ParticleEffect = arcticBarrage;
                break;
            case (CardTemplateName.DarkIce):
                card.ParticleEffect = darkIce;
                break;
            case (CardTemplateName.BlackIceBerg):
                card.ParticleEffect = blackIceBerg;
                break;
            case (CardTemplateName.ZealousDagger):
                card.ParticleEffect = zealousDagger;
                break;
            case (CardTemplateName.EnergyBarrage):
                card.ParticleEffect = energyBarrage;
                break;
            case (CardTemplateName.DragonFrost):
                card.ParticleEffect = dragonFrost;
                break;
            case (CardTemplateName.ShadowComet):
                card.ParticleEffect = shadowComet;
                break;
            case (CardTemplateName.HotIce):
                card.ParticleEffect = hotIce;
                break;
            case (CardTemplateName.ShimmeringBarrage):
                card.ParticleEffect = shimmeringBarrage;
                break;
            case (CardTemplateName.PoisonApples):
                card.ParticleEffect = poisonApples;
                break;
            case (CardTemplateName.AppleOfEden):
                card.ParticleEffect = appleOfEden;
                break;
            case (CardTemplateName.VolatileSky):
                card.ParticleEffect = volatileSky;
                break;
            case (CardTemplateName.MysticsWrath):
                card.ParticleEffect = mysticsWrath;
                break;
            case (CardTemplateName.ElectricStorm):
                card.ParticleEffect = electricStorm;
                break;
            case (CardTemplateName.PuritySabres):
                card.ParticleEffect = puritySabres;
                break;
            case (CardTemplateName.SinSpikes):
                card.ParticleEffect = sinSpikes;
                break;
            case (CardTemplateName.TrailingTumult):
                card.ParticleEffect = trailingTumult;
                break;
            case (CardTemplateName.UsheringBlade):
                card.ParticleEffect = usheringBlade;
                break;
            case (CardTemplateName.Oblivion):
                card.ParticleEffect = oblivion;
                break;
            case (CardTemplateName.Puncture):
                card.ParticleEffect = puncture;
                break;
            case (CardTemplateName.BlossomKick):
                card.ParticleEffect = blossomKick;
                break;
            case (CardTemplateName.ThunderousBoom):
                card.ParticleEffect = thunderousBoom;
                break;
            case (CardTemplateName.WeakeningThrust):
                card.ParticleEffect = weakeningThrust;
                break;
            case (CardTemplateName.RaijinsBolt):
                card.ParticleEffect = raijinsBolt;
                break;
            case (CardTemplateName.CascadingRuin):
                card.ParticleEffect = cascadingRuin;
                break;
            case (CardTemplateName.Cleft):
                card.ParticleEffect = cleft;
                break;
            case (CardTemplateName.FuryBlade):
                card.ParticleEffect = furyBlade;
                break;
        }

        if (card._cardTemplate.isAProjectile)
        {
            card.IsParticleAProjectile = true;
        }
        else
        {
            card.IsParticleAProjectile = false;
        }

        return card.ParticleEffect;
    }
}